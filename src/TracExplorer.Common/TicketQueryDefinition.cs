#region GPL Licence
/**********************************************************************
 VSTrac - Visual Studio Trac Integration
 Copyright (C) 2008 Mladen Mihajlovic
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
using System.Windows.Forms;
using Microsoft.Win32;

namespace TracExplorer.Common
{
    public class TicketQueryDefinition
    {
        #region Private Variables
        private ServerDetails serverDetails;
        private string name;
        private string filter;
        #endregion

        #region ctors
        public TicketQueryDefinition()
        { }

        public TicketQueryDefinition(string name, string filter)
        {
            this.name = name;
            this.filter = filter;
        }

        public TicketQueryDefinition(string name, string filter, ServerDetails serverDetails) : this(name, filter)
        {
            this.serverDetails = serverDetails;
        }
        #endregion

        #region Public Properties
        public ServerDetails ServerDetails
        {
            get { return serverDetails; }
            set { serverDetails = value; }
        }

        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        public string Filter
        {
            get { return this.filter; }
            set { this.filter = value; }
        }
        #endregion

        #region Save
        public void Save()
        {
            try
            {
                RegistryKey keySoftware = Registry.CurrentUser.OpenSubKey("Software", true); //I'm assuming this exists
                RegistryKey keyServers = keySoftware.CreateSubKey("VSTrac\\Servers", RegistryKeyPermissionCheck.ReadWriteSubTree);
                RegistryKey keyServer = keyServers.CreateSubKey(serverDetails.Server+"\\Tickets", RegistryKeyPermissionCheck.ReadWriteSubTree);
                RegistryKey keyTicketDef = keyServer.CreateSubKey(Name, RegistryKeyPermissionCheck.ReadWriteSubTree);

                keyTicketDef.SetValue("name", Name);
                keyTicketDef.SetValue("filter", Filter);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error saving server configuration", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region LoadAllTicketQueries
        public static List<TicketQueryDefinition> LoadAllTicketQueries(ServerDetails serverDetails)
        {
            List<TicketQueryDefinition> ticketQueries = new List<TicketQueryDefinition>();

            try
            {
                RegistryKey keySoftware = Registry.CurrentUser.OpenSubKey("Software", false);
                RegistryKey keyServers = keySoftware.OpenSubKey("VSTrac\\Servers", false);
                RegistryKey keyServer = keyServers.OpenSubKey(serverDetails.Server, false);
                RegistryKey keyTickets = keyServer.OpenSubKey("Tickets", false);

                if (keyTickets == null)
                    return ticketQueries;

                string[] subTicketQueries = keyTickets.GetSubKeyNames();

                foreach (string ticketQuery in subTicketQueries)
                {
                    RegistryKey keyTicketQuery = keyTickets.OpenSubKey(ticketQuery, false);

                    TicketQueryDefinition ticketQueryDef = new TicketQueryDefinition();
                    ticketQueryDef.serverDetails = serverDetails;
                    ticketQueryDef.Name = (string)keyTicketQuery.GetValue("name");
                    ticketQueryDef.Filter = (string)keyTicketQuery.GetValue("filter");

                    ticketQueries.Add(ticketQueryDef);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error loading ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return ticketQueries;
        }
        #endregion

        #region Delete
        public void Delete()
        {
            try
            {
                RegistryKey keySoftware = Registry.CurrentUser.OpenSubKey("Software", true); //I'm assuming this exists
                RegistryKey keyServers = keySoftware.OpenSubKey("VSTrac\\Servers", true);
                RegistryKey keyServer = keyServers.OpenSubKey(this.serverDetails.Server, true);
                RegistryKey keyTickets = keyServer.OpenSubKey("Tickets", true);

                keyTickets.DeleteSubKeyTree(this.name);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error deleting ticket query definition", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        } 
        #endregion

        #region ToString
        public override string ToString()
        {
            return name;
        }
        #endregion
    }
}
