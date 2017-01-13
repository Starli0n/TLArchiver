using TLArchiver.Core;

namespace TLArchiver.Exporter
{
    public class TxtExporter : FileExporter, IExporter
    {
        private static readonly string c_sExporterDirectory = "Text";
        private static readonly string c_sMessagesFile = "Messages.txt";

        public TxtExporter(Config config, string sDirectory) : base(config)
        {
            m_sExporterDirectory = c_sExporterDirectory;
            m_sMessagesFile = c_sMessagesFile;
            Initialize(sDirectory);
        }
    }
}
