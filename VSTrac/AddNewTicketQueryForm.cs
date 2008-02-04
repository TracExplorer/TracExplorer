using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace VSTrac
{
    public partial class AddNewTicketQueryForm : Form
    {
        private Connect vsTracConnect;

        #region Public Properties
        public Connect VsTracConnect
        {
            get { return vsTracConnect; }
            set { vsTracConnect = value; }
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
            vsTracConnect.OpenBrowser("http://trac.edgewall.org/wiki/TracQuery#QueryLanguage"); //TODO: Place this in config somewhere?
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
