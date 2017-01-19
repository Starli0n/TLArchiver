using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using TeleSharp.TL;
using TLArchiver.Core;
using TLArchiver.Entities;
using TLArchiver.Exporter;

namespace TLArchiver.UI
{
    public partial class FormExport : Form, IExporter
    {
        private TLAExporter m_exporter;
        private Thread m_exporterThread;

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
            // Prepare arguments
            FormTLArchiver arg = this.Owner as FormTLArchiver;
            if (arg == null)
                throw new TLUIException("Owner is not a FormTLArchiveMedia");
            Config config = arg.GetConfig();
            TLAArchiver archiver = arg.Archiver;
            ICollection<TLADialog> dialogList = arg.Dialogs.Where(d => d.Selected).ToList(); // Process selected dialogs only

            // How to: Create and Terminate Threads
            // https://msdn.microsoft.com/en-us/library/7a2f3ay4(v=vs.100).aspx

            // Create the worker thread object. This does not start the thread.
            m_exporter = new TLAExporter(config, dialogList);
            if (config.ExportText)
                m_exporter.AddExporter(new TxtExporter(config, m_exporter.ExportDirectory));
            if (config.ExportHtml)
                m_exporter.AddExporter(new HtmlExporter(config, m_exporter.ExportDirectory));
            m_exporter.AddExporter(this);
            m_exporterThread = new Thread(m_exporter.Start);
            m_exporterThread.Name = "Export Thread";

            // Start the worker thread.
            m_exporterThread.Start();

            // Loop until the worker thread activates.
            while (!m_exporterThread.IsAlive);
        }

        private void m_bAbort_Click(object sender, System.EventArgs e)
        {
            // Request that the worker thread stop itself.
            m_exporter.RequestStop();

            // Use the Thread.Join method to block the current thread
            // until the object's thread terminates.
            m_exporterThread.Join();
        }

        // Since the worker and the UI are not in the same thread use the 'Invoke' template
        // https://msdn.microsoft.com/en-us/library/ms171728(v=vs.110).aspx

        public void BeginDialogs(ICollection<TLADialog> dialogs)
        {
            BeginInvoke(new DialogsHandler(d =>
                {
                    m_pbDialogs.Maximum = d.Count;
                }),
                new object[] { dialogs });
        }

        public void BeginDialog(TLADialog dialog)
        {
            BeginInvoke(new DialogHandler(d =>
                {
                    m_pbMessages.Maximum = d.Total;
                    m_lStatusDialog.Text = d.Title;
                    m_lStatusCurrent.Text = "0";
                    m_lStatusTotal.Text = "/ " + m_pbMessages.Maximum.ToString();
                }),
                new object[] { dialog });
        }

        public void BeginMessage(TLAbsMessage absMessage)
        {

        }

        public void ExportMessage(TLMessage message)
        {

        }

        public void ExportMessageService(TLMessageService message)
        {

        }

        public void EndMessage(TLAbsMessage absMessage)
        {
            BeginInvoke((MethodInvoker)delegate
                {
                    m_pbMessages.Value++;
                    m_lStatusCurrent.Text = m_pbMessages.Value.ToString();
                });
        }

        public void EndDialog(TLADialog dialog)
        {
            BeginInvoke((MethodInvoker)delegate
                {
                    m_pbDialogs.Value++;
                });
        }

        public void EndDialogs(ICollection<TLADialog> m_dialogs)
        {
            BeginInvoke((MethodInvoker)delegate
            {
                m_bAbort.Enabled = false;
                m_bOk.Enabled = true;
            });
        }

        public void Abort()
        {

        }
    }
}
