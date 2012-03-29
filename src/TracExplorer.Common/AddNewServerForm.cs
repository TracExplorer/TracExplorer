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
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CookComputing.XmlRpc;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using TracExplorer.Common;
using System.Xml.Serialization;
using System.IO;

namespace TracExplorer.Common
{
    public partial class AddNewServerForm : Form
    {
        private ServerDetails _server;
        private bool _editMode = false;
        private MemoryStream _memoryStream = new MemoryStream();

        public AddNewServerForm()
        {
            _server = new ServerDetails();
            InitializeComponent();
        }

        public AddNewServerForm(ServerDetails server)
        {
            _editMode = true;
            _server = server;

            XmlSerializer s = new XmlSerializer(typeof(ServerDetails));
            s.Serialize(_memoryStream, server);

            InitializeComponent();
        }

        public ServerDetails Result 
        {
          get { return _server; }
          set { _server = value; }
        }

        /// <summary>
        /// Return a <see cref="ServerDetails"/> object filled with the form data.
        /// </summary>
        /// <returns></returns>
        private void UpdateServerDetails()
        {
            string tempServer = txtServer.Text.Trim();

            if (!tempServer.Contains(":"))
                tempServer = "http://" + tempServer;

            if (!tempServer.EndsWith("/"))
                tempServer += "/";
                
            Uri uriServer= new Uri(tempServer);

            if (uriServer.LocalPath.Contains("login/xmlrpc")) //TODO: Put in resource file...
            {
                MessageBox.Show("Please do not include the login/xmlrpc path. The basic trac path is sufficient.");
            }
            _server.Server = uriServer.ToString();
            _server.RequiredAuthentication = SelectedAuthType();
            _server.Username = txtUsername.Text;
            _server.Password = txtPassword.Text;
        }

        public AuthenticationTypes SelectedAuthType()
        {
            AuthenticationTypes output = AuthenticationTypes.None;

            if (rdoAuth_Basic.Checked)
            {
                output = AuthenticationTypes.BasicAuthentication;
            }
            else if (rdoAuth_Integrated.Checked)
            {
                output = AuthenticationTypes.IntegratedAuthentication;
            }
            else if (rdoAuth_ClientCert.Checked)
            {
                output = AuthenticationTypes.ClientCertAuthentication;
            }
            else if (rdoAuth_None.Checked)
            {
                output = AuthenticationTypes.IntegratedAuthentication;
            }
            else
            {
                throw new ApplicationException("Unknown authentication type selected, which should be impossible!");
            }

            return output;
        }

        /// <summary>
        /// Basically this is the is_valid method. If anything is not valid, the next button is disabled.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ControlChangedEvent(object sender, EventArgs e)
        {
            bool canMoveNext = true;

            grpBoxAuth.Enabled = rdoAuth_Basic.Checked || rdoAuth_ClientCert.Checked;
            txtPassword.Enabled = rdoAuth_Basic.Checked;

            if (txtServer.Text.Trim().Length == 0)
                canMoveNext = false;

            if (rdoAuth_Basic.Checked && txtUsername.Text.Trim().Length == 0)
                canMoveNext = false;

            if (rdoAuth_Basic.Checked && txtPassword.Text.Trim().Length == 0)
                canMoveNext = false;

            if (rdoAuth_ClientCert.Checked && txtUsername.Text.Trim().Length == 0)
                canMoveNext = false;

            wizard1.NextEnabled = canMoveNext;
        }

        private void wizard1_CloseFromCancel(object sender, CancelEventArgs e)
        {
            if (_editMode)
            {
                // Reload values from memory
                XmlSerializer s = new XmlSerializer(typeof(ServerDetails));
                _memoryStream.Position = 0;
                _server = (ServerDetails)s.Deserialize(_memoryStream);
            }
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

            lblStatus.Text = Properties.Resources.LabelChecking;
            lblError.Text = string.Empty;

            UpdateServerDetails();

            ITrac trac = TracCommon.GetTrac(_server);

            IAsyncResult asr = trac.BeginGetAPIVersion();

            while (asr.IsCompleted == false)
            {
                Application.DoEvents();
            }

            try
            {
                object[] ret = trac.EndGetAPIVersion(asr);
                lblStatus.Text = Properties.Resources.LabelCheckingSuccess;
            }
            catch (Exception ex)
            {
                lblStatus.Text = Properties.Resources.LabelCheckingFailure;
                lblError.Text = ex.Message;
            }

            wizard1.BackEnabled = wizard1.CancelEnabled = true;
            wizard1.NextEnabled = ( lblError.Text == string.Empty );

            pictureBox1.Visible = false;
            this.Cursor = Cursors.Default;

        }

