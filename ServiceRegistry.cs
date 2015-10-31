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

        public ServiceRegistry()
        {
            random = new Random();
        }
        public static ServiceRegistry GetInstance()
        {
            if (null == instance)
            {
                instance = new ServiceRegistry();
            }
            return instance;
        }

        public Random GetRandom()
        {
            return random;
        }
    }
}
