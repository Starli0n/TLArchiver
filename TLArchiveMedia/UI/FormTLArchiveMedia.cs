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
    }
}
