namespace TLArchiver.UI
{
    partial class FormTLSignIn
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormTLSignIn));
            this.m_lCode = new System.Windows.Forms.Label();
            this.m_lPhone = new System.Windows.Forms.Label();
            this.m_tbCode = new System.Windows.Forms.TextBox();
            this.m_tbPhone = new System.Windows.Forms.TextBox();
            this.m_lEnterCode = new System.Windows.Forms.Label();
            this.m_tbEnterCode = new System.Windows.Forms.TextBox();
            this.m_bNext = new System.Windows.Forms.Button();
            this.m_pPhone = new System.Windows.Forms.Panel();
            this.m_pSignIn = new System.Windows.Forms.Panel();
            this.m_bSignIn = new System.Windows.Forms.Button();
            this.m_pPhone.SuspendLayout();
            this.m_pSignIn.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_lCode
            // 
            this.m_lCode.AutoSize = true;
            this.m_lCode.Location = new System.Drawing.Point(0, 0);
            this.m_lCode.Name = "m_lCode";
            this.m_lCode.Size = new System.Drawing.Size(32, 13);
            this.m_lCode.TabIndex = 0;
            this.m_lCode.Text = "Code";
            // 
            // m_lPhone
            // 
            this.m_lPhone.AutoSize = true;
            this.m_lPhone.Location = new System.Drawing.Point(42, 0);
            this.m_lPhone.Name = "m_lPhone";
            this.m_lPhone.Size = new System.Drawing.Size(76, 13);
            this.m_lPhone.TabIndex = 1;
            this.m_lPhone.Text = "Phone number";
            // 
            // m_tbCode
            // 
            this.m_tbCode.Location = new System.Drawing.Point(0, 16);
            this.m_tbCode.Name = "m_tbCode";
            this.m_tbCode.Size = new System.Drawing.Size(39, 20);
            this.m_tbCode.TabIndex = 2;
            // 
            // m_tbPhone
            // 
            this.m_tbPhone.Location = new System.Drawing.Point(45, 16);
            this.m_tbPhone.Name = "m_tbPhone";
            this.m_tbPhone.Size = new System.Drawing.Size(100, 20);
            this.m_tbPhone.TabIndex = 3;
            // 
            // m_lEnterCode
            // 
            this.m_lEnterCode.AutoSize = true;
            this.m_lEnterCode.Location = new System.Drawing.Point(0, 5);
            this.m_lEnterCode.Name = "m_lEnterCode";
            this.m_lEnterCode.Size = new System.Drawing.Size(82, 13);
            this.m_lEnterCode.TabIndex = 4;
            this.m_lEnterCode.Text = "Enter your code";
            // 
            // m_tbEnterCode
            // 
            this.m_tbEnterCode.Location = new System.Drawing.Point(88, 2);
            this.m_tbEnterCode.Name = "m_tbEnterCode";
            this.m_tbEnterCode.Size = new System.Drawing.Size(57, 20);
            this.m_tbEnterCode.TabIndex = 5;
            // 
            // m_bNext
            // 
            this.m_bNext.Location = new System.Drawing.Point(151, 14);
            this.m_bNext.Name = "m_bNext";
            this.m_bNext.Size = new System.Drawing.Size(75, 23);
            this.m_bNext.TabIndex = 6;
            this.m_bNext.Text = "Next";
            this.m_bNext.UseVisualStyleBackColor = true;
            // 
            // m_pPhone
            // 
            this.m_pPhone.Controls.Add(this.m_bNext);
            this.m_pPhone.Controls.Add(this.m_lCode);
            this.m_pPhone.Controls.Add(this.m_lPhone);
            this.m_pPhone.Controls.Add(this.m_tbCode);
            this.m_pPhone.Controls.Add(this.m_tbPhone);
            this.m_pPhone.Location = new System.Drawing.Point(12, 12);
            this.m_pPhone.Name = "m_pPhone";
            this.m_pPhone.Size = new System.Drawing.Size(226, 38);
            this.m_pPhone.TabIndex = 7;
            // 
            // m_pSignIn
            // 
            this.m_pSignIn.Controls.Add(this.m_bSignIn);
            this.m_pSignIn.Controls.Add(this.m_lEnterCode);
            this.m_pSignIn.Controls.Add(this.m_tbEnterCode);
            this.m_pSignIn.Location = new System.Drawing.Point(12, 56);
            this.m_pSignIn.Name = "m_pSignIn";
            this.m_pSignIn.Size = new System.Drawing.Size(226, 23);
            this.m_pSignIn.TabIndex = 8;
            // 
            // m_bSignIn
            // 
            this.m_bSignIn.Location = new System.Drawing.Point(151, 0);
            this.m_bSignIn.Name = "m_bSignIn";
            this.m_bSignIn.Size = new System.Drawing.Size(75, 23);
            this.m_bSignIn.TabIndex = 7;
            this.m_bSignIn.Text = "Sign In";
            this.m_bSignIn.UseVisualStyleBackColor = true;
            // 
            // FormTLSignIn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(250, 91);
            this.Controls.Add(this.m_pSignIn);
            this.Controls.Add(this.m_pPhone);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormTLSignIn";
            this.Text = "Sign In";
            this.Load += new System.EventHandler(this.FormTLSignIn_Load);
            this.m_pPhone.ResumeLayout(false);
            this.m_pPhone.PerformLayout();
            this.m_pSignIn.ResumeLayout(false);
            this.m_pSignIn.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label m_lCode;
        private System.Windows.Forms.Label m_lPhone;
        private System.Windows.Forms.TextBox m_tbCode;
        private System.Windows.Forms.TextBox m_tbPhone;
        private System.Windows.Forms.Label m_lEnterCode;
        private System.Windows.Forms.TextBox m_tbEnterCode;
        private System.Windows.Forms.Button m_bNext;
        private System.Windows.Forms.Panel m_pPhone;
        private System.Windows.Forms.Panel m_pSignIn;
        private System.Windows.Forms.Button m_bSignIn;
    }
}