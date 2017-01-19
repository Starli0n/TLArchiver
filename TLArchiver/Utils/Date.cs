using System;

namespace TLArchiver.Utils
{
    public class Date
    {
        private static readonly DateTime c_date0 = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

        // Convert a Telegram date to DateTime
        static public DateTime TLConvert(int iTLDate)
        {
            return c_date0.AddSeconds(iTLDate).ToLocalTime();
        }

        // Convert a Telegram date to 'dd.mm.yy HH:mm'
        static public string TLConvertTxt(int iTLDate)
        {
            DateTime dt = TLConvert(iTLDate);
            return String.Format("{0:dd}.{0:MM}.{0:yy} {0:HH}:{0:mm}", dt);
        }

        // Convert a Telegram date to 'yyyy-MM-dd'
        static public string TLPrefix(int iTLDate)
        {
            DateTime dt = TLConvert(iTLDate);
            return String.Format("{0:yyyy}-{0:MM}-{0:dd}", dt);
        }
    }
}
