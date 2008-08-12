using System;
using System.Collections.Generic;
using System.Text;

namespace TracExplorer.Common
{
    public interface ITracConnect
    {
        void OpenBrowser(string url);

        void CreateTicketWindow(ServerDetails serverDetails, TicketQueryDefinition ticketDef);
    }
}
