using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_Assignment_2
{
    class Logger
    {
        public void Log(string message)
        {
            System.IO.File.AppendAllText(@"AI_Log.txt", message + "\n");
        }
    }
}