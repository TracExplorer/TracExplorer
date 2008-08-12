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

        public TicketSelector(ServerDetails serverDetails, TicketQueryDefinition ticketDefinition)
        {
            InitializeComponent();
        
            ticketView.ServerDetails = serverDetails;
            ticketView.TicketDefinition = ticketDefinition;
            ticketView.TracConnect = new BrowserConnect();
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
