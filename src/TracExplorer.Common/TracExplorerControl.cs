#region GPL Licence
/**********************************************************************
 TracExplorer - Trac Integration for Visual Studio and TortoiseSvn
 Copyright (C) 2008 Mladen Mihajlovic
 http://tracexplorer.devjavu.com/
 
 This program is free software: you can redistribute it and/or modify
 it under the terms of the GNU General Public License as published by
 the Free Software Foundation, either version 3 of the License, or
 (at your option) any later version.
 
 This program is distributed in the hope that it will be useful,
 but WITHOUT ANY WARRANTY; without even the implied warranty of
 MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 GNU General Public License for more details.
 
 You should have received a copy of the GNU General Public License
 along with this program.  If not, see <http://www.gnu.org/licenses/>.
**********************************************************************/
#endregion

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Web;
using System.Windows.Forms;

namespace TracExplorer.Common
{
    [ComVisible(true)]
    [DefaultEvent("TicketQueryClick")]
    public partial class TracExplorerControl : UserControl
    {
        #region Node Types
        private abstract class TracNode : TreeNode
        {
            private ServerDetails serverDetails;

            public TracNode() : base()
            { }

            public TracNode(string text) : base(text)
            { }

            public TracNode(string text, TreeNode[] children) : base(text, children)
            { }

            public TracNode(string text, int imageIndex, int selectedImageIndex) : base(text, imageIndex, selectedImageIndex)
            { }

            public TracNode(string text, int imageIndex, int selectedImageIndex, TreeNode[] children) : base(text, imageIndex, selectedImageIndex, children)
            { }

            public ServerDetails ServerDetails
            {
                get { return this.serverDetails; }
                set { this.serverDetails = value; }
            }

            public virtual void Refresh()
            { }
        }

        private class ServerNode : TracNode
        {
            private WikiPagesNode nodeWikiPages;
            private TicketsNode nodeTickets;
            private AttributesNode nodeAttributes;
            private bool detailsLoaded;

            public ServerNode(ServerDetails serverDetails) : base(serverDetails.Server, 1, 1)
            {
                this.ServerDetails = serverDetails;

                try
                {
                    nodeWikiPages = new WikiPagesNode(serverDetails);
                    nodeTickets = new TicketsNode(serverDetails);
                    nodeAttributes = new AttributesNode(serverDetails);

                    this.Nodes.Add(nodeWikiPages);
                    this.Nodes.Add(nodeTickets);
                    this.Nodes.Add(nodeAttributes);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            public bool DetailsLoaded 
            {
              get { return detailsLoaded; }
              set { detailsLoaded = value; }
            }

            public override void Refresh()
            {
                nodeWikiPages.Refresh();
                nodeTickets.Refresh();
                nodeAttributes.Refresh();

                this.Text = this.ServerDetails.Server;

                DetailsLoaded = true;
            }
        }

        private class WikiPagesNode : TracNode
        {
            private BackgroundWorker bgWorker = new BackgroundWorker();

            public WikiPagesNode(ServerDetails serverDetails)
            {
                this.Text = Properties.Resources.NodeWikiPages;
                this.ImageIndex = this.SelectedImageIndex = 2;
                this.ServerDetails = serverDetails;
            }

            public override void Refresh()
            {
                this.Text = string.Format("{0} ({1})", Properties.Resources.NodeWikiPages, Properties.Resources.NodeWaiting);

                //start thread for adding wiki pages
                bgWorker.DoWork += new DoWorkEventHandler(bgWorker_DoWork);
                bgWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgWorker_RunWorkerCompleted);
                if (!bgWorker.IsBusy)
                {
                    bgWorker.RunWorkerAsync(ServerDetails);
                }
            }

            void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
            {
                this.Nodes.Clear();

                if ( e.Result is List<string> )
                {
                    List<string> wikiPages = (List<string>)e.Result;
                    foreach (string wikiPage in wikiPages)
                    {
                        this.Nodes.Add(new WikiPageNode(wikiPage));
                    }

                    this.Text = string.Format("{0} ({1})", Properties.Resources.NodeWikiPages, wikiPages.Count.ToString());
                }

                if (e.Result is Exception)
                {
                    Exception ex = (Exception)e.Result;
                    this.Text = string.Format("{0} ({1})", Properties.Resources.NodeWikiPages, ex.Message);
                }
            }

            void bgWorker_DoWork(object sender, DoWorkEventArgs e)
            {
                try
                {
                    ITrac trac = TracCommon.GetTrac((ServerDetails)e.Argument);
                    string[] pages = trac.getAllPages();
                    List<string> wikiPages = new List<string>(pages);
                    wikiPages.Sort();
                    e.Result = wikiPages;
                }
                catch (Exception ex)
                {
                    e.Result = ex;
                }
            }
        }

