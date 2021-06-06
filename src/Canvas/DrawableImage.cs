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
    /// <summary>
    /// A representation of an image, drawable onscreen.
    /// </summary>
    public class DrawableImage : Drawable
    {
        private Bitmap internalImage;
        private Rectangle clippingRect;
        /// <summary>
        /// The center of the image.
        /// </summary>
        public PointF Center = new PointF();
        /// <summary>
        /// The X location of the image. Tied to <c>Center</c>.
        /// </summary>
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
        /// <summary>
        /// The Y location of the image. Tied to <c>Center</c>.
        /// </summary>
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
        /// <summary>
        /// Readonly width of the image. To change the image's size, use <c>Resize</c>.
        /// </summary>
        public int Width
        {
            get
            {
                return internalImage.Width;
            }
        }
        /// <summary>
        /// Readonly height of the image. To change the image's size, use <c>Resize</c>.
        /// </summary>
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
        /// <summary>
        /// Creates an image at the origin with the image's original size.
        /// </summary>
        /// <param name="localFileName"></param>
        public DrawableImage(String localFileName)
        {
            internalImage = (Bitmap)Image.FromFile(localFileName);
            Center.X = 0;
            Center.Y = 0;
        }
        /// <summary>
        /// Creates an image at the given x and y coordinates with the image's original size.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="localFileName"></param>
        public DrawableImage(float x, float y, String localFileName)
        {
            internalImage = (Bitmap)Image.FromFile(localFileName);
            Center.X = x;
            Center.Y = y;
        }
        /// <summary>
        /// Creates an image at the given x and y coordinates with the image rescaled to the given width and height.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="localFileName"></param>
        public DrawableImage(float x, float y, int width, int height, String localFileName)
        {
            internalImage = CUtil.ResizeImage(Image.FromFile(localFileName), width, height);
            Center.X = x;
            Center.Y = y;
        }
        /// <summary>
        /// Returns a copy of the image.
        /// </summary>
        /// <returns>A copy of the image.</returns>
        public DrawableImage Copy()
        {
            return new DrawableImage(Center.X, Center.Y, internalImage);
        }

        /// <summary>
        /// Resizes the image to the given width and height.
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public void Resize(int width, int height)
        {
            internalImage = CUtil.ResizeImage(internalImage, width, height);
        }

        /// <summary>
        /// Draws the image onscreen.
        /// </summary>
        /// <param name="g"></param>
        public void Draw(Graphics g)
        {
            g.DrawImage(internalImage, Center);
        }
    }
}
