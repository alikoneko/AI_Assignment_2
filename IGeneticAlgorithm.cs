using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_Assignment_2
{
    interface IGeneticAlgorithm
    {
        Dictionary<int, City> Mate(Dictionary<int, City> father);
        void Mutate();
        int CrossoverPoint();
    }
}
