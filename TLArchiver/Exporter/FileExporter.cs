using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TeleSharp.TL;
using TLArchiver.Core;
using TLArchiver.Entities;

namespace TLArchiver.Exporter
{
    public abstract class FileExporter : IExporter
    {
        protected static readonly string c_sUnknownAuthor = "Unknown ({0})";

        protected Config m_config;
        protected string m_sExporterDirectory;
        protected string m_sMessagesFile;
        protected string m_sDialogDirectory;
        protected string m_sAuthor;
        protected StringBuilder m_messages;

        public FileExporter(Config config)
        {
            m_config = config;
            m_messages = new StringBuilder();
        }

        protected void Initialize(string directory)
        {
            m_sExporterDirectory = Path.Combine(directory, m_sExporterDirectory);
            Directory.CreateDirectory(m_sExporterDirectory);
        }

        public virtual void BeginDialogs(ICollection<TLADialog> dialogs)
        {

        }

        public virtual void BeginDialog(TLADialog dialog)
        {
            string sub = dialog.Title;
            if (dialog.Type != TLADialogType.User)
                sub += " - " + dialog.Type.ToString();
            m_sDialogDirectory = Path.Combine(m_sExporterDirectory, sub);
            Directory.CreateDirectory(m_sDialogDirectory);

            if (m_config.ExportMessages)
                m_messages.Clear();
        }

        public virtual void BeginMessage(TLAbsMessage absMessage)
        {

        }

        public virtual void ExportMessage(TLMessage message)
        {
            if (message.from_id != null && m_config.Contacts.ContainsKey(message.from_id.Value))
                m_sAuthor = m_config.Contacts[message.from_id.Value];
            else
                m_sAuthor = String.Format(c_sUnknownAuthor, message.from_id);
        }

        public virtual void ExportMessageService(TLMessageService message)
        {

        }

        public virtual void EndMessage(TLAbsMessage absMessage)
        {

        }

        public virtual void EndDialog(TLADialog dialog)
        {
            if (m_config.ExportMessages)
            {
                string sFile = Path.Combine(m_sDialogDirectory, m_sMessagesFile);
                using (StreamWriter file = new StreamWriter(sFile))
                    file.Write(m_messages.ToString());
                m_messages.Clear();
            }
        }

        public virtual void EndDialogs(ICollection<TLADialog> m_dialogs)
        {

        }

        public virtual void Abort()
        {

        }

        protected void Prepend(string sMessage = "")
        {
            if (m_config.ExportMessages)
                m_messages.Insert(0, sMessage + Environment.NewLine);
        }
    }
}
