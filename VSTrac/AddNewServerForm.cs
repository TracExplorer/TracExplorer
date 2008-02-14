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
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CookComputing.XmlRpc;
using System.Net;
using System.Security.Cryptography.X509Certificates;

namespace VSTrac
{
    public partial class AddNewServerForm : Form
    {
        private ServerDetails result;

        public AddNewServerForm()
        {
            InitializeComponent();
        }

        public ServerDetails Result 
        {
          get { return result; }
          set { result = value; }
        }

        /// <summary>
        /// Return a <see cref="ServerDetails"/> object filled with the form data.
        /// </summary>
        /// <returns></returns>
        private ServerDetails GetServerDetails()
        {
            ServerDetails details = new ServerDetails();

            string tempServer = txtServer.Text.Trim();

            if (!tempServer.Contains(":"))
                tempServer = "http://" + tempServer;

            if (!tempServer.EndsWith("/"))
                tempServer += "/";
                
            Uri uriServer= new Uri(tempServer);

            if (uriServer.LocalPath.Contains("login/xmlrpc"))
                throw new ApplicationException("Please do not include the login/xmlrpc path. The basic trac path is sufficient.");

            details.Server = uriServer.ToString();
            details.Authenticated = chkAuthentication.Checked;
            details.Username = txtUsername.Text;
            details.Password = txtPassword.Text;

            return details;
        }

        private object CheckTracServer(ServerDetails details)
        {
            ITrac trac = TracCommon.GetTrac(details);

            object[] version = trac.getAPIVersion();

            return version;
        }

        /// <summary>
        /// Basically this is the is_valid method. If anything is not valid, the next button is disabled.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ControlChangedEvent(object sender, EventArgs e)
        {
            bool canMoveNext = true;

            groupBox1.Enabled = chkAuthentication.Checked;

            if (txtServer.Text.Trim().Length == 0)
                canMoveNext = false;

            if (chkAuthentication.Checked && txtUsername.Text.Trim().Length == 0)
                canMoveNext = false;

            if (chkAuthentication.Checked && txtPassword.Text.Trim().Length == 0)
                canMoveNext = false;

            wizard1.NextEnabled = canMoveNext;
        }

        private void wizard1_CloseFromCancel(object sender, CancelEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Start the background thread and disable all control while it works
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void wizardPage2_ShowFromNext(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            wizard1.NextEnabled = wizard1.BackEnabled = wizard1.CancelEnabled = false;
            pictureBox1.Visible = true;

            lblStatus.Text = "Checking...";
            lblError.Text = "";

            ServerDetails details = GetServerDetails();

            bgwCheckServer.RunWorkerAsync(details);
        }

        private void AddNewServerForm_Load(object sender, EventArgs e)
        {
            wizard1.NextEnabled = false;
        }

        private void bgwCheckServer_DoWork(object sender, DoWorkEventArgs e)
        {
            ServerDetails details = (ServerDetails)e.Argument;

            try
            {
                CheckTracServer(details);
            }
            catch (Exception ex)
            {
                e.Result = ex;
                return;
            }

            e.Result = true;
        }

        /// <summary>
        /// Check the result. If exception report as error, otherwise report as success and enable controls.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bgwCheckServer_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Result is Exception)
            {
                Exception ex = (Exception)e.Result;
                lblError.Text = ex.Message;
            }
            else
            {
                wizard1.NextEnabled = true;
                lblError.Text = "Successfully connected to Trac server.";
            }

            lblStatus.Text = "Checking done.";
            wizard1.BackEnabled = wizard1.CancelEnabled = true;
            pictureBox1.Visible = false;
            this.Cursor = Cursors.Default;
        }

        /// <summary>
        /// Add a list of default query definitions which can be chosen and added to the server node immediately
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void wizardPage3_ShowFromNext(object sender, EventArgs e)
        {
            List<TicketQueryDefinition> queries = new List<TicketQueryDefinition>();
            ServerDetails details = GetServerDetails();

            queries.Add(new TicketQueryDefinition("Active Tickets", "status!=closed", details));
            queries.Add(new TicketQueryDefinition("Active Tasks", "type=task&status!=closed", details));
            queries.Add(new TicketQueryDefinition("All Tickets", "status!=non_existant_status", details));
            queries.Add(new TicketQueryDefinition("New Tickets", "status=new", details));
            
            if ( details.Authenticated )
            {
                queries.Add(new TicketQueryDefinition("My Active Tickets", "status!=closed&owner="+details.Username, details));
                queries.Add(new TicketQueryDefinition("My Active Tasks", "type=task&status!=closed&owner=" + details.Username, details));
                queries.Add(new TicketQueryDefinition("Tickets Reported By Me", "reporter=" + details.Username, details));
            }

            lstTicketQueries.BeginUpdate();
            lstTicketQueries.Items.Clear();
            lstTicketQueries.Items.AddRange(queries.ToArray());
            lstTicketQueries.EndUpdate();
        }

        private void wizardPage3_CloseFromNext(object sender, Gui.Wizard.PageEventArgs e)
        {
            //do last bit here
            ServerDetails details = GetServerDetails();
            details.Save();

            foreach (TicketQueryDefinition query in lstTicketQueries.CheckedItems)
            {
                query.Save();
            }

            this.result = GetServerDetails();
        }
    }
}
