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
        private double distanceTraveled = 0;
        private List<City> orderVisited;
        private Random random;
        private Dictionary<City, Double> genotype;

        public Route(CityMap cities)
        {
            this.cities = cities;
            Initialize();
        }

        public void Randomize()
        {
            genotype = new Dictionary<City, double>();
            foreach (City city in cities.Cities)
            {
                genotype.Add(city, random.NextDouble());
            }
            ClearCache();
        }

        private void ClearCache()
        {
            orderVisited = null;
            distanceTraveled = 0;
        }

        public Route Mate(Route father)
        {
            Route newRoute = new Route(cities);
            newRoute.genotype = new Dictionary<City, double>();

            foreach (City city in cities.Cities)
            {
                Route parent = (random.Next() % 2 == 0) ? this : father;
                newRoute.genotype.Add(city, parent.genotype[city]);
            }

            newRoute.ClearCache();

            return newRoute;
        }

        public Route Split(Route father)
        {
            int cutoff = 50;
            int current = 0;
            Route newRoute = new Route(cities);
            newRoute.genotype = new Dictionary<City, double>();

            foreach (City city in cities.Cities)
            {
                Route parent = (++current < cutoff) ? this : father;
                newRoute.genotype.Add(city, parent.genotype[city]);
            }

            newRoute.ClearCache();

            return newRoute;
        }

        public Route Mutate()
        {
            Route newRoute = new Route(cities);
            newRoute.genotype = new Dictionary<City, double>();

            foreach (KeyValuePair<City, double> entry in genotype) {
                newRoute.genotype.Add(entry.Key, entry.Value);
            }

            newRoute.genotype[cities.Cities[random.Next(cities.Count)]] = random.NextDouble();
            newRoute.ClearCache();

            return newRoute;
        }

        private void Initialize()
        {
            random = ServiceRegistry.GetInstance().GetRandom();
        }

       
        private double Distance(City a, City b)
        {
            return Math.Sqrt(
                Math.Pow(a.X - b.X, 2) +
                Math.Pow(a.Y - b.Y, 2)
            );
        }

        //Properties
        
        public double DistanceTraveled
        {
            get
            {
                if (distanceTraveled == 0)
                {
                    for (int i = 1; i < OrderVisited.Count; i++)
                    {
                        distanceTraveled += Distance(OrderVisited[i], OrderVisited[i - 1]);
                    }
                    //last back to first
                    distanceTraveled += Distance(OrderVisited[OrderVisited.Count - 1], OrderVisited[0]); 
                }
                return distanceTraveled;
            }
        }

        
        public List<City> OrderVisited
        {
            get
            {
                if (orderVisited == null)
                {
                    orderVisited = (from entry in genotype orderby entry.Value ascending select entry.Key).ToList();
                }
                return orderVisited;
            }
        }

        public override string ToString()
        {
            String retString = "";
            retString += "Total distace Travelled: " + distanceTraveled + "\n";
            for (int i = 0; i < OrderVisited.Count; i++)
            {
                retString += OrderVisited[i].ToString() + "\n";
            }

            return retString;
        }
    }
}
