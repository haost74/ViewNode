using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Psql
{

    public class Rootobject
    {
        public Parameters parameters { get; set; }
    }

    public class Parameters
    {
        public string server { get; set; }
        public string user { get; set; }
        public string password { get; set; }
        public int port { get; set; }
        public string database { get; set; }
    }

}
