using System.Collections.Generic;
using CustomAntialiasing.Drawing2DMath;

namespace Antialiasing_app.Curves
{
	public static class DeCasteljau
	{

		/// <summary>
		/// Return point for the DeCasteljau recursion
		/// </summary>
		/// <param name="p"></param>
		/// <param name="r">Index</param>
		/// <param name="i">Index</param>
		/// <param name="t">Time parameter</param>
		/// <returns></returns>
		private static Matrix GetDeCasteljauPoint(ref List<Matrix> p, int r, int i, double t)
		{
			if (r == 0)
				return p[i];

			Matrix p1 = GetDeCasteljauPoint(ref p, r - 1, i, t);
			Matrix p2 = GetDeCasteljauPoint(ref p, r - 1, i + 1, t);

			return Matrix.GetVectorPointXY(
				(double)(1 - t) * p1[0, 0] + t * p2[0, 0],
				(double)(1 - t) * p1[0, 1] + t * p2[0, 1],
				Matrix.Direction.Horizontal);
		}

		/// <summary>
		/// Returns final points of the bezier curve
		/// </summary>
		/// <param name="inBaseControlPoints"></param>
		/// <param name="inTimeInterval"></param>
		/// <returns></returns>
		public static List<Matrix> Points(List<Matrix> inBaseControlPoints, double inTimeInterval)
		{
			// check the time interval
			if (!(inTimeInterval > 0.0 && inTimeInterval <= 1.0))
				return null;

			// check base points
			if (inBaseControlPoints == null || inBaseControlPoints.Count < 4)
				return null;

			List<Matrix> resultPoints = new List<Matrix>(); 

			for (double t = 0; t <= 1; t += inTimeInterval)
			{
				resultPoints.Add(GetDeCasteljauPoint(ref inBaseControlPoints, inBaseControlPoints.Count - 1, 0, t));
			}

			return resultPoints; 
		}

	}
}
