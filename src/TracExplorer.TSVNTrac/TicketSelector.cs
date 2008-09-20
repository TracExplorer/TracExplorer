#region GPL Licence
/**********************************************************************
 TracExplorer - Trac Integration for Visual Studio and TortoiseSvn
 Copyright (C) 2008 Jan Linnenkohl
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
using TracExplorer.Common;

namespace TracExplorer.TSVNTrac
{
    partial class TicketSelector : Form
    {
        private List<Ticket> _ticketsAffected = new List<Ticket>();

        public TicketSelector(ServerDetails serverDetails, TicketQueryDefinition ticketDefinition, Selection selection)
        {
            InitializeComponent();
        
            ticketView.ServerDetails = serverDetails;
            ticketView.TicketDefinition = ticketDefinition;
            ticketView.TracConnect = new BrowserConnect();
            ticketView.SelectionItems = selection.Items;
            ticketView.RunQuery();
        }

        public IEnumerable<Ticket> TicketsFixed
        {
            get { return _ticketsAffected; }
        }

        public TicketSelector()
        {
            InitializeComponent();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            _ticketsAffected = ticketView.SelectedTickets(); 
            
        }

        private void MyIssuesForm_Load(object sender, EventArgs e)
        {
            
        }
    }
}
