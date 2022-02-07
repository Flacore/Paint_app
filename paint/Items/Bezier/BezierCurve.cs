using Antialiasing_app.Tools;
using CustomAntialiasing.Drawing2DMath;
using Editor2D.Interfaces;
using System.Collections.Generic;
using System.Drawing;


namespace Antialiasing_app.Curves
{
	public class BezierCurve: IDrawable2DObject
	{
		private bool finished;
		private int size;
		private Color color;
		private Matrix startVertex;
		private Matrix endVertex; 
		private List<Matrix> controlPoints;
		private List<Matrix> selectedPoints;

		// Konstruktor
		public BezierCurve()
		{
			finished = false;
			color = Color.Black;
			controlPoints = new List<Matrix>(); 
			selectedPoints = new List<Matrix>();
			size = 10;
		}

		// Konstruktor 2
		public BezierCurve(Matrix inStartVertex, Matrix inEndVertex): this()
		{
			finished = false;
			color = Color.Black;
			startVertex = inStartVertex;
			endVertex = inEndVertex;
			controlPoints = new List<Matrix>();
			selectedPoints = new List<Matrix>();
			size = 10;
		}

		// Konstruktor 3
		public BezierCurve(Matrix inStartVertex, Matrix inEndVertex, Color c) : this()
		{
			finished = false;
			color = Color.Black;
			startVertex = inStartVertex;
			endVertex = inEndVertex;
			controlPoints = new List<Matrix>();
			selectedPoints = new List<Matrix>();
			size = 10;
		}

		// Konstruktor 4
		public BezierCurve(Matrix inStartVertex, Matrix inEndVertex,List<Matrix> points, Color c) : this()
		{
			finished = false;
			startVertex = inStartVertex;
			endVertex = inEndVertex;
			controlPoints = new List<Matrix>();
			selectedPoints = new List<Matrix>();
			color = c;
			controlPoints = points;
			size = 10;
		}

		// Konstruktor 5
		public BezierCurve(Matrix inStartVertex, Matrix inEndVertex, List<Matrix> points, Color c, int size_) : this()
		{
			finished = false;
			startVertex = inStartVertex;
			endVertex = inEndVertex;
			controlPoints = new List<Matrix>();
			selectedPoints = new List<Matrix>();
			color = c;
			controlPoints = points;
			size = size_;
		}

		// Konstruktor 6
		public BezierCurve(Matrix inStartVertex, Matrix inEndVertex, List<Matrix> points, Color c, int size_,List<Matrix> pts) : this()
		{
			finished = false;
			startVertex = inStartVertex;
			endVertex = inEndVertex;
			controlPoints = pts;
			selectedPoints = new List<Matrix>();
			color = c;
			controlPoints = points;
			size = size_;
		}

		public void addStart(Point pt) { 
			startVertex = Matrix.GetVectorPointXY(pt.X, pt.Y);
		}

		public void addPoint(Point pt)
		{
			controlPoints.Add(Matrix.GetVectorPointXY(pt.X, pt.Y));
		}

		public void addEnd(Point pt)
		{
			endVertex = Matrix.GetVectorPointXY(pt.X, pt.Y);
		}

		public void setColor(Color color_)
		{
			color = color_;
		}

		public void setSize(int size_)
		{
			size = size_;
		}

		public void SetAsFinished() {
			finished = true;
		}

		public List<Point> getBezierPoints() {
			List<Point> poi = new List<Point>();

			// get bezier point control vertices
			var bcc = GetBezierPointList();

			// calculate the actual bezier curve points
			List<Matrix> bcp = DeCasteljau.Points(bcc, 0.02);

			for (int i = 0; i < bcp.Count - 1; i++)
			{
				poi.Add(Math2DCalculations.WorldCoordToWindowCoord(new PointF((float)bcp[i][0, 0], (float)bcp[i][0, 1])));
			}

			return poi;
		}

		/// <summary>
		/// Add control vertex
		/// </summary>
		/// <param name="v"></param>
		public void AddControlVertex(Matrix v)
		{
			controlPoints.Add(v);
		}

		/// <summary>
		/// Vrati usporiadany zoznam bodov bezierovej krivky
		/// </summary>
		/// <returns></returns>
		private List<Matrix> GetBezierPointList()
		{
			List<Matrix> curvePoints = new List<Matrix>();

			curvePoints.Add(startVertex);

			foreach (Matrix p in controlPoints)
			{
				curvePoints.Add(p);
			}

			curvePoints.Add(endVertex);

			return curvePoints; 
		}

		/// <summary>
		/// Vrati usporiadany zoznam vybranych bodov bezierovej krivky
		/// </summary>
		/// <returns></returns>
		public List<Matrix> GetBezierPointSelected()
		{
			List<Matrix> curvePoints = new List<Matrix>();

			foreach (Matrix p in selectedPoints)
			{
				curvePoints.Add(p);
			}

			return curvePoints;
		}

