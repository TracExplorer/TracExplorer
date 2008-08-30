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
using System.Xml;

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
            String server;
            String ticketQuery;

            if (!Validate(parameters, out server, out ticketQuery))
            {
                MessageBox.Show("Parameters are invalid", "TracExplorer");
                return originalMessage;
            }
            
            CommonRoot servers = CommonRoot.Instance;

            ServerDetails serverDetails;

            serverDetails = servers.Servers.Find(delegate(ServerDetails obj) { return (obj.Server == server); });

            if (serverDetails == null)
            {
                MessageBox.Show("Can't find server information!", "TracExplorer");
                return originalMessage;
            }

            List<TicketQueryDefinition> ticketQueries = serverDetails.TicketQueries;

            TicketQueryDefinition ticketQueryDef;

            ticketQueryDef = ticketQueries.Find(delegate(TicketQueryDefinition obj) { return (obj.Name == ticketQuery); });

            if (ticketQueryDef == null)
            {
                MessageBox.Show("Can't find ticket query!", "TracExplorer");
                return originalMessage;
            }

            TicketSelector form = new TicketSelector(serverDetails, ticketQueryDef);

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
            return Properties.Resources.LinkText;
        }

        public bool ValidateParameters(IntPtr hParentWnd, string parameters)
        {
            if (parameters.Length == 0)
            {
                // The IBugTraqProvider interface did not support to return a new parameter, 
                // so we copy it to clipboard and set the validation to false
                ShowSelectQuery();
                return false;
            }
            else 
            {
                // Validate parameter string
                string server;
                string ticketQuery;
                if (Validate(parameters, out server, out ticketQuery))
                {
                    return true;
                }
                else
                {
                    if (MessageBox.Show("Parameters are invalid. Open TracExplorer to select query?", "TracExplorer", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        // The IBugTraqProvider interface did not support to return a new parameter, 
                        // so we copy it to clipboard and set the validation to false
                        ShowSelectQuery();
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
        }

        #endregion

        private void ShowSelectQuery()
        {
            ParameterConnect parameterConnect = new ParameterConnect();
            TracExplorerForm form = new TracExplorerForm(parameterConnect);
            parameterConnect.ParentForm = form;

            form.ShowDialog();

            if (parameterConnect.Parameters != null)
            {
                Clipboard.SetText(parameterConnect.Parameters);
            }
        }

        private bool Validate(string parameters, out string server, out string ticketQuery)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(parameters);
                server = doc.DocumentElement.GetAttribute("server");
                ticketQuery = doc.DocumentElement.GetAttribute("ticketquery");
            }
            catch (XmlException)
            {
                server = "";
                ticketQuery = "";
                return false;
            }
            return true;
        }
    }
}
