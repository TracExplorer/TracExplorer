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
using System.Windows.Forms;
using CookComputing.XmlRpc;
using TracExplorer.Common.Properties;

namespace TracExplorer.Common
{

    [ComVisible(true)]
    [DefaultEvent("TicketQueryUpdate")]
    public partial class TicketView : UserControl
    {
        #region Private Variables
        private SortableBindingList<Ticket> allTickets = new SortableBindingList<Ticket>();
        private SortableBindingList<Ticket> searchTickets = new SortableBindingList<Ticket>();
        private ITracConnect _tracConnect;
        private ServerDetails serverDetails;
        private TicketQueryDefinition ticketDefinition;
        private SortableBindingList<string> selectionItems;
        private object parentWindow;
        #endregion

        public delegate void TicketQueryUpdateEvent(object sender, TicketQueryArgs e);
        public event TicketQueryUpdateEvent TicketQueryUpdate;

        #region ctor
        public TicketView()
        {
            InitializeComponent();
        } 
        #endregion

        #region Public Properties
        public ITracConnect TracConnect
        {
            get { return _tracConnect; }
            set { _tracConnect = value; }
        }

        public ServerDetails ServerDetails
        {
            get { return serverDetails; }
            set { serverDetails = value; }
        }

        public TicketQueryDefinition TicketDefinition
        {
            get { return ticketDefinition; }
            set { ticketDefinition = value; }
        }

        public SortableBindingList<string> SelectionItems
        {
            get { return selectionItems; }
            set { selectionItems = value; }
        }

        public object ParentWindow
        {
            get { return parentWindow; }
            set { parentWindow = value; }
        }
        #endregion

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RunQuery();
        }

        private void InitSelection()
        {
            if (SelectionItems != null)
            {
                colSelection.DataSource = SelectionItems;
                colSelection.Visible = true;
            }
        }

        private void InitTicketQueries()
        {
            if (ServerDetails != null)
            {
                cmbTicketQuery.Items.Clear();

                foreach (TicketQueryDefinition item in ServerDetails.TicketQueries)
                {
                    cmbTicketQuery.Items.Add(item.Name);
                }
            }
        }
        
        public void RunQuery()
        {
            InitTicketQueries();
            InitSelection();

            cmbServer.ToolTipText = cmbServer.Text = serverDetails.ToString();
            cmbTicketQuery.ToolTipText = cmbTicketQuery.Text = TicketDefinition.ToString();

            lblResults.Text = "Refreshing...";
            cmbTicketQuery.Enabled = false;
            searchLabel.Enabled = false;
            searchTextBox.Enabled = false;
            searchTextBox.Text = "";
            if (!bgwTickets.IsBusy) 
 	        {
                bgwTickets.RunWorkerAsync();
            }
        }

