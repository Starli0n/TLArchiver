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

        public BeginProcessingDialogsDel BeginProcessingDialogs;
        public BeginProcessingDialogDel BeginProcessingDialog;
        public Action EndProcessingMessage;
        public Action EndProcessingDialog;
        public Action EndProcessingDialogs;

        Config m_config;
        TLAArchiver m_archiver;
        ICollection<TLADialog> m_dialogs;
        List<IExporter> m_exporters;

        public TLAExporter(Config config, TLAArchiver archiver, ICollection<TLADialog> dialogs)
        {
            m_config = config;
            m_archiver = archiver;
            m_dialogs = dialogs;
            m_exporters = new List<IExporter>();

            BeginProcessingDialogs = null;
            BeginProcessingDialog = null;
            EndProcessingMessage = null;
            EndProcessingDialog = null;
            EndProcessingDialogs = null;

            ExportDirectory = m_config.ExportDirectory;
#if (!DEBUG)
            ExportDirectory += DateTime.Now.ToString("_yyyy_MM_dd_HH_mm_ss");
#endif
            Directory.CreateDirectory(ExportDirectory);
        }

        public void Start()
        {
            if (BeginProcessingDialogs != null)
                BeginProcessingDialogs(m_dialogs);

            foreach (TLADialog dialog in m_dialogs)
            {
                if (!m_config.CountMessagesAtLaunch)
                    dialog.Total = m_archiver.GetTotalMessages(dialog);

                if (BeginProcessingDialog != null)
                    BeginProcessingDialog(dialog);

                foreach (TLAbsMessage absMessage in m_archiver.GetMessages(dialog))
                {
                    if (m_bAbort)
                        return;

                    if (absMessage is TLMessage)
                        ExportMessage((TLMessage)absMessage);
                    else if (absMessage is TLMessageService)
                        ExportMessageService((TLMessageService)absMessage);
                    else
                        throw new TLCoreException("The message is not a TLMessage or a TLMessageService");

                    if (EndProcessingMessage != null)
                        EndProcessingMessage();
                }
                if (EndProcessingDialog != null)
                    EndProcessingDialog();
            }
            if (EndProcessingDialogs != null)
                EndProcessingDialogs();
        }

        public void RequestStop()
        {
            m_bAbort = true;
        }

        public void AddExporter(IExporter exporter)
        {
            exporter.SetDirectory(ExportDirectory);
            m_exporters.Add(exporter);
        }

        public void ExportMessage(TLMessage message)
        {
            foreach(IExporter exporter in m_exporters)
                exporter.ExportMessage(message);
        }

        public void ExportMessageService(TLMessageService message)
        {
            foreach (IExporter exporter in m_exporters)
                exporter.ExportMessageService(message);
        }

        public string ExportDirectory { get; set; }
    }
}
