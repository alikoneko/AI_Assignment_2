using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_Assignment_2
{
    class CSVOutputter
    {
        string timeStamp;
        public CSVOutputter()
        {
            timeStamp = DateTime.Now.ToString("yyyy-MM-dd-HH-mm");
        }
        public void ToCSV(string message)
        {
            System.IO.File.AppendAllText(@"AI_CSV_" + timeStamp + ".csv", message + "\n");
        }
    }
}
