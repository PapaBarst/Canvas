using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Canvas
{
    public class RegularPolygonP : PolygonP
    {
        private int faces;
        private double displacement;

        public int Faces
        {
            get
            {
                return faces;
            }
        }

        public double Displacement
        {
            get
            {
                return displacement;
            }
        }

        public RegularPolygonP(PointF center, float radius, int numFaces) : base(center)
        {
            if (numFaces < 3)
            {
                throw new ArgumentException("Number of faces must be 3 or greater");
            }
            faces = numFaces;
            displacement = (2 * Math.PI) / (double)numFaces;
            for(int i=0; i<numFaces; i++)
            {
                points.Add(new PointP(i * displacement, radius));
            }
        }

        public RegularPolygonP(float x, float y, float radius, int numFaces) : base(x,y)
        {
            if (numFaces < 3)
            {
                throw new ArgumentException("Number of faces must be 3 or greater");
            }
            faces = numFaces;
            displacement = (2 * Math.PI) / (double)numFaces;
            for (int i = 0; i < numFaces; i++)
            {
                points.Add(new PointP(i * displacement, radius));
            }
        }
    }
}
