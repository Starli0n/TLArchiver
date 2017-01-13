using System.IO;
using TeleSharp.TL;

namespace TLArchiver.Exporter
{
    public class HtmlExporter : IExporter
    {
        private static readonly string c_sSubDirectory = "Html";
        private string m_sExportDirectory;

        public HtmlExporter()
        {

        }

        public void SetDirectory(string directory)
        {
            m_sExportDirectory = Path.Combine(directory, c_sSubDirectory);
            Directory.CreateDirectory(m_sExportDirectory);
        }

        public void ExportMessage(TLMessage message)
        {

        }

        public void ExportMessageService(TLMessageService message)
        {

        }
    }
}
