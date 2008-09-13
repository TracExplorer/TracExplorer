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

namespace TracExplorer.Common
{
    [XmlRoot("root")]
    public class CommonRoot
    {
        static private CommonRoot instance = null;

        private List<ServerDetails> servers = new List<ServerDetails>();

        public List<ServerDetails> Servers
        {
            get { return servers; }
            set { servers = value; }
        }

        static public CommonRoot Instance
        {
            get
            {
                if (instance == null)
                {
                    // Deserialization
                    try
                    {
                        String path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                        String fileName = path + @"\TracExplorer\TracExplorer.Common.user.config";                
                        XmlSerializer s = new XmlSerializer(typeof(CommonRoot));
                        TextReader r = new StreamReader(fileName);
                        instance = (CommonRoot)s.Deserialize(r);
                        r.Close();
                    }
                    catch (Exception)
                    {
                        instance = new CommonRoot();
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
                String fileName = path + @"\TracExplorer.Common.user.config";
                XmlSerializer s = new XmlSerializer(typeof(CommonRoot));
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