        private void AddNewServerForm_Load(object sender, EventArgs e)
        {
            if (_server != null) 
            {
                this.Text = Properties.Resources.CaptionEditServer; 
                txtServer.Text = _server.Server; 
                txtUsername.Text = _server.Username; 
                txtPassword.Text = _server.Password; 
                switch (_server.RequiredAuthentication)
                {
                    case AuthenticationTypes.BasicAuthentication:
                        rdoAuth_Basic.Checked = true;
                        break;
                    case AuthenticationTypes.IntegratedAuthentication:
                        rdoAuth_Integrated.Checked = true;
                        break;
                    case AuthenticationTypes.ClientCertAuthentication:
                        rdoAuth_ClientCert.Checked = true;
                        break;
                    case AuthenticationTypes.None:
                        rdoAuth_None.Checked = true;
                        break;
                }
 
                wizard1.NextEnabled = true; 
            } 
            else 
            { 
                wizard1.NextEnabled = false; 
            } 
        }


        /// <summary>
        /// Add a list of default query definitions which can be chosen and added to the server node immediately
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void wizardPage3_ShowFromNext(object sender, EventArgs e)
        {
            List<TicketQueryDefinition> queries = new List<TicketQueryDefinition> ();
            
            //TODO: Put these labels in resource file...
            AddOnlyNewQuery(queries, new TicketQueryDefinition("Active Tickets", "status!=closed"));
            AddOnlyNewQuery(queries, new TicketQueryDefinition("Active Tasks", "type=task&status!=closed"));
            AddOnlyNewQuery(queries, new TicketQueryDefinition("All Tickets", "status!=non_existant_status"));
            AddOnlyNewQuery(queries,new TicketQueryDefinition("New Tickets", "status=new"));
            
            if ( _server.RequiredAuthentication != AuthenticationTypes.None )
            {
                AddOnlyNewQuery(queries, new TicketQueryDefinition("My Active Tickets", "status!=closed&owner=" + _server.Username));
                AddOnlyNewQuery(queries, new TicketQueryDefinition("My Active Tasks", "type=task&status!=closed&owner=" + _server.Username));
                AddOnlyNewQuery(queries, new TicketQueryDefinition("Tickets Reported By Me", "reporter=" + _server.Username));
            }

            lstTicketQueries.BeginUpdate();
            lstTicketQueries.Items.Clear();
            lstTicketQueries.Items.AddRange(_server.TicketQueries.ToArray());
             
            for (int i = 0; i < lstTicketQueries.Items.Count; i++)
            {
                lstTicketQueries.SetItemChecked(i,true);
            }
            lstTicketQueries.Items.AddRange(queries.ToArray());
            lstTicketQueries.EndUpdate();
        }

        private void AddOnlyNewQuery(List<TicketQueryDefinition> queries, TicketQueryDefinition newQuery)
        {
            TicketQueryDefinition ticketQuery = _server.TicketQueries.Find(delegate(TicketQueryDefinition obj) { return (obj.Name == newQuery.Name); });

            if (ticketQuery == null)
            {
                queries.Add(newQuery);
            }
        }

        private void wizardPage3_CloseFromNext(object sender, Gui.Wizard.PageEventArgs e)
        {
            // Add selected ticket queries
            _server.TicketQueries.Clear();
            foreach (TicketQueryDefinition query in lstTicketQueries.CheckedItems)
            {
                _server.TicketQueries.Add(query);
            }

            if (!_editMode)
            {
                CommonRoot.Instance.Servers.Add(_server);
            }
            CommonRoot.SaveInstance();
        }

        private void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSelectAll.CheckState == CheckState.Checked)
            {
                for (int i = 0; i < lstTicketQueries.Items.Count; i++)
                {
                    lstTicketQueries.SetItemChecked(i, true);
                }
            }
            else if (chkSelectAll.CheckState == CheckState.Unchecked)
            {
                for (int i = 0; i < lstTicketQueries.Items.Count; i++)
                {
                    lstTicketQueries.SetItemChecked(i, false);
                }
            }
        }

        private void lstTicketQueries_SelectedValueChanged(object sender, EventArgs e)
        {
            int count = 0;

            for (int i = 0; i < lstTicketQueries.Items.Count; i++)
            {
                if (lstTicketQueries.GetItemChecked(i))
                {
                    count++;
                }
            }
            if (count == 0)
            {
                chkSelectAll.CheckState = CheckState.Unchecked;
            }
            else if (count == lstTicketQueries.Items.Count)
            {
                chkSelectAll.CheckState = CheckState.Checked;
            }
            else
            {
                chkSelectAll.CheckState = CheckState.Indeterminate;
            }
        }

        private void rdoAuth_None_CheckedChanged(object sender, EventArgs e)
        {
            ControlChangedEvent(sender, e);
        }

        private void rdoAuth_Integrated_CheckedChanged(object sender, EventArgs e)
        {
            ControlChangedEvent(sender, e);
        }

        private void rdoAuth_Basic_CheckedChanged(object sender, EventArgs e)
        {
            ControlChangedEvent(sender, e);
        }

        private void rdoAuth_ClientCert_CheckedChanged(object sender, EventArgs e)
        {
            ControlChangedEvent(sender, e);
        }
    }
}
