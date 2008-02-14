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

        private ServerDetails GetServerDetails()
        {
            ServerDetails details = new ServerDetails();

            string tempServer = txtServer.Text.Trim();

            if (!tempServer.Contains(":"))
                tempServer = "http://" + tempServer;
                
            Uri uriServer= new Uri(tempServer);

            if (uriServer.LocalPath.Contains("login/xmlrpc"))
                throw new ApplicationException("Please do not include the login/xmlrpc path. The basic trac path is sufficient.");

            details.Server = uriServer.ToString();
            details.Authenticated = chkAuthentication.Checked;
            details.Username = txtUsername.Text;
            details.Password = txtPassword.Text;

            return details;
        }

        private bool CheckTracServer()
        {
            this.Cursor = Cursors.WaitCursor;

            try
            {
                ServerDetails details = GetServerDetails();
                ITrac trac = TracCommon.GetTrac(details);

                object[] version = trac.getAPIVersion();
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }

            return true;
        }

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
    }
}
