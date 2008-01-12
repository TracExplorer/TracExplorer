#region GPL Licence
/**********************************************************************
 VSTrac - Visual Studo Trac Integration
 Copyright (C) 2008 Mladen Mihajlovic
 http://vstrac.devjavu.com/
 
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
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using CookComputing.XmlRpc;
using System.Net;
using System.Threading;

namespace VSTrac
{
    public partial class TracExplorer : UserControl
    {
        #region Node Types
        private class ServerNode : TreeNode
        {
            private WikiPagesNode nodeWikiPages;
            private TicketsNode nodeTickets;
            private AttributesNode nodeAttributes;

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

            public ServerDetails ServerDetails { get; set; }

            public bool DetailsLoaded { get; set; }

            public void Refresh()
            {
                nodeWikiPages.Refresh();
                //nodeTickets.Refresh();
                nodeAttributes.Refresh();

                DetailsLoaded = true;
            }
        }

        private class WikiPagesNode : TreeNode
        {
            BackgroundWorker bgWorker = new BackgroundWorker();
            ServerDetails serverDetails;

            public WikiPagesNode(ServerDetails serverDetails)
            {
                this.Text = Properties.Resources.WikiPagesNode;
                this.ImageIndex = this.SelectedImageIndex = 2;
                this.serverDetails = serverDetails;
            }

            public void Refresh()
            {
                this.Text = string.Format("{0} ({1})", Properties.Resources.WikiPagesNode, Properties.Resources.NodeWaiting);

                //start thread for adding wiki pages
                bgWorker.DoWork += new DoWorkEventHandler(bgWorker_DoWork);
                bgWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgWorker_RunWorkerCompleted);
                bgWorker.RunWorkerAsync(serverDetails);
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

                    this.Text = string.Format("{0} ({1})", Properties.Resources.WikiPagesNode, wikiPages.Count.ToString());
                }

                if (e.Result is Exception)
                {
                    Exception ex = (Exception)e.Result;
                    this.Text = string.Format("{0} ({1})", Properties.Resources.WikiPagesNode, ex.Message);
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

        private class WikiPageNode : TreeNode
        {
            public WikiPageNode(string wikiPage) : base(wikiPage, 3, 3)
            { }
        }

        private class TicketsNode : TreeNode
        {
            public TicketsNode(ServerDetails serverDetails)
            {
                this.Text = Properties.Resources.TicketsNode;
                this.ImageIndex = this.SelectedImageIndex = 2;

                //TODO: load all ticket queries from registry

                TicketQueryDefinition t = new TicketQueryDefinition();
                t.Name = "All Tickets";
                t.Filter = "id!=-1";

                this.Nodes.Add(new TicketNode(t));

            }
        }

        private class TicketNode : TreeNode
        {
            public TicketNode(TicketQueryDefinition ticketQueryDefinition)
            {
                this.Text = ticketQueryDefinition.Name;
                this.ImageIndex = this.SelectedImageIndex = 3;
            }
        }

        private class AttributesNode : TreeNode
        {
            private ServerDetails serverDetails;
            private BackgroundWorker bgWorker;

            public AttributesNode(ServerDetails serverDetails)
            {
                this.Text = Properties.Resources.AttributesNode;
                this.ImageIndex = this.SelectedImageIndex = 2;
                this.serverDetails = serverDetails;
            }

            public void Refresh()
            {
                this.Text = string.Format("{0} ({1})", Properties.Resources.AttributesNode, Properties.Resources.NodeWaiting);

                bgWorker = new BackgroundWorker();
                bgWorker.DoWork += new DoWorkEventHandler(bgWorker_DoWork);
                bgWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgWorker_RunWorkerCompleted);

                bgWorker.RunWorkerAsync(this.serverDetails);
            }

            void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
            {
                if (e.Result is Exception)
                {
                    Exception ex = (Exception)e.Result;

                    this.Text = string.Format("{0} ({1})", Properties.Resources.AttributesNode, ex.Message);
                    return;
                }

                if (e.Result is object[])
                {
                    this.Text = Properties.Resources.AttributesNode;

                    object[] results = (object[])e.Result;
                    object[] methodResults = (object[])results[0];

                    // Components
                    if (methodResults.Length > 0)
                    {
                        List<string> components = new List<string>((string[])((object[])results[0])[0]);
                        components.Sort();

                        TreeNode nodeComponents = this.Nodes.Add("nodeComponents", string.Format("Components ({0})", components.Count), 3, 3);

                        foreach (string component in components)
                        {
                            nodeComponents.Nodes.Add(component, component, 3, 3);
                        }
                    }
                    else
                        this.Nodes.Add("nodeComponents", "Components (0)", 3, 3);


                    // Milestones
                    methodResults = (object[])results[1];

                    if ( methodResults.Length > 0 && ((object[])methodResults[0]).Length > 0)
                    {
                        List<string> milestones = new List<string>((string[])(object[])methodResults[0]);
                        milestones.Sort();

                        TreeNode nodeMilestones = this.Nodes.Add("nodeMilestones", string.Format("Milestones ({0})", milestones.Count), 3, 3);

                        foreach (string milestone in milestones)
                        {
                            nodeMilestones.Nodes.Add(milestone, milestone, 3, 3);
                        }
                    }
                    else
                        this.Nodes.Add("nodeMilestones", "Milestones (0)", 3, 3);

                }
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
        private Dictionary<Type, TreeNodeMouseClickEventHandler> clickHandlers = new Dictionary<Type, TreeNodeMouseClickEventHandler>();
        #endregion

        #region ctor
        public TracExplorer()
        {
            InitializeComponent();

            // Set handlers
            clickHandlers.Add(typeof(WikiPageNode), WikiPageDoubleClick);

            //TODO: Load servers from registry
            List<ServerDetails> servers = ServerDetails.LoadAll();

            treeView1.BeginUpdate();

            foreach (ServerDetails server in servers)
            {
                ServerNode nodeServer = new ServerNode(server);
                treeView1.Nodes["nodeServers"].Nodes.Add(nodeServer);
            }

            treeView1.Nodes["nodeServers"].Expand();

            treeView1.EndUpdate();
        }
        #endregion

        #region Event Handlers
        private void btnNewTracServer_Click(object sender, EventArgs e)
        {
            AddNewServerForm form = new AddNewServerForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                form.Result.Save();

                treeView1.Nodes["nodeServers"].Nodes.Add(new ServerNode(form.Result));
                
            }
        }

        private void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (clickHandlers.ContainsKey(e.Node.GetType()))
                clickHandlers[e.Node.GetType()](sender, e);
        }
        #endregion

        #region Double Click Handlers
        private void WikiPageDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            WikiPageNode wikiPageNode = e.Node as WikiPageNode;
            ServerNode serverNode = e.Node.Parent.Parent as ServerNode;

            Connect.OpenBrowser(serverNode.ServerDetails.Server + "/wiki/" + wikiPageNode.Text);
        }
        #endregion

        private void treeView1_AfterExpand(object sender, TreeViewEventArgs e)
        {
            if (e.Node is ServerNode)
            {
                ServerNode nodeServer = (ServerNode)e.Node;
                if (!nodeServer.DetailsLoaded)
                    nodeServer.Refresh();
            }
        }
    }
}
