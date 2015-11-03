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
        private int distanceTraveled;
        private List<City> orderVisited;
        Random random;

        public Route(CityMap cities)
        {
            this.cities = cities;
            distanceTraveled = 0;
            orderVisited = new List<City>();
            Initialize();
        }

        public void GenerateFirstRoute()
        {
            City city = cities.Cities[random.Next(cities.Count)];
            orderVisited.Add(city);
            while (orderVisited.Count < cities.Count)
            {
                List<City> sortedCities = city.Closest(cities.Cities);
                foreach (City newCity in sortedCities)
                {
                    if(!orderVisited.Contains(newCity))
                    {
                        orderVisited.Add(newCity);
                        city = newCity;
                        break;
                    }
                }
            }
            CalculateTotalDistance();
        }

        public Route Mutate()
        {
            Route newRoute = new Route(cities);
            foreach(City city in orderVisited)
            {
                newRoute.orderVisited.Add(city);
            }

            int offset = random.Next(10);

            int randomNumA = random.Next(cities.MaxCitites - offset);

            newRoute.Swap(randomNumA, randomNumA + offset);
            
            newRoute.CalculateTotalDistance();

            return newRoute;
                  
        }

        private int Crossover()
        {
            throw new NotImplementedException();
        }

        private void Swap(int a, int b)
        {
            City temp = orderVisited[a];
            orderVisited[a] = orderVisited[b];
            orderVisited[b] = temp;
        }


        private void Initialize()
        {
            random = ServiceRegistry.GetInstance().GetRandom();
        }

        private void CalculateTotalDistance()
        {
            distanceTraveled = 0;
            for (int i = 1; i < orderVisited.Count; i++)
            {
                distanceTraveled += Distance(orderVisited[i], orderVisited[i - 1]);
            }
            //last back to first
            distanceTraveled += Distance(orderVisited[orderVisited.Count - 1], orderVisited[0]);

        }

        private int Distance(City a, City b)
        {
            return (int)(Math.Sqrt(Math.Abs((Math.Pow(b.X, 2) - Math.Pow(a.X, 2)))
                    + Math.Abs((Math.Pow(b.Y, 2) - Math.Pow(a.Y, 2)))));
        }

        //Properties
        public int Count
        {
            get
            {
                return orderVisited.Count;
            }
        }

        public double DistanceTraveled
        {
            get
            {
                return distanceTraveled;
            }
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
