namespace VSTrac
{
    partial class TracExplorer
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Favourites");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Trac Servers");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TracExplorer));
            this.ctmServers = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.btnNewServer = new System.Windows.Forms.ToolStripMenuItem();
            this.treeTrac = new System.Windows.Forms.TreeView();
            this.imageSmall = new System.Windows.Forms.ImageList(this.components);
            this.ctmServer = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.btnServerRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.btnServerDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnNewTracServer = new System.Windows.Forms.ToolStripButton();
            this.btnDelTracServer = new System.Windows.Forms.ToolStripButton();
            this.ctmTickets = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.btnNewTicketQuery = new System.Windows.Forms.ToolStripMenuItem();
            this.ctmTicket = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.btnQueryOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.btnDelQuery = new System.Windows.Forms.ToolStripMenuItem();
            this.ctmServers.SuspendLayout();
            this.ctmServer.SuspendLayout();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.ctmTickets.SuspendLayout();
            this.ctmTicket.SuspendLayout();
            this.SuspendLayout();
            // 
            // ctmServers
            // 
            this.ctmServers.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNewServer});
            this.ctmServers.Name = "ctmServers";
            this.ctmServers.Size = new System.Drawing.Size(160, 26);
            // 
            // btnNewServer
            // 
            this.btnNewServer.Image = global::VSTrac.Properties.Resources.server_new;
            this.btnNewServer.Name = "btnNewServer";
            this.btnNewServer.Size = new System.Drawing.Size(159, 22);
            this.btnNewServer.Text = "New Trac Server";
            this.btnNewServer.Click += new System.EventHandler(this.btnNewTracServer_Click);
            // 
            // treeTrac
            // 
            this.treeTrac.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeTrac.ImageIndex = 0;
            this.treeTrac.ImageList = this.imageSmall;
            this.treeTrac.Location = new System.Drawing.Point(0, 0);
            this.treeTrac.Name = "treeTrac";
            treeNode1.Name = "nodeFavourites";
            treeNode1.Text = "Favourites";
            treeNode2.ContextMenuStrip = this.ctmServers;
            treeNode2.Name = "nodeServers";
            treeNode2.Text = "Trac Servers";
            this.treeTrac.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2});
            this.treeTrac.SelectedImageIndex = 0;
            this.treeTrac.Size = new System.Drawing.Size(263, 302);
            this.treeTrac.TabIndex = 0;
            this.treeTrac.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseDoubleClick);
            this.treeTrac.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeTrac_NodeMouseClick);
            this.treeTrac.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterExpand);
            // 
            // imageSmall
            // 
            this.imageSmall.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageSmall.ImageStream")));
            this.imageSmall.TransparentColor = System.Drawing.Color.Magenta;
            this.imageSmall.Images.SetKeyName(0, "server.png");
            this.imageSmall.Images.SetKeyName(1, "trac.png");
            this.imageSmall.Images.SetKeyName(2, "VSFolder_closed.bmp");
            this.imageSmall.Images.SetKeyName(3, "VSProject_genericfile.bmp");
            // 
            // ctmServer
            // 
            this.ctmServer.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnServerRefresh,
            this.btnServerDelete});
            this.ctmServer.Name = "contextMenuStrip1";
            this.ctmServer.Size = new System.Drawing.Size(118, 48);
            // 
            // btnServerRefresh
            // 
            this.btnServerRefresh.Image = global::VSTrac.Properties.Resources.refresh;
            this.btnServerRefresh.ImageTransparentColor = System.Drawing.Color.Fuchsia;
            this.btnServerRefresh.Name = "btnServerRefresh";
            this.btnServerRefresh.Size = new System.Drawing.Size(117, 22);
            this.btnServerRefresh.Text = "Refresh";
            this.btnServerRefresh.Click += new System.EventHandler(this.btnServerRefresh_Click);
            // 
            // btnServerDelete
            // 
            this.btnServerDelete.Image = global::VSTrac.Properties.Resources.delete;
            this.btnServerDelete.ImageTransparentColor = System.Drawing.Color.Fuchsia;
            this.btnServerDelete.Name = "btnServerDelete";
            this.btnServerDelete.Size = new System.Drawing.Size(117, 22);
            this.btnServerDelete.Text = "Remove";
            this.btnServerDelete.Click += new System.EventHandler(this.btnDelTracServer_Click);
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.treeTrac);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(263, 302);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(263, 327);
            this.toolStripContainer1.TabIndex = 1;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip1);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNewTracServer,
            this.btnDelTracServer});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(263, 25);
            this.toolStrip1.Stretch = true;
            this.toolStrip1.TabIndex = 0;
            // 
            // btnNewTracServer
            // 
            this.btnNewTracServer.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNewTracServer.Image = global::VSTrac.Properties.Resources.server_new;
            this.btnNewTracServer.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNewTracServer.Name = "btnNewTracServer";
            this.btnNewTracServer.Size = new System.Drawing.Size(23, 22);
            this.btnNewTracServer.Text = "Add new Trac server";
            this.btnNewTracServer.Click += new System.EventHandler(this.btnNewTracServer_Click);
            // 
            // btnDelTracServer
            // 
            this.btnDelTracServer.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnDelTracServer.Enabled = false;
            this.btnDelTracServer.Image = global::VSTrac.Properties.Resources.delete;
            this.btnDelTracServer.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDelTracServer.Name = "btnDelTracServer";
            this.btnDelTracServer.Size = new System.Drawing.Size(23, 22);
            this.btnDelTracServer.Text = "Remove Trac server";
            this.btnDelTracServer.Click += new System.EventHandler(this.btnDelTracServer_Click);
            // 
            // ctmTickets
            // 
            this.ctmTickets.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNewTicketQuery});
            this.ctmTickets.Name = "ctmTickets";
            this.ctmTickets.Size = new System.Drawing.Size(169, 26);
            // 
            // btnNewTicketQuery
            // 
            this.btnNewTicketQuery.Name = "btnNewTicketQuery";
            this.btnNewTicketQuery.Size = new System.Drawing.Size(168, 22);
            this.btnNewTicketQuery.Text = "New Ticket Query";
            this.btnNewTicketQuery.Click += new System.EventHandler(this.btnNewTicketQuery_Click);
            // 
            // ctmTicket
            // 
            this.ctmTicket.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnQueryOpen,
            this.btnDelQuery});
            this.ctmTicket.Name = "ctmTicket";
            this.ctmTicket.Size = new System.Drawing.Size(118, 48);
            // 
            // btnQueryOpen
            // 
            this.btnQueryOpen.Name = "btnQueryOpen";
            this.btnQueryOpen.Size = new System.Drawing.Size(117, 22);
            this.btnQueryOpen.Text = "Open";
            this.btnQueryOpen.Click += new System.EventHandler(this.btnQueryOpen_Click);
            // 
            // btnDelQuery
            // 
            this.btnDelQuery.Image = global::VSTrac.Properties.Resources.delete;
            this.btnDelQuery.ImageTransparentColor = System.Drawing.Color.Fuchsia;
            this.btnDelQuery.Name = "btnDelQuery";
            this.btnDelQuery.Size = new System.Drawing.Size(117, 22);
            this.btnDelQuery.Text = "Remove";
            this.btnDelQuery.Click += new System.EventHandler(this.btnDelQuery_Click);
            // 
            // TracExplorer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.toolStripContainer1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "TracExplorer";
            this.Size = new System.Drawing.Size(263, 327);
            this.ctmServers.ResumeLayout(false);
            this.ctmServer.ResumeLayout(false);
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ctmTickets.ResumeLayout(false);
            this.ctmTicket.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treeTrac;
        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ImageList imageSmall;
        private System.Windows.Forms.ToolStripButton btnNewTracServer;
        private System.Windows.Forms.ContextMenuStrip ctmServer;
        private System.Windows.Forms.ToolStripMenuItem btnServerRefresh;
        private System.Windows.Forms.ToolStripMenuItem btnServerDelete;
        private System.Windows.Forms.ToolStripButton btnDelTracServer;
        private System.Windows.Forms.ContextMenuStrip ctmServers;
        private System.Windows.Forms.ToolStripMenuItem btnNewServer;
        private System.Windows.Forms.ContextMenuStrip ctmTickets;
        private System.Windows.Forms.ToolStripMenuItem btnNewTicketQuery;
        private System.Windows.Forms.ContextMenuStrip ctmTicket;
        private System.Windows.Forms.ToolStripMenuItem btnDelQuery;
        private System.Windows.Forms.ToolStripMenuItem btnQueryOpen;
    }
}
