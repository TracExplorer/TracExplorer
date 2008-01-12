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

namespace VSTrac
{
    public class Ticket
    {
        public Bitmap Icon { get; set; }
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastModified { get; set; }
        
        public string Status { get; set; }
        public string Description { get; set; }
        public string Reporter { get; set; }
        public string CC { get; set; }
        public string Resolution { get; set; }
        public string Component { get; set; }
        public string Summary { get; set; }
        public string Priority { get; set; }
        public string Keywords { get; set; }
        public string Version { get; set; }
        public string Milestone { get; set; }
        public string Owner { get; set; }        
        public string TicketType { get; set; }
        public string Severity { get; set; }
    }
}
