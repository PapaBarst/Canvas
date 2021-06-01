using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Canvas
{
    public class DrawableImage : Drawable
    {
        private Bitmap internalImage;
        private Rectangle clippingRect;
        public PointF Center = new PointF();
        public float X
        {
            get
            {
                return Center.X;
            }
            set
            {
                Center.X = value;
            }
        }
        public float Y
        {
            get
            {
                return Center.Y;
            }
            set
            {
                Center.Y = value;
            }
        }
        public int Width
        {
            get
            {
                return internalImage.Width;
            }
        }
        public int Height
        {
            get
            {
                return internalImage.Height;
            }
        }

        private DrawableImage(float x, float y, Image i)
        {
            Center.X = x;
            Center.Y = y;
            internalImage = (Bitmap)(Image)i.Clone();
        }

        public DrawableImage(String localFileName)
        {
            internalImage = (Bitmap)Image.FromFile(localFileName);
            Center.X = 0;
            Center.Y = 0;
        }
        public DrawableImage(float x, float y, String localFileName)
        {
            internalImage = (Bitmap)Image.FromFile(localFileName);
            Center.X = x;
            Center.Y = y;
        }

        public DrawableImage(float x, float y, int width, int height, String localFileName)
        {
            internalImage = CUtil.ResizeImage(Image.FromFile(localFileName), width, height);
            Center.X = x;
            Center.Y = y;
        }

        public DrawableImage Copy()
        {
            return new DrawableImage(Center.X, Center.Y, internalImage);
        }

        public void Draw(Graphics g)
        {
            g.DrawImage(internalImage, Center);
        }
    }
}
