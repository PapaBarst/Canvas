using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Canvas
{
    class CurveApproximation : Drawable
    {
        private PointF[] cachedPoints;
        private NCalc.Expression internalExpression;
        public List<PointF> Points;
        public bool Closed = false;
        public bool Border = true;
        public bool ForceScale = false;

        public float X
        {
            get;
            private set;
        }
        public float Y
        {
            get;
            private set;
        }
        public float Width
        {
            get;
            private set;
        }
        public float Height
        {
            get;
            private set;
        }
        public Color FillColor = Color.Gray;
        public Color BorderColor = Color.Black;
        public int NumPoints
        {
            get;
            private set;
        }

        //produces
        public CurveApproximation(float posX, float posY, String formula, double startx, double endx, int numPoints)
        {
            Points = new List<PointF>();
            NumPoints = numPoints+1;

            if (numPoints-2 <= 0)
            {
                throw new ArgumentException("numPoints must be greater than 2");
            }
            internalExpression = new NCalc.Expression(formula);
            double increment = (endx - startx) / (numPoints - 1);
            float minHeight = 0;
            float maxHeight = 0;
            for (double i=startx; i <= endx; i += increment)
            {
                internalExpression.Parameters.Add("x", i);
                Points.Add(new PointF(posX+(float)i, posY-(float)((double)internalExpression.Evaluate())));
                internalExpression.Parameters.Remove("x");

                if (Points[Points.Count - 1].Y > maxHeight)
                {
                    maxHeight = Points[Points.Count - 1].Y;
                }
                if (Points[Points.Count - 1].Y < minHeight)
                {
                    minHeight = Points[Points.Count - 1].Y;
                }
            }

            Width = (float)(endx - startx);
            Height = maxHeight - minHeight;

            X = posX;
            Y = posY;

            //foreach(PointF p in Points)
            //{
            //    Console.WriteLine("x: " + p.X + " y: " + p.Y);
            //}
            //Console.WriteLine();
        }

        //this constructor forces scaling
        public CurveApproximation(float posX, float posY, float width, float height, String formula, double startx, double endx, int numPoints)
        {
            Points = new List<PointF>();
            NumPoints = numPoints + 1;
            ForceScale = true;

            if (numPoints - 2 <= 0)
            {
                throw new ArgumentException("numPoints must be greater than 2");
            }
            internalExpression = new NCalc.Expression(formula);
            double increment = (endx - startx) / (numPoints - 1);
            float minHeight = 0;
            float maxHeight = 0;
            for (double i = startx; i <= endx; i += increment)
            {
                internalExpression.Parameters.Add("x", i);
                Points.Add(new PointF((float)i, (float)((double)internalExpression.Evaluate())));
                if (Points[Points.Count - 1].Y > maxHeight)
                {
                    maxHeight = Points[Points.Count - 1].Y;
                }
                if (Points[Points.Count - 1].Y < minHeight)
                {
                    minHeight = Points[Points.Count - 1].Y;
                }
                internalExpression.Parameters.Remove("x");
                //
            }

            List<PointF> PointsCorrected = new List<PointF>();
            for (int i=0; i<Points.Count; i++)
            {
                PointF p = Points[i];
                PointsCorrected.Add(new PointF(posX+(float)(((p.X-startx)/(endx-startx))*width), posY + (float)(((p.Y - minHeight) / (maxHeight - minHeight)) * height)));
            }
            Points = PointsCorrected;

            Width = width;
            Height = height;

            X = posX;
            Y = posY;
        }

        public CurveApproximation()
        {
            Points = new List<PointF>();
        }
        public void Draw(Graphics g)
        {
            if (cachedPoints == null)
            {
                cachedPoints = Points.ToArray();
            }
            //foreach(PointF p in cachedPoints)
            //{
            //    Console.WriteLine("x: " + p.X + " y: " + p.Y);
            //    g.FillEllipse(Brushes.Black, p.X, p.Y, 5, 5);
            //}

            if (Closed)
            {
                g.FillClosedCurve(new SolidBrush(FillColor), cachedPoints);
            }
            if (Border)
            {
                g.DrawCurve(new Pen(BorderColor), cachedPoints);
            }
        }
    }
}
