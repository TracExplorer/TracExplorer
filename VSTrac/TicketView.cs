﻿#region GPL Licence
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
using System.Security.Cryptography.X509Certificates;
using Microsoft.VisualStudio.Shell.Interop;

namespace VSTrac
{
    public partial class TicketView : UserControl
    {
        List<Ticket> allTickets = new List<Ticket>();

        public TicketView()
        {
            InitializeComponent();
        }

        public Connect VSTracConnect { get; set; }
        public ServerDetails ServerDetails { get; set; }
        public TicketQueryDefinition TicketDefinition { get; set; }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RunQuery();
        }

        public void RunQuery()
        {
            lblResults.Text = "Refreshing...";
            bgwTickets.RunWorkerAsync();
        }

        private void OpenInBrowser(int id)
        {
            try
            {
                VSTracConnect.OpenBrowser(ServerDetails.Server + "/ticket/" + id);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bgwTickets_DoWork(object sender, DoWorkEventArgs e)
        {
            ServicePointManager.ServerCertificateValidationCallback = delegate(object certsender, X509Certificate cert, X509Chain chain, System.Net.Security.SslPolicyErrors error)
            {
                if (MessageBox.Show(null, "There was an error reading the site's certificate. Do you want to continue?\r\n" + cert.ToString() + "\r\n\r\n" + error.ToString(), "Error with SSL certificate.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    return true;
                }
                else
                    return false;
            };

            try
            {

                //TODO: Create a trac instance.
                ITrac trac = TracCommon.GetTrac(ServerDetails);

                int[] tickets = trac.queryTickets(TicketDefinition.Filter);
                List<MulticallItem> ticketItems = new List<MulticallItem>();

                foreach (int id in tickets)
                    ticketItems.Add(new MulticallItem("ticket.get", new string[] { id.ToString() }));

                object[] results = trac.multicall(ticketItems.ToArray());

                // convert
                List<Ticket> ticketArr = new List<Ticket>();

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
                    t.Reporter = (string)attributes["reported"];
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

                    t.Icon = string.IsNullOrEmpty(t.Resolution) ? Properties.Resources.newticket : Properties.Resources.closedticket;

                    ticketArr.Add(t);
                }

                e.Result = ticketArr;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private DateTime ParseDate(object value)
        {
            if (value is DateTime)
                return (DateTime)value;
            else
                return new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds((int)value);
        }

        private void bgwTickets_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Result != null)
            {
                allTickets = e.Result as List<Ticket>;
                dataGridView1.AutoGenerateColumns = false;
                dataGridView1.DataSource = allTickets;
                lblResults.Text = string.Format("{0} Tickets returned", allTickets.Count);
            }
            else
                lblResults.Text = "Error refreshing. Please check server connection and try again.";
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Ticket ticket = (Ticket)dataGridView1.Rows[e.RowIndex].DataBoundItem;
            OpenInBrowser(ticket.Id);
        }

        private void dataGridView1_CellContextMenuStripNeeded(object sender, DataGridViewCellContextMenuStripNeededEventArgs e)
        {
        }
    }
}