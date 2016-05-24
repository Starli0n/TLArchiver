using System;
using System.IO;
using System.Windows.Forms;

namespace TLArchiveMedia
{
    public partial class FormTLArchiveMedia : Form
    {
        private Config m_config;
        private TLArchiveMedia m_tlArchiveMedia;
        private bool m_bConnected;

        public FormTLArchiveMedia()
        {
            InitializeComponent();
            m_config = new Config();
            Config.Load(m_config);
            m_tlArchiveMedia = new TLArchiveMedia(m_config);
        }

        private bool Connected
        {
            get { return m_bConnected; }
            set
            {
                if (value)
                    m_status.Text = "Connected";
                else
                    m_status.Text = "Waiting for the login code...";
                m_lCode.Enabled = !value;
                m_tbCode.Enabled = !value;
                m_bLogin.Enabled = !value;
                m_tbCode.Text = "";
                m_bConnected = value;
            }
        }

        private void FormTLArchiveMedia_Load(object sender, System.EventArgs e)
        {
            Connected = m_tlArchiveMedia.Connect();
            if (!Connected)
                m_tlArchiveMedia.SendCodeRequest();
        }

        private void m_bLogin_Click(object sender, System.EventArgs e)
        {
            Connected = m_tlArchiveMedia.MakeAuth(m_tbCode.Text);
        }

        private void m_cbFromDate_CheckedChanged(object sender, System.EventArgs e)
        {
            m_dtpFrom.Enabled = m_cbFromDate.Checked;
        }

        private void m_cbToDate_CheckedChanged(object sender, System.EventArgs e)
        {
            m_dtpTo.Enabled = m_cbToDate.Checked;
        }

        private void m_bExport_Click(object sender, System.EventArgs e)
        {
            m_tlArchiveMedia.ExportDirectory = m_config.ExportDirectory;
#if (!DEBUG)
            m_tlArchiveMedia.ExportDirectory += DateTime.Now.ToString("_yyyy_MM_dd_HH_mm_ss");
#endif
            Directory.CreateDirectory(m_tlArchiveMedia.ExportDirectory);
            m_tlArchiveMedia.Export(
                m_cbFromDate.Checked ? m_dtpFrom.Value : DateTime.MinValue,
                m_cbToDate.Checked ? m_dtpTo.Value : DateTime.MaxValue);
        }
    }
}
