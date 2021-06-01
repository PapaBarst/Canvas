using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Canvas
{
    public class RectangleP : PolygonP
    {

        //TODO: Make getters and setters for these below
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

        //defines a rectangle by its top left corner, width, and height
        public RectangleP(float x, float y, float width, float height) : base(x, y)
        {
            Height = height;
            Width = width;

            points.Add(new PointP(0, 0));
            points.Add(new PointP(0, width));
            points.Add(new PointP(CUtil.GetAngle(width,height), (float)Math.Sqrt(Math.Pow(width, 2) + Math.Pow(height, 2))));
            points.Add(new PointP((3*Math.PI)/2, height));
        }

        //defines a rectangle by two points
        public RectangleP(PointF p1, PointF p2) : base(p1)
        {
            Width = p2.X - p1.X;
            Height = p2.Y - p1.Y;

            points.Add(new PointP(0, 0));
            points.Add(new PointP(0, Width));
            points.Add(new PointP(CUtil.GetAngle(Width, Height), (float)Math.Sqrt(Math.Pow(Width, 2) + Math.Pow(Height, 2))));
            points.Add(new PointP((3 * Math.PI) / 2, Height));
        }

        //defines a rectangle by its top left corner, width, and height
        public RectangleP(PointF p1, float width, float height) : base(p1)
        {
            Height = height;
            Width = width;

            points.Add(new PointP(0, 0));
            points.Add(new PointP(0, width));
            points.Add(new PointP(CUtil.GetAngle(width, height), (float)Math.Sqrt(Math.Pow(width, 2) + Math.Pow(height, 2))));
            points.Add(new PointP((3 * Math.PI) / 2, height));
        }

        //defines a rectangle
        public RectangleP(PointF center, float x, float y, float width, float height) : base(center)
        {

        }
    }
}
