using System;
using System.Collections.Generic;
using TeleSharp.TL;
using TLArchiver.Entities;

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

        TLAArchiver m_archiver;
        ICollection<TLADialog> m_dialogs;

        public TLAExporter(TLAArchiver archiver, ICollection<TLADialog> dialogs)
        {
            m_archiver = archiver;
            m_dialogs = dialogs;
            BeginProcessingDialogs = null;
        }

        public void Start()
        {
            if (BeginProcessingDialogs != null)
                BeginProcessingDialogs(m_dialogs);

            foreach (TLADialog dialog in m_dialogs)
            {
                dialog.Total = m_archiver.GetTotalMessages(dialog);

                if (BeginProcessingDialog != null)
                    BeginProcessingDialog(dialog);

                foreach (TLAbsMessage absMessage in m_archiver.GetMessages(dialog))
                {
                    if (m_bAbort)
                        return;

                    var message = absMessage as TLMessage;
                    if (message != null)
                    {
                        // TODO: Process a message
                    }
                    else
                    {
                        var messageService = absMessage as TLMessageService;
                        if (messageService == null)
                            throw new TLCoreException("The message is not a TLMessage or a TLMessageService");
                        // TODO: Process a message service
                    }
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
    }
}
