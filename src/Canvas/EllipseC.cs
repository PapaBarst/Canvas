using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Canvas
{
    /// <summary>
    /// A representation of an ellipse.
    /// </summary>
    public class EllipseC : Drawable
    {
        /// <summary>
        /// X location of the ellipse.
        /// </summary>
        public float X
        {
            get
            {
                return BoundingRect.X;
            }
            set
            {
                BoundingRect.X = value;
            }
        }
        /// <summary>
        /// Y location of the ellipse.
        /// </summary>
        public float Y
        {
            get
            {
                return BoundingRect.Y;
            }
            set
            {
                BoundingRect.Y = value;
            }

        }
        /// <summary>
        /// Width of the ellipse.
        /// </summary>
        public float Width
        {
            get
            {
                return BoundingRect.Width;
            }
            set
            {
                BoundingRect.Width = value;
            }
        }
        /// <summary>
        /// Height of the ellipse.
        /// </summary>
        public float Height
        {
            get
            {
                return BoundingRect.Height;
            }
            set
            {
                BoundingRect.Height = value;
            }
        }
        /// <summary>
        /// The point the ellipse will rotate around.
        /// </summary>
        public PointF RotationCenter;
        /// <summary>
        /// The rotation of the ellipse in radians.
        /// </summary>
        public double Rotation = 0;
        private RectangleF BoundingRect;
        /// <summary>
        /// Controls if the ellipse is filled.
        /// </summary>
        public bool Fill = true;
        /// <summary>
        /// Controls if the ellipse has a border.
        /// </summary>
        public bool Border = false;
        /// <summary>
        /// The fill color of the ellipse.
        /// </summary>
        public Color FillColor = Color.Gray;
        /// <summary>
        /// The border color of the ellipse.
        /// </summary>
        public Color BorderColor = Color.Black;
        /// <summary>
        /// Creates an ellipse with the given bounding rectangle.
        /// </summary>
        /// <param name="boundingRectangle"></param>
        public EllipseC(RectangleF boundingRectangle)
        {
            BoundingRect = boundingRectangle;
        }

        //this will make an ellipse centered at that point
        /// <summary>
        /// Creates an ellipse of the given height and width centered at the given point.
        /// </summary>
        /// <param name="center"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public EllipseC(PointF center, float width, float height)
        {
            BoundingRect = new RectangleF(center.X-(width/2), center.Y-(height/2), width, height);
            RotationCenter = BoundingRect.Location;
        }
        /// <summary>
        /// Creates an ellipse of the given height and width with the top left corner at the given x and y coordinates.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public EllipseC(float x, float y, float width, float height)
        {
            BoundingRect = new RectangleF(x, y, width, height);
            RotationCenter = BoundingRect.Location;
        }
        /// <summary>
        /// Creates an ellipse at the given point with the given bounding rectangle.
        /// </summary>
        /// <param name="center"></param>
        /// <param name="boundingRectangle"></param>
        public EllipseC(PointF center, RectangleF boundingRectangle)
        {
            BoundingRect = boundingRectangle;
            BoundingRect.X = center.X;
            BoundingRect.Y = center.Y;
            RotationCenter = BoundingRect.Location;
        }
        /// <summary>
        /// Creates an ellipse between the given points.
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        public EllipseC(PointF p1, PointF p2)
        {
            BoundingRect = new RectangleF(p1.X, p1.Y, p2.X-p1.X, p2.Y-p1.X);
            RotationCenter = BoundingRect.Location;
        }
        /// <summary>
        /// Draws the ellipse.
        /// </summary>
        /// <param name="g"></param>
        public void Draw(Graphics g)
        {
            if (Rotation != 0)
            {
                g.TranslateTransform(RotationCenter.X, RotationCenter.Y);
                g.RotateTransform(180 * (float)(Rotation / Math.PI));
                RectangleF tempRect = new RectangleF(BoundingRect.X - RotationCenter.X, BoundingRect.Y - RotationCenter.Y, BoundingRect.Width, BoundingRect.Height);
                if (Fill)
                {
                    g.FillEllipse(new SolidBrush(FillColor), tempRect);
                }
                if (Border)
                {
                    g.DrawEllipse(new Pen(BorderColor), tempRect);
                }
            }
            else
            {
                if (Fill)
                {
                    g.FillEllipse(new SolidBrush(FillColor), BoundingRect);
                }
                if (Border)
                {
                    g.DrawEllipse(new Pen(BorderColor), BoundingRect);
                }
            }
        }
    }
}
