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
using System.Diagnostics;
using System.Text;
using TracExplorer.Common;
using System.Windows.Forms;

namespace TracExplorer.TSVNTrac
{
    class ParameterConnect : ITracConnect
    {
        private string _parameters;

        public string Parameters
        {
            get { return _parameters; }
            set { _parameters = value; }
        }

        private Form _parentForm;

        public Form ParentForm
        {
            get { return _parentForm; }
            set { _parentForm = value; }
        }


        #region ITracConnect Members

        public void OpenBrowser(string url)
        {
            Process.Start(url);
        }

        public void CreateTicketWindow(ServerDetails serverDetails, TicketQueryDefinition ticketDef, string guid)
        {
            Parameters = string.Format("<TSVNTrac server=\"{0}\" ticketquery=\"{1}\"/>", serverDetails.Server, ticketDef.Name);

            MessageBox.Show("Please paste clipboard text into parameter field!", "Trac Explorer");

            if (_parentForm != null)
            {
                _parentForm.Close();
            }
        }

        #endregion
    }
}
