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

        int ELITE_COUNT = 2;
        int MUTATION_RATE = 50;
        double TOP_PERCENT = 10;
        double TARGET = 20000.0;

        //variables
        private CityMap map;
        private List<Route> routes;
        private List<Route> eliteRoutes;
        private int populationSize;
        private Random random;
        private int generations;
        private Logger log;


        public SalesmanSolver(int population, int generations)
        {
            map = new CityMap();
            routes = new List<Route>();
            eliteRoutes = new List<Route>();
            this.populationSize = population;
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
            for (int i = 0; i < populationSize; i++)
            {
                Route route = new Route(map);
                route.Randomize();
                routes.Add(route);
            }

            log.Log("Initial best: " + routes.OrderBy(r => r.DistanceTraveled).First().DistanceTraveled);

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
            routes = routes.OrderBy(r => r.DistanceTraveled).Take((int)(routes.Count / TOP_PERCENT)).ToList();
            eliteRoutes.AddRange(routes.OrderBy(r => r.DistanceTraveled).Take((int)(ELITE_COUNT)).ToList());
            eliteRoutes = eliteRoutes.OrderBy(r => r.DistanceTraveled).Take((int)(ELITE_COUNT)).ToList();

            List<Route> newRoutes = new List<Route>();

            newRoutes.AddRange(eliteRoutes);

            while (newRoutes.Count < populationSize)
            {
                switch (ServiceRegistry.GetInstance().GetReproductionMethod())
                {
                    case ReproductionMethod.Methods.Asexual:
                        newRoutes.Add(routes[random.Next(routes.Count)].Mutate());
                        break;
                    case ReproductionMethod.Methods.Mate:
                        Route parent1 = routes[random.Next(routes.Count)];
                        Route parent2 = routes[random.Next(routes.Count)];;
                        Route child1 = parent1.Mate(parent2);
                        newRoutes.Add(child1);
                        break;
                    default:
                        throw new NotImplementedException("ReproductionMethod " + ServiceRegistry.GetInstance().GetReproductionMethod() + " not implemented");
                }
                
            }

            routes = newRoutes;
        }
        
        public void Run()
        {
            GeneratePopulation();
            routes = routes.OrderBy(r => r.DistanceTraveled).ToList();

            //log.Log("" + routes[0].DistanceTraveled);

            for (int i = 0; i < generations; i++)
            {
                Repopulate();

            }

            log.Log("Final Best: " + routes[0].DistanceTraveled);
            //log.Log(routes[0].ToString());

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
