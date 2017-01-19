using System;
using TeleSharp.TL;
using TLArchiver.Core;
using TLArchiver.Utils;

namespace TLArchiver.Exporter
{
    public class TxtExporter : FileExporter, IExporter
    {
        private const string c_sMessageHeader = "{0}, [{1}]"; // Author, dd.mm.yy hh:mm
        private const string c_sExporterDirectory = "Text";
        private const string c_sMessagesFile = "Messages.txt";

        protected string m_sHeader;

        public TxtExporter(Config config, string sDirectory) : base(config)
        {
            m_sExporterDirectory = c_sExporterDirectory;
            m_sMessagesFile = c_sMessagesFile;
            Initialize(sDirectory);
        }

        public override void ExportMessage(TLMessage message)
        {
            m_sAuthor = GetAuthor(message.from_id);
            m_sHeader = String.Format(c_sMessageHeader, m_sAuthor, Date.TLConvertTxt(message.date));

            if (message.media != null)
            {
                m_sPrefix = Date.TLPrefix(message.date);
                if (message.media.GetType() == typeof(TLMessageMediaPhoto))
                {
                    string sFileName = ExportPhoto((TLMessageMediaPhoto)message.media);
                    Prepend(String.Format("[{0}]", sFileName));
                }
            }

            if (message.message != "")
                Prepend(message.message);
            Prepend(m_sHeader);
        }

        public override void ExportMessageService(TLMessageService message)
        {
            m_sAuthor = GetAuthor(message.from_id);
            m_sHeader = String.Format(c_sMessageHeader, m_sAuthor, Date.TLConvertTxt(message.date));

            string sMessage = "";
            if (message.action.GetType() == typeof(TLMessageActionChannelCreate))
                sMessage = "ChannelCreate";
            else if (message.action.GetType() == typeof(TLMessageActionChannelMigrateFrom))
                sMessage = "ChannelMigrateFrom";
            else if (message.action.GetType() == typeof(TLMessageActionChatAddUser))
                sMessage = "ChatAddUser";
            else if (message.action.GetType() == typeof(TLMessageActionChatCreate))
                sMessage = "ChatCreate";
            else if (message.action.GetType() == typeof(TLMessageActionChatDeletePhoto))
                sMessage = "ChatDeletePhoto";
            else if (message.action.GetType() == typeof(TLMessageActionChatDeleteUser))
                sMessage = "ChatDeleteUser";
            else if (message.action.GetType() == typeof(TLMessageActionChatEditPhoto))
                sMessage = "ChatEditPhoto";
            else if (message.action.GetType() == typeof(TLMessageActionChatEditTitle))
                sMessage = "ChatEditTitle";
            else if (message.action.GetType() == typeof(TLMessageActionChatJoinedByLink))
                sMessage = "ChatJoinedByLink";
            else if (message.action.GetType() == typeof(TLMessageActionChatMigrateTo))
                sMessage = "ChatMigrateTo";
            else if (message.action.GetType() == typeof(TLMessageActionEmpty))
                sMessage = "Empty";
            else if (message.action.GetType() == typeof(TLMessageActionGameScore))
                sMessage = "GameScore";
            else if (message.action.GetType() == typeof(TLMessageActionHistoryClear))
                sMessage = "HistoryClear";
            else if (message.action.GetType() == typeof(TLMessageActionPinMessage))
                sMessage = "PinMessage";
            else
                throw new TLCoreException("Unknown message service");

            Prepend(sMessage);
            Prepend(m_sHeader);
        }

        public override void EndMessage(TLAbsMessage absMessage)
        {
            Prepend();
        }
    }
}
