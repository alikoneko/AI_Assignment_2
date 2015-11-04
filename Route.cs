using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_Assignment_2
{
    class Route
    {
        enum MutationMethod { Smart, Stupid };
        const MutationMethod MUTATION_METHOD = MutationMethod.Smart;
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
            FinishRoute();
        }

        private void FinishRoute()
        {
            City city = orderVisited[orderVisited.Count - 1];

            while (orderVisited.Count < cities.Count)
            {
                List<City> sortedCities = city.Closest(cities.Cities).ToList();

                foreach (City newCity in sortedCities)
                {
                    if (!orderVisited.Contains(newCity))
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
            int offset;
            switch (MUTATION_METHOD) {
                case MutationMethod.Smart:
                    offset = random.Next(cities.Count - 2) + 1;
                    newRoute.orderVisited.AddRange(orderVisited.Take(offset).ToList());

                    City city = newRoute.orderVisited[newRoute.orderVisited.Count - 1];

                    List<City> sortedCities = city.Closest(cities.Cities).Where(c => !newRoute.orderVisited.Contains(c)).Take(5).ToList();
                    newRoute.orderVisited.Add(sortedCities[random.Next(sortedCities.Count - 1)]);
                    newRoute.FinishRoute();
                    break;
                case MutationMethod.Stupid:
                    newRoute.orderVisited.AddRange(orderVisited.ToList());
                    //offset = random.Next(newRoute.orderVisited.Count - 2);
                    newRoute.Swap(random.Next(newRoute.orderVisited.Count), random.Next(newRoute.orderVisited.Count));
                    newRoute.CalculateTotalDistance();
                    break;
                default:
                    newRoute.orderVisited.AddRange(orderVisited.ToList());
                    newRoute.CalculateTotalDistance();
                    break;
            }
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
                retString += orderVisited[i].ToString() + "\n";
            }

            return retString;
        }
    }
}
