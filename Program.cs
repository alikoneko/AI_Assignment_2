using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_Assignment_2
{
    class Program
    {
        static void Main(string[] args)
        {
            CityMap map = new CityMap();
            foreach (City city in map.Cities)
            {
                Console.WriteLine(city);
            }
            Console.ReadKey();
        }
    }
}
