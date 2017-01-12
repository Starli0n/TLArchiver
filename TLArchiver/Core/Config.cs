using System;
using System.Configuration;

namespace TLArchiver.Core
{
    // https://my.telegram.org to obtain your own ApiId and ApiHash

    public class Config
    {
        public string ApiHash { get; set; }
        public int ApiId { get; set; }
        public string SignInPhoneNumberCode { get; set; }
        public string SignInPhoneNumber { get; set; }
        public string NumberToAuthenticate
        {
            get
            {
                return SignInPhoneNumberCode + SignInPhoneNumber.TrimStart('0');
            }
        }
        public string CodeToAuthenticate { get; set; }
        public string NotRegisteredNumberToSignUp { get; set; }
        public string NumberToSendMessage { get; set; }
        public string UserNameToSendMessage { get; set; }
        public string NumberToGetUserFull { get; set; }
        public string NumberToAddToChat { get; set; }
        public string ExportDirectory { get; set; }
        public int MessagesReadLimit { get; set; }
        public bool CountMessagesAtLaunch { get; set; }

        static public void Load(Config config)
        {
            config.ApiHash = ConfigurationManager.AppSettings["ApiHash"];
            config.ApiId = Int32.Parse(ConfigurationManager.AppSettings["ApiId"]);
            //config.NumberToAuthenticate = ConfigurationManager.AppSettings["NumberToAuthenticate"];
            config.SignInPhoneNumberCode = ConfigurationManager.AppSettings["SignInPhoneNumberCode"];
            config.SignInPhoneNumber = ConfigurationManager.AppSettings["SignInPhoneNumber"];            
            config.CodeToAuthenticate = ConfigurationManager.AppSettings["CodeToAuthenticate"];
            config.NotRegisteredNumberToSignUp = ConfigurationManager.AppSettings["NotRegisteredNumberToSignUp"];
            config.NumberToSendMessage = ConfigurationManager.AppSettings["NumberToSendMessage"];
            config.UserNameToSendMessage = ConfigurationManager.AppSettings["UserNameToSendMessage"];
            config.NumberToGetUserFull = ConfigurationManager.AppSettings["NumberToGetUserFull"];
            config.NumberToAddToChat = ConfigurationManager.AppSettings["NumberToAddToChat"];
            config.ExportDirectory = ConfigurationManager.AppSettings["ExportDirectory"];
            config.MessagesReadLimit = Int32.Parse(ConfigurationManager.AppSettings["MessagesReadLimit"]);
            config.CountMessagesAtLaunch = Boolean.Parse(ConfigurationManager.AppSettings["CountMessagesAtLaunch"]);
        }
    }
}
