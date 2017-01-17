using System;

namespace TLArchiver.Utils
{
    public class Date
    {
        private static readonly DateTime c_date0 = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

        static public DateTime TLConvert(int iTLDate)
        {
            return c_date0.AddSeconds(iTLDate).ToLocalTime();
        }

        static public string TLConvertTxt(int iTLDate)
        {
            DateTime dt = TLConvert(iTLDate);
            return String.Format("{0:dd}.{0:MM}.{0:yy} {0:HH}:{0:mm}", dt);
        }
    }
}
