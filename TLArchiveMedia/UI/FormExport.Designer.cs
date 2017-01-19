namespace TLArchiveMedia.UI
{
    partial class FormExport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormExport));
            this.m_pbDialogs = new System.Windows.Forms.ProgressBar();
            this.m_pbMessages = new System.Windows.Forms.ProgressBar();
            this.m_lDialogs = new System.Windows.Forms.Label();
            this.m_lMessages = new System.Windows.Forms.Label();
            this.m_status = new System.Windows.Forms.StatusStrip();
            this.m_lStatusDialog = new System.Windows.Forms.ToolStripStatusLabel();
            this.m_lStatusCurrent = new System.Windows.Forms.ToolStripStatusLabel();
            this.m_lStatusTotal = new System.Windows.Forms.ToolStripStatusLabel();
            this.m_bAbort = new System.Windows.Forms.Button();
            this.m_bOk = new System.Windows.Forms.Button();
            this.m_status.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_pbDialogs
            // 
            this.m_pbDialogs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_pbDialogs.Location = new System.Drawing.Point(73, 12);
            this.m_pbDialogs.Name = "m_pbDialogs";
            this.m_pbDialogs.Size = new System.Drawing.Size(199, 23);
            this.m_pbDialogs.TabIndex = 1;
            // 
            // m_pbMessages
            // 
            this.m_pbMessages.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_pbMessages.Location = new System.Drawing.Point(73, 41);
            this.m_pbMessages.Name = "m_pbMessages";
            this.m_pbMessages.Size = new System.Drawing.Size(199, 23);
            this.m_pbMessages.TabIndex = 3;
            // 
            // m_lDialogs
            // 
            this.m_lDialogs.AutoSize = true;
            this.m_lDialogs.Location = new System.Drawing.Point(12, 19);
            this.m_lDialogs.Name = "m_lDialogs";
            this.m_lDialogs.Size = new System.Drawing.Size(42, 13);
            this.m_lDialogs.TabIndex = 0;
            this.m_lDialogs.Text = "Dialogs";
            // 
            // m_lMessages
            // 
            this.m_lMessages.AutoSize = true;
            this.m_lMessages.Location = new System.Drawing.Point(12, 48);
            this.m_lMessages.Name = "m_lMessages";
            this.m_lMessages.Size = new System.Drawing.Size(55, 13);
            this.m_lMessages.TabIndex = 2;
            this.m_lMessages.Text = "Messages";
            // 
            // m_status
            // 
            this.m_status.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_lStatusDialog,
            this.m_lStatusCurrent,
            this.m_lStatusTotal});
            this.m_status.Location = new System.Drawing.Point(0, 96);
            this.m_status.Name = "m_status";
            this.m_status.Size = new System.Drawing.Size(284, 22);
            this.m_status.TabIndex = 6;
            this.m_status.Text = "statusStrip";
            // 
            // m_lStatusDialog
            // 
            this.m_lStatusDialog.Name = "m_lStatusDialog";
            this.m_lStatusDialog.Size = new System.Drawing.Size(73, 17);
            this.m_lStatusDialog.Text = "DialogName";
            // 
            // m_lStatusCurrent
            // 
            this.m_lStatusCurrent.Name = "m_lStatusCurrent";
            this.m_lStatusCurrent.Size = new System.Drawing.Size(93, 17);
            this.m_lStatusCurrent.Text = "CurrentMessage";
            // 
            // m_lStatusTotal
            // 
            this.m_lStatusTotal.Name = "m_lStatusTotal";
            this.m_lStatusTotal.Size = new System.Drawing.Size(80, 17);
            this.m_lStatusTotal.Text = "TotalMessage";
            // 
            // m_bAbort
            // 
            this.m_bAbort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_bAbort.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_bAbort.Location = new System.Drawing.Point(197, 70);
            this.m_bAbort.Name = "m_bAbort";
            this.m_bAbort.Size = new System.Drawing.Size(75, 23);
            this.m_bAbort.TabIndex = 5;
            this.m_bAbort.Text = "Abort";
            this.m_bAbort.UseVisualStyleBackColor = true;
            this.m_bAbort.Click += new System.EventHandler(this.m_bAbort_Click);
            // 
            // m_bOk
            // 
            this.m_bOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_bOk.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_bOk.Location = new System.Drawing.Point(116, 70);
            this.m_bOk.Name = "m_bOk";
            this.m_bOk.Size = new System.Drawing.Size(75, 23);
            this.m_bOk.TabIndex = 4;
            this.m_bOk.Text = "OK";
            this.m_bOk.UseVisualStyleBackColor = true;
            // 
            // FormExport
            // 
            this.AcceptButton = this.m_bOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.m_bAbort;
            this.ClientSize = new System.Drawing.Size(284, 118);
            this.Controls.Add(this.m_bOk);
            this.Controls.Add(this.m_bAbort);
            this.Controls.Add(this.m_status);
            this.Controls.Add(this.m_lMessages);
            this.Controls.Add(this.m_lDialogs);
            this.Controls.Add(this.m_pbMessages);
            this.Controls.Add(this.m_pbDialogs);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormExport";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Export";
            this.Load += new System.EventHandler(this.FormExport_Load);
            this.Shown += new System.EventHandler(this.FormExport_Shown);
            this.m_status.ResumeLayout(false);
            this.m_status.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar m_pbDialogs;
        private System.Windows.Forms.ProgressBar m_pbMessages;
        private System.Windows.Forms.Label m_lDialogs;
        private System.Windows.Forms.Label m_lMessages;
        private System.Windows.Forms.StatusStrip m_status;
        private System.Windows.Forms.Button m_bAbort;
        private System.Windows.Forms.ToolStripStatusLabel m_lStatusDialog;
        private System.Windows.Forms.ToolStripStatusLabel m_lStatusCurrent;
        private System.Windows.Forms.ToolStripStatusLabel m_lStatusTotal;
        private System.Windows.Forms.Button m_bOk;
    }
}