        private class WikiPageNode : TracNode
        {
            public WikiPageNode(string wikiPage) : base(wikiPage, 3, 3)
            { }
        }

        private class TicketsNode : TracNode
        {
            public TicketsNode(ServerDetails serverDetails)
            {
                this.Text = Properties.Resources.NodeTickets;
                this.ImageIndex = this.SelectedImageIndex = 2;
                this.ServerDetails = serverDetails;

                this.Refresh();
            }

            public override void Refresh()
            {
                List<TicketQueryDefinition> ticketQueries = ServerDetails.TicketQueries;

                this.Nodes.Clear();
                foreach (TicketQueryDefinition ticketQuery in ticketQueries)
                {
                    TicketNode node = new TicketNode(ServerDetails, ticketQuery);
                    this.Nodes.Add(node);
                }
            }
        }

        private class TicketNode : TracNode
        {
            public TicketNode(ServerDetails serverDetails, TicketQueryDefinition ticketQueryDefinition)
            {
                this.Text = ticketQueryDefinition.Name;
                this.ImageIndex = this.SelectedImageIndex = 3;

                this.ServerDetails = serverDetails;
                this.TicketDefinition = ticketQueryDefinition;
            }

            private TicketQueryDefinition ticketDefinition;

            public TicketQueryDefinition TicketDefinition
            {
                get { return this.ticketDefinition; }
                set { this.ticketDefinition = value; }
            }

            public override void Refresh()
            {
                this.Text = this.TicketDefinition.Name;
            }
        }

        private class AttributesNode : TracNode
        {
            private BackgroundWorker bgWorker = new BackgroundWorker();

            public AttributesNode(ServerDetails serverDetails)
            {
                this.Text = Properties.Resources.NodeAttributes;
                this.ImageIndex = this.SelectedImageIndex = 2;
                this.ServerDetails = serverDetails;
            }

            public override void Refresh()
            {
                this.Text = string.Format("{0} ({1})", Properties.Resources.NodeAttributes, Properties.Resources.NodeWaiting);

                bgWorker.DoWork += new DoWorkEventHandler(bgWorker_DoWork);
                bgWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgWorker_RunWorkerCompleted);
                if (!bgWorker.IsBusy)
                {
                    bgWorker.RunWorkerAsync(this.ServerDetails);
                }
            }

            void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
            {
                if (e.Result is Exception)
                {
                    Exception ex = (Exception)e.Result;

                    this.Text = string.Format("{0} ({1})", Properties.Resources.NodeAttributes, ex.Message);
                    return;
                }

                if (e.Result is object[])
                {
                    this.Nodes.Clear();
                    this.Text = Properties.Resources.NodeAttributes;

                    object[] results = (object[])e.Result;

                    object[] methodResults = (object[])results[0];
                    AddAttributes(methodResults, "nodeComponents", Properties.Resources.NodeComponents);

                    methodResults = (object[])results[1];
                    AddAttributes(methodResults, "nodeMilestones", Properties.Resources.NodeMilestones);

                    methodResults = (object[])results[2];
                    AddAttributes(methodResults, "nodeSeverities", Properties.Resources.NodeSeverities);

                    methodResults = (object[])results[3];
                    AddAttributes(methodResults, "nodeTicketTypes", Properties.Resources.NodeTicketTypes);

                    methodResults = (object[])results[4];
                    AddAttributes(methodResults, "nodePriorities", Properties.Resources.NodePriorities);

                    methodResults = (object[])results[5];
                    AddAttributes(methodResults, "nodeStatuses", Properties.Resources.NodeStatuses);

                    methodResults = (object[])results[6];
                    AddAttributes(methodResults, "nodeResolutions", Properties.Resources.NodeResolutions);

                    methodResults = (object[])results[7];
                    AddAttributes(methodResults, "nodeVersions", Properties.Resources.NodeVersions);


                }
            }

