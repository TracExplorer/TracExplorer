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
using TracExplorer.Common;

namespace TracExplorer.VSTrac
{
    public partial class AddNewTicketQueryForm : Form
    {
        private ITracConnect _tracConnect;

        #region Public Properties
        public ITracConnect TracConnect
        {
            get { return _tracConnect; }
            set { _tracConnect = value; }
        }

        public string TicketQueryName
        {
            get { return txtName.Text; }
            set { txtName.Text = value; }
        }

        public string TicketQueryFilter
        {
            get { return txtFilter.Text; }
            set { txtFilter.Text = value; }
        }
        #endregion

        public AddNewTicketQueryForm()
        {
            InitializeComponent();
        }

        private void lnkFilterHelp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            TracConnect.OpenBrowser("http://trac.edgewall.org/wiki/TracQuery#QueryLanguage"); //TODO: Place this in config somewhere?
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (!IsValid())
                return;

            this.DialogResult = DialogResult.OK;
        }

        private bool IsValid()
        {
            bool valid = true;

            if (txtName.Text.Trim().Length == 0)
            {
                errorProvider1.SetError(txtName, "Please enter a valid name.");
                valid = false;
            }
            else
                errorProvider1.SetError(txtName, "");

            if (txtFilter.Text.Trim().Length == 0)
            {
                errorProvider1.SetError(txtFilter, "Please enter a valid filter query.");
                valid = false;
            }
            else
                errorProvider1.SetError(txtFilter, "");

            return valid;
        }
    }
}
