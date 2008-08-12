using System;
using System.Collections.Generic;
using System.Text;
using TracExplorer.Common;
using System.Diagnostics;

namespace TracExplorer.TSVNTrac
{
    class BrowserConnect : ITracConnect
    {
        #region ITracConnect Members

        public void OpenBrowser(string url)
        {
            Process.Start(url);
        }

        public void CreateTicketWindow(ServerDetails serverDetails, TicketQueryDefinition ticketDef)
        {
            // Not implemented!
        }

        #endregion
    }
}
