using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perceptron.Utility
{
    public class MapLine
    {
        public Tuple<double, double>[,] map = null;
        public double radius = 0;
        public Tuple<double, double>[,] GetMap(Func< int, int, Tuple<double, double>[,]> func)
        {
            Tuple<double, double>[,] res = null;

            for (var i = 0; i < map.Length; i++)
            {
                for (var j = 0; j < map.Length; j++)
                {
                   res = func(i , j);
                }
            }

            return res;
        }

    }
}
