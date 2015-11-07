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
        //TODO: make cities aware of neighboring cities.
        private double MAX_XY = 1000.0;
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
            HashSet<PointD> coordinates = new HashSet<PointD>();
            for (int i = 0; i < MAX_CITIES; i++)
            {
                while (true)
                {
                    PointD point = new PointD(random.NextDouble() * MAX_XY, random.NextDouble() * MAX_XY);
                    if(!coordinates.Contains(point))
                    {
                        coordinates.Add(point);
                        cities.Add(new City(point));
                        break;
                    }
                }
            }
        }

        public int Count
        {
            get
            {
                return cities.Count;
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
