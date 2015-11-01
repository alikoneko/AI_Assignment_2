using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace AI_Assignment_2
{
    class CityMap
    {
        private float MAX_XY = 1000.0f;
        private int MAX_CITIES = 100;
        private Random random;

        private List<City> cities;

        public CityMap(List<City> cities)
        {
            this.cities = cities;
            Initialize();
        }

        public CityMap()
        {
            cities = new List<City>();
            Initialize();
            GenerateCities();
        }

        private void Initialize()
        {
            random = ServiceRegistry.GetInstance().GetRandom();
        }

        private void GenerateCities()
        {
            HashSet<Point> coordinates = new HashSet<Point>();
            for (int i = 0; i < MAX_CITIES; i++)
            {
                while (true)
                {
                    Point point = new Point(random.Next(1000), random.Next(1000));
                    if(!coordinates.Contains(point))
                    {
                        coordinates.Add(point);
                        cities.Add(new City(point));
                        break;
                    }
                }
            }
        }

        public List<City> Cities
        {
            get
            {
                return cities;
            }
        }

        public int MaxCitites
        {
            get
            {
                return MAX_CITIES;
            }
        }
    }
}
