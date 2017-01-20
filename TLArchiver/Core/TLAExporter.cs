using System;
using System.Collections.Generic;
using System.IO;
using TeleSharp.TL;
using TLArchiver.Entities;
using TLArchiver.Exporter;

namespace TLArchiver.Core
{
    delegate void DialogsHandler(ICollection<TLADialog> dialogs);
    delegate void DialogHandler(TLADialog dialog);
    delegate void AbsMessageHandler(TLAbsMessage dialog);
    delegate void MessageHandler(TLMessage dialog);
    delegate void MessageServiceHandler(TLMessageService dialog);

    class TLAExporter
    {
        private volatile bool m_bAbort = false;

        private event DialogsHandler OnBeginDialogs;
        private event DialogHandler OnBeginDialog;
        private event AbsMessageHandler OnBeginMessage;
        private event MessageHandler OnExportMessage;
        private event MessageServiceHandler OnExportMessageService;
        private event AbsMessageHandler OnEndMessage;
        private event DialogHandler OnEndDialog;
        private event DialogsHandler OnEndDialogs;
        private event Action OnAbort;

        private Config m_config;
        private TLAArchiver m_archiver;
        private ICollection<TLADialog> m_dialogs;

        public TLAExporter(Config config, ICollection<TLADialog> dialogs)
        {
            m_config = config;
            m_archiver = config.Archiver;
            m_dialogs = dialogs;

            OnBeginDialogs = null;
            OnBeginDialog = null;
            OnEndMessage = null;
            OnEndDialog = null;
            OnEndDialogs = null;

            ExportDirectory = m_config.ExportDirectory;
#if (!DEBUG)
            m_sExportDirectory += DateTime.Now.ToString("_yyyy_MM_dd_HH_mm_ss");
#endif
            Directory.CreateDirectory(ExportDirectory);
        }

        public void Start()
        {
            try
            {
                if (OnBeginDialogs != null)
                    OnBeginDialogs(m_dialogs);

                foreach (TLADialog dialog in m_dialogs)
                {
                    if (!m_config.CountMessagesAtLaunch)
                        dialog.Total = m_archiver.GetTotalMessages(dialog);

                    if (OnBeginDialog != null)
                        OnBeginDialog(dialog);

                    foreach (TLAbsMessage absMessage in m_archiver.GetMessages(dialog))
                    {
                        if (m_bAbort)
                        {
                            if (OnAbort != null)
                                OnAbort();
                            return;
                        }

                        if (OnBeginMessage != null)
                            OnBeginMessage(absMessage);

                        if (absMessage is TLMessage && OnExportMessage != null)
                            OnExportMessage((TLMessage)absMessage);
                        else if (absMessage is TLMessageService && OnExportMessageService != null)
                            OnExportMessageService((TLMessageService)absMessage);
                        else
                            throw new TLCoreException("The message is not a TLMessage or a TLMessageService");

                        if (OnEndMessage != null)
                            OnEndMessage(absMessage);
                    }
                    if (OnEndDialog != null)
                        OnEndDialog(dialog);
                }
                if (OnEndDialogs != null)
                    OnEndDialogs(m_dialogs);
            }
            catch(Exception e)
            {
                if (OnAbort != null)
                    OnAbort();
                throw e;
            }
        }

        public void RequestStop()
        {
            m_bAbort = true;
        }

        public void AddExporter(IExporter exporter)
        {
            OnBeginDialogs += new DialogsHandler(exporter.BeginDialogs);
            OnBeginDialog += new DialogHandler(exporter.BeginDialog);
            OnBeginMessage += new AbsMessageHandler(exporter.BeginMessage);
            OnExportMessage += new MessageHandler(exporter.ExportMessage);
            OnExportMessageService += new MessageServiceHandler(exporter.ExportMessageService);
            OnEndMessage += new AbsMessageHandler(exporter.EndMessage);
            OnEndDialog += new DialogHandler(exporter.EndDialog);
            OnEndDialogs += new DialogsHandler(exporter.EndDialogs);
            OnAbort += new Action(exporter.Abort);
        }

        public string ExportDirectory { get; private set; }
    }
}