        public List<Ticket> SelectedTickets()
        {
            List<Ticket> TicketList = new List<Ticket>();

            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                Ticket ticket = (Ticket)dataGridView1.Rows[i].DataBoundItem;

                if (ticket.Selection != null && ticket.Selection.Length != 0)
                {
                    TicketList.Add(ticket);
                }
            }
            return TicketList;
        }

        private void OpenInBrowser(int id)
        {
            try
            {
                if (TracConnect != null)
                {
                    TracConnect.OpenBrowser(ServerDetails.TicketUrl(id));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bgwTickets_DoWork(object sender, DoWorkEventArgs e)
        {

            try
            {
                ITrac trac = TracCommon.GetTrac(ServerDetails);

                List<int> tickets = new List<int>();

                try
                {
                    for (int i = 1; i < 1000; i++)///TODO: replace 1000 by number of tickets/100
                        tickets.AddRange(trac.queryTickets(string.Format("{0}&page={1}", TicketDefinition.Filter, i)));
                }
                catch (XmlRpcFaultException)
                {
                    //There are no more pages left to fetch   
                }

                List<MulticallItem> ticketItems = new List<MulticallItem>();

                foreach (int id in tickets)
                    ticketItems.Add(new MulticallItem("ticket.get", new string[] { id.ToString() }));

                object[] results = trac.multicall(ticketItems.ToArray());

                // convert
                SortableBindingList<Ticket> ticketArr = new SortableBindingList<Ticket>();

                foreach (object[] result in results)
                {
                    object[] items = result[0] as object[];
                    XmlRpcStruct attributes = items[3] as XmlRpcStruct;

                    Ticket t = new Ticket();

                    t.Id = int.Parse(items[0].ToString());
                    t.Created = ParseDate(items[1]);
                    t.LastModified = ParseDate(items[2]);

                    t.Status = (string)attributes["status"];
                    t.Description = (string)attributes["description"];
                    t.Reporter = (string)attributes["reporter"];
                    t.CC = (string)attributes["cc"];
                    t.Resolution = (string)attributes["resolution"];
                    t.Component = (string)attributes["component"];
                    t.Summary = (string)attributes["summary"];
                    t.Priority = (string)attributes["priority"];
                    t.Keywords = (string)attributes["keywords"];
                    t.Version = (string)attributes["version"];
                    t.Milestone = (string)attributes["milestone"];
                    t.Owner = (string)attributes["owner"];
                    t.TicketType = (string)attributes["type"];
                    t.Severity = (string)attributes["severity"];

                    t.Icon = string.IsNullOrEmpty(t.Resolution) ? Resources.newticket : Resources.closedticket;

                    ticketArr.Add(t);
                }

                e.Result = ticketArr;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Parse the date in value which could be a unix timestamp or already 
        /// a DateTime value depending on the version of XmlRpcPlugin the trac
        /// site is using.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private DateTime ParseDate(object value)
        {
            if (value is DateTime)
                return (DateTime)value;
            else
                return new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds((int)value); // convert from unit timestamp
        }

        private void bgwTickets_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Result != null)
            {
                allTickets = e.Result as SortableBindingList<Ticket>;
                dataGridView1.AutoGenerateColumns = false;
                dataGridView1.DataSource = allTickets;

                lblResults.Text = string.Format("{0} Tickets returned", allTickets.Count);
            }
            else
            {
                lblResults.Text = "Error refreshing. Please check server connection and try again.";
            }

            cmbTicketQuery.Enabled = true;
            searchLabel.Enabled = true;
            searchTextBox.Enabled = true;
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                Ticket ticket = (Ticket)dataGridView1.Rows[e.RowIndex].DataBoundItem;
                OpenInBrowser(ticket.Id);
            }
        }

        private void cmbTicketQuery_SelectedIndexChanged(object sender, EventArgs e)
        {
            TicketQueryDefinition ticketQueryDef;

            ticketQueryDef = serverDetails.TicketQueries.Find(delegate(TicketQueryDefinition obj) { return (obj.Name == cmbTicketQuery.SelectedItem.ToString()); });

            // Check if query is valid
            if ((ticketQueryDef != null) && (ticketDefinition != ticketQueryDef))
            {
                ticketDefinition = ticketQueryDef;

                if (TicketQueryUpdate != null)
                {
                    TicketQueryUpdate(this, new TicketQueryArgs(ServerDetails, ticketQueryDef));
                }

                RunQuery();
            }
        }

        private void searchTextBox_TextChanged(object sender, EventArgs e)
        {
            string searchText = searchTextBox.Text;
            searchText = searchText.ToLowerInvariant();

            if (searchText == "")
            {
                dataGridView1.DataSource = allTickets;
                lblResults.Text = string.Format("{0} Tickets returned", allTickets.Count);
            }
            else
            {
                searchTickets.Clear();
                foreach (Ticket ticket in allTickets)
                {
                    if (ticket.Summary.ToLowerInvariant().Contains(searchText))
                    {
                        searchTickets.Add(ticket);
                    }
                }
                dataGridView1.DataSource = searchTickets;
                lblResults.Text = string.Format("{0} from {1} Tickets filtered", searchTickets.Count, allTickets.Count);
            }
        }
    }
}
