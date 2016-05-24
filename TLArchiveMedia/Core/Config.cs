using System;
using System.Configuration;

namespace TLArchiveMedia
{
    class Config
    {
        public string ApiHash { get; set; }
        public int ApiId { get; set; }
        public string NumberToAuthenticate { get; set; }
        public string NumberToSendMessage { get; set; }
        public string UserNameToSendMessage { get; set; }
        public string ExportDirectory { get; set; }

        static public void Load(Config config)
        {
            config.ApiHash = ConfigurationManager.AppSettings["ApiHash"];
            config.ApiId = Int32.Parse(ConfigurationManager.AppSettings["ApiId"]);
            config.NumberToAuthenticate = ConfigurationManager.AppSettings["NumberToAuthenticate"];
            config.NumberToSendMessage = ConfigurationManager.AppSettings["NumberToSendMessage"];
            config.UserNameToSendMessage = ConfigurationManager.AppSettings["UserNameToSendMessage"];
            config.ExportDirectory = ConfigurationManager.AppSettings["ExportDirectory"];
        }
    }
}
