using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Canvas
{
    class RectangleC : PolygonC
    {
        public float X
        {
            get
            {
                return center.X;
            }
            set
            {
                MoveTo(value, center.Y);
            }
        }
        public float Y
        {
            get
            {
                return center.Y;
            }
            set
            {
                MoveTo(center.X, value);
            }
        }
        public RectangleC(float x, float y, float width, float height) : base(x,y)
        {
            points.Add(new PointF(x,y));
            points.Add(new PointF(x+width, y));
            points.Add(new PointF(x+width, y+height));
            points.Add(new PointF(x, y+height));
        }
    }
}
