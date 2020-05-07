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

        /*
         
             // сохранение данных
            using (FileStream fs = new FileStream("user.json", FileMode.OpenOrCreate))
            {
                Person tom = new Person() { Name = "Tom", Age = 35 };
                await JsonSerializer.SerializeAsync<Person>(fs, tom);
                Console.WriteLine("Data has been saved to file");
            }
 
            // чтение данных
            using (FileStream fs = new FileStream("user.json", FileMode.OpenOrCreate))
            {
                Person restoredPerson = await JsonSerializer.DeserializeAsync<Person>(fs);
                Console.WriteLine($"Name: {restoredPerson.Name}  Age: {restoredPerson.Age}");
            }
             */

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
