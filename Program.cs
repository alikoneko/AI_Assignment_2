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
            Console.WriteLine("Running...");
            for (int i = 0; i < 5; i++)
            {
                SalesmanSolver solve = new SalesmanSolver(300, 10000);
                solve.Run();
            }
            Console.WriteLine("Done!");

            //Console.Write(solve);
            Console.ReadKey();
        }
    }
}
