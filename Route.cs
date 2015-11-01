using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_Assignment_2
{
    class Route
    {
        private CityMap cities;
        private double distanceTraveled;
        private Dictionary<int, City> orderVisited;
        Random random;

        public Route(CityMap cities)
        {
            this.cities = cities;
            distanceTraveled = 0;
            orderVisited = new Dictionary<int, City>();
            Initialize();
        }

        private void Initialize()
        {
            random = ServiceRegistry.GetInstance().GetRandom();
        }

        private void CalculateTotalDistance()
        {
          
            for (int i = 1; i < orderVisited.Count; i++)
            {
                distanceTraveled += Math.Sqrt(Math.Abs((Math.Pow(orderVisited[i].Coordinate.X, 2) - Math.Pow(orderVisited[i-1].Coordinate.X, 2)))
                    + Math.Abs((Math.Pow(orderVisited[i].Coordinate.Y, 2) - Math.Pow(orderVisited[i-1].Coordinate.Y, 2))));
            }
            distanceTraveled += Math.Sqrt(Math.Abs((Math.Pow(orderVisited[orderVisited.Count - 1].Coordinate.X, 2) - Math.Pow(orderVisited[0].Coordinate.X, 2))) + 
                Math.Abs(Math.Pow(orderVisited[orderVisited.Count - 1].Coordinate.Y, 2) - Math.Pow(orderVisited[0].Coordinate.Y, 2)));
            
        }

        public void GenerateFirstRoute()
        {
            HashSet<City> used = new HashSet<City>();
            for (int i = 0; i < cities.Cities.Count; i++)
            {
                while (true)
                {
                    City city = cities.Cities[random.Next(100)];
                    if (!used.Contains(city))
                    {
                        used.Add(city);
                        orderVisited.Add(i, city);
                        break;
                    }
                }
            }

            CalculateTotalDistance();
        }

        public override string ToString()
        {
            String retString = "";
            retString += "Total distace Travelled: " + distanceTraveled + "\n";
            for (int i = 0; i < orderVisited.Count; i++)
            {
                retString += i + ": " + orderVisited[i].ToString() + "\n";
            }

            return retString;
        }
    }
}
