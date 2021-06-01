using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Canvas
{
    public class PointP
    {
        private double theta;
        private float r;

        public double Theta
        {
            get
            {
                return theta;
            }
            set
            {
                if (double.IsNaN(Theta))
                {
                    theta = Theta % (Math.PI * 2);
                }
            }
        }

        public float R
        {
            get
            {
                return r;
            }
            set
            {
                if (float.IsNaN(R))
                {
                    r = R;
                }
            }
        }

        public PointP(double theta, float r)
        {
            if (Double.IsInfinity(theta))
            {
                throw new ArgumentException("theta may not be infinity");
            }
            if (float.IsNaN(r))
            {
                throw new ArgumentException("r may not be NaN");
            }
            if (float.IsInfinity(r))
            {
                throw new ArgumentException("r may not be infinity");
            }
            this.theta = theta % (Math.PI * 2);
            if (this.theta < 0)
            {
                this.theta += 2 * Math.PI;
            }
            this.r = r;
        }

        public void addTheta(double addTheta)
        {
            theta += addTheta;
        }
    }
}
