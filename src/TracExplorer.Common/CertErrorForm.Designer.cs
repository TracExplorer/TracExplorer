namespace TracExplorer.VSTrac
{
    partial class CertErrorForm
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
            this.lblUrlName = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnReject = new System.Windows.Forms.Button();
            this.btnAccTemp = new System.Windows.Forms.Button();
            this.btnAccPerm = new System.Windows.Forms.Button();
            this.lblMessage1 = new System.Windows.Forms.Label();
            this.lblFingerprint = new System.Windows.Forms.Label();
            this.lblDistName = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblUrlName
            // 
            this.lblUrlName.AutoSize = true;
            this.lblUrlName.Location = new System.Drawing.Point(66, 9);
            this.lblUrlName.Name = "lblUrlName";
            this.lblUrlName.Size = new System.Drawing.Size(190, 13);
            this.lblUrlName.TabIndex = 0;
            this.lblUrlName.Text = "Error validating server certificate for {0}";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::TracExplorer.Common.Properties.Resources.error;
            this.pictureBox1.Location = new System.Drawing.Point(12, 39);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(48, 48);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // btnReject
            // 
            this.btnReject.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReject.Location = new System.Drawing.Point(363, 104);
            this.btnReject.Name = "btnReject";
            this.btnReject.Size = new System.Drawing.Size(75, 23);
            this.btnReject.TabIndex = 2;
            this.btnReject.Text = "Reject";
            this.btnReject.UseVisualStyleBackColor = true;
            this.btnReject.Click += new System.EventHandler(this.btnReject_Click);
            // 
            // btnAccTemp
            // 
            this.btnAccTemp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAccTemp.Location = new System.Drawing.Point(254, 104);
            this.btnAccTemp.Name = "btnAccTemp";
            this.btnAccTemp.Size = new System.Drawing.Size(103, 23);
            this.btnAccTemp.TabIndex = 3;
            this.btnAccTemp.Text = "Accept Once";
            this.btnAccTemp.UseVisualStyleBackColor = true;
            this.btnAccTemp.Click += new System.EventHandler(this.btnAccTemp_Click);
            // 
            // btnAccPerm
            // 
            this.btnAccPerm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAccPerm.Location = new System.Drawing.Point(112, 104);
            this.btnAccPerm.Name = "btnAccPerm";
            this.btnAccPerm.Size = new System.Drawing.Size(136, 23);
            this.btnAccPerm.TabIndex = 4;
            this.btnAccPerm.Text = "Accept Permanently";
            this.btnAccPerm.UseVisualStyleBackColor = true;
            this.btnAccPerm.Click += new System.EventHandler(this.btnAccPerm_Click);
            // 
            // lblMessage1
            // 
            this.lblMessage1.AutoSize = true;
            this.lblMessage1.Location = new System.Drawing.Point(66, 22);
            this.lblMessage1.Name = "lblMessage1";
            this.lblMessage1.Size = new System.Drawing.Size(83, 13);
            this.lblMessage1.TabIndex = 5;
            this.lblMessage1.Text = "Error message 1";
            // 
            // lblFingerprint
            // 
            this.lblFingerprint.AutoSize = true;
            this.lblFingerprint.Location = new System.Drawing.Point(66, 35);
            this.lblFingerprint.Name = "lblFingerprint";
            this.lblFingerprint.Size = new System.Drawing.Size(56, 13);
            this.lblFingerprint.TabIndex = 6;
            this.lblFingerprint.Text = "Fingerprint";
            // 
            // lblDistName
            // 
            this.lblDistName.AutoSize = true;
            this.lblDistName.Location = new System.Drawing.Point(66, 48);
            this.lblDistName.Name = "lblDistName";
            this.lblDistName.Size = new System.Drawing.Size(118, 13);
            this.lblDistName.TabIndex = 7;
            this.lblDistName.Text = "Distinguished Name {0}";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(66, 74);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(127, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "Do you want to proceed?";
            // 
            // CertErrorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(450, 139);
            this.ControlBox = false;
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lblDistName);
            this.Controls.Add(this.lblFingerprint);
            this.Controls.Add(this.lblMessage1);
            this.Controls.Add(this.btnAccPerm);
            this.Controls.Add(this.btnAccTemp);
            this.Controls.Add(this.btnReject);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lblUrlName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CertErrorForm";
            this.Text = "Certification Error";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblUrlName;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnReject;
        private System.Windows.Forms.Button btnAccTemp;
        private System.Windows.Forms.Button btnAccPerm;
        private System.Windows.Forms.Label lblMessage1;
        private System.Windows.Forms.Label lblFingerprint;
        private System.Windows.Forms.Label lblDistName;
        private System.Windows.Forms.Label label6;
    }
}