namespace TLArchiver.UI
{
    partial class FormTLArchiver
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormTLArchiver));
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.m_status = new System.Windows.Forms.ToolStripStatusLabel();
            this.m_lCode = new System.Windows.Forms.Label();
            this.m_tbCode = new System.Windows.Forms.TextBox();
            this.m_bLogin = new System.Windows.Forms.Button();
            this.m_cbFromDate = new System.Windows.Forms.CheckBox();
            this.m_dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.m_cbToDate = new System.Windows.Forms.CheckBox();
            this.m_dtpTo = new System.Windows.Forms.DateTimePicker();
            this.m_bExport = new System.Windows.Forms.Button();
            this.m_dgvDialogs = new System.Windows.Forms.DataGridView();
            this.m_gbDialogs = new System.Windows.Forms.GroupBox();
            this.m_cbChannels = new System.Windows.Forms.CheckBox();
            this.m_cbUsers = new System.Windows.Forms.CheckBox();
            this.m_cbChats = new System.Windows.Forms.CheckBox();
            this.m_cbDialogsAll = new System.Windows.Forms.CheckBox();
            this.m_gbContent = new System.Windows.Forms.GroupBox();
            this.m_cbVoiceMessages = new System.Windows.Forms.CheckBox();
            this.m_cbFiles = new System.Windows.Forms.CheckBox();
            this.m_cbVideos = new System.Windows.Forms.CheckBox();
            this.m_cbPhotos = new System.Windows.Forms.CheckBox();
            this.m_cbMessages = new System.Windows.Forms.CheckBox();
            this.m_cbContentAll = new System.Windows.Forms.CheckBox();
            this.m_gbExport = new System.Windows.Forms.GroupBox();
            this.m_cbHtml = new System.Windows.Forms.CheckBox();
            this.m_cbText = new System.Windows.Forms.CheckBox();
            this.m_cbExportAll = new System.Windows.Forms.CheckBox();
            this.statusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_dgvDialogs)).BeginInit();
            this.m_gbDialogs.SuspendLayout();
            this.m_gbContent.SuspendLayout();
            this.m_gbExport.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_status});
            this.statusStrip.Location = new System.Drawing.Point(0, 681);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(761, 22);
            this.statusStrip.TabIndex = 0;
            // 
            // m_status
            // 
            this.m_status.Name = "m_status";
            this.m_status.Size = new System.Drawing.Size(0, 17);
            // 
            // m_lCode
            // 
            this.m_lCode.AutoSize = true;
            this.m_lCode.Location = new System.Drawing.Point(12, 17);
            this.m_lCode.Name = "m_lCode";
            this.m_lCode.Size = new System.Drawing.Size(32, 13);
            this.m_lCode.TabIndex = 1;
            this.m_lCode.Text = "Code";
            // 
            // m_tbCode
            // 
            this.m_tbCode.Location = new System.Drawing.Point(50, 14);
            this.m_tbCode.Name = "m_tbCode";
            this.m_tbCode.Size = new System.Drawing.Size(100, 20);
            this.m_tbCode.TabIndex = 2;
            // 
            // m_bLogin
            // 
            this.m_bLogin.Location = new System.Drawing.Point(156, 12);
            this.m_bLogin.Name = "m_bLogin";
            this.m_bLogin.Size = new System.Drawing.Size(75, 23);
            this.m_bLogin.TabIndex = 3;
            this.m_bLogin.Text = "Login";
            this.m_bLogin.UseVisualStyleBackColor = true;
            this.m_bLogin.Click += new System.EventHandler(this.m_bLogin_Click);
            // 
            // m_cbFromDate
            // 
            this.m_cbFromDate.AutoSize = true;
            this.m_cbFromDate.Location = new System.Drawing.Point(12, 43);
            this.m_cbFromDate.Name = "m_cbFromDate";
            this.m_cbFromDate.Size = new System.Drawing.Size(49, 17);
            this.m_cbFromDate.TabIndex = 4;
            this.m_cbFromDate.Text = "From";
            this.m_cbFromDate.UseVisualStyleBackColor = true;
            this.m_cbFromDate.CheckedChanged += new System.EventHandler(this.m_cbFromDate_CheckedChanged);
            // 
            // m_dtpFrom
            // 
            this.m_dtpFrom.Enabled = false;
            this.m_dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.m_dtpFrom.Location = new System.Drawing.Point(67, 40);
            this.m_dtpFrom.Name = "m_dtpFrom";
            this.m_dtpFrom.Size = new System.Drawing.Size(83, 20);
            this.m_dtpFrom.TabIndex = 5;
            // 
            // m_cbToDate
            // 
            this.m_cbToDate.AutoSize = true;
            this.m_cbToDate.Location = new System.Drawing.Point(12, 69);
            this.m_cbToDate.Name = "m_cbToDate";
            this.m_cbToDate.Size = new System.Drawing.Size(39, 17);
            this.m_cbToDate.TabIndex = 6;
            this.m_cbToDate.Text = "To";
            this.m_cbToDate.UseVisualStyleBackColor = true;
            this.m_cbToDate.CheckedChanged += new System.EventHandler(this.m_cbToDate_CheckedChanged);
            // 
            // m_dtpTo
            // 
            this.m_dtpTo.Enabled = false;
            this.m_dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.m_dtpTo.Location = new System.Drawing.Point(67, 66);
            this.m_dtpTo.Name = "m_dtpTo";
            this.m_dtpTo.Size = new System.Drawing.Size(83, 20);
            this.m_dtpTo.TabIndex = 7;
            // 
            // m_bExport
            // 
            this.m_bExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_bExport.Location = new System.Drawing.Point(674, 655);
            this.m_bExport.Name = "m_bExport";
            this.m_bExport.Size = new System.Drawing.Size(75, 23);
            this.m_bExport.TabIndex = 10;
            this.m_bExport.Text = "Export";
            this.m_bExport.UseVisualStyleBackColor = true;
            this.m_bExport.Click += new System.EventHandler(this.m_bExport_Click);
            // 
            // m_dgvDialogs
            // 
            this.m_dgvDialogs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_dgvDialogs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.m_dgvDialogs.Location = new System.Drawing.Point(12, 106);
            this.m_dgvDialogs.Name = "m_dgvDialogs";
            this.m_dgvDialogs.Size = new System.Drawing.Size(737, 543);
            this.m_dgvDialogs.TabIndex = 11;
            this.m_dgvDialogs.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.m_dgvDialogs_DataBindingComplete);
            // 
            // m_gbDialogs
            // 
            this.m_gbDialogs.Controls.Add(this.m_cbChannels);
            this.m_gbDialogs.Controls.Add(this.m_cbUsers);
            this.m_gbDialogs.Controls.Add(this.m_cbChats);
            this.m_gbDialogs.Controls.Add(this.m_cbDialogsAll);
            this.m_gbDialogs.Location = new System.Drawing.Point(237, 12);
            this.m_gbDialogs.Name = "m_gbDialogs";
            this.m_gbDialogs.Size = new System.Drawing.Size(125, 88);
            this.m_gbDialogs.TabIndex = 12;
            this.m_gbDialogs.TabStop = false;
            this.m_gbDialogs.Text = "Dialogs";
            // 
            // m_cbChannels
            // 
            this.m_cbChannels.AutoSize = true;
            this.m_cbChannels.Location = new System.Drawing.Point(49, 19);
            this.m_cbChannels.Name = "m_cbChannels";
            this.m_cbChannels.Size = new System.Drawing.Size(70, 17);
            this.m_cbChannels.TabIndex = 3;
            this.m_cbChannels.Text = "Channels";
            this.m_cbChannels.UseVisualStyleBackColor = true;
            this.m_cbChannels.CheckedChanged += new System.EventHandler(this.m_cbDialogs_CheckedChanged);
            // 
            // m_cbUsers
            // 
            this.m_cbUsers.AutoSize = true;
            this.m_cbUsers.Location = new System.Drawing.Point(49, 65);
            this.m_cbUsers.Name = "m_cbUsers";
            this.m_cbUsers.Size = new System.Drawing.Size(53, 17);
            this.m_cbUsers.TabIndex = 2;
            this.m_cbUsers.Text = "Users";
            this.m_cbUsers.UseVisualStyleBackColor = true;
            this.m_cbUsers.CheckedChanged += new System.EventHandler(this.m_cbDialogs_CheckedChanged);
            // 
            // m_cbChats
            // 
            this.m_cbChats.AutoSize = true;
            this.m_cbChats.Location = new System.Drawing.Point(49, 42);
            this.m_cbChats.Name = "m_cbChats";
            this.m_cbChats.Size = new System.Drawing.Size(53, 17);
            this.m_cbChats.TabIndex = 1;
            this.m_cbChats.Text = "Chats";
            this.m_cbChats.UseVisualStyleBackColor = true;
            this.m_cbChats.CheckedChanged += new System.EventHandler(this.m_cbDialogs_CheckedChanged);
            // 
            // m_cbDialogsAll
            // 
            this.m_cbDialogsAll.AutoSize = true;
            this.m_cbDialogsAll.Location = new System.Drawing.Point(6, 19);
            this.m_cbDialogsAll.Name = "m_cbDialogsAll";
            this.m_cbDialogsAll.Size = new System.Drawing.Size(37, 17);
            this.m_cbDialogsAll.TabIndex = 0;
            this.m_cbDialogsAll.Text = "All";
            this.m_cbDialogsAll.UseVisualStyleBackColor = true;
            this.m_cbDialogsAll.CheckedChanged += new System.EventHandler(this.m_cbDialogsAll_CheckedChanged);
            // 
            // m_gbContent
            // 
            this.m_gbContent.Controls.Add(this.m_cbVoiceMessages);
            this.m_gbContent.Controls.Add(this.m_cbFiles);
            this.m_gbContent.Controls.Add(this.m_cbVideos);
            this.m_gbContent.Controls.Add(this.m_cbPhotos);
            this.m_gbContent.Controls.Add(this.m_cbMessages);
            this.m_gbContent.Controls.Add(this.m_cbContentAll);
            this.m_gbContent.Location = new System.Drawing.Point(368, 12);
            this.m_gbContent.Name = "m_gbContent";
            this.m_gbContent.Size = new System.Drawing.Size(193, 88);
            this.m_gbContent.TabIndex = 13;
            this.m_gbContent.TabStop = false;
            this.m_gbContent.Text = "Content";
            // 
            // m_cbVoiceMessages
            // 
            this.m_cbVoiceMessages.AutoSize = true;
            this.m_cbVoiceMessages.Location = new System.Drawing.Point(49, 65);
            this.m_cbVoiceMessages.Name = "m_cbVoiceMessages";
            this.m_cbVoiceMessages.Size = new System.Drawing.Size(101, 17);
            this.m_cbVoiceMessages.TabIndex = 5;
            this.m_cbVoiceMessages.Text = "VoiceMessages";
            this.m_cbVoiceMessages.UseVisualStyleBackColor = true;
            this.m_cbVoiceMessages.CheckedChanged += new System.EventHandler(this.m_cbContent_CheckedChanged);
            // 
            // m_cbFiles
            // 
            this.m_cbFiles.AutoSize = true;
            this.m_cbFiles.Location = new System.Drawing.Point(129, 19);
            this.m_cbFiles.Name = "m_cbFiles";
            this.m_cbFiles.Size = new System.Drawing.Size(47, 17);
            this.m_cbFiles.TabIndex = 4;
            this.m_cbFiles.Text = "Files";
            this.m_cbFiles.UseVisualStyleBackColor = true;
            this.m_cbFiles.CheckedChanged += new System.EventHandler(this.m_cbContent_CheckedChanged);
            // 
            // m_cbVideos
            // 
            this.m_cbVideos.AutoSize = true;
            this.m_cbVideos.Location = new System.Drawing.Point(129, 42);
            this.m_cbVideos.Name = "m_cbVideos";
            this.m_cbVideos.Size = new System.Drawing.Size(58, 17);
            this.m_cbVideos.TabIndex = 3;
            this.m_cbVideos.Text = "Videos";
            this.m_cbVideos.UseVisualStyleBackColor = true;
            this.m_cbVideos.CheckedChanged += new System.EventHandler(this.m_cbContent_CheckedChanged);
            // 
            // m_cbPhotos
            // 
            this.m_cbPhotos.AutoSize = true;
            this.m_cbPhotos.Location = new System.Drawing.Point(49, 42);
            this.m_cbPhotos.Name = "m_cbPhotos";
            this.m_cbPhotos.Size = new System.Drawing.Size(59, 17);
            this.m_cbPhotos.TabIndex = 2;
            this.m_cbPhotos.Text = "Photos";
            this.m_cbPhotos.UseVisualStyleBackColor = true;
            this.m_cbPhotos.CheckedChanged += new System.EventHandler(this.m_cbContent_CheckedChanged);
            // 
            // m_cbMessages
            // 
            this.m_cbMessages.AutoSize = true;
            this.m_cbMessages.Location = new System.Drawing.Point(49, 19);
            this.m_cbMessages.Name = "m_cbMessages";
            this.m_cbMessages.Size = new System.Drawing.Size(74, 17);
            this.m_cbMessages.TabIndex = 1;
            this.m_cbMessages.Text = "Messages";
            this.m_cbMessages.UseVisualStyleBackColor = true;
            this.m_cbMessages.CheckedChanged += new System.EventHandler(this.m_cbContent_CheckedChanged);
            // 
            // m_cbContentAll
            // 
            this.m_cbContentAll.AutoSize = true;
            this.m_cbContentAll.Location = new System.Drawing.Point(6, 19);
            this.m_cbContentAll.Name = "m_cbContentAll";
            this.m_cbContentAll.Size = new System.Drawing.Size(37, 17);
            this.m_cbContentAll.TabIndex = 0;
            this.m_cbContentAll.Text = "All";
            this.m_cbContentAll.UseVisualStyleBackColor = true;
            this.m_cbContentAll.CheckedChanged += new System.EventHandler(this.m_cbContentAll_CheckedChanged);
            // 
            // m_gbExport
            // 
            this.m_gbExport.Controls.Add(this.m_cbHtml);
            this.m_gbExport.Controls.Add(this.m_cbText);
            this.m_gbExport.Controls.Add(this.m_cbExportAll);
            this.m_gbExport.Location = new System.Drawing.Point(567, 12);
            this.m_gbExport.Name = "m_gbExport";
            this.m_gbExport.Size = new System.Drawing.Size(102, 88);
            this.m_gbExport.TabIndex = 13;
            this.m_gbExport.TabStop = false;
            this.m_gbExport.Text = "Export";
            // 
            // m_cbHtml
            // 
            this.m_cbHtml.AutoSize = true;
            this.m_cbHtml.Location = new System.Drawing.Point(49, 42);
            this.m_cbHtml.Name = "m_cbHtml";
            this.m_cbHtml.Size = new System.Drawing.Size(47, 17);
            this.m_cbHtml.TabIndex = 2;
            this.m_cbHtml.Text = "Html";
            this.m_cbHtml.UseVisualStyleBackColor = true;
            this.m_cbHtml.CheckedChanged += new System.EventHandler(this.m_cbExport_CheckedChanged);
            // 
            // m_cbText
            // 
            this.m_cbText.AutoSize = true;
            this.m_cbText.Location = new System.Drawing.Point(49, 19);
            this.m_cbText.Name = "m_cbText";
            this.m_cbText.Size = new System.Drawing.Size(47, 17);
            this.m_cbText.TabIndex = 1;
            this.m_cbText.Text = "Text";
            this.m_cbText.UseVisualStyleBackColor = true;
            this.m_cbText.CheckedChanged += new System.EventHandler(this.m_cbExport_CheckedChanged);
            // 
            // m_cbExportAll
            // 
            this.m_cbExportAll.AutoSize = true;
            this.m_cbExportAll.Location = new System.Drawing.Point(6, 19);
            this.m_cbExportAll.Name = "m_cbExportAll";
            this.m_cbExportAll.Size = new System.Drawing.Size(37, 17);
            this.m_cbExportAll.TabIndex = 0;
            this.m_cbExportAll.Text = "All";
            this.m_cbExportAll.UseVisualStyleBackColor = true;
            this.m_cbExportAll.CheckedChanged += new System.EventHandler(this.m_cbExportAll_CheckedChanged);
            // 
            // FormTLArchiver
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(761, 703);
            this.Controls.Add(this.m_gbExport);
            this.Controls.Add(this.m_gbContent);
            this.Controls.Add(this.m_gbDialogs);
            this.Controls.Add(this.m_dgvDialogs);
            this.Controls.Add(this.m_bExport);
            this.Controls.Add(this.m_dtpTo);
            this.Controls.Add(this.m_cbToDate);
            this.Controls.Add(this.m_dtpFrom);
            this.Controls.Add(this.m_cbFromDate);
            this.Controls.Add(this.m_bLogin);
            this.Controls.Add(this.m_tbCode);
            this.Controls.Add(this.m_lCode);
            this.Controls.Add(this.statusStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormTLArchiver";
            this.Text = "TLArchiveMedia";
            this.Load += new System.EventHandler(this.FormTLArchiveMedia_Load);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_dgvDialogs)).EndInit();
            this.m_gbDialogs.ResumeLayout(false);
            this.m_gbDialogs.PerformLayout();
            this.m_gbContent.ResumeLayout(false);
            this.m_gbContent.PerformLayout();
            this.m_gbExport.ResumeLayout(false);
            this.m_gbExport.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel m_status;
        private System.Windows.Forms.Label m_lCode;
        private System.Windows.Forms.TextBox m_tbCode;
        private System.Windows.Forms.Button m_bLogin;
        private System.Windows.Forms.CheckBox m_cbFromDate;
        private System.Windows.Forms.DateTimePicker m_dtpFrom;
        private System.Windows.Forms.CheckBox m_cbToDate;
        private System.Windows.Forms.DateTimePicker m_dtpTo;
        private System.Windows.Forms.Button m_bExport;
        private System.Windows.Forms.DataGridView m_dgvDialogs;
        private System.Windows.Forms.GroupBox m_gbDialogs;
        private System.Windows.Forms.CheckBox m_cbUsers;
        private System.Windows.Forms.CheckBox m_cbChats;
        private System.Windows.Forms.CheckBox m_cbDialogsAll;
        private System.Windows.Forms.GroupBox m_gbContent;
        private System.Windows.Forms.CheckBox m_cbVoiceMessages;
        private System.Windows.Forms.CheckBox m_cbFiles;
        private System.Windows.Forms.CheckBox m_cbVideos;
        private System.Windows.Forms.CheckBox m_cbPhotos;
        private System.Windows.Forms.CheckBox m_cbMessages;
        private System.Windows.Forms.CheckBox m_cbContentAll;
        private System.Windows.Forms.GroupBox m_gbExport;
        private System.Windows.Forms.CheckBox m_cbHtml;
        private System.Windows.Forms.CheckBox m_cbText;
        private System.Windows.Forms.CheckBox m_cbExportAll;
        private System.Windows.Forms.CheckBox m_cbChannels;
    }
}

