namespace TLArchiveMedia
{
    partial class FormTLArchiveMedia
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormTLArchiveMedia));
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.m_status = new System.Windows.Forms.ToolStripStatusLabel();
            this.m_lCode = new System.Windows.Forms.Label();
            this.m_tbCode = new System.Windows.Forms.TextBox();
            this.m_bLogin = new System.Windows.Forms.Button();
            this.m_cbFromDate = new System.Windows.Forms.CheckBox();
            this.m_dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.m_cbToDate = new System.Windows.Forms.CheckBox();
            this.m_dtpTo = new System.Windows.Forms.DateTimePicker();
            this.m_lTalk = new System.Windows.Forms.Label();
            this.m_cbTalk = new System.Windows.Forms.ComboBox();
            this.m_bExport = new System.Windows.Forms.Button();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_status});
            this.statusStrip.Location = new System.Drawing.Point(0, 151);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(258, 22);
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
            // m_lTalk
            // 
            this.m_lTalk.AutoSize = true;
            this.m_lTalk.Location = new System.Drawing.Point(12, 95);
            this.m_lTalk.Name = "m_lTalk";
            this.m_lTalk.Size = new System.Drawing.Size(28, 13);
            this.m_lTalk.TabIndex = 8;
            this.m_lTalk.Text = "Talk";
            // 
            // m_cbTalk
            // 
            this.m_cbTalk.FormattingEnabled = true;
            this.m_cbTalk.Location = new System.Drawing.Point(50, 92);
            this.m_cbTalk.Name = "m_cbTalk";
            this.m_cbTalk.Size = new System.Drawing.Size(121, 21);
            this.m_cbTalk.TabIndex = 9;
            // 
            // m_bExport
            // 
            this.m_bExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_bExport.Location = new System.Drawing.Point(171, 125);
            this.m_bExport.Name = "m_bExport";
            this.m_bExport.Size = new System.Drawing.Size(75, 23);
            this.m_bExport.TabIndex = 10;
            this.m_bExport.Text = "Export";
            this.m_bExport.UseVisualStyleBackColor = true;
            this.m_bExport.Click += new System.EventHandler(this.m_bExport_Click);
            // 
            // FormTLArchiveMedia
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(258, 173);
            this.Controls.Add(this.m_bExport);
            this.Controls.Add(this.m_cbTalk);
            this.Controls.Add(this.m_lTalk);
            this.Controls.Add(this.m_dtpTo);
            this.Controls.Add(this.m_cbToDate);
            this.Controls.Add(this.m_dtpFrom);
            this.Controls.Add(this.m_cbFromDate);
            this.Controls.Add(this.m_bLogin);
            this.Controls.Add(this.m_tbCode);
            this.Controls.Add(this.m_lCode);
            this.Controls.Add(this.statusStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormTLArchiveMedia";
            this.Text = "TLArchiveMedia";
            this.Load += new System.EventHandler(this.FormTLArchiveMedia_Load);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
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
        private System.Windows.Forms.Label m_lTalk;
        private System.Windows.Forms.ComboBox m_cbTalk;
        private System.Windows.Forms.Button m_bExport;
    }
}

