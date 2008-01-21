#region GPL Licence
/**********************************************************************
 VSTrac - Visual Studo Trac Integration
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
using System.Text;
using Microsoft.Win32;
using System.Windows.Forms;

namespace VSTrac
{
    public class ServerDetails
    {
        public string Server { get; set; }
        public bool Authenticated { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public ServerDetails()
        { }

        public ServerDetails(string server)
        {
            this.Server = server;
        }

        public ServerDetails(string server, string username, string password)
            : this(server)
        {
            this.Authenticated = true;
            this.Username = username;
            this.Password = password;
        }

        public void Save()
        {
            try
            {
                RegistryKey keySoftware = Registry.CurrentUser.OpenSubKey("Software", true); //I'm assuming this exists
                RegistryKey keyServers = keySoftware.CreateSubKey("VSTrac\\Servers", RegistryKeyPermissionCheck.ReadWriteSubTree);
                
                RegistryKey keyServer = keyServers.CreateSubKey(this.Server,  RegistryKeyPermissionCheck.ReadWriteSubTree);
                keyServer.SetValue("server", this.Server);
                keyServer.SetValue("authenticated", this.Authenticated ? 1 : 0);
                keyServer.SetValue("username", this.Username);
                keyServer.SetValue("password", this.Password);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error saving server configuration", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static List<ServerDetails> LoadAll()
        {
            List<ServerDetails> servers = new List<ServerDetails>();

            try
            {
                RegistryKey keySoftware = Registry.CurrentUser.OpenSubKey("Software", true); //I'm assuming this exists
                RegistryKey keyServers = keySoftware.OpenSubKey("VSTrac\\Servers", true);

                if (keyServers == null) // no defined servers;
                    return servers;

                string[] subkeyServers = keyServers.GetSubKeyNames();

                foreach (string subkeyServer in subkeyServers)
                {
                    RegistryKey keyServer = keyServers.OpenSubKey(subkeyServer, false);

                    ServerDetails server = new ServerDetails();
                    server.Server = (string)keyServer.GetValue("server");
                    server.Authenticated = (int)keyServer.GetValue("authenticated") == 1 ? true : false;
                    server.Username = (string)keyServer.GetValue("username");
                    server.Password = (string)keyServer.GetValue("password");

                    servers.Add(server);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error loading server configuration", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return servers;
        }
    }
}
