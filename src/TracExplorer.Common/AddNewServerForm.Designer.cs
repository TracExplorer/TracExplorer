namespace TracExplorer.Common
{
    partial class AddNewServerForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddNewServerForm));
            this.wizard1 = new Gui.Wizard.Wizard();
            this.wizardPage1 = new Gui.Wizard.WizardPage();
            this.rdoAuth_ClientCert = new System.Windows.Forms.RadioButton();
            this.rdoAuth_Basic = new System.Windows.Forms.RadioButton();
            this.rdoAuth_Integrated = new System.Windows.Forms.RadioButton();
            this.rdoAuth_None = new System.Windows.Forms.RadioButton();
            this.grpBoxAuth = new System.Windows.Forms.GroupBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtServer = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.wizardPage3 = new Gui.Wizard.WizardPage();
            this.chkSelectAll = new System.Windows.Forms.CheckBox();
            this.lstTicketQueries = new System.Windows.Forms.CheckedListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.wizardPage2 = new Gui.Wizard.WizardPage();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblError = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.wizard1.SuspendLayout();
            this.wizardPage1.SuspendLayout();
            this.grpBoxAuth.SuspendLayout();
            this.wizardPage3.SuspendLayout();
            this.wizardPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // wizard1
            // 
            this.wizard1.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.wizard1.Controls.Add(this.wizardPage1);
            this.wizard1.Controls.Add(this.wizardPage3);
            this.wizard1.Controls.Add(this.wizardPage2);
            this.wizard1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wizard1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.wizard1.Location = new System.Drawing.Point(0, 0);
            this.wizard1.Name = "wizard1";
            this.wizard1.Pages.AddRange(new Gui.Wizard.WizardPage[] {
            this.wizardPage1,
            this.wizardPage2,
            this.wizardPage3});
            this.wizard1.Size = new System.Drawing.Size(411, 312);
            this.wizard1.TabIndex = 0;
            this.wizard1.CloseFromCancel += new System.ComponentModel.CancelEventHandler(this.wizard1_CloseFromCancel);
            // 
            // wizardPage1
            // 
            this.wizardPage1.Controls.Add(this.rdoAuth_ClientCert);
            this.wizardPage1.Controls.Add(this.rdoAuth_Basic);
            this.wizardPage1.Controls.Add(this.rdoAuth_Integrated);
            this.wizardPage1.Controls.Add(this.rdoAuth_None);
            this.wizardPage1.Controls.Add(this.grpBoxAuth);
            this.wizardPage1.Controls.Add(this.txtServer);
            this.wizardPage1.Controls.Add(this.label1);
            this.wizardPage1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wizardPage1.IsFinishPage = false;
            this.wizardPage1.Location = new System.Drawing.Point(0, 0);
            this.wizardPage1.Name = "wizardPage1";
            this.wizardPage1.Size = new System.Drawing.Size(411, 264);
            this.wizardPage1.TabIndex = 1;
            // 
            // rdoAuth_ClientCert
            // 
            this.rdoAuth_ClientCert.AutoSize = true;
            this.rdoAuth_ClientCert.Location = new System.Drawing.Point(12, 128);
            this.rdoAuth_ClientCert.Name = "rdoAuth_ClientCert";
            this.rdoAuth_ClientCert.Size = new System.Drawing.Size(286, 17);
            this.rdoAuth_ClientCert.TabIndex = 7;
            this.rdoAuth_ClientCert.Text = "SSL Client Cert Authentication (user name=cert name)";
            this.rdoAuth_ClientCert.UseVisualStyleBackColor = true;
            this.rdoAuth_ClientCert.CheckedChanged += new System.EventHandler(this.rdoAuth_ClientCert_CheckedChanged);
            // 
            // rdoAuth_Basic
            // 
            this.rdoAuth_Basic.AutoSize = true;
            this.rdoAuth_Basic.Location = new System.Drawing.Point(12, 104);
            this.rdoAuth_Basic.Name = "rdoAuth_Basic";
            this.rdoAuth_Basic.Size = new System.Drawing.Size(122, 17);
            this.rdoAuth_Basic.TabIndex = 6;
            this.rdoAuth_Basic.Text = "Basic Authentication";
            this.rdoAuth_Basic.UseVisualStyleBackColor = true;
            this.rdoAuth_Basic.CheckedChanged += new System.EventHandler(this.rdoAuth_Basic_CheckedChanged);
            // 
            // rdoAuth_Integrated
            // 
            this.rdoAuth_Integrated.AutoSize = true;
            this.rdoAuth_Integrated.Location = new System.Drawing.Point(12, 80);
            this.rdoAuth_Integrated.Name = "rdoAuth_Integrated";
            this.rdoAuth_Integrated.Size = new System.Drawing.Size(150, 17);
            this.rdoAuth_Integrated.TabIndex = 5;
            this.rdoAuth_Integrated.Text = "Integrated Authentication";
            this.rdoAuth_Integrated.UseVisualStyleBackColor = true;
            this.rdoAuth_Integrated.CheckedChanged += new System.EventHandler(this.rdoAuth_Integrated_CheckedChanged);
            // 
            // rdoAuth_None
            // 
            this.rdoAuth_None.AutoSize = true;
            this.rdoAuth_None.Checked = true;
            this.rdoAuth_None.Location = new System.Drawing.Point(12, 55);
            this.rdoAuth_None.Name = "rdoAuth_None";
            this.rdoAuth_None.Size = new System.Drawing.Size(128, 17);
            this.rdoAuth_None.TabIndex = 4;
            this.rdoAuth_None.TabStop = true;
            this.rdoAuth_None.Text = "No Authentication";
            this.rdoAuth_None.UseVisualStyleBackColor = true;
            this.rdoAuth_None.CheckedChanged += new System.EventHandler(this.rdoAuth_None_CheckedChanged);
            // 
            // grpBoxAuth
            // 
            this.grpBoxAuth.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grpBoxAuth.Controls.Add(this.txtPassword);
            this.grpBoxAuth.Controls.Add(this.label3);
            this.grpBoxAuth.Controls.Add(this.txtUsername);
            this.grpBoxAuth.Controls.Add(this.label2);
            this.grpBoxAuth.Enabled = false;
            this.grpBoxAuth.Location = new System.Drawing.Point(15, 153);
            this.grpBoxAuth.Name = "grpBoxAuth";
            this.grpBoxAuth.Size = new System.Drawing.Size(384, 103);
            this.grpBoxAuth.TabIndex = 3;
            this.grpBoxAuth.TabStop = false;
            this.grpBoxAuth.Text = "Authentication";
            // 
            // txtPassword
            // 
            this.txtPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPassword.Location = new System.Drawing.Point(6, 71);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(372, 21);
            this.txtPassword.TabIndex = 3;
            this.txtPassword.UseSystemPasswordChar = true;
            this.txtPassword.TextChanged += new System.EventHandler(this.ControlChangedEvent);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 55);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Password";
            // 
            // txtUsername
            // 
            this.txtUsername.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtUsername.Location = new System.Drawing.Point(6, 32);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(372, 21);
            this.txtUsername.TabIndex = 1;
            this.txtUsername.TextChanged += new System.EventHandler(this.ControlChangedEvent);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Username:";
            // 
            // txtServer
            // 
            this.txtServer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtServer.Location = new System.Drawing.Point(12, 25);
            this.txtServer.Name = "txtServer";
            this.txtServer.Size = new System.Drawing.Size(387, 21);
            this.txtServer.TabIndex = 1;
            this.txtServer.TextChanged += new System.EventHandler(this.ControlChangedEvent);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Trac URL";
            // 
            // wizardPage3
            // 
            this.wizardPage3.Controls.Add(this.chkSelectAll);
            this.wizardPage3.Controls.Add(this.lstTicketQueries);
            this.wizardPage3.Controls.Add(this.label4);
            this.wizardPage3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wizardPage3.IsFinishPage = true;
            this.wizardPage3.Location = new System.Drawing.Point(0, 0);
            this.wizardPage3.Name = "wizardPage3";
            this.wizardPage3.Size = new System.Drawing.Size(411, 264);
            this.wizardPage3.TabIndex = 3;
            this.wizardPage3.CloseFromNext += new Gui.Wizard.PageEventHandler(this.wizardPage3_CloseFromNext);
            this.wizardPage3.ShowFromNext += new System.EventHandler(this.wizardPage3_ShowFromNext);
            // 
            // chkSelectAll
            // 
            this.chkSelectAll.AutoSize = true;
            this.chkSelectAll.Location = new System.Drawing.Point(16, 239);
            this.chkSelectAll.Name = "chkSelectAll";
            this.chkSelectAll.Size = new System.Drawing.Size(118, 17);
            this.chkSelectAll.TabIndex = 2;
            this.chkSelectAll.Text = "Select / deselect all";
            this.chkSelectAll.UseVisualStyleBackColor = true;
            this.chkSelectAll.CheckedChanged += new System.EventHandler(this.chkSelectAll_CheckedChanged);
            // 
            // lstTicketQueries
            // 
            this.lstTicketQueries.CheckOnClick = true;
            this.lstTicketQueries.FormattingEnabled = true;
            this.lstTicketQueries.Location = new System.Drawing.Point(12, 69);
            this.lstTicketQueries.Name = "lstTicketQueries";
            this.lstTicketQueries.Size = new System.Drawing.Size(387, 148);
            this.lstTicketQueries.TabIndex = 0;
            this.lstTicketQueries.SelectedValueChanged += new System.EventHandler(this.lstTicketQueries_SelectedValueChanged);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(13, 13);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(386, 62);
            this.label4.TabIndex = 1;
            this.label4.Text = "Please select which ticket queries you would like automatically added for this se" +
                "rver. You can add more yourself once the server has been added the the Trac Expl" +
                "orer.";
            // 
            // wizardPage2
            // 
            this.wizardPage2.Controls.Add(this.pictureBox1);
            this.wizardPage2.Controls.Add(this.lblError);
            this.wizardPage2.Controls.Add(this.lblStatus);
            this.wizardPage2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wizardPage2.IsFinishPage = false;
            this.wizardPage2.Location = new System.Drawing.Point(0, 0);
            this.wizardPage2.Name = "wizardPage2";
            this.wizardPage2.Size = new System.Drawing.Size(411, 264);
            this.wizardPage2.TabIndex = 2;
            this.wizardPage2.ShowFromNext += new System.EventHandler(this.wizardPage2_ShowFromNext);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::TracExplorer.Common.Properties.Resources.download_FTP_00;
            this.pictureBox1.Location = new System.Drawing.Point(62, 34);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(272, 60);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // lblError
            // 
            this.lblError.Location = new System.Drawing.Point(12, 34);
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(387, 217);
            this.lblError.TabIndex = 1;
            this.lblError.Text = "ErrorText if any...";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(12, 9);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(62, 13);
            this.lblStatus.TabIndex = 0;
            this.lblStatus.Text = "Checking...";
            // 
            // AddNewServerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(411, 312);
            this.ControlBox = false;
            this.Controls.Add(this.wizard1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AddNewServerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add New Trac Server";
            this.Load += new System.EventHandler(this.AddNewServerForm_Load);
            this.wizard1.ResumeLayout(false);
            this.wizardPage1.ResumeLayout(false);
            this.wizardPage1.PerformLayout();
            this.grpBoxAuth.ResumeLayout(false);
            this.grpBoxAuth.PerformLayout();
            this.wizardPage3.ResumeLayout(false);
            this.wizardPage3.PerformLayout();
            this.wizardPage2.ResumeLayout(false);
            this.wizardPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtServer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox grpBoxAuth;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.TextBox txtPassword;
        private Gui.Wizard.Wizard wizard1;
        private Gui.Wizard.WizardPage wizardPage1;
        private Gui.Wizard.WizardPage wizardPage2;
        private System.Windows.Forms.Label lblError;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.PictureBox pictureBox1;
        private Gui.Wizard.WizardPage wizardPage3;
        private System.Windows.Forms.CheckedListBox lstTicketQueries;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox chkSelectAll;
        private System.Windows.Forms.RadioButton rdoAuth_Basic;
        private System.Windows.Forms.RadioButton rdoAuth_Integrated;
        private System.Windows.Forms.RadioButton rdoAuth_None;
        private System.Windows.Forms.RadioButton rdoAuth_ClientCert;
    }
}