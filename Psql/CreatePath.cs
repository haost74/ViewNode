using System.IO;
using System.Threading.Tasks;

namespace Psql
{
    public class CreatePath
    {
        public static string Path { get; private set; } = "";
        public static async Task<string> GetPath(string file = "connectParameters.json")
        {
            if (!File.Exists(file))
                await Create(file);
            await new JobJeson().ReaderJeson<Parameters>(file, new Parameters())
                              .ContinueWith(_res =>
                              {
                                  var r = _res.Result;

                              });

            return Path;
        }

        static async Task Create(string file)
        {
            JobJeson jobJeson = new JobJeson();
            var v = new { server = "", port = 5432, user = "", password = "", database = "" };

            await jobJeson.SaveJeson(file, v)
                 .ContinueWith(rest =>
                 {
                     if (rest.Result)
                     {

                     }
                 });
        }
    }
}
