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
using System.IO;
using System.Xml.Serialization;

namespace TracExplorer.VSTrac
{
    [XmlRoot("root")]
    public class VSTracRoot
    {
        static private VSTracRoot instance = null;

        private bool openTracExplorerOnStartup;

        public bool OpenTracExplorerOnStartup
        {
            get { return openTracExplorerOnStartup; }
            set { openTracExplorerOnStartup = value; }
        }

        private List<TicketWindowDefinition> ticketWindowList;

        public List<TicketWindowDefinition> TicketWindowList
        {
            get { return ticketWindowList; }
            set { ticketWindowList = value; }
        }

        static public VSTracRoot Instance
        {
            get
            {
                if (instance == null)
                {
                    // Deserialization
                    try
                    {
                        String path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                        String fileName = path + @"\TracExplorer\TracExplorer.VSTrac.user.config";                
                        XmlSerializer s = new XmlSerializer(typeof(VSTracRoot));
                        TextReader r = new StreamReader(fileName);
                        instance = (VSTracRoot)s.Deserialize(r);
                        r.Close();
                    }
                    catch (Exception)
                    {
                        instance = new VSTracRoot();
                    }
                }
                return instance;
            }
        }

        static public void SaveInstance()
        {
            // Serialization
            try
            {
                String path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                path += @"\TracExplorer";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                String fileName = path + @"\TracExplorer.VSTrac.user.config";
                XmlSerializer s = new XmlSerializer(typeof(VSTracRoot));
                TextWriter w = new StreamWriter(fileName);
                s.Serialize(w, instance);
                w.Close();
            }
            catch (Exception)
            {
            }
        }
    }
}
