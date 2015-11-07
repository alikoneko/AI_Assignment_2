using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_Assignment_2
{
    class ServiceRegistry
    {
        private static ServiceRegistry instance;
        private Random random;
        private Logger log;
        private ReproductionMethod reproductionMethod;

        public ServiceRegistry()
        {
            random = new Random();
            log = new Logger();
            reproductionMethod = new ReproductionMethod(ReproductionMethod.Methods.Mate);

        }
        public static ServiceRegistry GetInstance()
        {
            if (null == instance)
            {
                instance = new ServiceRegistry();
            }
            return instance;
        }

        public ReproductionMethod.Methods GetReproductionMethod()
        {
            return reproductionMethod.Method;
        }

        public Random GetRandom()
        {
            return random;
        }

        public Logger GetLog()
        {
            return log;
        }
    }
}
