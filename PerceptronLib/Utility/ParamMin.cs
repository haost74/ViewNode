using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerceptronLib.Utility
{
    public struct ParamMin
    {
        public double min;
        public double max;
        public Random rn;
        

        public double ran()
        {
            return min + rn.NextDouble() * (max - min);
        }
    }
}
