using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Canvas
{
    class StarP : PolygonP
    {
        private int numPoints;
        private double displacement;

        public int NumPoints
        {
            get
            {
                return numPoints;
            }
        }

        public double Displacement
        {
            get
            {
                return displacement;
            }
        }

        public StarP(PointF center, float innerRadius, float outerRadius, int numPoints) : base(center)
        {
            if (numPoints < 3)
            {
                throw new ArgumentException("Number of points must be 3 or greater");
            }
            this.numPoints = numPoints;
            double fracPI = (2 * Math.PI) / (double)(2*numPoints);
            displacement = fracPI;
            bool flip = true;
            for (int i = 0; i < 2*numPoints; i++)
            {
                if (flip)
                {
                    points.Add(new PointP(i * fracPI, outerRadius));
                }
                else
                {
                    points.Add(new PointP(i * fracPI, innerRadius));
                }
                flip = !flip;
            }
        }

        public StarP(float x, float y, float innerRadius, float outerRadius, int numPoints) : base(x, y)
        {
            if (numPoints < 3)
            {
                throw new ArgumentException("Number of points must be 3 or greater");
            }
            this.numPoints = numPoints;
            double fracPI = (2 * Math.PI) / (double)(2 * numPoints);
            displacement = fracPI;
            bool flip = true;
            for (int i = 0; i < 2*numPoints; i++)
            {
                if (flip)
                {
                    points.Add(new PointP(i * fracPI, outerRadius));
                }
                else
                {
                    points.Add(new PointP(i * fracPI, innerRadius));
                }
                flip = !flip;
            }
        }
    }
}
