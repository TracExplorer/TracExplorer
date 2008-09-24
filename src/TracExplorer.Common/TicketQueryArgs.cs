using System;
using System.Collections.Generic;
using System.Text;

namespace TracExplorer.Common
{
    public class TicketQueryArgs : EventArgs
    {
        private ServerDetails _serverDetails;

        public ServerDetails ServerDetails
        {
            get { return _serverDetails; }
            set { _serverDetails = value; }
        }
        private TicketQueryDefinition _ticketQuery;

        public TicketQueryDefinition TicketQuery
        {
            get { return _ticketQuery; }
            set { _ticketQuery = value; }
        }

        public TicketQueryArgs(ServerDetails serverDetails, TicketQueryDefinition ticketQuery)
        {
            _ticketQuery = ticketQuery;
            _serverDetails = serverDetails;
        }
    }
}
