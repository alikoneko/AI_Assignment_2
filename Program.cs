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
            SalesmanSolver solve = new SalesmanSolver(5000, 50000);
            solve.Run();
            Console.Write(solve);
            Console.ReadKey();
        }
    }
}
