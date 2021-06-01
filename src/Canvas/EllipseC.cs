using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Canvas
{
    public class EllipseC : Drawable
    {
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
        public PointF RotationCenter;
        public double Rotation = 0;
        private RectangleF BoundingRect;
        public bool Fill = true;
        public bool Border = false;
        public Color FillColor = Color.Gray;
        public Color BorderColor = Color.Black;

        public EllipseC(RectangleF boundingRectangle)
        {
            BoundingRect = boundingRectangle;
        }

        //this will make an ellipse centered at that point
        public EllipseC(PointF center, float width, float height)
        {
            BoundingRect = new RectangleF(center.X-(width/2), center.Y-(height/2), width, height);
            RotationCenter = BoundingRect.Location;
        }

        public EllipseC(float x, float y, float width, float height)
        {
            BoundingRect = new RectangleF(x, y, width, height);
            RotationCenter = BoundingRect.Location;
        }

        public EllipseC(PointF center, RectangleF boundingRectangle)
        {
            BoundingRect = boundingRectangle;
            BoundingRect.X = center.X;
            BoundingRect.Y = center.Y;
            RotationCenter = BoundingRect.Location;
        }

        public EllipseC(PointF p1, PointF p2)
        {
            BoundingRect = new RectangleF(p1.X, p1.Y, p2.X-p1.X, p2.Y-p1.X);
            RotationCenter = BoundingRect.Location;
        }

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
