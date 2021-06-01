using Canvas;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;




//This is the notes section
//Implement cartesian defined shapes
//Pictures represented by a separate picture box?

namespace OtherNamespace
{
    class Runner : CBase
    {
        bool flip = false;
        StarP star;
        CurveApproximation approx;
        DrawableImage d;
        int xt = 200;
        int yt = 0;
        int rtick = 5;
        int linusCounter = 0;
        Random r = new Random();
        float oldMouseX;
        public static void Main()
        {
            //Console.WriteLine("Assembly name: " + Assembly.GetExecutingAssembly().GetName());
            CBase.Main("OtherNamespace","Runner");
        }
        public override void Draw()
        {
            //rtick += 10;
            //approx = new CurveApproximation(400, 300, "x*Sin(.2*x)", -300, 300, rtick);
            //approx.FillColor = Color.FromArgb(100, 0, 255, 0);
            //approx.Closed = true;
            //Draw(approx);

            //Rectangle(xt, yt, 200, 200);

            if (flip)
            {
                xt-=1;
            }
            else
            {
                xt+=1;
            }

            if ((xt+200) >= Width || (xt)<=0)
            {
                flip = !flip;
            }
            //star.Rotate(.001f);
            //star.Move(1, 0);
        }

        public override void DrawOnce()
        {

            //rect = new RectangleP(100, 100, 100, 50);
            //rect.drawToCenter = true;
            //Draw(rect);

            //Rectangle(100, 100, 200, 200);

            star = new StarP(100, 100, 5, 15, 5);
            star.border = true;
            star.FillColor = Color.LightBlue;
            star.BorderColor = Color.Black;
            //star.drawToCenter = true;
            //Draw(star);

            //Ellipse(0, 0, 200, 200);
            EllipseC c = new EllipseC(200, 200, 400, 200);
            c.RotationCenter = new PointF(200, 200);
            c.Rotation = (Math.PI / 4);
            Draw(c);

            d = new DrawableImage(100, 100, 40,40,"C:/Users/papab/source/repos/Canvas/Canvas/TestingResources/linus.jpg");
            Draw(d);

            //cart = new RectangleC(100, 100, 200, 200);
            //Draw(cart);

            //approx = new CurveApproximation(100, 100, 200,200, "Sin(x)", -10, 10, 600);
            //approx.BorderColor = Color.Red;
            //Draw(approx);
        }

        public override void Setup()
        {
            this.refreshRate = 60;
            Width = 1200;
            Height = 600;
        }

        public override void Button1Pressed()
        {
            Console.WriteLine("This is button 1!");
        }

        public override void MouseClicked()
        {
            

            //background.InsertBelow(star.Copy(), star);
        }

        public override void MouseHeld()
        {
            background.InsertBelow(d.Copy(), d);
            linusCounter++;
            Console.WriteLine(linusCounter);
        }

        public override void MouseMoved()
        {
            d.Center.X = MouseX-20;
            d.Center.Y = MouseY-20;
            star.MoveTo(MouseX, MouseY);
            if (MouseX > oldMouseX)
            {
                star.Rotate(.015f);
            }
            if (MouseX < oldMouseX)
            {
                star.Rotate(-.015f);
            }


            oldMouseX = MouseX;
        }
    }
}