            private void AddAttributes(object[] methodResults, string nodeName, string nodeText)
            {
                if (methodResults.Length > 0 && ((object[])methodResults[0]).Length > 0)
                {
                    List<string> items = new List<string>((string[])(object[])methodResults[0]);
                    items.Sort();

                    TreeNode node = this.Nodes.Add(nodeName, string.Format("{0} ({1})", nodeText, items.Count), 2, 2);

                    foreach (string item in items)
                    {
                        node.Nodes.Add(item, item, 3, 3);
                    }
                }
                else
                    this.Nodes.Add(nodeName, nodeText + " (0)", 2, 2);
            }

            void bgWorker_DoWork(object sender, DoWorkEventArgs e)
            {
                try
                {
                    ITrac trac = TracCommon.GetTrac((ServerDetails)e.Argument);

                    //TODO: Multicall
                    List<MulticallItem> attributes = new List<MulticallItem>();
                    attributes.Add(new MulticallItem("ticket.component.getAll", new string[] { }));
                    attributes.Add(new MulticallItem("ticket.milestone.getAll", new string[] {}));
                    attributes.Add(new MulticallItem("ticket.severity.getAll", new string[] {}));
                    attributes.Add(new MulticallItem("ticket.type.getAll", new string[] {}));
                    attributes.Add(new MulticallItem("ticket.priority.getAll", new string[] { }));
                    attributes.Add(new MulticallItem("ticket.status.getAll", new string[] { }));
                    attributes.Add(new MulticallItem("ticket.resolution.getAll", new string[] { }));
                    attributes.Add(new MulticallItem("ticket.version.getAll", new string[] { }));

                    object[] result = trac.multicall(attributes.ToArray());

                    e.Result = result;
                }
                catch (Exception ex)
                {
                    e.Result = ex;
                }
            }
        }
        
        #endregion

        #region Private Variables
        private Dictionary<Type, TreeNodeMouseClickEventHandler> doubleClickHandlers = new Dictionary<Type, TreeNodeMouseClickEventHandler>();
        private ITracConnect _tracConnect;
        #endregion

        public delegate void TicketQueryClickEvent(object sender, TicketQueryArgs e);
        public event TicketQueryClickEvent TicketQueryClick;


        #region ctor
        public TracExplorerControl()
        {
            InitializeComponent();

            // Set handlers
            doubleClickHandlers.Add(typeof(WikiPageNode), WikiPageDoubleClick);
            doubleClickHandlers.Add(typeof(TicketNode), TicketDoubleClick);

            CommonRoot servers = CommonRoot.Instance;

            treeTrac.BeginUpdate();

            foreach (ServerDetails server in servers.Servers)
            {
                ServerNode nodeServer = new ServerNode(server);
                treeTrac.Nodes["nodeServers"].Nodes.Add(nodeServer);
            }

            treeTrac.Nodes["nodeServers"].Expand();

            treeTrac.EndUpdate();

            // captions
            btnDelTracServer.Text = btnDelTracServer.ToolTipText = btnServerDelete.Text = Properties.Resources.MenuDelete;
            btnNewTracServer.Text = btnNewTracServer.ToolTipText = btnNewServer.Text = Properties.Resources.MenuNewServer;
            btnServerRefresh.Text = btnServerRefresh.ToolTipText = Properties.Resources.MenuRefresh;
        }
        #endregion

        #region Public Properties
        public ITracConnect TracConnect 
        {
            get { return _tracConnect; }
            set { _tracConnect = value; }
        }
        #endregion

        #region Event Handlers
        private void btnNewTracServer_Click(object sender, EventArgs e)
        {
            AddNewServerForm form = new AddNewServerForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                treeTrac.Nodes["nodeServers"].Nodes.Add(new ServerNode(form.Result));
                
            }
        }

