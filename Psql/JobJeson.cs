using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace Psql
{
    public class JobJeson
    {
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

        public async Task<T> ReaderJeson<T>(string nameFile, T obj) where T : class
        {
            try
            {
                using (FileStream fs = new FileStream(nameFile, FileMode.OpenOrCreate))
                {
                    return await JsonSerializer.DeserializeAsync<T>(fs);
                }
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public async Task<bool> SaveJeson<T>(string nameFile, T obj) where T : class
        {
            bool isres = false;
            try
            {
                using (FileStream fs = new FileStream(nameFile, FileMode.OpenOrCreate))
                {
                    await JsonSerializer.SerializeAsync<T>(fs, obj);
                    isres = true;
                }
            }
            catch (System.Exception ex)
            {
                isres = false;
            }
            return isres;
        }

    }
}
