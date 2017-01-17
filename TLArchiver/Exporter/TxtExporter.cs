using System;
using TeleSharp.TL;
using TLArchiver.Core;
using TLArchiver.Utils;

namespace TLArchiver.Exporter
{
    public class TxtExporter : FileExporter, IExporter
    {
        private static readonly string c_sMessageHeader = "{0}, [{1}]"; // Author, dd.mm.yy hh:mm
        private static readonly string c_sExporterDirectory = "Text";
        private static readonly string c_sMessagesFile = "Messages.txt";

        protected string m_sHeader;

        public TxtExporter(Config config, string sDirectory) : base(config)
        {
            m_sExporterDirectory = c_sExporterDirectory;
            m_sMessagesFile = c_sMessagesFile;
            Initialize(sDirectory);
        }

        public override void ExportMessage(TLMessage message)
        {
            base.ExportMessage(message);

            m_sHeader = String.Format(c_sMessageHeader, m_sAuthor, Date.TLConvertTxt(message.date));
            Prepend(message.message);
            Prepend(m_sHeader);
        }

        public override void EndMessage(TLAbsMessage absMessage)
        {
            Prepend();
        }
    }
}
