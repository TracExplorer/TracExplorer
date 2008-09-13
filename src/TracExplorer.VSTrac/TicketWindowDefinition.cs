﻿#region GPL Licence
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
using TracExplorer.Common;
using EnvDTE;
using System.Xml.Serialization;

namespace TracExplorer.VSTrac
{
    public class TicketWindowDefinition
    {
        private string guid;

        public string Guid
        {
            get { return guid; }
            set { guid = value; }
        }

        private string serverName;

        public string ServerName
        {
            get { return serverName; }
            set { serverName = value; }
        }

        private string ticketQueryName;

        public string TicketQueryName
        {
            get { return ticketQueryName; }
            set { ticketQueryName = value; }
        }

        private Window toolWindow;

        [XmlIgnore()]
        public Window ToolWindow
        {
            get { return toolWindow; }
            set { toolWindow = value; }
        }
        
    }
}
