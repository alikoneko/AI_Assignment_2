using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_Assignment_2
{
    class SalesmanSolver
    {
        //TODO: Decide on a kill the dinosaurs method. Comets can be good-bgad
        //constants adjust to taste
        double TARGET = 20000.0;
        int ELITE_PERCENT = 2;
        int MUTATION_RATE = 10;
        //variables
        CityMap map;
        Dictionary<Route, int> routes;
        int initialPopulation;
        Random random;
        int generations;

        public SalesmanSolver(int population, int generations)
        {
            map = new CityMap();
            routes = new Dictionary<Route, int>();
            this.initialPopulation = population;
            this.generations = generations;
            Initialize();
            GeneratePopulation();
            Tournament();
        }

        private void Initialize()
        {
            random = ServiceRegistry.GetInstance().GetRandom();
        }

        private void GeneratePopulation()
        {
            for (int i = 0; i < initialPopulation; i++)
            {
                Route route = new Route(map);
                route.GenerateFirstRoute();
                int fitness = CalculateFitness(route);
                routes.Add(route, fitness);
            }
        }

        //higher = closer to target
        private int CalculateFitness(Route route)
        {
            return (int)((TARGET / route.DistanceTraveled) * 100);
        }

        private void Tournament()
        {
            Dictionary<Route, int> sorted = routes.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
            foreach (KeyValuePair<Route, int> pair in sorted)
            {
                Console.WriteLine("{0} : {1}", pair.Key.DistanceTraveled, pair.Value);
            }
        }

        private Route Mate(Route father)
        {
            throw new NotImplementedException();
        }

        private void Mutate()
        {
            throw new NotImplementedException();
        }

        private int CrossoverPoint()
        {
            throw new NotImplementedException();
        }

        public void Run()
        {

        }

        public override string ToString()
        {
            string retString = "";
            foreach (int fitness in routes.Values.ToList())
            {
                retString += fitness + "\n";
            }

            return retString;
        }
    }
}
