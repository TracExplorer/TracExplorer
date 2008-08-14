#region GPL Licence
/**********************************************************************
 Trac Explorer - Visual Studio Trac Integration
 Copyright (C) 2008 Jan Linnenkohl
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
