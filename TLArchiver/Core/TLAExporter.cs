using System;
using System.Collections.Generic;
using System.IO;
using TeleSharp.TL;
using TLArchiver.Entities;
using TLArchiver.Exporter;

namespace TLArchiver.Core
{
    delegate void BeginProcessingDialogsDel(ICollection<TLADialog> dialogs);
    delegate void BeginProcessingDialogDel(TLADialog dialog);

    class TLAExporter
    {
        private volatile bool m_bAbort = false;

        public BeginProcessingDialogsDel OnBegingDialogs;
        public BeginProcessingDialogDel OnBegingDialog;
        public Action OnEndMessage;
        public Action OnEndDialog;
        public Action OnEndDialogs;

        private Config m_config;
        private TLAArchiver m_archiver;
        private ICollection<TLADialog> m_dialogs;
        private List<IExporter> m_exporters;

        public TLAExporter(Config config, TLAArchiver archiver, ICollection<TLADialog> dialogs)
        {
            m_config = config;
            m_archiver = archiver;
            m_dialogs = dialogs;
            m_exporters = new List<IExporter>();

            OnBegingDialogs = null;
            OnBegingDialog = null;
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
            BegingDialogs(m_dialogs);
            foreach (TLADialog dialog in m_dialogs)
            {
                BegingDialog(dialog);
                foreach (TLAbsMessage absMessage in m_archiver.GetMessages(dialog))
                {
                    if (m_bAbort)
                        return;

                    BegingMessage(absMessage);

                    if (absMessage is TLMessage)
                        ExportMessage((TLMessage)absMessage);
                    else if (absMessage is TLMessageService)
                        ExportMessageService((TLMessageService)absMessage);
                    else
                        throw new TLCoreException("The message is not a TLMessage or a TLMessageService");

                    EndMessage(absMessage);
                }
                EndDialog(dialog);
            }
            EndDialogs(m_dialogs);
        }

        private void BegingDialogs(ICollection<TLADialog> dialogs)
        {
            if (OnBegingDialogs != null)
                OnBegingDialogs(dialogs);
        }

        private void BegingDialog(TLADialog dialog)
        {
            if (!m_config.CountMessagesAtLaunch)
                dialog.Total = m_archiver.GetTotalMessages(dialog);

            if (OnBegingDialog != null)
                OnBegingDialog(dialog);

            foreach (IExporter exporter in m_exporters)
                exporter.BeginDialog(dialog);
        }

        private void BegingMessage(TLAbsMessage absMessage)
        {

        }

        public void ExportMessage(TLMessage message)
        {
            foreach (IExporter exporter in m_exporters)
                exporter.ExportMessage(message);
        }

        public void ExportMessageService(TLMessageService message)
        {
            foreach (IExporter exporter in m_exporters)
                exporter.ExportMessageService(message);
        }

        private void EndMessage(TLAbsMessage absMessage)
        {
            if (OnEndMessage != null)
                OnEndMessage();
        }

        private void EndDialog(TLADialog dialog)
        {
            if (OnEndDialog != null)
                OnEndDialog();
        }

        private void EndDialogs(ICollection<TLADialog> m_dialogs)
        {
            if (OnEndDialogs != null)
                OnEndDialogs();
        }

        public void RequestStop()
        {
            m_bAbort = true;
        }

        public void AddExporter(IExporter exporter)
        {
            m_exporters.Add(exporter);
        }

        public string ExportDirectory { get; private set; }
    }
}
