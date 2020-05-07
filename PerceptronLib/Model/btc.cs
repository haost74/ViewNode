using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace PerceptronLib.Model
{
    public class Binances
    {
        public Binance[] Property1 { get; set; }

        public void ParseJson(string nameFile)
        {
            //DataContractJsonSerializer deserializer = new DataContractJsonSerializer(typeof(BlogSite));
            DataContractJsonSerializer ds = new DataContractJsonSerializer(typeof(Binance));
        }
    }

    public class Binance
    {
        public string price { get; set; }
        public float value { get; set; }
    }

}
