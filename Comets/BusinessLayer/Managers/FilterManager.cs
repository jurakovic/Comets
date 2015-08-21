using Comets.Application;
using Comets.BusinessLayer.Business;
using Comets.BusinessLayer.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DataTypeEnum = Comets.BusinessLayer.Business.Filter.DataTypeEnum;
using ValueCompareEnum = Comets.BusinessLayer.Business.Filter.ValueCompareEnum;
using PropertyEnum = Comets.BusinessLayer.Business.Comet.PropertyEnum;

namespace Comets.BusinessLayer.Managers
{
	public static class FilterManager
	{
		#region PanelDefinition

		private class PanelDefinition
		{
			public PropertyEnum Property;
			public string Text;
			public int InitialCompareIndex;
			public bool StringVisible;
			public bool ValueVisible;
			public bool DateVisible;
			public string LabelStr;
			public Filter.DataTypeEnum DataType;
			public LeMiMa ValueLemima;

			public PanelDefinition(PropertyEnum property, string text, int compareIx, bool stringVisible, bool valueVisible, bool dateVisible, string labelStr, Filter.DataTypeEnum dataType, LeMiMa valueLemima)
			{
				Property = property;
				Text = text;
				InitialCompareIndex = compareIx;
				StringVisible = stringVisible;
				ValueVisible = valueVisible;
				DateVisible = dateVisible;
				LabelStr = labelStr;
				DataType = dataType;
				ValueLemima = valueLemima;
			}
		}

		#endregion

		#region Const

		public static string CheckedName = "Checked";
		public static string PropertyName = "Property";
		public static string CompareName = "Compare";
		public static string StringName = "String";
		public static string ValueName = "Value";
		public static string DateName = "Date";
		public static string LabelName = "Label";
		public static string RemoveName = "Remove";

		private const string DateTimeFormat = "dd.MM.yyyy HH:mm:ss";
		private static string[] StringCompare = new string[] { "Contains", "Does not contain" };
		private static string[] ValueCompare = new string[] { "Greather than (>)", "Less than (<)" };

		private static List<PanelDefinition> PanelDefinitions = new List<PanelDefinition>
		{
			new PanelDefinition(PropertyEnum.full,				"Full name",						0, true,	false,	false,	"",		DataTypeEnum.String, null),
			new PanelDefinition(PropertyEnum.name,				"Discoverer",						0, true,	false,	false,	"",		DataTypeEnum.String, null),
			new PanelDefinition(PropertyEnum.id,				"Designation",						0, true,	false,	false,	"",		DataTypeEnum.String, null),
			new PanelDefinition(PropertyEnum.T,					"Perihelion date",					0, false,	false,	true,	"",		DataTypeEnum.Double, null),
			new PanelDefinition(PropertyEnum.q,					"Perihelion distance",				1, false,	true,	false,	"AU",	DataTypeEnum.Double, new LeMiMa(0.0, 15.0, 6)),
			new PanelDefinition(PropertyEnum.PerihEarthDist,	"Perihelion distance from Earth",	1, false,	true,	false,	"AU",	DataTypeEnum.Double, new LeMiMa(0.0, 15.0, 6)),
			new PanelDefinition(PropertyEnum.PerihMag,			"Perihelion magnitude",				1, false,	true,	false,	"",		DataTypeEnum.Double, new LeMiMa(-20.0, 40.0, 2)),
			new PanelDefinition(PropertyEnum.CurrentSunDist,	"Current distance from Sun",		1, false,	true,	false,	"AU",	DataTypeEnum.Double, new LeMiMa(0.0, 150.0, 6)),
			new PanelDefinition(PropertyEnum.CurrentEarthDist,	"Current distance from Earth",		1, false,	true,	false,	"AU",	DataTypeEnum.Double, new LeMiMa(0.0, 150.0, 6)),
			new PanelDefinition(PropertyEnum.CurrentMag,		"Current magnitude",				1, false,	true,	false,	"",		DataTypeEnum.Double, new LeMiMa(-20.0, 40.0, 2)),
			new PanelDefinition(PropertyEnum.P,					"Period",							1, false,	true,	false,	"years",DataTypeEnum.Double, new LeMiMa(0.0, 10000.0, 6)),
			new PanelDefinition(PropertyEnum.Q,					"Aphelion distance",				1, false,	true,	false,	"AU",	DataTypeEnum.Double, new LeMiMa(0.0, 1000.0, 6)),
			new PanelDefinition(PropertyEnum.a,					"Semi-major axis",					1, false,	true,	false,	"AU",	DataTypeEnum.Double, new LeMiMa(0.0, 1000.0, 6)),
			new PanelDefinition(PropertyEnum.e,					"Eccentricity",						1, false,	true,	false,	"",		DataTypeEnum.Double, new LeMiMa(0.0, 1.2, 6)),
			new PanelDefinition(PropertyEnum.i,					"Inclination",						1, false,	true,	false,	"°",	DataTypeEnum.Double, new LeMiMa(0.0, 179.9999, 4)),
			new PanelDefinition(PropertyEnum.N,					"Longitude of Ascending Node",		1, false,	true,	false,	"°",	DataTypeEnum.Double, new LeMiMa(0.0, 359.9999, 4)),
			new PanelDefinition(PropertyEnum.w,					"Argument of Pericenter",			1, false,	true,	false,	"°",	DataTypeEnum.Double, new LeMiMa(0.0, 359.9999, 4))
		};

