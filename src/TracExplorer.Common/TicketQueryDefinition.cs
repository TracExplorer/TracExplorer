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


namespace TracExplorer.Common
{
    public class TicketQueryDefinition
    {
        #region Private Variables
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
        #endregion

        #region Public Properties
               
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

        #region ToString
        public override string ToString()
        {
            return name;
        }
        #endregion
    }
}
