using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_Assignment_2
{
    class Route
    {
        enum ReproductionMethod { Mate, Asexual };
        
        const ReproductionMethod REPRODUCTION_METHOD = ReproductionMethod.Asexual;
        private CityMap cities;
        private double distanceTraveled;
        private List<City> orderVisited;
        private Random random;
        private Dictionary<City, Double> genotype;

        public Route(CityMap cities)
        {
            this.cities = cities;
            distanceTraveled = 0;
            orderVisited = new List<City>();
            Initialize();
            if (ReproductionMethod.Mate == 0)
            {
                Gen
            }
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

        public Route Mate(Route father)
        {
            Route newRoute = new Route(cities);
            return newRoute;
        }


        public Route Mutate()
        {
            Route newRoute = new Route(cities);
            int offset;
            switch (REPRODUCTION_METHOD) {
                case ReproductionMethod.Mate:
                    
                    break;
                case ReproductionMethod.Asexual:
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

        private void Crossover(Route a, Route b)
        {
            List<City> even = a.orderVisited.Where((item, index) => index % 2 == 0).ToList();
            List<City> odd = b.orderVisited.Where((item, index) => index % 2 != 0).ToList();
            List<City> sorted;
            int even_count = 0;
            int odd_count = 0;
            int count = 0;
            while ((even_count + odd_count) < orderVisited.Count)
            {
                if (count % 2 == 0)
                {
                    if (!orderVisited.Contains(even[even_count]))
                    {
                        orderVisited.Add(even[even_count]);
                    }
                    else
                    {
                        sorted = even[even_count].Closest(cities.Cities).Where(c => !orderVisited.Contains(c)).Take(1).ToList();
                        orderVisited.AddRange(sorted);
                    }
                    even_count++;
                    count++;

                }
                else
                {
                    if (!orderVisited.Contains(odd[odd_count]))
                    {
                        orderVisited.Add(odd[odd_count]);
                    }
                    else
                    {
                        sorted = odd[odd_count].Closest(cities.Cities).Where(c => !orderVisited.Contains(c)).Take(1).ToList();
                        orderVisited.AddRange(sorted);
                    }
                    odd_count++;
                    count++;
                }
            }
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

        private double Distance(City a, City b)
        {
            return Math.Sqrt(
                Math.Pow(a.X - b.X, 2) +
                Math.Pow(a.Y - b.Y, 2)
            );
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
