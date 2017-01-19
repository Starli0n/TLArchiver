using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using TeleSharp.TL;
using TLArchiveMedia.Entities;

namespace TLArchiveMedia.UI
{
    public partial class FormExport : Form
    {
        private bool m_bUserAbort = false;

        public FormExport()
        {
            InitializeComponent();
        }

        private void FormExport_Load(object sender, EventArgs e)
        {
            m_bOk.Enabled = false;
        }

        private void FormExport_Shown(object sender, System.EventArgs e)
        {
            FormTLArchiveMedia arg = this.Owner as FormTLArchiveMedia;
            if (arg == null)
                throw new TLUIException("Owner is not a FormTLArchiveMedia");

            // Process selected dialogs only
            ICollection<TLADialog> dialogs = arg.Dialogs.Where(d => d.Selected).ToList();

            m_pbDialogs.Maximum = dialogs.Count;
            foreach (TLADialog dialog in dialogs)
            {
                m_pbMessages.Maximum = arg.Archiver.GetTotalMessages(dialog);

                m_lStatusDialog.Text = dialog.Title;
                m_lStatusCurrent.Text = m_pbMessages.Value.ToString();
                m_lStatusTotal.Text = "/ " + m_pbMessages.Maximum.ToString();

                foreach (TLAbsMessage absMessage in arg.Archiver.GetMessages(dialog, m_pbMessages.Maximum))
                {

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
                    m_pbMessages.Value++;
                    m_lStatusCurrent.Text = m_pbMessages.Value.ToString();
                }
                m_pbDialogs.Value++;
            }

            m_bAbort.Enabled = false;
            m_bOk.Enabled = true;
        }

        private void m_bAbort_Click(object sender, System.EventArgs e)
        {
            // TODO: Main process should be in a thread to be able to be aborted
            m_bUserAbort = true;
        }
    }
}