		/// <summary>
		/// Pre Zadanu oblast oznaci vsetky body.
		/// Berie do uvahy ze bod ma okolo seba bod ktory je od stredu na kazdu stranu +5px;
		/// </summary>
		/// <returns></returns>
		public bool SelectArea(Point p1, Point p2)
		{
			bool finded = false;
			int minX, maxX;
			int minY, maxY;

			if (p1.X - (592 / 2) < p2.X - (592 / 2))
			{
				minX = p1.X - (592 / 2) - 5;
				maxX = p2.X - (592 / 2) + 5;
			}
			else {
				minX = p2.X - (592 / 2) - 5;
				maxX = p1.X - (592 / 2) + 5;
			}

			if ((450 / 2) - p1.Y < (450 / 2) - p2.Y)
			{
				minY = (450 / 2) - p1.Y - 5;
				maxY = (450 / 2) - p2.Y + 5;
			}
			else
			{
				minY = (450 / 2) - p2.Y - 5;
				maxY = (450 / 2) - p1.Y + 5;
			}


			selectedPoints.Clear();


			foreach (var c in this.GetBezierPointList())
			{
				if (minX <= c[0, 0] && c[0,0] <= maxX && minY <= c[0, 1] && c[0, 1] <= maxY)
				{
					selectedPoints.Add(c);
					finded = true;
				}
			}

			return finded;
		}

		/// <summary>
		/// Zadany bod oznaci za vybrany.
		/// Berie do uvahy ze bod ma okolo seba bod ktory je od stredu na kazdu stranu +5px;
		/// </summary>
		/// <returns></returns>
		public bool SelectPoint(Point p1)
		{
			p1.X = p1.X - (592 / 2);
			p1.Y = (450/ 2) - p1.Y;

			selectedPoints.Clear();

			for (int x = (p1.X - 5); x <= (p1.X + 5); x++) {
				for (int y = (p1.Y - 5); y <= (p1.Y + 5); y++)
				{
					foreach (var c in this.GetBezierPointList())
					{
						if (c.ToString() == Matrix.GetVectorPointXY(x, y).ToString())
						{
							selectedPoints.Add(c);
							return true;
						}
					}
				}
			}

			return false;
		}

		/// <summary>
		/// Odstrani vsetky oznacenia
		/// </summary>
		/// <returns></returns>
		public void clearSelection() {
			selectedPoints.Clear();
		}

		/// <summary>
		/// Skontroluje ci je prvok vybranny
		/// </summary>
		/// <returns></returns>
		private bool isSelected(Matrix p)
		{
			foreach(var c in selectedPoints) {
				if (p.ToString() == c.ToString())
					return true;
			}

			return false;
		}

		/// <summary>
		/// Vrati pocet oznacenich prvkov
		/// </summary>
		/// <returns></returns>
		private int thereAreSelected()
		{
			return selectedPoints.Count;
		}

		/// <summary>
		/// Change value of vertex
		/// </summary>
		/// <param name="selected"></param>
		/// <param name="nev"></param>
		public bool ChangeVertex(Matrix selected, Matrix nev) {
			bool for_return = false;

			if (startVertex.ToString() == selected.ToString()) {
				startVertex = nev;
				for_return = true;
			}

			if (endVertex.ToString() == selected.ToString()) {
				endVertex = nev;
				for_return = true;
			}

			int n = 0;
			foreach (var c in controlPoints)
			{
				if (selected.ToString() == c.ToString())
				{
					controlPoints.Insert(n,nev);
					controlPoints.Remove(c);
					for_return = true;
					break;
				}
				n++;
			}

			if (for_return)
			{
				selectedPoints.Remove(selected);
				selectedPoints.Add(nev);
			}

			return for_return;
		}

		/// <summary>
		/// Move value of vertex
		/// </summary>
		/// <param name="selected"></param>
		/// <param name="nev"></param>
		public void MoveVertex(int diffX, int diffY)
		{
			bool change = false, start = false, end = false ;
			foreach (var k in selectedPoints)
			{
				int n = 0;
				foreach (var c in controlPoints)
				{
					if (k.ToString() == c.ToString())
					{
						controlPoints.Insert(n, Matrix.GetVectorPointXY(k[0,0]+diffX,k[0,1]+diffY));
						controlPoints.Remove(c);
						change = true;
						break;
					}
					if (k.ToString() == startVertex.ToString()) {
						start = true;
						break;
					}
					if (k.ToString() == endVertex.ToString())
					{
						end = true;
						break;
					}
					n++;
				}
			}

			if (change)
			{
				List<Matrix> tmp = new List<Matrix>(selectedPoints);
				foreach (var k in tmp)
				{
					selectedPoints.Add(Matrix.GetVectorPointXY(k[0, 0] + diffX, k[0, 1] + diffY));
					selectedPoints.Remove(k);
				}
			}

			if (start)
			{
				selectedPoints.Remove(startVertex);
				startVertex = Matrix.GetVectorPointXY(startVertex[0, 0] + diffX, startVertex[0, 1] + diffY);
				selectedPoints.Add(startVertex);
			}

			if (end)
			{
				selectedPoints.Remove(endVertex);
				endVertex = Matrix.GetVectorPointXY(endVertex[0, 0] + diffX, endVertex[0, 1] + diffY);
				selectedPoints.Add(endVertex);
			}
		}

