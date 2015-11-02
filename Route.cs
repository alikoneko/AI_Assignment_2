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
            HashSet<City> used = new HashSet<City>();
            for (int i = 0; i < cities.Cities.Count; i++)
            {
                while (true)
                {
                    City city = cities.Cities[random.Next(100)];
                    if (!used.Contains(city))
                    {
                        used.Add(city);
                        orderVisited.Add(city);
                        break;
                    }
                }
            }
            if (distanceTraveled != 0)
            {
                distanceTraveled = 0;
            }

            CalculateTotalDistance();
        }

        public void Mutate()
        {
            int mutation = random.Next(2);
            switch (mutation)
            {
                case 0:
                    GenerateFirstRoute();
                    break;
                case 1:
                    int randomNumA = random.Next(cities.MaxCitites);
                    int randomNumB = random.Next(cities.MaxCitites - 1);
                    if (randomNumA == randomNumB)
                    {
                        randomNumB++;
                    }
                    Swap(randomNumA, randomNumB);
                    randomNumA = random.Next(cities.MaxCitites);
                    randomNumB = random.Next(cities.MaxCitites - 1); 
                    if (randomNumA == randomNumB)
                    {
                        randomNumB++;
                    }
                    Swap(randomNumA, randomNumB);
                    distanceTraveled = 0;
                    CalculateTotalDistance();
                    break;
                default:
                    break;
            }
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

            for (int i = 1; i < orderVisited.Count; i++)
            {
                distanceTraveled += (int)(Math.Sqrt(Math.Abs((Math.Pow(orderVisited[i].Coordinate.X, 2) - Math.Pow(orderVisited[i - 1].Coordinate.X, 2)))
                    + Math.Abs((Math.Pow(orderVisited[i].Coordinate.Y, 2) - Math.Pow(orderVisited[i - 1].Coordinate.Y, 2)))));
            }
            //last back to first
            distanceTraveled += (int)(Math.Sqrt(Math.Abs((Math.Pow(orderVisited[orderVisited.Count - 1].Coordinate.X, 2) - Math.Pow(orderVisited[0].Coordinate.X, 2))) +
                Math.Abs(Math.Pow(orderVisited[orderVisited.Count - 1].Coordinate.Y, 2) - Math.Pow(orderVisited[0].Coordinate.Y, 2))));

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
