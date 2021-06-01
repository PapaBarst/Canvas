using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Canvas
{
    //A less performance heavy rectangle class that can draw multiple rectangles at one. No rotation or scaling.
    class FastRectangle : Drawable
    {
        public List<RectangleF> rects
        {
            private set;
            get;
        }
        public bool Fill = true;
        public bool Border = false;
        public Color FillColor = Color.Gray;
        public Color BorderColor = Color.Black;

        public FastRectangle(float x, float y, float width, float height)
        {
            rects = new List<RectangleF>();
            rects.Add(new RectangleF(x, y, width, height));
        }

        //this will make a rectangle centered at that point
        public FastRectangle(PointF center, float width, float height)
        {
            rects = new List<RectangleF>();
            rects.Add(new RectangleF(center.X-(width/2), center.Y-(height/2), width, height));
        }

        public FastRectangle(RectangleF rectangle)
        {
            rects = new List<RectangleF>();
            rects.Add(rectangle);
        }

        public void Add(float x, float y, float width, float height)
        {
            rects.Add(new RectangleF(x, y, width, height));
        }
        public void Draw(Graphics g)
        {
            RectangleF[] tempRects = rects.ToArray();
            if (Fill)
            {
                g.FillRectangles(new SolidBrush(FillColor), tempRects);
            }
            if (Border)
            {
                g.DrawRectangles(new Pen(BorderColor), tempRects);
            }
        }
    }
}
