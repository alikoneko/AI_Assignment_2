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
            for (int i = 0; i < 50; i++)
            {
<<<<<<< HEAD
                SalesmanSolver solve = new SalesmanSolver(500, 2000);
=======
                SalesmanSolver solve = new SalesmanSolver(100, 1000);
>>>>>>> parent of f35a38a... Minor refactor
                solve.Run();
            }
            //Console.Write(solve);
            //Console.ReadKey();
        }
    }
}
