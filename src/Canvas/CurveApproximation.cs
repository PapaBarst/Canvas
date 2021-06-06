using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Canvas
{
    /// <summary>
    /// A drawable approximation of a mathematical function using NCalc. Works best with continuous functions. 
    /// </summary>
    public class CurveApproximation : Drawable
    {
        private PointF[] cachedPoints;
        private NCalc.Expression internalExpression;
        /// <summary>
        /// The list of points for the approximation, generated on creation of a <c>CurveApproximation</c> object.
        /// </summary>
        public List<PointF> Points;
        /// <summary>
        /// Controls if the curve is drawn closed.
        /// </summary>
        public bool Closed = false;
        /// <summary>
        /// Controls if the curve is drawn with a border.
        /// </summary>
        public bool Border = true;
        /// <summary>
        /// Controls whether the approximation is forcibly scaled to fit within the <c>Width</c> and <c>Height</c> of this object.
        /// </summary>
        /// <remarks>Currently, changing this property after object creation does nothing.</remarks>
        public bool ForceScale = false;
        /// <summary>
        /// The X location of the <c>CurveApproximation</c>.
        /// </summary>
        public float X
        {
            get;
            private set;
        }
        /// <summary>
        /// The Y location of the <c>CurveApproximation</c>.
        /// </summary>
        public float Y
        {
            get;
            private set;
        }
        /// <summary>
        /// The width of the <c>CurveApproximation</c>. If no width is given on object creation, will be the width of the curve.
        /// </summary>
        /// <remarks>Currently, changing this property after object creation does nothing.</remarks>
        public float Width
        {
            get;
            private set;
        }
        /// <summary>
        /// The height of the <c>CurveApproximation</c>. If no height is given on object creation, will be the height of the curve.
        /// </summary>
        /// <remarks>Currently, changing this property after object creation does nothing.</remarks>
        public float Height
        {
            get;
            private set;
        }
        /// <summary>
        /// The fill color of the <c>CurveApproximation</c>.
        /// </summary>
        public Color FillColor = Color.Gray;
        /// <summary>
        /// The border color of the <c>CurveApproximation</c>.
        /// </summary>
        public Color BorderColor = Color.Black;
        /// <summary>
        /// The amount of points in the approximated curve.
        /// </summary>
        public int NumPoints
        {
            get;
            private set;
        }
        /// <summary>
        /// Approximates the curve between the given x-values with the given number of points. Higher point amounts lead to higher accuracy but lower performance.
        /// </summary>
        /// <remarks>Formula format can be very specific. You must use the mathematical symbols, not shorthand. Refer to NCalc documentation for more info.</remarks>
        /// <param name="posX"></param>
        /// <param name="posY"></param>
        /// <param name="formula"></param>
        /// <param name="startx"></param>
        /// <param name="endx"></param>
        /// <param name="numPoints"></param>
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
        /// <summary>
        /// Approximates the curve between the given x-values with the given number of points. Scales the curve to fit the given width and height. Higher point amounts lead to higher accuracy but lower performance.
        /// </summary>
        /// <remarks>Formula format can be very specific. You must use mathematical symbols, not shorthand. Refer to NCalc documentation for more info.</remarks>
        /// <param name="posX"></param>
        /// <param name="posY"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="formula"></param>
        /// <param name="startx"></param>
        /// <param name="endx"></param>
        /// <param name="numPoints"></param>
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
        /// <summary>
        /// An empty curve approximation, for whatever reason. Simply initializes variables, nothing more.
        /// </summary>
        public CurveApproximation()
        {
            Points = new List<PointF>();
        }
        /// <summary>
        /// Draws a curve through the listed points.
        /// </summary>
        /// <param name="g"></param>
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