        private void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (doubleClickHandlers.ContainsKey(e.Node.GetType()))
                doubleClickHandlers[e.Node.GetType()](sender, e);
        }

        private void treeView1_AfterExpand(object sender, TreeViewEventArgs e)
        {
            if (e.Node is ServerNode)
            {
                ServerNode nodeServer = (ServerNode)e.Node;
                if (!nodeServer.DetailsLoaded)
                    nodeServer.Refresh();
            }
        }

        private void treeTrac_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            btnDelTracServer.Enabled = (e.Node is ServerNode);

            if (e.Button == MouseButtons.Right)
            {
                treeTrac.SelectedNode = e.Node;

                if (e.Node is ServerNode)
                {
                    ctmServer.Show(treeTrac, e.X, e.Y);
                    return;
                }

                if (e.Node is TicketsNode)
                {
                    ctmTickets.Show(treeTrac, e.X, e.Y);
                    return;
                }

                if (e.Node is TicketNode)
                {
                    ctmTicket.Show(treeTrac, e.X, e.Y);
                    return;
                }
            }
            else if (e.Button == MouseButtons.Left)
            {
                if (e.Node is TicketNode)
                {
                    TicketNode ticketNode = e.Node as TicketNode;
                    if (TicketQueryClick != null)
                    {
                        TicketQueryClick(this, new TicketQueryArgs(ticketNode.ServerDetails, ticketNode.TicketDefinition));
                    }
                    return;
                }
            }

        }

        private void btnDelTracServer_Click(object sender, EventArgs e)
        {
            ServerNode node = treeTrac.SelectedNode as ServerNode;

            if (MessageBox.Show(string.Format(Properties.Resources.ConfirmDelTracServer, node.ServerDetails.Server), Properties.Resources.CaptionConfirmDelete, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                CommonRoot.Instance.Servers.Remove(node.ServerDetails);
                CommonRoot.SaveInstance();
                node.Remove();
            }
        }

        private void btnServerRefresh_Click(object sender, EventArgs e)
        {
            ServerNode node = treeTrac.SelectedNode as ServerNode;

            node.Refresh();
        }

        private void btnNewTicketQuery_Click(object sender, EventArgs e)
        {
            TicketsNode node = treeTrac.SelectedNode as TicketsNode;

            AddNewTicketQueryForm form = new AddNewTicketQueryForm(this._tracConnect);

            if (form.ShowDialog() == DialogResult.OK)
            {
                TicketQueryDefinition ticketQueryDef = form.TicketDefinition;
                node.ServerDetails.TicketQueries.Add(ticketQueryDef);
                CommonRoot.SaveInstance();

                node.Refresh();
            }
        }

        private void btnDelQuery_Click(object sender, EventArgs e)
        {
            TicketNode node = treeTrac.SelectedNode as TicketNode;
            node.ServerDetails.TicketQueries.Remove(node.TicketDefinition);
            CommonRoot.SaveInstance();
            node.Remove();
        }

        private void btnQueryOpen_Click(object sender, EventArgs e)
        {
            TicketNode node = treeTrac.SelectedNode as TicketNode;

            TracConnect.CreateTicketWindow(node.ServerDetails, node.TicketDefinition, null);
        }
        #endregion

        #region Double Click Handlers
        private void WikiPageDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            WikiPageNode wikiPageNode = e.Node as WikiPageNode;
            ServerNode serverNode = e.Node.Parent.Parent as ServerNode;

            string UrlEncoded = HttpUtility.UrlPathEncode(wikiPageNode.Text);
            TracConnect.OpenBrowser(serverNode.ServerDetails.WikiPageUrl(UrlEncoded));
        }

        private void TicketDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            try
            {
                TicketNode ticketNode = e.Node as TicketNode;
                TracConnect.CreateTicketWindow(ticketNode.ServerDetails, ticketNode.TicketDefinition, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion


        private void mnuQueryEdit_Click(object sender, EventArgs e)
        {
            TicketNode node = treeTrac.SelectedNode as TicketNode;

            AddNewTicketQueryForm form = new AddNewTicketQueryForm(this._tracConnect,node.TicketDefinition);

            if (form.ShowDialog() == DialogResult.OK)
            {
                CommonRoot.SaveInstance();

                node.Refresh();
            }
        }

        private void mnuServerEdit_Click(object sender, EventArgs e)
        {
            ServerNode node = treeTrac.SelectedNode as ServerNode;

            AddNewServerForm form = new AddNewServerForm(node.ServerDetails);
            if (form.ShowDialog() == DialogResult.OK)
            {
                node.Refresh();
            }
        }
    }
}