		/// <summary>
		/// Vykreslenie bezierovej krivky
		/// </summary>
		/// <param name="graphics"></param>
		public void Draw(Graphics g, Pen colr)
		{
			// get bezier point control vertices
			var bcc = GetBezierPointList();

			// calculate the actual bezier curve points
			List<Matrix> bcp = DeCasteljau.Points(bcc, 0.02);

			if (bcp == null)
				return;

			if (!finished)
			{
				// draw lines between control points
				for (int i = 0; i < bcc.Count - 1; i++)
				{
					var p1 = Math2DCalculations.WorldCoordToWindowCoord(new PointF((float)bcc[i][0, 0], (float)bcc[i][0, 1]));
					var p2 = Math2DCalculations.WorldCoordToWindowCoord(new PointF((float)bcc[i + 1][0, 0], (float)bcc[i + 1][0, 1]));
					g.DrawLine(Pens.Black, p1, p2);
				}

				//control points ends
				int pointIndex = 0;
				using (var f = new Font("Arial", 10))
				{
					foreach (var p in bcc)
					{
						var point = Math2DCalculations.WorldCoordToWindowCoord(new PointF((float)p[0, 0], (float)p[0, 1]));
						var rect = new Rectangle(new Point(point.X - 5, point.Y - 5), new Size(10, 10));
						if (isSelected(p))
							g.FillRectangle(Brushes.Green, rect);
						else
							g.FillRectangle(Brushes.DarkOrange, rect);
						g.DrawRectangle(Pens.Black, rect);
						g.DrawString("C" + pointIndex++.ToString(), f, Brushes.Black, new Point(rect.X + 10, rect.Y + 10));
					}
				}
			}
			
			// draw linear interpolation between points
			for (int i = 0; i < bcp.Count - 1; i++)
			{
				var p1 = Math2DCalculations.WorldCoordToWindowCoord(new PointF((float)bcp[i][0, 0], (float)bcp[i][0,1]));
				var p2 = Math2DCalculations.WorldCoordToWindowCoord(new PointF((float)bcp[i+1][0, 0], (float)bcp[i+1][0, 1]));
				g.DrawLine(colr, p1, p2);
			}

			if (!finished)
			{
				// draw bezier curve points
				foreach (var p in bcp)
				{
					PointF tp = new PointF((float)p[0, 0], (float)p[0, 1]);
					var point = Math2DCalculations.WorldCoordToWindowCoord(tp);

					g.FillRectangle(Brushes.Red, new Rectangle(point.X, point.Y, 2, 2));
				}
			}
		}

		/// <summary>
		/// Vykreslenie bezierovej krivky
		/// </summary>
		/// <param name="graphics"></param>
		public void Draw(Graphics g)
		{
			// get bezier point control vertices
			var bcc = GetBezierPointList();

			// calculate the actual bezier curve points
			List<Matrix> bcp = DeCasteljau.Points(bcc, 0.02);

			if (bcp == null)
				return;

			if (!finished)
			{
				// draw lines between control points
				for (int i = 0; i < bcc.Count - 1; i++)
				{
					var p1 = Math2DCalculations.WorldCoordToWindowCoord(new PointF((float)bcc[i][0, 0], (float)bcc[i][0, 1]));
					var p2 = Math2DCalculations.WorldCoordToWindowCoord(new PointF((float)bcc[i + 1][0, 0], (float)bcc[i + 1][0, 1]));
					g.DrawLine(Pens.Black, p1, p2);
				}

				//control points ends
				int pointIndex = 0;
				using (var f = new Font("Arial", 10))
				{
					foreach (var p in bcc)
					{
						var point = Math2DCalculations.WorldCoordToWindowCoord(new PointF((float)p[0, 0], (float)p[0, 1]));
						var rect = new Rectangle(new Point(point.X - 5, point.Y - 5), new Size(10, 10));
						if (isSelected(p))
							g.FillRectangle(Brushes.Green, rect);
						else
							g.FillRectangle(Brushes.DarkOrange, rect);
						g.DrawRectangle(Pens.Black, rect);
						g.DrawString("C" + pointIndex++.ToString(), f, Brushes.Black, new Point(rect.X + 10, rect.Y + 10));
					}
				}
			}

			// draw linear interpolation between points
			for (int i = 0; i < bcp.Count - 1; i++)
			{
				var p1 = Math2DCalculations.WorldCoordToWindowCoord(new PointF((float)bcp[i][0, 0], (float)bcp[i][0, 1]));
				var p2 = Math2DCalculations.WorldCoordToWindowCoord(new PointF((float)bcp[i + 1][0, 0], (float)bcp[i + 1][0, 1]));
				g.DrawLine(new Pen(color,size), p1, p2);
			}

			if (!finished)
			{
				// draw bezier curve points
				foreach (var p in bcp)
				{
					PointF tp = new PointF((float)p[0, 0], (float)p[0, 1]);
					var point = Math2DCalculations.WorldCoordToWindowCoord(tp);

					g.FillRectangle(Brushes.Red, new Rectangle(point.X, point.Y, 2, 2));
				}
			}
		}
    }
}
