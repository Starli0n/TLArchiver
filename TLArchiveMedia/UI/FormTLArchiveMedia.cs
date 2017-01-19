using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using TLArchiveMedia.Entities;

namespace TLArchiveMedia.UI
{
    public partial class FormTLArchiveMedia : Form
    {
        private Config m_config;
        private TLArchiver m_archiver;
        private bool m_bConnected;
        private bool m_bIsDialogsCheckedChanging = false;
        private bool m_bIsContentCheckedChanging = false;
        private bool m_bIsExportCheckedChanging = false;
        private BindingSource m_dialogSource = new BindingSource();
        private BindingList<TLADialog> m_dialogView = new BindingList<TLADialog>();
        private List<TLADialog> m_dialogs = new List<TLADialog>();
        private int m_orderIndex = 0;
        private ListSortDirection m_direction = ListSortDirection.Ascending;
        private DatagridViewCheckBoxHeaderCell m_cbHeader;

        public TLArchiver Archiver { get { return m_archiver; } }
        public ICollection<TLADialog> Dialogs { get { return m_dialogView; } }

        public bool IsFromDate { get { return m_cbFromDate.Checked; } }
        public bool IsToDate { get { return m_cbToDate.Checked; } }
        public DateTime FromDate { get { return m_dtpFrom.Value; } }
        public DateTime ToDate { get { return m_dtpTo.Value; } }

        public bool ExportMessages { get { return m_cbMessages.Checked; } }
        public bool ExportFiles { get { return m_cbFiles.Checked; } }
        public bool ExportPhotos { get { return m_cbPhotos.Checked; } }
        public bool ExportVideos { get { return m_cbVideos.Checked; } }
        public bool ExportVoiceMessages { get { return m_cbVoiceMessages.Checked; } }

        public bool ExportText { get { return m_cbText.Checked; } }
        public bool ExportHtml { get { return m_cbHtml.Checked; } }