		#endregion

		#region FilterList

		public static List<Comet> FilterList(List<Comet> mainList, FilterCollection filters)
		{
			List<Comet> list = new List<Comet>();
			List<bool> checks = new List<bool>();

			var fs = filters.Where(x => x.Checked).ToList();

			foreach (Comet comet in mainList)
			{
				checks.Clear();

				foreach (Filter f in fs)
				{
					object value = comet.GetType().GetProperty(f.Property.ToString()).GetValue(comet, null);

					if (f.DataType == DataTypeEnum.String)
					{
						string full = value.ToString();
						string[] names = f.Text.Split(',');

						if (f.ValueCompare == ValueCompareEnum.Greather_Contains)
							checks.Add(names.Any(x => full.ToLower().Contains(x.Trim().ToLower())));
						else
							checks.Add(!names.Any(x => full.ToLower().Contains(x.Trim().ToLower())));
					}
					else
					{
						double d = Convert.ToDouble(value);

						if (f.ValueCompare == ValueCompareEnum.Greather_Contains)
							checks.Add(d > f.Value);
						else
							checks.Add(d < f.Value);
					}
				}

				if (!checks.Any(x => x == false))
					list.Add(comet);
			}

			return list;
		}

		#endregion

		#region ValidateFilters

		public static bool ValidateFilters(FilterCollection filters)
		{
			bool retval = true;

			foreach (Filter f in filters.Where(x => x.Checked))
			{
				if (f.Value == 0.0)
				{
					string message = String.Format("Please enter value for \"{0}\" \t\t", PanelDefinitions.First(x => x.Property == f.Property).Text);
					MessageBox.Show(message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					retval = false;
					break;
				}
			}

			return retval;
		}

		#endregion

		#region GetDataType

		public static DataTypeEnum GetDataType(PropertyEnum property)
		{
			return PanelDefinitions.First(x => x.Property == property).DataType;
		}

		#endregion

		#region + Filter Panel

		#region CreateFilterPanel

		public static void CreateFilterPanel(Control parent, int id, Point location, DateTime dt, Filter filter, PropertyEnum? property)
		{
			parent.SuspendLayout();

			Panel pnlPanel = new System.Windows.Forms.Panel();

			CheckBox cbxChecked = new System.Windows.Forms.CheckBox();
			ComboBox cboProperty = new System.Windows.Forms.ComboBox();
			ComboBox cboCompare = new System.Windows.Forms.ComboBox();

			TextBox txtString = new System.Windows.Forms.TextBox();
			TextBox txtValue = new System.Windows.Forms.TextBox();
			Button btnDate = new System.Windows.Forms.Button();

			Label lblLabel = new System.Windows.Forms.Label();
			Button btnRemove = new System.Windows.Forms.Button();

			pnlPanel.SuspendLayout();

			pnlPanel.Controls.Add(cbxChecked);
			pnlPanel.Controls.Add(cboProperty);
			pnlPanel.Controls.Add(cboCompare);

			pnlPanel.Controls.Add(txtString);
			pnlPanel.Controls.Add(txtValue);
			pnlPanel.Controls.Add(btnDate);

			pnlPanel.Controls.Add(lblLabel);
			pnlPanel.Controls.Add(btnRemove);

			// 
			// Panel
			// 
			pnlPanel.Location = location;
			pnlPanel.Name = id.ToString();
			pnlPanel.Size = new Size(550, 25);
			pnlPanel.TabIndex = id;

			// 
			// Checked
			// 
			cbxChecked.AutoSize = true;
			cbxChecked.Location = new Point(3, 6);
			cbxChecked.Name = CheckedName;
			cbxChecked.Size = new Size(15, 14);
			cbxChecked.TabIndex = 0;
			cbxChecked.UseVisualStyleBackColor = true;
			cbxChecked.Visible = false;

			// 
			// Property
			// 
			cboProperty.DropDownStyle = ComboBoxStyle.DropDownList;
			cboProperty.FormattingEnabled = true;
			cboProperty.Items.AddRange(PanelDefinitions.Select(x => x.Text).ToArray());
			cboProperty.Location = new Point(20, 2);
			cboProperty.Name = PropertyName;
			cboProperty.Size = new Size(190, 21);
			cboProperty.TabIndex = 1;

			// 
			// Compare
			// 
			cboCompare.DropDownStyle = ComboBoxStyle.DropDownList;
			cboCompare.FormattingEnabled = true;
			cboCompare.Location = new Point(214, 2);
			cboCompare.Name = CompareName;
			cboCompare.Size = new Size(118, 21);
			cboCompare.TabIndex = 2;
			cboCompare.Visible = false;

			// 
			// String
			// 
			txtString.Location = new Point(336, 2);
			txtString.Name = StringName;
			txtString.Size = new Size(182, 20);
			txtString.TabIndex = 3;
			txtString.Visible = false;

			// 
			// Value
			// 
			txtValue.Location = new Point(336, 2);
			txtValue.MaxLength = 100;
			txtValue.Name = ValueName;
			txtValue.Size = new Size(113, 20);
			txtValue.TabIndex = 4;
			txtValue.Visible = false;

			// 
			// Date
			// 
			btnDate.Location = new Point(336, 1);
			btnDate.Name = DateName;
			btnDate.Size = new Size(182, 23);
			btnDate.TabIndex = 5;
			btnDate.Tag = dt;
			btnDate.Text = dt.ToString(DateTimeFormat);
			btnDate.UseVisualStyleBackColor = true;
			btnDate.Click += btnDate_Click;
			btnDate.Visible = false;

			// 
			// Label
			// 
			lblLabel.AutoSize = true;
			lblLabel.Location = new Point(450, 5);
			lblLabel.Name = LabelName;
			lblLabel.Size = new Size(22, 13);
			lblLabel.TabIndex = 6;
			lblLabel.Visible = false;

			// 
			// Remove
			// 
			btnRemove.Font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(238)));
			btnRemove.Location = new Point(522, 1);
			btnRemove.Name = RemoveName;
			btnRemove.Size = new Size(25, 23);
			btnRemove.TabIndex = 7;
			btnRemove.Text = "-";
			btnRemove.TextAlign = ContentAlignment.TopCenter;
			btnRemove.UseVisualStyleBackColor = true;

