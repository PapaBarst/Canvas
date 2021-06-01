using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Canvas
{
    public class PolygonP : Drawable
    {
        protected PointF[] cachedPoints;
        protected List<PointP> points;
        protected PointF center;
        //this impacts performance a LOT; Add disclaimer
        public bool drawToCenter = false;
        public bool fill = true;
        public bool border = false;
        public Color FillColor;
        public Color BorderColor;



        public PointF Center
        {
            get
            {
                return center;
            }
        }

        //protected PolygonP()
        //{
        //    points = new List<PointP>();
        //    center = new PointF(0, 0);
        //    FillColor = Color.Gray;
        //    BorderColor = Color.Black;
        //}

        protected PolygonP(float x, float y)
        {

            points = new List<PointP>();
            center = new PointF(x, y);
            FillColor = Color.Gray;
            BorderColor = Color.Black;
        }

        protected PolygonP(PointF center)
        {

            points = new List<PointP>();
            this.center = center;
            FillColor = Color.Gray;
            BorderColor = Color.Black;
        }

        public PolygonP(PointF center, List<PointP> points)
        {
            if (center == null)
            {
                throw new ArgumentNullException();
            }
            if (points == null)
            {
                throw new ArgumentNullException();
            }

            this.center = center;
            this.points = points;

            FillColor = Color.Gray;
            BorderColor = Color.Black;
        }

        public PolygonP(PointF center, PointP[] points)
        {
            if (center == null)
            {
                throw new ArgumentNullException();
            }
            if (points == null)
            {
                throw new ArgumentNullException();
            }

            this.points = new List<PointP>();
            this.points.AddRange(points);

            this.center = center;

            FillColor = Color.Gray;
            BorderColor = Color.Black;
        }

        public PointF[] Convert()
        {
            PointF[] toReturn = new PointF[points.Count];
            for(int i=0; i<points.Count; i++)
            {
                PointP p = points[i];
                toReturn[i] = new PointF(center.X + (p.R * (float)Math.Cos(p.Theta)), center.Y + (p.R * -(float)Math.Sin(p.Theta)));
            }
            return toReturn;
        }

        public void Rotate(double theta)
        {
            theta *= -1;
            for(int i=0; i<points.Count; i++)
            {
                points[i].addTheta(theta);
            }
            cachedPoints = this.Convert();
        }

        public void Move(PointF p)
        {
            Move(p.X, p.Y);
            cachedPoints = this.Convert();
        }

        public void Move(float x, float y)
        {
            center.X += x;
            center.Y += y;
            cachedPoints = this.Convert();
        }

        public void MoveTo(PointF p)
        {
            MoveTo(p.X, p.Y);
            cachedPoints = this.Convert();
        }

        public void MoveTo(float x, float y)
        {
            center.X = x;
            center.Y = y;
            cachedPoints = this.Convert();
        }

        public void Redefine(PointF p)
        {
            Redefine(p.X, p.Y);
            //cachedPoints = this.Convert();
        }

        public void Redefine(float x, float y)
        {
            double pointDist = (Math.Sqrt(Math.Pow((y - center.Y), 2) + Math.Pow((x - center.X), 2)));
            double pointTheta = Math.Atan((x - center.X) / (y - center.Y));
            
            List<PointP> newPoints = new List<PointP>();
            for (int i=0; i<points.Count; i++)
            {
                PointP p = points[i];
                Console.WriteLine("Old R: " + p.R);
                Console.WriteLine("Old Theta: " + p.Theta);
                double newR;
                double newTheta;
                if (p.R != 0)
                {
                    newR = Math.Sqrt(Math.Pow(p.R, 2) + Math.Pow(pointDist, 2) - Math.Abs((2*p.R * pointDist * Math.Cos(Math.Abs(p.Theta - ((Math.PI / 2) + pointTheta))))));

                    if (x<=center.X)
                    {
                        //good?
                        newTheta = (Math.PI / 2) + pointTheta + Math.Acos((Math.Pow(p.R, 2) + Math.Pow(pointDist, 2) - (Math.Pow(newR, 2))) / (2 * p.R * pointDist));
                    }
                    else if(x>center.X)
                    {
                        //good?
                        newTheta = (Math.PI/2)+pointTheta+Math.Acos((Math.Pow(newR, 2) + Math.Pow(pointDist, 2) - (Math.Pow(p.R, 2))) / (2 * p.R * pointDist));
                    }
                    else
                    {
                        //hopefully this is unreachable
                        newTheta = Math.PI;
                    }
                    
                    //newTheta = Math.Acos((Math.Pow(p.R,2)+Math.Pow(pointDist,2)-(Math.Pow(newR,2))) / (2 * p.R * pointDist));
                    //if (newTheta < 0)
                    //{
                    //    newTheta = -newTheta - (Math.PI / 2);
                    //}
                    //else
                    //{
                    //    newTheta = Math.PI - newTheta;
                    //}
                }
                else
                {
                    newR = pointDist;
                    newTheta = pointTheta+(Math.PI/2);
                }


                //Console.WriteLine("New R: "+newR);
                //Console.WriteLine("New Theta: "+newTheta);

                center.X = x;
                center.Y = y;

                newPoints.Add(new PointP(newTheta, (float)newR));

                Console.WriteLine("New R: " + newR);
                Console.WriteLine("New Theta: " + newTheta);
                Console.WriteLine();
            }

            points = newPoints;

            cachedPoints = this.Convert();
        }

        public PolygonP Copy()
        {
            PolygonP toReturn = new PolygonP(Center, points);
            toReturn.cachedPoints = cachedPoints;
            toReturn.drawToCenter=drawToCenter;
            toReturn.fill = fill;
            toReturn.border = border;
            toReturn.FillColor = FillColor;
            toReturn.BorderColor = BorderColor;
            return toReturn;
    }

        public virtual void Draw(Graphics g)
        {
            if (cachedPoints == null)
            {
                cachedPoints = this.Convert();
            }
            
            if (fill)
            {
                g.FillPolygon(new SolidBrush(FillColor), cachedPoints);
            }
            if (border)
            {
                g.DrawPolygon(new Pen(BorderColor), cachedPoints);
            }
            if (drawToCenter)
            {
                foreach(PointF p in cachedPoints)
                {
                    g.DrawLine(new Pen(BorderColor), center, p);
                }
            }
        }
    }
}
