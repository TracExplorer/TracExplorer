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
using CookComputing.XmlRpc;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;

namespace VSTrac
{
    public static class TracCommon
    {
        static TracCommon()
        {
            ServicePointManager.ServerCertificateValidationCallback = delegate(object certsender, X509Certificate cert, X509Chain chain, System.Net.Security.SslPolicyErrors error)
            {
                //TODO: Make better message + put in resource
                if (MessageBox.Show(null, "There was an error reading the site's certificate. Do you want to continue?\r\n" + cert.ToString() + "\r\n\r\n" + error.ToString(), "Error with SSL certificate.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    return true;
                }
                else
                    return false;
            };

        }

        public static ITrac GetTrac(ServerDetails serverDetails)
        {
            ITrac trac = XmlRpcProxyGen.Create<ITrac>();
            trac.Proxy = WebRequest.DefaultWebProxy;
            trac.Url = serverDetails.Server + "/login/xmlrpc";

            if (serverDetails.Authenticated)
                trac.Credentials = new NetworkCredential(serverDetails.Username, serverDetails.Password);

            return trac;
        }
    }
}