        public FormTLArchiveMedia()
        {
            InitializeComponent();
            m_config = new Config();
            Config.Load(m_config);
            m_archiver = new TLArchiver(m_config);
            // Grid configuration
            m_dialogSource.DataSource = m_dialogView; // m_dialogSource.SupportsFiltering: true
            m_dgvDialogs.DataSource = m_dialogSource; // Initialize the grid
            m_dgvDialogs.RowHeadersVisible = false;
            m_dgvDialogs.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            m_dgvDialogs.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            m_dgvDialogs.AllowUserToAddRows = false;
            // Fill the last column to match the resize of the form
            m_dgvDialogs.Columns[m_dgvDialogs.Columns.Count - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            // The grid is readonly except for the 1st column which is the selection
            foreach (DataGridViewColumn col in m_dgvDialogs.Columns)
                col.ReadOnly = true;
            DataGridViewColumn selectedCol = m_dgvDialogs.Columns[0];
            selectedCol.ReadOnly = false;
            selectedCol.Width = 50;
            m_dgvDialogs.Columns[3].Width = 200; // Enlarge the 'Title' column
            // Add an event on the header cell to sort the grid
            m_dgvDialogs.ColumnHeaderMouseClick += new DataGridViewCellMouseEventHandler(m_dgvDialogs_ColumnHeaderMouseClick);
            // CheckBox event handler walkaround https://stackoverflow.com/questions/11843488/datagridview-checkbox-event
            m_dgvDialogs.CellContentClick += new DataGridViewCellEventHandler(m_dgvDialogs_CellContentClick);
            m_dgvDialogs.CellValueChanged += new DataGridViewCellEventHandler(m_dgvDialogs_CellValueChanged);
        }

        private bool Connected
        {
            get { return m_bConnected; }
            set
            {
                if (value)
                {
                    m_status.Text = "Connected";
                    LoadGrid();
                }
                else
                    m_status.Text = "Waiting for the login code...";
                m_lCode.Enabled = !value;
                m_tbCode.Enabled = !value;
                m_bLogin.Enabled = !value;
                m_tbCode.Text = "";
                m_bConnected = value;
            }
        }

        private void LoadGrid()
        {
            m_dialogs.Clear();
            m_dialogs.AddRange(m_archiver.GetUserDialogs());
            m_dialogs.AddRange(m_archiver.GetContacts());

            UpdateView();
        }

        private void UpdateView()
        {
            m_dialogView.Clear();

            if (m_cbDialogsAll.CheckState == CheckState.Unchecked)
                return;

            IEnumerable<TLADialog> filter;
            // Where Clause
            if (m_cbDialogsAll.CheckState == CheckState.Indeterminate)
            {
                filter = Enumerable.Empty<TLADialog>();
                if (m_cbChannels.Checked)
                    filter = filter.Union<TLADialog>(m_dialogs
                        .Where(d => d.Type == TLADialogType.Channel));
                if (m_cbChats.Checked)
                    filter = filter.Union<TLADialog>(m_dialogs
                        .Where(d => d.Type == TLADialogType.Chat));
                if (m_cbUsers.Checked)
                    filter = filter.Union<TLADialog>(m_dialogs
                        .Where(d => d.Type == TLADialogType.User));
            }
            else // if (m_cbDialogsAll.CheckState == CheckState.Checked)
                filter = m_dialogs;

            // OrderBy Clause
            if (m_orderIndex == 1)
            {
                if (m_direction == ListSortDirection.Ascending)
                    filter = filter.OrderBy(d => d.Id);
                else
                    filter = filter.OrderByDescending(d => d.Id);
            }
            else if (m_orderIndex == 2)
            {
                if (m_direction == ListSortDirection.Ascending)
                    filter = filter.OrderBy(d => d.Type);
                else
                    filter = filter.OrderByDescending(d => d.Type);
            }
            else if (m_orderIndex == 3)
            {
                if (m_direction == ListSortDirection.Ascending)
                    filter = filter.OrderBy(d => d.Title);
                else
                    filter = filter.OrderByDescending(d => d.Title);
            }
            else if (m_orderIndex == 4)
            {
                if (m_direction == ListSortDirection.Ascending)
                    filter = filter.OrderBy(d => d.Date);
                else
                    filter = filter.OrderByDescending(d => d.Date);
            }
            else if (m_orderIndex == 5)
            {
                if (m_direction == ListSortDirection.Ascending)
                    filter = filter.OrderBy(d => d.Closed);
                else
                    filter = filter.OrderByDescending(d => d.Closed);
            }

            // Update the View with the filter
            foreach (TLADialog dialog in filter)
                m_dialogView.Add(dialog);
        }

        private void FormTLArchiveMedia_Load(object sender, System.EventArgs e)
        {
            m_cbDialogsAll.Checked = true;
            m_cbContentAll.Checked = true;

            Connected = m_archiver.Connect();
            if (!Connected)
                m_archiver.SendCodeRequest();
        }

        private void m_bLogin_Click(object sender, System.EventArgs e)
        {
            Connected = m_archiver.MakeAuth(m_tbCode.Text);
        }

        private void m_cbFromDate_CheckedChanged(object sender, System.EventArgs e)
        {
            m_dtpFrom.Enabled = m_cbFromDate.Checked;
        }

        private void m_cbToDate_CheckedChanged(object sender, System.EventArgs e)
        {
            m_dtpTo.Enabled = m_cbToDate.Checked;
        }

        private void m_cbDialogsAll_CheckedChanged(object sender, System.EventArgs e)
        {
            if (m_bIsDialogsCheckedChanging)
                return;
            m_bIsDialogsCheckedChanging = true;
            try
            {
                m_cbChannels.Checked = m_cbDialogsAll.Checked;
                m_cbChats.Checked = m_cbDialogsAll.Checked;
                m_cbUsers.Checked = m_cbDialogsAll.Checked;
            }
            finally
            {
                m_bIsDialogsCheckedChanging = false;
            }
            UpdateView();
        }

        private void m_cbDialogs_CheckedChanged(object sender, System.EventArgs e)
        {
            if (m_bIsDialogsCheckedChanging)
                return;
            m_bIsDialogsCheckedChanging = true;
            try
            {
                if (m_cbChannels.Checked && m_cbChats.Checked && m_cbUsers.Checked)
                    m_cbDialogsAll.CheckState = CheckState.Checked;
                else if (!m_cbChannels.Checked && !m_cbChats.Checked && !m_cbUsers.Checked)
                    m_cbDialogsAll.CheckState = CheckState.Unchecked;
                else
                    m_cbDialogsAll.CheckState = CheckState.Indeterminate;
            }
            finally
            {
                m_bIsDialogsCheckedChanging = false;
            }
            UpdateView();
        }

        private void m_cbContentAll_CheckedChanged(object sender, System.EventArgs e)
        {
            if (m_bIsContentCheckedChanging)
                return;
            m_bIsContentCheckedChanging = true;
            try
            {
                m_cbMessages.Checked = m_cbContentAll.Checked;
                m_cbFiles.Checked = m_cbContentAll.Checked;
                m_cbPhotos.Checked = m_cbContentAll.Checked;
                m_cbVideos.Checked = m_cbContentAll.Checked;
                m_cbVoiceMessages.Checked = m_cbContentAll.Checked;
            }
            finally
            {
                m_bIsContentCheckedChanging = false;
            }
        }

        private void m_cbContent_CheckedChanged(object sender, System.EventArgs e)
        {
            if (m_bIsContentCheckedChanging)
                return;
            m_bIsContentCheckedChanging = true;
            try
            {
                if (m_cbMessages.Checked && m_cbFiles.Checked && m_cbPhotos.Checked && m_cbVideos.Checked && m_cbVoiceMessages.Checked)
                    m_cbContentAll.CheckState = CheckState.Checked;
                else if (!m_cbMessages.Checked && !m_cbFiles.Checked && !m_cbPhotos.Checked && !m_cbVideos.Checked && !m_cbVoiceMessages.Checked)
                    m_cbContentAll.CheckState = CheckState.Unchecked;
                else
                    m_cbContentAll.CheckState = CheckState.Indeterminate;
            }
            finally
            {
                m_bIsContentCheckedChanging = false;
            }
        }

        private void m_cbExportAll_CheckedChanged(object sender, System.EventArgs e)
        {
            if (m_bIsExportCheckedChanging)
                return;
            m_bIsExportCheckedChanging = true;
            try
            {
                m_cbText.Checked = m_cbExportAll.Checked;
                m_cbHtml.Checked = m_cbExportAll.Checked;
            }
            finally
            {
                m_bIsExportCheckedChanging = false;
            }
        }

        private void m_cbExport_CheckedChanged(object sender, System.EventArgs e)
        {
            if (m_bIsExportCheckedChanging)
                return;
            m_bIsExportCheckedChanging = true;
            try
            {
                if (m_cbText.Checked && m_cbHtml.Checked)
                    m_cbExportAll.CheckState = CheckState.Checked;
                else if (!m_cbText.Checked && !m_cbHtml.Checked)
                    m_cbExportAll.CheckState = CheckState.Unchecked;
                else
                    m_cbExportAll.CheckState = CheckState.Indeterminate;
            }
            finally
            {
                m_bIsExportCheckedChanging = false;
            }
        }

        private void m_bExport_Click(object sender, System.EventArgs e)
        {
            using (FormExport export = new FormExport())
                export.ShowDialog(this);
            //List<string> contacts = m_tlArchiveMedia.GetContacts();
            //List<string> dialogs = m_tlArchiveMedia.GetUserDialogs();

            /*m_tlArchiveMedia.ExportDirectory = m_config.ExportDirectory;
#if (!DEBUG)
            m_tlArchiveMedia.ExportDirectory += DateTime.Now.ToString("_yyyy_MM_dd_HH_mm_ss");
#endif
            Directory.CreateDirectory(m_tlArchiveMedia.ExportDirectory);
            m_tlArchiveMedia.Export(
                m_cbFromDate.Checked ? m_dtpFrom.Value : DateTime.MinValue,
                m_cbToDate.Checked ? m_dtpTo.Value : DateTime.MaxValue);*/
        }

        private void m_dgvDialogs_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            // Remove the text header of the first column and replace it by a "(Un)Select All" checkbox
            m_cbHeader = new DatagridViewCheckBoxHeaderCell();
            m_cbHeader.Value = "";
            m_cbHeader.OnCheckBoxClicked += new CheckBoxClickedHandler(m_cbHeader_CheckBoxClicked);
            m_dgvDialogs.Columns[0].HeaderCell = m_cbHeader;
            m_dgvDialogs.ClearSelection();
        }

        public void m_cbHeader_CheckBoxClicked(bool bState)
        {
            // Propagate the state of the checbox header to all checkbox in the column
            m_dgvDialogs.ClearSelection();
            foreach (TLADialog dialog in m_dialogs)
                dialog.Selected = bState;
            m_dgvDialogs.Update();
            m_dgvDialogs.Refresh();
            m_bExport.Select(); // Set the focus on Export button
        }

        private void m_dgvDialogs_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0) // Only for selected column
                m_dgvDialogs.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void m_dgvDialogs_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            m_cbHeader.IsDirty = true; // Mix between selected and unselected
            m_dgvDialogs.Invalidate(); // Trigger the Paint() event
        }

        private void m_dgvDialogs_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            m_orderIndex = e.ColumnIndex;
            UpdateView();
            m_direction = m_direction == ListSortDirection.Ascending
                ? ListSortDirection.Descending : ListSortDirection.Ascending;
        }
    }
}
