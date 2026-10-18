using Comets.Core.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace SpecVectors
{
	/// <summary>
	/// Reference implementations of the two designation parsers as written in
	/// docs/spec/02-data-model.md, and a check that they agree with the desktop regexes.
	/// <para>
	/// Pattern 00 is safe to use as a regex in any engine. Pattern 02 is not: its nested
	/// quantifiers backtrack exponentially on a long line with no designation in it (the
	/// desktop app hangs on such a file), so the spec defines it as the linear procedure in
	/// <see cref="Parse02"/> instead.
	/// </para>
	/// </summary>
	public static class NameParsing
	{
		public sealed class Result
		{
			public string Id = "", Name = "", Fragment = "";
			public override string ToString() => $"id='{Id}' name='{Name}' fragment='{Fragment}'";
			public bool SameAs(Result o) => o != null && Id == o.Id && Name == o.Name && Fragment == o.Fragment;
		}

		// The desktop patterns, verbatim, with a timeout so the comparison cannot hang.
		const string Desktop00 = @"^(?<id>[0-9]+[PCXDIA]|[PCXDIA]\/-*[0-9]+ *[a-zA-Z]*[0-9]*)-*(?<fragment>[a-zA-Z]*[0-9]*)[ \/]*\(*(?<name>['`a-zA-Z0-9- ]+)*\)*";
		const string Desktop02 = @"^([PCXDIA]\/)*((?<name>['`a-zA-Z- ]+ *[0-9]*)* )*\(*(?<id>[0-9]+[PCXDIA]|[PCXDIA]\/-*[0-9]+ [a-zA-Z]*[0-9]*)-*(?<fragment>[a-zA-Z]*[0-9]*)\)*";

		static readonly Regex _desktop00 = new Regex(Desktop00, RegexOptions.None, TimeSpan.FromSeconds(2));
		static readonly Regex _desktop02 = new Regex(Desktop02, RegexOptions.None, TimeSpan.FromSeconds(2));

		/// <summary>Spec form of pattern 00: the desktop regex with "(name)*" written as "(name)?".</summary>
		static readonly Regex _spec00 = new Regex(@"^(?<id>[0-9]+[PCXDIA]|[PCXDIA]/-*[0-9]+ *[a-zA-Z]*[0-9]*)-*(?<fragment>[a-zA-Z]*[0-9]*)[ /]*\(*(?<name>['`a-zA-Z0-9\- ]+)?");

		static readonly Regex _rest02 = new Regex(@"^\(*(?<id>[0-9]+[PCXDIA]|[PCXDIA]/-*[0-9]+ [a-zA-Z]*[0-9]*)-*(?<fragment>[a-zA-Z]*[0-9]*)");

		static bool IsNameChar(char c) => c == '\'' || c == '`' || c == '-' || c == ' ' || (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z');
		static bool IsDigit(char c) => c >= '0' && c <= '9';
		static bool IsType(char c) => "PCXDIA".IndexOf(c) >= 0;

		/// <summary>null when the input does not match (the desktop throws "Error parsing comet name").</summary>
		public static Result Parse00(string s)
		{
			Match m = _spec00.Match(s);
			if (!m.Success) return null;
			return new Result { Id = m.Groups["id"].Value, Name = m.Groups["name"].Value, Fragment = m.Groups["fragment"].Value };
		}

		/// <summary>
		/// Linear-time equivalent of the desktop pattern 02 (name first: "Name (id-fragment)").
		/// </summary>
		public static Result Parse02(string s)
		{
			// Leading type prefixes "X/" (X in PCXDIA), tried most first.
			int maxPrefixes = 0;
			while (2 * maxPrefixes + 1 < s.Length && IsType(s[2 * maxPrefixes]) && s[2 * maxPrefixes + 1] == '/')
				maxPrefixes++;

			for (int k = maxPrefixes; k >= 0; k--)
			{
				string t = s.Substring(2 * k);

				// Name part: the longest prefix of t that uses only name characters and digits,
				// starts with a name character and ends with a space; tried longest first, then
				// the empty name part.
				int run = 0;
				while (run < t.Length && (IsNameChar(t[run]) || IsDigit(t[run])))
					run++;

				var lengths = new List<int>();
				if (t.Length > 0 && IsNameChar(t[0]))
					for (int j = run - 1; j >= 0; j--)
						if (t[j] == ' ')
							lengths.Add(j + 1);
				lengths.Add(0);

				foreach (int m in lengths)
				{
					Match rest = _rest02.Match(t.Substring(m));
					if (!rest.Success)
						continue;

					return new Result
					{
						Id = rest.Groups["id"].Value,
						Fragment = rest.Groups["fragment"].Value,
						Name = m == 0 ? "" : LastToken(t.Substring(0, m - 1)),
					};
				}
			}

			return null;
		}

		/// <summary>
		/// Splits text (name characters and digits, starting with a name character) into tokens,
		/// each a maximal run of name characters followed by a maximal run of digits, and returns
		/// the last one ("" for empty text).
		/// </summary>
		static string LastToken(string text)
		{
			string last = "";
			int i = 0;
			while (i < text.Length)
			{
				int start = i;
				while (i < text.Length && IsNameChar(text[i])) i++;
				while (i < text.Length && IsDigit(text[i])) i++;
				if (i == start) throw new InvalidOperationException("Name part does not start with a name character: " + text);
				last = text.Substring(start, i - start);
			}
			return last;
		}

		static Result Desktop(Regex r, string s, out bool timedOut)
		{
			timedOut = false;
			try
			{
				Match m = r.Match(s);
				if (!m.Success) return null;
				return new Result { Id = m.Groups["id"].Value, Name = m.Groups["name"].Value, Fragment = m.Groups["fragment"].Value };
			}
			catch (RegexMatchTimeoutException)
			{
				timedOut = true;
				return null;
			}
		}

		/// <summary>
		/// Compares the spec procedures with the desktop patterns on the given inputs plus a
		/// deterministic fuzz set. Throws on any disagreement, except where the desktop pattern
		/// times out (the spec result there is "no match").
		/// </summary>
		public static (int checkedCount, int desktopTimeouts) Verify(IEnumerable<string> names00, IEnumerable<string> names02)
		{
			// The desktop uses the same .NET engine, so check the verbatim pattern too.
			if (CometManagerParse00Differs("C/1995 O1 (Hale-Bopp)"))
				throw new InvalidOperationException("Desktop00 constant differs from CometManager.");

			var rnd = new Random(20261018);
			const string alphabet = "PCXDIAEOabz019 -/()'`";
			var fuzz = Enumerable.Range(0, 20000).Select(_ =>
			{
				int len = rnd.Next(1, 22);
				return new string(Enumerable.Range(0, len).Select(__ => alphabet[rnd.Next(alphabet.Length)]).ToArray());
			}).ToList();

			int count = 0, timeouts = 0;

			foreach (string s in names00.Concat(fuzz))
			{
				Result a = Desktop(_desktop00, s, out bool to);
				Result b = Parse00(s);
				if (to) { timeouts++; continue; }
				if (!((a == null && b == null) || (a != null && a.SameAs(b))))
					throw new InvalidOperationException($"pattern00 mismatch for '{s}': desktop {a}, spec {b}");
				count++;
			}

			foreach (string s in names02.Concat(fuzz))
			{
				Result a = Desktop(_desktop02, s, out bool to);
				Result b = Parse02(s);
				if (to) { timeouts++; continue; }
				if (!((a == null && b == null) || (a != null && a.SameAs(b))))
					throw new InvalidOperationException($"pattern02 mismatch for '{s}': desktop {a}, spec {b}");
				count++;
			}

			return (count, timeouts);
		}

		static bool CometManagerParse00Differs(string s)
		{
			CometManager.GetIdNameFromFull(s, out string id, out string name, out string fragment);
			Result r = Desktop(_desktop00, s, out _);
			return r == null || r.Id != id || r.Name != name || r.Fragment != fragment;
		}
	}
}
