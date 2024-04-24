using System;

namespace Comets.OrbitViewer
{
	public class Matrix
	{
		#region Const

		const double GeneralPrec = 360.0 / 25920;
		const double PrecLimit = 30.0;

		#endregion

		#region Properties

		public double A11 { get; private set; }
		public double A12 { get; private set; }
		public double A13 { get; private set; }

		public double A21 { get; private set; }
		public double A22 { get; private set; }
		public double A23 { get; private set; }

		public double A31 { get; private set; }
		public double A32 { get; private set; }
		public double A33 { get; private set; }

		#endregion

		#region Constructor

		public Matrix()
		{
			this.A11 = this.A12 = this.A13 =
			this.A21 = this.A22 = this.A23 =
			this.A31 = this.A32 = this.A33 = 0.0;
		}

		public Matrix(double a11, double a12, double a13,
					  double a21, double a22, double a23,
					  double a31, double a32, double a33)
		{
			this.A11 = a11; this.A12 = a12; this.A13 = a13;
			this.A21 = a21; this.A22 = a22; this.A23 = a23;
			this.A31 = a31; this.A32 = a32; this.A33 = a33;
		}

		#endregion

		#region Mul

		/// <summary>
		/// Multiplication of Matrix
		/// </summary>
		/// <param name="mtx"></param>
		/// <returns></returns>
		public Matrix Mul(Matrix mtx)
		{
			double a11 = this.A11 * mtx.A11 + this.A12 * mtx.A21 + this.A13 * mtx.A31;
			double a21 = this.A21 * mtx.A11 + this.A22 * mtx.A21 + this.A23 * mtx.A31;
			double a31 = this.A31 * mtx.A11 + this.A32 * mtx.A21 + this.A33 * mtx.A31;

			double a12 = this.A11 * mtx.A12 + this.A12 * mtx.A22 + this.A13 * mtx.A32;
			double a22 = this.A21 * mtx.A12 + this.A22 * mtx.A22 + this.A23 * mtx.A32;
			double a32 = this.A31 * mtx.A12 + this.A32 * mtx.A22 + this.A33 * mtx.A32;

			double a13 = this.A11 * mtx.A13 + this.A12 * mtx.A23 + this.A13 * mtx.A33;
			double a23 = this.A21 * mtx.A13 + this.A22 * mtx.A23 + this.A23 * mtx.A33;
			double a33 = this.A31 * mtx.A13 + this.A32 * mtx.A23 + this.A33 * mtx.A33;

			return new Matrix(a11, a12, a13,
							  a21, a22, a23,
							  a31, a32, a33);
		}

		/// <summary>
		/// Multiplication of Matrix by double
		/// </summary>
		/// <param name="x"></param>
		/// <returns></returns>
		public Matrix Mul(double x)
		{
			double a11 = this.A11 * x;
			double a21 = this.A21 * x;
			double a31 = this.A31 * x;

			double a12 = this.A12 * x;
			double a22 = this.A22 * x;
			double a32 = this.A32 * x;

			double a13 = this.A13 * x;
			double a23 = this.A23 * x;
			double a33 = this.A33 * x;

			return new Matrix(a11, a12, a13,
							  a21, a22, a23,
							  a31, a32, a33);
		}

		#endregion

		#region RotateX

		/// <summary>
		/// Create Rotation Matrix Around X-Axis
		/// </summary>
		/// <param name="angle"></param>
		/// <returns></returns>
		public static Matrix RotateX(double angle)
		{
			double a11 = 1.0;
			double a12 = 0.0;
			double a13 = 0.0;
			double a21 = 0.0;
			double a22 = Math.Cos(angle);
			double a23 = Math.Sin(angle);
			double a31 = 0.0;
			double a32 = -Math.Sin(angle);
			double a33 = Math.Cos(angle);

			return new Matrix(a11, a12, a13,
							  a21, a22, a23,
							  a31, a32, a33);
		}

		#endregion

		#region RotateY