			//populate data

			PanelDefinition definition;
			bool isChecked = false;
			int propertyIndex = -1;
			int compareIndex = -1;

			if (filter != null || property != null)
			{
				if (filter != null)
				{
					definition = PanelDefinitions.First(x => x.Property == filter.Property);
					propertyIndex = (int)filter.Property;
					compareIndex = filter.Index;
					isChecked = filter.Checked;

					if (filter.DataType == DataTypeEnum.String)
					{
						txtString.Text = filter.Text;
					}
					else if (filter.Property == PropertyEnum.T)
					{
						DateTime date = Utils.JDToDateTime(filter.Value);
						btnDate.Tag = dt;
						btnDate.Text = dt.ToString(DateTimeFormat);
					}
					else
					{
						txtValue.Text = filter.Text;
					}
				}
				else
				{
					definition = PanelDefinitions.First(x => x.Property == property);
					isChecked = false;
					propertyIndex = PanelDefinitions.IndexOf(definition);
					compareIndex = definition.InitialCompareIndex;
				}

				txtString.Visible = definition.StringVisible;

				txtValue.Visible = definition.ValueVisible;
				if (definition.ValueLemima != null)
					txtValue.Tag = definition.ValueLemima;

				btnDate.Visible = definition.DateVisible;

				lblLabel.Visible = !System.String.IsNullOrEmpty(definition.LabelStr);
				lblLabel.Text = definition.LabelStr;

				if (definition.DataType == DataTypeEnum.String)
					cboCompare.Items.AddRange(StringCompare);
				else
					cboCompare.Items.AddRange(ValueCompare);

				cboCompare.Visible = true;
				cboCompare.SelectedIndex = compareIndex;

				cbxChecked.Visible = true;
				cbxChecked.Checked = isChecked;
			}

			cboProperty.SelectedIndex = propertyIndex;

			cboProperty.SelectedIndexChanged += new EventHandler(property_SelectedIndexChanged);
			txtValue.KeyPress += new KeyPressEventHandler(txtCommon_KeyPress);
			txtValue.TextChanged += new EventHandler(txtCommon_TextChanged);
			txtString.TextChanged += new EventHandler(txtCommon_TextChanged);
			btnRemove.Click += btnRemove_Click;

