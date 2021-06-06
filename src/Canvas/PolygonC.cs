using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Canvas
{
    public class PolygonC : Drawable
    {
        protected List<PointF> points;
        protected PointF center;
        public bool drawToCenter = false;
        public bool fill = true;
        public bool border = false;
        public Color FillColor;
        public Color BorderColor;


        //Constructors

        protected PolygonC(PointF center)
        {
            this.points = new List<PointF>();
            this.center = center;
            FillColor = Color.Gray;
            BorderColor = Color.Black;
        }

        protected PolygonC(float x, float y)
        {
            this.points = new List<PointF>();
            this.center = new PointF(x, y);
            FillColor = Color.Gray;
            BorderColor = Color.Black;
        }
        public PolygonC(PointF center, PointF[] points)
        {
            this.points = new List<PointF>();
            this.points.AddRange(points);
            this.center = center;
            FillColor = Color.Gray;
            BorderColor = Color.Black;
        }

        public PolygonC(float x, float y, PointF[] points)
        {
            this.points = new List<PointF>();
            this.center = new PointF(x, y);
            FillColor = Color.Gray;
            BorderColor = Color.Black;
        }
        public PolygonC(PointF center, PointF[] points, Color c)
        {
            this.points = new List<PointF>();
            this.points.AddRange(points);
            this.center = center;
            this.FillColor = c;
            BorderColor = Color.Black;
        }

        public PolygonC(float x, float y, PointF[] points, Color c)
        {
            this.points = new List<PointF>();
            this.center = new PointF(x, y);
            this.FillColor = c;
            BorderColor = Color.Black;
        }

        public PolygonC(PointF center, PointF[] points, Color fillColor, Color borderColor)
        {
            this.points = new List<PointF>();
            this.points.AddRange(points);
            this.center = center;
            this.FillColor = fillColor;
            this.BorderColor = borderColor;
        }

        public PolygonC(float x, float y, PointF[] points, Color fillColor, Color borderColor)
        {
            this.points = new List<PointF>();
            this.points.AddRange(points);
            this.center = new PointF(x, y);
            this.FillColor = fillColor;
            this.BorderColor = borderColor;
        }

        public PolygonC(PointF center, List<PointF> points)
        {
            this.points = new List<PointF>();
            this.points.AddRange(points);
            this.center = center;
            FillColor = Color.Gray;
            BorderColor = Color.Black;
        }

        public PolygonC(float x, float y, List<PointF> points)
        {
            this.points = new List<PointF>();
            this.center = new PointF(x, y);
            FillColor = Color.Gray;
            BorderColor = Color.Black;
        }

        public PolygonC(PointF center, List<PointF> points, Color c)
        {
            this.points = new List<PointF>();
            this.points.AddRange(points);
            this.center = center;
            this.FillColor = c;
            BorderColor = Color.Black;
        }

        public PolygonC(float x, float y, List<PointF> points, Color c)
        {
            this.points = new List<PointF>();
            this.center = new PointF(x, y);
            this.FillColor = c;
            BorderColor = Color.Black;
        }

        public PolygonC(PointF center, List<PointF> points, Color fillColor, Color borderColor)
        {
            this.points = new List<PointF>();
            this.points.AddRange(points);
            this.center = center;
            this.FillColor = fillColor;
            this.BorderColor = borderColor;
        }

        public PolygonC(float x, float y, List<PointF> points, Color fillColor, Color borderColor)
        {
            this.points = new List<PointF>();
            this.points.AddRange(points);
            this.center = new PointF(x, y);
            this.FillColor = fillColor;
            this.BorderColor = borderColor;
        }

        //movement methods
        public void Move(PointF p)
        {
            Move(p.X, p.Y);
        }

        public void Move(float x, float y)
        {
            center.X += x;
            center.Y += y;
            for(int i=0; i<points.Count; i++)
            {
                PointF p = points[i];
                p.X += x;
                p.Y += y;
            }
        }

        public void MoveTo(PointF p)
        {
            MoveTo(p.X, p.Y);
        }

        public void MoveTo(float x, float y)
        {
            for (int i = 0; i < points.Count; i++)
            {
                PointF p = points[i];
                p.X = ((p.X-center.X)+x);
                p.Y = ((p.Y - center.Y) + y);
            }

            center.X = x;
            center.Y = y;
        }

        //Other methods

        public void Draw(Graphics g)
        {
            PointF[] pointArr = points.ToArray();
            if (fill)
            {
                g.FillPolygon(new SolidBrush(FillColor), pointArr);
            }
            if (border)
            {
                g.DrawPolygon(new Pen(BorderColor), pointArr);
            }
        }

    }
}
