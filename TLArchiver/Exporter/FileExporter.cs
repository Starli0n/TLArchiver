using System.Collections.Generic;
using System.IO;
using TeleSharp.TL;
using TLArchiver.Core;
using TLArchiver.Entities;

namespace TLArchiver.Exporter
{
    public abstract class FileExporter : IExporter
    {
        protected Config m_config;
        protected string m_sExporterDirectory;
        protected string m_sMessagesFile;
        protected string m_sDialogDirectory;
        protected StreamWriter m_file;

        public FileExporter(Config config)
        {
            m_config = config;
            m_file = null;
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
            {
                string sFile = Path.Combine(m_sDialogDirectory, m_sMessagesFile);
                m_file = new StreamWriter(sFile);
            }
        }

        public virtual void BeginMessage(TLAbsMessage absMessage)
        {

        }

        public virtual void ExportMessage(TLMessage message)
        {

        }

        public virtual void ExportMessageService(TLMessageService message)
        {

        }

        public virtual void EndMessage(TLAbsMessage absMessage)
        {

        }

        public virtual void EndDialog(TLADialog dialog)
        {
            CloseFile();
        }

        public virtual void EndDialogs(ICollection<TLADialog> m_dialogs)
        {

        }

        public virtual void Abort()
        {
            CloseFile();
        }

        private void CloseFile()
        {
            if (m_file != null)
            {
                m_file.Flush();
                m_file.Close();
                m_file.Dispose();
                m_file = null;
            }
        }
    }
}
