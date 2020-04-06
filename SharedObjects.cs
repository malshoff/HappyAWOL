using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Config.Net;
using HappyRebellion.Config;
using TaleWorlds.Library;

namespace HappyRebellion {
    public class SharedObjects {
        
        private static SharedObjects _instance;
        public static SharedObjects Instance => _instance ?? (_instance = new SharedObjects());

        public static IMySettings Settings = loadSettings();

        private static IMySettings loadSettings() {
            string path = Path.Combine(BasePath.Name, "Modules", "HappyRebellion");
            if (!Directory.Exists(path))
                throw new Exception("Cannot find the module named HappyRebellion");
            path = Path.Combine(path, "ModuleData","Config" );
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            path = Path.Combine(path, "config.ini");
            if (!File.Exists(path)) 
                throw new Exception($"No config file found at path {path}");

            return new ConfigurationBuilder<IMySettings>()
            .UseIniFile(path)
            .Build();
}
    }
}
