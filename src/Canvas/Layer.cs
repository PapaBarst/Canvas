using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Canvas
{
    public class Layer
    {
        public List<Drawable> drawstack;
        public bool retain;

        public Layer()
        {
            drawstack = new List<Drawable>();
            retain = false;
        }

        public void Add(Drawable shape)
        {
            drawstack.Add(shape);
        }

        public void AddToBottom(Drawable shape)
        {
            drawstack.Insert(0, shape);
        }

        public void InsertBelow(Drawable newDrawable, Drawable oldDrawable)
        {
            drawstack.Insert(drawstack.IndexOf(oldDrawable), newDrawable);
        }

        public void Insert(int index, Drawable shape)
        {
            drawstack.Insert(index, shape);
        }
    }
}
