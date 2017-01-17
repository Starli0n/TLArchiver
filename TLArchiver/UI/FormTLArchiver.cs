using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using TLArchiver.Core;
using TLArchiver.Entities;
using TLSharp.Core.Network;

namespace TLArchiver.UI
{
    public partial class FormTLArchiver : Form
    {
        private Config m_config;
        private TLAArchiver m_archiver;
        private bool m_bConnected;
        private bool m_bIsHeaderCheckBoxClicked = false;
        private bool m_bIsDialogsCheckedChanging = false;
        private bool m_bIsContentCheckedChanging = false;
        private bool m_bIsExportCheckedChanging = false;
        private BindingSource m_dialogSource = new BindingSource();
        private BindingList<TLADialog> m_dialogView = new BindingList<TLADialog>();
        private List<TLADialog> m_dialogs = new List<TLADialog>();
        private string m_orderColumnName;
        private ListSortDirection m_direction = ListSortDirection.Ascending;
        private DatagridViewCheckBoxHeaderCell m_cbHeader;

        public TLAArchiver Archiver { get { return m_archiver; } }
        public ICollection<TLADialog> Dialogs { get { return m_dialogView; } }

        public FormTLArchiver()
        {
            InitializeComponent();
            m_config = new Config();
            Config.Load(m_config);
            m_archiver = new TLAArchiver(m_config);
            m_orderColumnName = "";
            // Grid configuration
            m_dialogSource.DataSource = m_dialogView; // m_dialogSource.SupportsFiltering: true
            m_dgvDialogs.DataSource = m_dialogSource; // Initialize the grid
            m_dgvDialogs.RowHeadersVisible = false;
            m_dgvDialogs.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            m_dgvDialogs.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            m_dgvDialogs.AllowUserToAddRows = false;
            // The grid is readonly except for the 1st column which is the selection
            foreach (DataGridViewColumn col in m_dgvDialogs.Columns)
                col.ReadOnly = true;
            DataGridViewColumn selectedCol = m_dgvDialogs.Columns["Selected"];
            selectedCol.ReadOnly = false;
            selectedCol.Width = 50;
            if (m_config.CountMessagesAtLaunch)
            {
                // Fill the last column to match the resize of the form
                m_dgvDialogs.Columns["Total"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            else
            {
                m_dgvDialogs.Columns["Total"].Visible = false; // Hide the 'Total' column
                m_dgvDialogs.Columns["Closed"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill; // Become the last column
                Width -= 80;
            }
            m_dgvDialogs.Columns["AccessHash"].Visible = false; // Hide the 'AccessHash' column
            m_dgvDialogs.Columns["Title"].Width = 200; // Enlarge the 'Title' column
            // Add an event on the header cell to sort the grid
            m_dgvDialogs.ColumnHeaderMouseClick += new DataGridViewCellMouseEventHandler(m_dgvDialogs_ColumnHeaderMouseClick);
            // CheckBox event handler walkaround https://stackoverflow.com/questions/11843488/datagridview-checkbox-event
            m_dgvDialogs.CellContentClick += new DataGridViewCellEventHandler(m_dgvDialogs_CellContentClick);
            m_dgvDialogs.CellValueChanged += new DataGridViewCellEventHandler(m_dgvDialogs_CellValueChanged);
            // Checkbox header
            m_cbHeader = new DatagridViewCheckBoxHeaderCell();
            m_cbHeader.Value = "";
            m_cbHeader.OnCheckBoxClicked += new CheckBoxClickedHandler(m_cbHeader_CheckBoxClicked);
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
            // Remove the text header of the first column and replace it by a "(Un)Select All" checkbox
            m_dgvDialogs.Columns["Selected"].HeaderCell = m_cbHeader;
            m_dgvDialogs.ClearSelection();

            m_dialogs.Clear();
            m_dialogs.AddRange(m_archiver.GetContacts());
            m_config.Contacts = m_dialogs.ToDictionary(d => d.Id, d => d.Title);
            m_dialogs.AddRange(m_archiver.GetUserDialogs());

            if (m_config.CountMessagesAtLaunch)
                foreach (TLADialog dialog in m_dialogs)
                {
                    try
                    {
                        dialog.Total = m_archiver.GetTotalMessages(dialog);
                    }
                    catch (AggregateException e)
                    {
                        if (e.InnerException.GetType() == typeof(InvalidOperationException))
                        {
                            // CHANNEL_PRIVATE
                        }
                        else if (e.InnerException.GetType() == typeof(FloodException))
                        {
                            FloodException f = (FloodException)e.InnerException;
                            Thread.Sleep((int)f.TimeToWait.TotalMilliseconds);
                        }
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                }

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
            if (m_orderColumnName == "Selected")
            {
                if (m_direction == ListSortDirection.Ascending)
                    filter = filter.OrderBy(d => d.Selected);
                else
                    filter = filter.OrderByDescending(d => d.Selected);
            }
            else if (m_orderColumnName == "Id")
            {
                if (m_direction == ListSortDirection.Ascending)
                    filter = filter.OrderBy(d => d.Id);
                else
                    filter = filter.OrderByDescending(d => d.Id);
            }
            else if (m_orderColumnName == "AccessHash")
            {
                if (m_direction == ListSortDirection.Ascending)
                    filter = filter.OrderBy(d => d.AccessHash);
                else
                    filter = filter.OrderByDescending(d => d.AccessHash);
            }
            else if (m_orderColumnName == "Type")
            {
                if (m_direction == ListSortDirection.Ascending)
                    filter = filter.OrderBy(d => d.Type);
                else
                    filter = filter.OrderByDescending(d => d.Type);
            }
            else if (m_orderColumnName == "Title")
            {
                if (m_direction == ListSortDirection.Ascending)
                    filter = filter.OrderBy(d => d.Title);
                else
                    filter = filter.OrderByDescending(d => d.Title);
            }
            else if (m_orderColumnName == "Date")
            {
                if (m_direction == ListSortDirection.Ascending)
                    filter = filter.OrderBy(d => d.Date);
                else
                    filter = filter.OrderByDescending(d => d.Date);
            }
            else if (m_orderColumnName == "Closed")
            {
                if (m_direction == ListSortDirection.Ascending)
                    filter = filter.OrderBy(d => d.Closed);
                else
                    filter = filter.OrderByDescending(d => d.Closed);
            }
            else if (m_orderColumnName == "Total")
            {
                if (m_direction == ListSortDirection.Ascending)
                    filter = filter.OrderBy(d => d.Total);
                else
                    filter = filter.OrderByDescending(d => d.Total);
            }
            else if (m_orderColumnName != "")
                throw new TLUIException("Column name not referenced for sorting data");

            // Update the View with the filter
            foreach (TLADialog dialog in filter)
                m_dialogView.Add(dialog);
        }

        private void FormTLArchiveMedia_Load(object sender, System.EventArgs e)
        {
            m_cbDialogsAll.Checked = true;
            m_cbContentAll.Checked = true;
            m_cbExportAll.Checked = true;

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
                SetExportStatus();
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
                SetExportStatus();
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
                SetExportStatus();
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
                SetExportStatus();
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
                SetExportStatus();
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
                SetExportStatus();
            }
            finally
            {
                m_bIsExportCheckedChanging = false;
            }
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
            SetExportStatus();
            m_bIsHeaderCheckBoxClicked = true;
        }

        private void m_dgvDialogs_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0) // Only for "Selected" column
                m_dgvDialogs.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void m_dgvDialogs_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            m_cbHeader.SetDirty(); // Mix between selected and unselected
            SetExportStatus();
        }

        private void m_dgvDialogs_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (m_bIsHeaderCheckBoxClicked)
            {
                // Do not sort whenever the title checkbox is clicked
                m_bIsHeaderCheckBoxClicked = false;
                return;
            }

            m_orderColumnName = m_dgvDialogs.Columns[e.ColumnIndex].Name;
            UpdateView();
            m_direction = m_direction == ListSortDirection.Ascending
                ? ListSortDirection.Descending : ListSortDirection.Ascending;
        }

