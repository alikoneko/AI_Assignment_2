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
            for (int i = 0; i < 1; i++)
            {
                SalesmanSolver solve = new SalesmanSolver(500, 1000);
                solve.Run();
            }
            //Console.Write(solve);
            //Console.ReadKey();
        }
    }
}
