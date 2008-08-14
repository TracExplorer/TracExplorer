namespace TracExplorer.TSVNTrac
{
    partial class TracExplorerForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TracExplorerForm));
            this.tracExplorerControl = new TracExplorer.Common.TracExplorerControl();
            this.SuspendLayout();
            // 
            // tracExplorerControl
            // 
            this.tracExplorerControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tracExplorerControl.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tracExplorerControl.Location = new System.Drawing.Point(0, 0);
            this.tracExplorerControl.Name = "tracExplorerControl";
            this.tracExplorerControl.Size = new System.Drawing.Size(411, 328);
            this.tracExplorerControl.TabIndex = 0;
            this.tracExplorerControl.TracConnect = null;
            // 
            // TracExplorerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(411, 327);
            this.Controls.Add(this.tracExplorerControl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TracExplorerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Please select ticket query";
            this.ResumeLayout(false);

        }

        #endregion

        private TracExplorer.Common.TracExplorerControl tracExplorerControl;
    }
}