using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerceptronLib.Model
{

    public class Rootobject
    {
        public Datum[] data { get; set; }
    }

    public class Datum
    {
        public double param { get; set; }
        public double weight { get; set; }
    }


}
