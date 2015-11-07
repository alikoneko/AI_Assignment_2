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
        private PointD coordinate;

        public City(PointD coordinate)
        {
            this.coordinate = coordinate;
        }

        public double X
        {
            get
            {
                return coordinate.X;
            }
        }

        public double Y
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

        private double Distance(City city)
        {
            return (double)Math.Sqrt(Math.Abs((Math.Pow(coordinate.X, 2) - Math.Pow(city.X, 2)))
                    + Math.Abs((Math.Pow(coordinate.Y, 2) - Math.Pow(city.Y, 2))));
        }
    }
}
