namespace TracExplorer.TSVNTrac
{
    partial class AddNewProviderForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddNewProviderForm));
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.textFormat = new System.Windows.Forms.TextBox();
            this.wizard1 = new Gui.Wizard.Wizard();
            this.wizardPage2 = new Gui.Wizard.WizardPage();
            this.lblFormat = new System.Windows.Forms.Label();
            this.chkSelectAll = new System.Windows.Forms.CheckBox();
            this.lstSelectionStatus = new System.Windows.Forms.CheckedListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.wizardPage1 = new Gui.Wizard.WizardPage();
            this.label1 = new System.Windows.Forms.Label();
            this.tracExplorerControl = new TracExplorer.Common.TracExplorerControl();
            this.wizard1.SuspendLayout();
            this.wizardPage2.SuspendLayout();
            this.wizardPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // textFormat
            // 
            this.textFormat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textFormat.Location = new System.Drawing.Point(140, 256);
            this.textFormat.Name = "textFormat";
            this.textFormat.Size = new System.Drawing.Size(135, 21);
            this.textFormat.TabIndex = 6;
            this.toolTip1.SetToolTip(this.textFormat, "Use the following values as placeholder: {0} Name, {1} Id, {2} Description");
            // 
            // wizard1
            // 
            this.wizard1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.wizard1.Controls.Add(this.wizardPage2);
            this.wizard1.Controls.Add(this.wizardPage1);
            this.wizard1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.wizard1.Location = new System.Drawing.Point(0, -1);
            this.wizard1.Name = "wizard1";
            this.wizard1.Pages.AddRange(new Gui.Wizard.WizardPage[] {
            this.wizardPage1,
            this.wizardPage2});
            this.wizard1.Size = new System.Drawing.Size(411, 328);
            this.wizard1.TabIndex = 1;
            this.wizard1.Load += new System.EventHandler(this.wizard1_Load);
            // 
            // wizardPage2
            // 
            this.wizardPage2.Controls.Add(this.lblFormat);
            this.wizardPage2.Controls.Add(this.textFormat);
            this.wizardPage2.Controls.Add(this.chkSelectAll);
            this.wizardPage2.Controls.Add(this.lstSelectionStatus);
            this.wizardPage2.Controls.Add(this.label4);
            this.wizardPage2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wizardPage2.IsFinishPage = false;
            this.wizardPage2.Location = new System.Drawing.Point(0, 0);
            this.wizardPage2.Name = "wizardPage2";
            this.wizardPage2.Size = new System.Drawing.Size(411, 280);
            this.wizardPage2.TabIndex = 2;
            this.wizardPage2.CloseFromNext += new Gui.Wizard.PageEventHandler(this.wizardPage2_CloseFromNext);
            this.wizardPage2.ShowFromNext += new System.EventHandler(this.wizardPage2_ShowFromNext);
            // 
            // lblFormat
            // 
            this.lblFormat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblFormat.AutoSize = true;
            this.lblFormat.Location = new System.Drawing.Point(13, 259);
            this.lblFormat.Name = "lblFormat";
            this.lblFormat.Size = new System.Drawing.Size(41, 13);
            this.lblFormat.TabIndex = 7;
            this.lblFormat.Text = "Format";
            // 
            // chkSelectAll
            // 
            this.chkSelectAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkSelectAll.AutoSize = true;
            this.chkSelectAll.Location = new System.Drawing.Point(16, 237);
            this.chkSelectAll.Name = "chkSelectAll";
            this.chkSelectAll.Size = new System.Drawing.Size(118, 17);
            this.chkSelectAll.TabIndex = 5;
            this.chkSelectAll.Text = "Select / deselect all";
            this.chkSelectAll.UseVisualStyleBackColor = true;
            this.chkSelectAll.CheckedChanged += new System.EventHandler(this.chkSelectAll_CheckedChanged);
            // 
            // lstSelectionStatus
            // 
            this.lstSelectionStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lstSelectionStatus.FormattingEnabled = true;
            this.lstSelectionStatus.Location = new System.Drawing.Point(12, 51);
            this.lstSelectionStatus.Name = "lstSelectionStatus";
            this.lstSelectionStatus.Size = new System.Drawing.Size(387, 180);
            this.lstSelectionStatus.TabIndex = 3;
            this.lstSelectionStatus.SelectedValueChanged += new System.EventHandler(this.lstSelectionStatus_SelectedValueChanged);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.Location = new System.Drawing.Point(13, 18);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(386, 30);
            this.label4.TabIndex = 4;
            this.label4.Text = "Please choose selection status which you want to be able to select in the ticket " +
                "queries.";
            // 
            // wizardPage1
            // 
            this.wizardPage1.Controls.Add(this.label1);
            this.wizardPage1.Controls.Add(this.tracExplorerControl);
            this.wizardPage1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wizardPage1.IsFinishPage = false;
            this.wizardPage1.Location = new System.Drawing.Point(0, 0);
            this.wizardPage1.Name = "wizardPage1";
            this.wizardPage1.Size = new System.Drawing.Size(411, 280);
            this.wizardPage1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(129, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Please select ticket query";
            // 
            // tracExplorerControl
            // 
            this.tracExplorerControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tracExplorerControl.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tracExplorerControl.Location = new System.Drawing.Point(3, 24);
            this.tracExplorerControl.Name = "tracExplorerControl";
            this.tracExplorerControl.Size = new System.Drawing.Size(405, 253);
            this.tracExplorerControl.TabIndex = 2;
            this.tracExplorerControl.TracConnect = null;
            this.tracExplorerControl.TicketQueryClick += new TracExplorer.Common.TracExplorerControl.TicketQueryClickEvent(this.tracExplorerControl_TicketQueryClick);
            // 
            // AddNewProviderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(411, 327);
            this.Controls.Add(this.wizard1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(419, 358);
            this.Name = "AddNewProviderForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add new Trac provider";
            this.wizard1.ResumeLayout(false);
            this.wizardPage2.ResumeLayout(false);
            this.wizardPage2.PerformLayout();
            this.wizardPage1.ResumeLayout(false);
            this.wizardPage1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Gui.Wizard.Wizard wizard1;
        private Gui.Wizard.WizardPage wizardPage2;
        private Gui.Wizard.WizardPage wizardPage1;
        private TracExplorer.Common.TracExplorerControl tracExplorerControl;
        private System.Windows.Forms.CheckBox chkSelectAll;
        private System.Windows.Forms.CheckedListBox lstSelectionStatus;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblFormat;
        private System.Windows.Forms.TextBox textFormat;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}