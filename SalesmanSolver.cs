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
        int ELITE_PERCENT = 2;
        int MUTATION_RATE = 10;
        double TARGET = 20000.0;
        //variables
        CityMap map;
        List<Route> routes;
        List<Route> bestAllTime;
        int initialPopulation;
        Random random;
        int generations;

        public SalesmanSolver(int population, int generations)
        {
            map = new CityMap();
            routes = new List<Route>();
            bestAllTime = new List<Route>();
            this.initialPopulation = population;
            this.generations = generations;
            Initialize();
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
                routes.Add(route);
            }
        }

        private void Tournament()
        {
            routes = routes.OrderBy(r => r.DistanceTraveled).ToList();
            List<Route> top = new List<Route>();
            if (routes.Count > 200)
            {
                for (int i = 0; i < 100; i++)
                {
                    top.Add(routes[i]);
                }
            }
            else
            {
                for (int i = 0; i < 10; i++)
                {
                    top.Add(routes[i]);
                }
            }

            routes = top;
        }

        private void Repopulate()
        {
            //copy the top 2 -- check
            //mutate 10% -- check
            //repopulate with new routes 30% of initial population (try new things!) --check!
            List<Route> elites = new List<Route>();
            List<Route> mutants = new List<Route>();
            routes = routes.OrderBy(r => r.DistanceTraveled).ToList();
            for (int i = 0; i < ELITE_PERCENT; i++)
            {
                elites.Add(routes[i]);
            }
            int mutation_count = routes.Count / MUTATION_RATE;
            for (int i = 0; i < mutation_count; i++)
            {
                int rand = random.Next(routes.Count);
                routes[rand].Mutate();
            }

            foreach (Route route in elites)
            {
                if (!bestAllTime.Contains(route))
                {
                    bestAllTime.Add(route);
                }
                routes.Add(route);
            }

            bestAllTime = bestAllTime.OrderBy(x => x.DistanceTraveled).ToList();
            if (bestAllTime.Count > 5)
            {
                List<Route> temp = new List<Route>();
                for (int i = 0; i < 5; i++)
                {
                    temp.Add(bestAllTime[i]);
                }
                bestAllTime = temp;
            }

            foreach (Route route in bestAllTime)
            {
                routes.Add(route);
            }

            while (routes.Count < (int)(initialPopulation * 0.30))
            {
                Route temp = new Route(map);
                temp.GenerateFirstRoute();
                routes.Add(temp);
            }

        }
        
        public void Run()
        {
            GeneratePopulation();
            for (int i = 0; i < generations; i++)
            {
                Tournament();
                Repopulate();
            }
            Tournament();

        }
        public override string ToString()
        {
            string retString = "";
            foreach (Route route in routes)
            {
                retString += route.DistanceTraveled + " : " + TARGET + " \n";
            }

            return retString;
        }
    }
}
