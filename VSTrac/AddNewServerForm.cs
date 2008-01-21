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
        public AddNewServerForm()
        {
            InitializeComponent();
            lblServerCheck.Text = "";
        }

        public ServerDetails Result { get; set; }

        private bool IsValid()
        {
            bool valid = true;

            if (string.IsNullOrEmpty(txtServer.Text))
            {
                errorProvider1.SetError(txtServer, "Please enter a valid Trac url. This must NOT include the \"/login/xmlrpc\" part. eg: http://vstrac.devjavu.com");
                valid = false;
            }
            else
                errorProvider1.SetError(txtServer, "");

            if (groupBox1.Enabled && string.IsNullOrEmpty(txtUsername.Text))
            {
                errorProvider1.SetError(txtUsername, "Please enter a valid username.");
                valid = false;
            }
            else
                errorProvider1.SetError(txtUsername, "");

            if (groupBox1.Enabled && string.IsNullOrEmpty(txtPassword.Text))
            {
                errorProvider1.SetError(txtPassword, "Please enter a valid password.");
                valid = false;
            }
            else
                errorProvider1.SetError(txtPassword, "");


            return valid;

        }

        private void bntOk_Click(object sender, EventArgs e)
        {
            if (!IsValid()) return;

            if (!CheckTracServer()) return;

            Result = GetServerDetails();

            this.DialogResult = DialogResult.OK;
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
            lblServerCheck.Text = "Checking...";

            try
            {
                ServerDetails details = GetServerDetails();
                ITrac trac = TracCommon.GetTrac(details);

                object[] version = trac.getAPIVersion();
            }
            catch (Exception ex)
            {
                lblServerCheck.Text = "Failed check. " + ex.Message;
                return false;
            }

            lblServerCheck.Text = "Passed check.";
            return true;
        }

        private void chkAuthentication_CheckedChanged(object sender, EventArgs e)
        {
            groupBox1.Enabled = chkAuthentication.Checked;
        }
    }
}
