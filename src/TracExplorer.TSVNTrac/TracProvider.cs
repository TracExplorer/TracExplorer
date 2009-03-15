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
    public class TracProvider : Interop.BugTraqProvider.IBugTraqProvider2
    {
        #region IBugTraqProvider Members

        public string GetCommitMessage(IntPtr hParentWnd, string parameters, string commonRoot, string[] pathList, string originalMessage)
        {
            String server;
            String ticketQuery;
            Selection selection = new Selection();

            if (!Validate(parameters, out server, out ticketQuery, out selection))
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

            if (selection.Items.Count <= 1)
            {
                MessageBox.Show("Can't find selection definitions!", "TracExplorer");
                return originalMessage;
            }
            
            TicketSelector form = new TicketSelector(serverDetails, ticketQueryDef, selection);

            if (form.ShowDialog() != DialogResult.OK)
                return originalMessage;

            StringBuilder result = new StringBuilder(originalMessage);
            if (originalMessage.Length != 0 && !originalMessage.EndsWith("\n"))
                result.AppendLine();

            // Correct wrong format in new line
            selection.Format = selection.Format.Replace("\\n", "\n");

            foreach (Ticket ticket in form.TicketsFixed)
            {
                result.AppendFormat(selection.Format, ticket.Selection, ticket.Id, ticket.Summary);
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
                Selection selection;

                if (Validate(parameters, out server, out ticketQuery, out selection))
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
            BrowserConnect parameterConnect = new BrowserConnect();
            AddNewProviderForm form = new AddNewProviderForm(parameterConnect);

            if (form.ShowDialog() == DialogResult.OK)
            {
                String parameters = AddDefaultSelections(form.Parameters, form.Selection);
                Clipboard.SetText(parameters);
                MessageBox.Show("Please paste clipboard text into parameter field!", "Trac Explorer");
            }
        }

        private String AddDefaultSelections(string parameters, Selection selection)
        {
            try
            {
                // Add default Selection definitions:
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(parameters);

                AddSelection(ref doc, selection);
               
                return doc.OuterXml;

            }
            catch (XmlException)
            {
                return null;
            }
        }

        private bool AddSelection(ref XmlDocument doc, Selection selection)
        {
            try
            {
                XmlElement childElement = doc.CreateElement("Selection");

                XmlAttribute formatAttribute = doc.CreateAttribute("Format");
                formatAttribute.Value = selection.Format;
                childElement.Attributes.Append(formatAttribute);

                foreach (string item in selection.Items)
                {
                    XmlElement itemElement = doc.CreateElement("Item");
                    itemElement.InnerText = item;
                    childElement.AppendChild(itemElement);
                }                

                doc.DocumentElement.AppendChild(childElement);
                return true;
            }
            catch (XmlException)
            {
                return false;
            }
        }


        private bool Validate(string parameters, out string server, out string ticketQuery, out Selection selection)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(parameters);
                server = doc.DocumentElement.GetAttribute("server");
                ticketQuery = doc.DocumentElement.GetAttribute("ticketquery");

                selection = new Selection();

                // Add empty selection
                selection.Items.Add("");

                XmlElement childSelection = (XmlElement) doc.DocumentElement.FirstChild;

                selection.Format = childSelection.GetAttribute("Format");
                
                foreach (XmlElement childElement in childSelection.ChildNodes)
                {
                    String item = childElement.InnerText;
                    selection.Items.Add(item);
                }
            }
            catch (XmlException)
            {
                server = "";
                ticketQuery = "";
                selection = new Selection();
                return false;
            }
            return true;
        }

        #region IBugTraqProvider2 Members

        public string CheckCommit(IntPtr hParentWnd, string parameters, string commonURL, string commonRoot, string[] pathList, string commitMessage)
        {
            return "";
        }

        public string GetCommitMessage2(IntPtr hParentWnd, string parameters, string commonURL, string commonRoot, string[] pathList, string originalMessage, string bugID, out string bugIDOut, out string[] revPropNames, out string[] revPropValues)
        {
            bugIDOut = "";
            revPropNames = null;
            revPropValues = null;
            return GetCommitMessage(hParentWnd, parameters, commonRoot, pathList, originalMessage);
        }

        public bool HasOptions()
        {
            return true;
        }

        public string OnCommitFinished(IntPtr hParentWnd, string commonRoot, string[] pathList, string logMessage, int revision)
        {
            return "";
        }

        public string ShowOptionsDialog(IntPtr hParentWnd, string parameters)
        {
            BrowserConnect parameterConnect = new BrowserConnect();
            AddNewProviderForm form = new AddNewProviderForm(parameterConnect);

            if (form.ShowDialog() == DialogResult.OK)
            {
                return AddDefaultSelections(form.Parameters, form.Selection);
            }
            else
            {
                return parameters;
            }
        }

        #endregion
    }
}
