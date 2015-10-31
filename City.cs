using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace AI_Assignment_2
{
    class City
    {
        private Point coordinate;

        public City(Point coordinate)
        {
            this.coordinate = coordinate;
        }

        public Point Coordinate
        {
            get
            {
                return coordinate;
            }
        }

        public override string ToString()
        {
            return coordinate.ToString();
        }
    }
}
