using System.IO;
using System.Threading.Tasks;

namespace Psql
{
    public class CreatePath
    {
        public static string Path { get; private set; } = "";
        public static async Task<string> GetPath(string file = "connectParameters.json")
        {
            if (string.IsNullOrEmpty(Path))
            {
                if (!File.Exists(file))
                    await Create(file);
                await new JobJeson().ReaderJeson<Parameters>(file, new Parameters())
                                  .ContinueWith(_res =>
                                  {
                                      var param = _res.Result;

                                      Path = $"Server={param.server}; Port={param.port}; User Id={param.user}; Password={param.password}; Database={param.database}";
                                  });
            }

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