		/// <summary>
		/// Create Rotation Matrix Around Y-Axis
		/// </summary>
		/// <param name="angle"></param>
		/// <returns></returns>
		public static Matrix RotateY(double angle)
		{
			double a11 = Math.Cos(angle);
			double a12 = 0.0;
			double a13 = -Math.Sin(angle);
			double a21 = 0.0;
			double a22 = 1.0;
			double a23 = 0.0;
			double a31 = Math.Sin(angle);
			double a32 = 0.0;
			double a33 = Math.Cos(angle);

			return new Matrix(a11, a12, a13,
							  a21, a22, a23,
							  a31, a32, a33);
		}

		#endregion

		#region RotateZ

		/// <summary>
		/// Create Rotation Matrix Around Z-Axis
		/// </summary>
		/// <param name="angle"></param>
		/// <returns></returns>
		public static Matrix RotateZ(double angle)
		{
			double a11 = Math.Cos(angle);
			double a12 = Math.Sin(angle);
			double a13 = 0.0;
			double a21 = -Math.Sin(angle);
			double a22 = Math.Cos(angle);
			double a23 = 0.0;
			double a31 = 0.0;
			double a32 = 0.0;
			double a33 = 1.0;

			return new Matrix(a11, a12, a13,
							  a21, a22, a23,
							  a31, a32, a33);
		}

		#endregion

		#region Invert

		/// <summary>
		/// Invert Matrix
		/// </summary>
		public void Invert()
		{
			double a = 1.0 /
				(this.A11 * (this.A22 * this.A33 - this.A23 * this.A32)
			   - this.A12 * (this.A21 * this.A33 - this.A23 * this.A31)
			   + this.A13 * (this.A21 * this.A32 - this.A22 * this.A31));

			double a11 = 1.0 * a * (this.A22 * this.A33 - this.A23 * this.A32);
			double a12 = -1.0 * a * (this.A12 * this.A33 - this.A13 * this.A32);
			double a13 = 1.0 * a * (this.A12 * this.A23 - this.A13 * this.A22);

			double a21 = -1.0 * a * (this.A21 * this.A33 - this.A23 * this.A31);
			double a22 = 1.0 * a * (this.A11 * this.A33 - this.A13 * this.A31);
			double a23 = -1.0 * a * (this.A11 * this.A23 - this.A13 * this.A21);

			double a31 = 1.0 * a * (this.A21 * this.A32 - this.A22 * this.A31);
			double a32 = -1.0 * a * (this.A11 * this.A32 - this.A12 * this.A31);
			double a33 = 1.0 * a * (this.A11 * this.A22 - this.A12 * this.A21);

			this.A11 = a11; this.A12 = a12; this.A13 = a13;
			this.A21 = a21; this.A22 = a22; this.A23 = a23;
			this.A31 = a31; this.A32 = a32; this.A33 = a33;
		}

		#endregion

		#region PrecMatrix

