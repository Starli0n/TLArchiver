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
        private const string c_sMessagesFile = "Messages.txt"; // Messages for one dialog
        private const string c_sLinksFile = "Links.txt"; // Links for one dialog
        private const string c_sLogsFile = "Logs.txt"; // Logs for one dialog
        private const string c_sErrorTag = "### Error:"; // Used to identify error in logs

        protected string m_sHeader;

        public TxtExporter(Config config, string sDirectory) : base(config)
        {
            m_sExporterDirectory = c_sExporterDirectory;
            m_sMessagesFile = c_sMessagesFile;
            m_sLinksFile = c_sLinksFile;
            m_sLogsFile = c_sLogsFile;
            Initialize(sDirectory);
        }

        public override void ExportMessage(TLMessage message)
        {
            m_sAuthor = GetAuthor(message.from_id);
            m_sHeader = String.Format(c_sMessageHeader, m_sAuthor, Date.TLConvertTxt(message.date));

            string sError = "";
            try
            {
                if (message.media != null)
                {
                    m_sPrefix = Date.TLPrefix(message.date);
                    if (message.media.GetType() == typeof(TLMessageMediaPhoto))
                    {
                        string sFileName = ExportPhoto((TLMessageMediaPhoto)message.media);
                        Prepend(String.Format("[{0}]", sFileName));
                    }
                    else if (message.media.GetType() == typeof(TLMessageMediaDocument))
                    {
                        string sFileName = ExportDocument((TLMessageMediaDocument)message.media);
                        Prepend(String.Format("[{0}]", sFileName));
                    }
                    else if (message.media.GetType() == typeof(TLMessageMediaWebPage))
                    {
                        //string sFileName = ExportLink((TLMessageMediaWebPage)message.media);
                        //Prepend(String.Format("[{0}]", sFileName));
                    }
                    else
                        throw new TLCoreException(String.Format("Media not handled: {0}", message.media.ToString()));
                }
            }
            catch (TLCoreException e)
            {
                sError = String.Format("{0} [{1}] {2}", c_sErrorTag, message.id, e.Message);
                PrependLog(sError);
                if (message.message != "")
                    PrependLog(message.message);
                PrependLog(m_sHeader);
                PrependLog();
            }

            if (sError != "")
                Prepend(sError);

            if (message.message != "")
                Prepend(message.message);
            Prepend(m_sHeader);

            if (ProcessLink(message.message))
            {
                PrependLink(m_sHeader);
                PrependLink("");
            }
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