        private void SetExportStatus()
        {
            m_bExport.Enabled = m_cbDialogsAll.CheckState != CheckState.Unchecked
                && m_cbContentAll.CheckState != CheckState.Unchecked
                && m_cbExportAll.CheckState != CheckState.Unchecked
                && m_cbHeader.CheckState != CheckBoxState.UncheckedNormal;
        }

        private void m_bExport_Click(object sender, System.EventArgs e)
        {
            UpdateConfig();
            using (FormExport export = new FormExport())
                export.ShowDialog(this);
            /*m_tlArchiveMedia.Export(
                m_cbFromDate.Checked ? m_dtpFrom.Value : DateTime.MinValue,
                m_cbToDate.Checked ? m_dtpTo.Value : DateTime.MaxValue);*/
        }

        private void UpdateConfig()
        {
            m_config.IsFromDate = m_cbFromDate.Checked;
            m_config.IsToDate = m_cbToDate.Checked;
            m_config.FromDate = m_dtpFrom.Value;
            m_config.ToDate = m_dtpTo.Value;

            m_config.ExportMessages = m_cbMessages.Checked;
            m_config.ExportFiles = m_cbFiles.Checked;
            m_config.ExportPhotos = m_cbPhotos.Checked;
            m_config.ExportVideos = m_cbVideos.Checked;
            m_config.ExportVoiceMessages = m_cbVoiceMessages.Checked;

            m_config.ExportText = m_cbText.Checked;
            m_config.ExportHtml = m_cbHtml.Checked;
        }

        public Config GetConfig()
        {
            return m_config;
        }
    }
}
