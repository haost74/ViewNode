namespace Perceptron
{
    public class Config
    {
        private Config() { }
        private static Config config;
        private static object obj = new object();
        public static Config Cfg
        {
            get
            {
                if (config == null) config = new Config();
                return config;
            }
        }

        public Perceptron.Utility.Colors Colors { get; set; } = new Perceptron.Utility.Colors();
    }
}
