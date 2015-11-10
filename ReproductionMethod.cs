using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_Assignment_2
{
    class ReproductionMethod
    {
        public enum Methods { Asexual, Mate, Split };
        private Methods method;

        public ReproductionMethod(Methods method)
        {
            this.method = method;
        }

        public Methods Method
        {
            get 
            {
                return method;
            }
        }
    }
}
