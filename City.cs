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

        public int X
        {
            get
            {
                return coordinate.X;
            }
        }

        public int Y
        {
            get
            {
                return coordinate.Y;
            }
        }

        public List<City> Closest(List<City> cities)
        {
            return cities.OrderBy(x => x.Distance(this)).ToList();
        }

        public override string ToString()
        {
            return coordinate.X + "," + coordinate.Y;
        }

        public bool Equals(City obj)
        {
            return coordinate.Equals(obj.coordinate);
        }

        public override int GetHashCode()
        {
            return coordinate.GetHashCode();
        }

        private int Distance(City city)
        {
            return (int)Math.Sqrt(Math.Abs((Math.Pow(coordinate.X, 2) - Math.Pow(city.X, 2)))
                    + Math.Abs((Math.Pow(coordinate.Y, 2) - Math.Pow(city.Y, 2))));
        }
    }
}