		/// <summary>
		/// Create Precession Matrix
		/// </summary>
		/// <param name="oldEpoch"></param>
		/// <param name="newEpoch"></param>
		/// <returns></returns>
		public static Matrix PrecMatrix(double oldEpoch, double newEpoch)
		{
			double jd = 0.0;
			bool swapEpoch = false;
			bool outerNewcomb = false;

			if (newEpoch == oldEpoch)
			{
				return new Matrix(1.0, 0.0, 0.0,
								  0.0, 1.0, 0.0,
								  0.0, 0.0, 1.0);
			}

			double T = (oldEpoch - Astro.JD2000) / 36525.0;

			if (T < -PrecLimit || PrecLimit < T)
			{
				swapEpoch = true;
				double temp = newEpoch;
				newEpoch = oldEpoch;
				oldEpoch = temp;
				T = (oldEpoch - Astro.JD2000) / 36525.0;
			}

			double T2 = T * T;
			double tt, t;
			tt = t = (newEpoch - oldEpoch) / 36525.0;

			if (tt < -PrecLimit)
			{
				outerNewcomb = true;
				t = -PrecLimit;
				jd = -PrecLimit * 36525.0 + Astro.JD2000;
			}

			if (PrecLimit < tt)
			{
				outerNewcomb = true;
				t = PrecLimit;
				jd = PrecLimit * 36525.0 + Astro.JD2000;
			}

			double t2 = t * t;
			double t3 = t2 * t;

			double zeta0 = ((2306.2181 + 1.39656 * T - 0.000139 * T2) * t + (0.30188 - 0.000344 * T) * t2 + 0.017998 * t3) / 3600.0;
			double zpc = ((2306.2181 + 1.39656 * T - 0.000139 * T2) * t + (1.09468 + 0.000066 * T) * t2 + 0.018203 * t3) / 3600.0;
			double theta = ((2004.3109 - 0.85330 * T - 0.000217 * T2) * t - (0.42665 + 0.000217 * T) * t2 - 0.041833 * t3) / 3600.0;

			Matrix mtx1, mtx2, mtx3;
			mtx1 = RotateZ((90.0 - zeta0) * Math.PI / 180.0);
			mtx2 = RotateX(theta * Math.PI / 180.0);
			mtx3 = mtx2.Mul(mtx1);
			mtx1 = RotateZ((-90 - zpc) * Math.PI / 180.0);

			Matrix mtxPrec;
			mtxPrec = mtx1.Mul(mtx3);

			if (outerNewcomb)
			{
				double dJd;

				if (tt < -PrecLimit)
				{
					dJd = (newEpoch - oldEpoch) + PrecLimit * 36525.0;
				}
				else
				{
					dJd = (newEpoch - oldEpoch) - PrecLimit * 36525.0;
				}

				double precPrm = -dJd / 365.24 * GeneralPrec * Math.PI / 180.0;
				double eps = ATime.GetEp(jd);
				mtx1 = RotateX(eps);
				mtx2 = RotateZ(precPrm);
				mtx3 = mtx2.Mul(mtx1);
				mtx2 = RotateX(-eps);
				mtx1 = mtx2.Mul(mtx3);
				mtxPrec = mtx1.Mul(mtxPrec);
			}

			if (swapEpoch)
			{
				mtxPrec.Invert();
			}

			return mtxPrec;
		}

		#endregion

		#region VectorConstant

		/// <summary>
		/// Get Vector Constant from Angle Elements
		/// </summary>
		/// <param name="peri"></param>
		/// <param name="node"></param>
		/// <param name="incl"></param>
		/// <param name="equinox"></param>
		/// <returns></returns>
		public static Matrix VectorConstant(double peri, double node, double incl, ATime equinox)
		{
			// Equinox
			double t1 = equinox.T;
			double t2 = equinox.T2;

			// Obliquity of Ecliptic
			double eps;
			if (t2 < -40.0)
			{
				eps = 23.83253 * Math.PI / 180.0;
			}
			else if (t2 > 40.0)
			{
				eps = 23.05253 * Math.PI / 180.0;
			}
			else
			{
				eps = 23.44253 - 0.00013 * t1
					+ 0.00256 * Math.Cos((249.0 - 19.3 * t1) * Math.PI / 180.0)
					+ 0.00015 * Math.Cos((198.0 + 720.0 * t1) * Math.PI / 180.0);
				eps *= Math.PI / 180.0;
			}

			double sinEps = Math.Sin(eps);
			double cosEps = Math.Cos(eps);

			double sinPeri = Math.Sin(peri);
			double sinNode = Math.Sin(node);
			double sinIncl = Math.Sin(incl);
			double cosPeri = Math.Cos(peri);
			double cosNode = Math.Cos(node);
			double cosIncl = Math.Cos(incl);
			double wa = cosPeri * sinNode + sinPeri * cosIncl * cosNode;
			double wb = -sinPeri * sinNode + cosPeri * cosIncl * cosNode;

			double a11 = cosPeri * cosNode - sinPeri * cosIncl * sinNode;
			double a21 = wa * cosEps - sinPeri * sinIncl * sinEps;
			double a31 = wa * sinEps + sinPeri * sinIncl * cosEps;
			double a12 = -sinPeri * cosNode - cosPeri * cosIncl * sinNode;
			double a22 = wb * cosEps - cosPeri * sinIncl * sinEps;
			double a32 = wb * sinEps + cosPeri * sinIncl * cosEps;

			return new Matrix(a11, a12, 0.0,
							  a21, a22, 0.0,
							  a31, a32, 0.0);
		}

		#endregion
	}
}