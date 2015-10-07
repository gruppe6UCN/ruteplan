using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    class Point
    {
        public double X { get; private set; }
        public double Y { get; private set; }

        public Point(double X, double Y)
        {
            this.X = X;
            this.Y = Y;
        }

        public override String ToString()
        {
            return String.Format("({0},{1})", X, Y);
        }
    }
}