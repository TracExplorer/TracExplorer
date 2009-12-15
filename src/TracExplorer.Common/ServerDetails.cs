#region GPL Licence
/**********************************************************************
 TracExplorer - Trac Integration for Visual Studio and TortoiseSvn
 Copyright (C) 2008 Mladen Mihajlovic
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

using System.Collections.Generic;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Serialization;

namespace TracExplorer.Common
{
    public class ServerDetails
    {
        #region Private Variables
        private string server;
        private AuthenticationTypes requiredAuthentication;
        private string username;
        private byte[] encryptPassword;
        private List<TicketQueryDefinition> ticketQueries = new List<TicketQueryDefinition>();
        #endregion

        static private byte[] entropy = { 17, 6, 19, 74 };

        #region Public Properties
        public string Server
        {
            get { return this.server; }
            set { this.server = value; }
        }

        public AuthenticationTypes RequiredAuthentication
        {
            get { return this.requiredAuthentication; }
            set { this.requiredAuthentication = value; }
        }

        public string Username
        {
            get { return this.username; }
            set { this.username = value; }
        }
 
        [XmlIgnore()]
        public string Password
        {
            get 
            {
                if (this.encryptPassword != null)
                {
                    byte[] cipherBytes = ProtectedData.Unprotect(this.encryptPassword, entropy, DataProtectionScope.CurrentUser);
                    return Encoding.Unicode.GetString(cipherBytes);
                }
                else
                {
                    return "";
                }
            }
            set
            {
                byte[] cipherBytes;

                // Convert the string value.
                cipherBytes = Encoding.Unicode.GetBytes(value);

                // Encrypt the string value (cipher text).
                this.encryptPassword = ProtectedData.Protect(cipherBytes, entropy, DataProtectionScope.CurrentUser);
            }
        }
    
        public byte[] EncryptPassword
        {
            get { return this.encryptPassword; }
            set { this.encryptPassword = value; }
        }

        public List<TicketQueryDefinition> TicketQueries
        {
            get { return ticketQueries; }
            set { ticketQueries = value; }
        }
        #endregion

        #region Public Methods
        public string XmlRpcUrl()
        {
            return this.server + "login/xmlrpc";
        }
        
        public string WikiPageUrl(string WikiPageName)
        {
            return this.server + "wiki/" + WikiPageName;
        }

        public string TicketUrl(int ticketId)
        {
            return this.server + "ticket/" + ticketId.ToString();
        }

        public string NewTicketUrl()
        {
            return this.server + "newticket/";
        }
        #endregion

        #region ctors
        public ServerDetails()
        { }

        public ServerDetails(string server)
        {
            this.Server = server;
            this.RequiredAuthentication = AuthenticationTypes.None;
        }

        public ServerDetails(string server, bool bUseIntergratedAuthentication)
        {
            this.Server = server;
            if (!bUseIntergratedAuthentication)
            {
                this.RequiredAuthentication = AuthenticationTypes.None;
            }
            else
            {
                this.requiredAuthentication = AuthenticationTypes.IntegratedAuthentication;
            }
        }

        public ServerDetails(string server, string username, string password)
            : this(server)
        {
            this.RequiredAuthentication = AuthenticationTypes.BasicAuthentication;
            this.Username = username;
            this.Password = password;
        }

        #endregion



        #region ToString
        public override string ToString()
        {
            return server;
        }
        #endregion
    }
}
