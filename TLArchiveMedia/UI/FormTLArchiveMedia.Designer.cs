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
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_status});
            this.statusStrip.Location = new System.Drawing.Point(0, 240);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(284, 22);
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
            // FormTLArchiveMedia
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
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
    }
}

