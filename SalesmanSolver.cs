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
        int ELITES = 2;
        int MUTATION_RATE = 50;
        double TARGET = 20000.0;
        //variables
        CityMap map;
        List<Route> routes;
        List<Route> eliteRoutes;
        int initialPopulation;
        Random random;
        int generations;
        Logger log;

        public SalesmanSolver(int population, int generations)
        {
            map = new CityMap();
            routes = new List<Route>();
            eliteRoutes = new List<Route>();
            this.initialPopulation = population;
            this.generations = generations;
            Initialize();
        }

        private void Initialize()
        {
            random = ServiceRegistry.GetInstance().GetRandom();
            log = ServiceRegistry.GetInstance().GetLog();
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
            routes = routes.OrderBy(r => r.DistanceTraveled).Take(10).ToList();
        }

        private void Repopulate()
        {
            // make elites an instance variable
            // take the top 2 from the current generation, add to elites
            // sort elites by distance
            // elites becomse elites.take(2), keeping the best 2 of the new tops + old elites
            // put elites into population
            // mutate 100% of population
            // repopulate with percent of new random routes
            eliteRoutes.AddRange(routes.OrderBy(r => r.DistanceTraveled).Take(2).ToList());
            eliteRoutes = eliteRoutes.OrderBy(r => r.DistanceTraveled).Take(2).ToList();

            List<Route> newRoutes = new List<Route>();

            newRoutes.AddRange(eliteRoutes);

            List<Route> bestRoutes = routes.OrderBy(r => r.DistanceTraveled).Take(2).ToList();

            while (newRoutes.Count < initialPopulation)
            {
                newRoutes.Add(bestRoutes[random.Next(bestRoutes.Count)].Mutate());
            }

            routes = newRoutes;
        }
        
        public void Run()
        {
            GeneratePopulation();
            log.Log("Initial Pop:");
            routes = routes.OrderBy(r => r.DistanceTraveled).ToList();
            foreach (Route route in routes)
            {
                log.Log("" + route.DistanceTraveled);
            }

            for (int i = 0; i < generations; i++)
            {
                Tournament();
                Repopulate();

            }

            log.Log("Final:");
            routes = routes.OrderBy(r => r.DistanceTraveled).ToList();
            foreach (Route route in routes.OrderBy(r => r.DistanceTraveled).ToList())
            {
                log.Log("" + route.DistanceTraveled);
            }
            log.Log(routes[0].ToString());

        }
        public override string ToString()
        {
            string retString = "";
            routes = routes.OrderBy(r => r.DistanceTraveled).ToList();
            foreach (Route route in routes)
            {
                retString += route.DistanceTraveled + "\n";
            }

            return retString;
        }
    }
}
