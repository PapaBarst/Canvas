using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Canvas
{
    class TriangleP : PolygonP
    {
        protected TriangleP(float x, float y) : base(x, y)
        {

        }
        public TriangleP(float centerx, float centery, double theta1, float r1, double theta2, float r2, double theta3, float r3) : base(centerx,centery)
        {
            points.Add(new PointP(theta1, r1));
            points.Add(new PointP(theta2, r2));
            points.Add(new PointP(theta3, r3));
        }

        public TriangleP(float centerx, float centery, PointP p1, PointP p2, PointP p3) : base(centerx, centery)
        {
            points.Add(p1);
            points.Add(p2);
            points.Add(p3);
        }

        public TriangleP(float x, float y, double rotation, float side1, float side2, float side3) : base(x, y)
        {

        }
    }
}
