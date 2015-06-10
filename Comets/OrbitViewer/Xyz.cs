using System;

namespace Comets.OrbitViewer
{
	/// <summary>
	/// 3-Dimensional Vector
	/// </summary>
	public class Xyz
	{
		#region Properties

		public double X { get; private set; }
		public double Y { get; private set; }
		public double Z { get; private set; }

		#endregion

		#region Constructor

		public Xyz(double X = 0.0, double Y = 0.0, double Z = 0.0)
		{
			this.X = X;
			this.Y = Y;
			this.Z = Z;
		}

		#endregion

		#region + Methods

		/// <summary>
		/// Rotation of Vector with Matrix
		/// </summary>
		/// <param name="mtx"></param>
		/// <returns></returns>
		public Xyz Rotate(Matrix mtx)
		{
			double X = mtx.A11 * this.X + mtx.A12 * this.Y + mtx.A13 * this.Z;
			double Y = mtx.A21 * this.X + mtx.A22 * this.Y + mtx.A23 * this.Z;
			double Z = mtx.A31 * this.X + mtx.A32 * this.Y + mtx.A33 * this.Z;
			return new Xyz(X, Y, Z);
		}

		/// <summary>
		/// V := V1 + V2
		/// </summary>
		/// <param name="xyz"></param>
		/// <returns></returns>
		public Xyz Add(Xyz xyz)
		{
			double X = this.X + xyz.X;
			double Y = this.Y + xyz.Y;
			double Z = this.Z + xyz.Z;
			return new Xyz(X, Y, Z);
		}

		/// <summary>
		/// V := V1 - V2
		/// </summary>
		/// <param name="xyz"></param>
		/// <returns></returns>
		public Xyz Sub(Xyz xyz)
		{
			double X = this.X - xyz.X;
			double Y = this.Y - xyz.Y;
			double Z = this.Z - xyz.Z;
			return new Xyz(X, Y, Z);
		}


		/// <summary>
		/// V := x * V
		/// </summary>
		/// <param name="A"></param>
		/// <returns></returns>
		public Xyz Mul(double A)
		{
			double X = this.X * A;
			double Y = this.Y * A;
			double Z = this.Z * A;
			return new Xyz(X, Y, Z);
		}

		/// <summary>
		/// x := abs(V)
		/// </summary>
		/// <returns></returns>
		public double Abs()
		{
			return Math.Sqrt(this.X * this.X
						   + this.Y * this.Y
						   + this.Z * this.Z);
		}

		#endregion
	}
}