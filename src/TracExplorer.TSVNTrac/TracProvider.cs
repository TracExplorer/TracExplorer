using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using TracExplorer.Common;

namespace TracExplorer.TSVNTrac
{
    [ComVisible(true),
     Guid("05BC2E49-E116-44e3-BF43-DD90EEC031DD"),
     ClassInterface(ClassInterfaceType.None)]
    public class TracProvider : Interop.BugTraqProvider.IBugTraqProvider
    {
        #region IBugTraqProvider Members

        public string GetCommitMessage(IntPtr hParentWnd, string parameters, string commonRoot, string[] pathList, string originalMessage)
        {
            List<ServerDetails> servers = ServerDetails.LoadAll();

            List<TicketQueryDefinition> ticketQueries = TicketQueryDefinition.LoadAllTicketQueries(servers[0]);
            
            TicketSelector form = new TicketSelector(servers[0],ticketQueries[0]);

            if (form.ShowDialog() != DialogResult.OK)
                return originalMessage;

            StringBuilder result = new StringBuilder(originalMessage);
            if (originalMessage.Length != 0 && !originalMessage.EndsWith("\n"))
                result.AppendLine();

            foreach (Ticket ticket in form.TicketsFixed)
            {
                result.AppendFormat("Fixed #{0}: {1}", ticket.Id, ticket.Summary);
                result.AppendLine();
            }

            return result.ToString();
        }

        public string GetLinkText(IntPtr hParentWnd, string parameters)
        {
            return "Choose Issue";
        }

        public bool ValidateParameters(IntPtr hParentWnd, string parameters)
        {
            return true;
        }

        #endregion
    }
}
