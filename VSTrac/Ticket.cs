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
using System.Drawing;
using System.Collections;

namespace VSTrac
{
    public class Ticket
    {
        #region Private Variables
        private Bitmap icon;
        private int id;
        private DateTime created;
        private DateTime lastModified;
        private string status;
        private string description;
        private string reporter;
        private string cc;
        private string resolution;
        private string component;
        private string summary;
        private string priority;
        private string keywords;
        private string version;
        private string milestone;
        private string owner;
        private string ticketType;
        private string severity; 
        #endregion

        #region Public Properties
        public Bitmap Icon
        {
            get { return icon; }
            set { icon = value; }
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public DateTime Created
        {
            get { return created; }
            set { created = value; }
        }

        public DateTime LastModified
        {
            get { return lastModified; }
            set { lastModified = value; }
        }

        public string Status
        {
            get { return status; }
            set { status = value; }
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public string Reporter
        {
            get { return reporter; }
            set { reporter = value; }
        }

        public string CC
        {
            get { return cc; }
            set { cc = value; }
        }

        public string Resolution
        {
            get { return resolution; }
            set { resolution = value; }
        }

        public string Component
        {
            get { return component; }
            set { component = value; }
        }

        public string Summary
        {
            get { return summary; }
            set { summary = value; }
        }

        public string Priority
        {
            get { return priority; }
            set { priority = value; }
        }

        public string Keywords
        {
            get { return keywords; }
            set { keywords = value; }
        }

        public string Version
        {
            get { return version; }
            set { version = value; }
        }

        public string Milestone
        {
            get { return milestone; }
            set { milestone = value; }
        }

        public string Owner
        {
            get { return owner; }
            set { owner = value; }
        }

        public string TicketType
        {
            get { return ticketType; }
            set { ticketType = value; }
        }

        public string Severity
        {
            get { return severity; }
            set { severity = value; }
        } 
        #endregion
    }
}