			pnlPanel.ResumeLayout(false);
			pnlPanel.PerformLayout();

			parent.Controls.Add(pnlPanel);
			parent.ResumeLayout(false);
			parent.PerformLayout();
		}

		#endregion

		#region property_SelectedIndexChanged

		static void property_SelectedIndexChanged(object sender, EventArgs e)
		{
			ComboBox cbxProperty = sender as ComboBox;
			PanelDefinition definition = PanelDefinitions.ElementAt(cbxProperty.SelectedIndex);
			PropertyEnum property = definition.Property;

			Panel panel = cbxProperty.Parent as Panel;

			CheckBox cbx = null;
			ComboBox compare = null;
			TextBox str = null;
			TextBox value = null;
			Button date = null;
			Label label = null;

			foreach (Control c in panel.Controls)
			{
				if (c.Name == CheckedName)
					cbx = c as CheckBox;
				else if (c.Name == CompareName)
					compare = c as ComboBox;
				else if (c.Name == StringName)
					str = c as TextBox;
				else if (c.Name == ValueName)
					value = c as TextBox;
				else if (c.Name == DateName)
					date = c as Button;
				else if (c.Name == LabelName)
					label = c as Label;
			}

			if (definition.DataType == DataTypeEnum.String)
			{
				compare.Items.Clear();
				compare.Items.AddRange(StringCompare);
			}
			else
			{
				compare.Items.Clear();
				compare.Items.AddRange(ValueCompare);
			}

			cbx.Visible = true;

			compare.Visible = true;
			compare.SelectedIndex = definition.InitialCompareIndex;

			str.Visible = definition.StringVisible;
			value.Visible = definition.ValueVisible;
			date.Visible = definition.DateVisible;

			if (definition.ValueLemima != null)
				value.Tag = definition.ValueLemima;

			label.Visible = !String.IsNullOrEmpty(definition.LabelStr);
			label.Text = definition.LabelStr;
		}

		#endregion

		#region txtCommon_KeyPress

		static void txtCommon_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = Utils.HandleKeyPress(sender, e);
		}

		#endregion

		#region txtCommon_TextChanged

		static void txtCommon_TextChanged(object sender, EventArgs e)
		{
			TextBox txt = sender as TextBox;
			txt.Parent.Controls.OfType<CheckBox>().First().Checked = txt.Text.Trim().Length > 0;
		}

		#endregion

		#region btnDate_Click

		static void btnDate_Click(object sender, EventArgs e)
		{
			Button btn = sender as Button;
			DateTime dt = (DateTime)btn.Tag;

			FormDatabase fdb = btn.FindForm() as FormDatabase;
			double? T = fdb.SelectedComet != null ? (double?)fdb.SelectedComet.NextT : null;

			using (FormDateTime fdt = new FormDateTime(FormMain.DefaultDateStart, dt, T))
			{
				Form form = btn.FindForm();
				fdt.TopMost = form.TopMost;

				if (fdt.ShowDialog() == DialogResult.OK)
				{
					dt = fdt.SelectedDateTime;

					btn.Tag = dt;
					btn.Text = dt.ToString(DateTimeFormat);
					btn.Parent.Controls.OfType<CheckBox>().First().Checked = true;
				}
			}
		}

		#endregion

		#region btnRemove_Click

		static void btnRemove_Click(object sender, EventArgs e)
		{
			Button btn = sender as Button;
			Panel panel = btn.Parent as Panel;
			Panel container = panel.Parent as Panel;

			int panelId = panel.Name.Int();

			Panel toRemove = null;
			Button addNew = null;

			foreach (Control c in container.Controls)
			{
				if (c is Panel && c.Name.Int() == panelId)
				{
					toRemove = c as Panel;
					if (addNew != null)
						break;
				}
				else if (c is Button && c.Name == "btnNewFilter")
				{
					addNew = c as Button;
					if (toRemove != null)
						break;
				}
			}

			int offset = toRemove.Height + 6; //margin
			container.Controls.Remove(toRemove);

			int count = 0;
			//move other panels up
			foreach (Control c in container.Controls)
			{
				if (c is Panel)
				{
					count++;
					if (c.Name.Int() > panelId)
					{
						c.Location = new Point(c.Location.X, c.Location.Y - offset);
					}
				}
			}
			addNew.Visible = count < 10;
			addNew.Location = new Point(addNew.Location.X, addNew.Location.Y - offset);
		}

		#endregion

		#endregion
	}
}
