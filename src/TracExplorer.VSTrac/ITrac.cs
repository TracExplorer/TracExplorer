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
 
**********************************************************************
 XML-RPC.NET Copyright (c) 2006 Charles Cook
**********************************************************************/
#endregion

using System;
using System.Collections.Generic;
using System.Text;
using xmlrpc;
using CookComputing.XmlRpc;

namespace VSTrac
{
    #region MulticallItem
	public struct MulticallItem
    {
        public string methodName;
        public string[] @params;

        public MulticallItem(string methodName, string[] @params)
        {
            this.methodName = methodName;
            this.@params = @params;
        }
    }

	#endregion

    #region PageInfo
    public struct PageInfo
    {
        public DateTime lastModified;
        public int version;
        public string name;
        public string author;
    }
    #endregion

    #region ComponentInfo
    public struct ComponentInfo
    {
        public string owner;
        public string name;
        public string description;
    }
    #endregion

    /// <summary>
    /// XMLRPC Trac proxy for ver 0.2
    /// </summary>
    public interface ITrac : IXmlRpcProxy
    {
        #region Wiki
        [XmlRpcMethod("wiki.getRecentChanges")] //TODO:Check what struct when the api starts working
        object getRecentChanges(DateTime since);

        [XmlRpcMethod("wiki.getRPCVersionSupported")]
        int getRPCVersionSupported();

        [XmlRpcMethod("wiki.getPage")]
        string getPage(string pagename, int version);

        [XmlRpcMethod("wiki.getPage")]
        string getPage(string pagename);

        [XmlRpcMethod("wiki.getPageVersion")]
        string getPageVersion(string pagename, int version);

        [XmlRpcMethod("wiki.getPageVersion")]
        string getPageVersion(string pagename);

        [XmlRpcMethod("wiki.getPageHTML")]
        string getPageHTML(string pagename, int version);

        [XmlRpcMethod("wiki.getPageHTML")]
        string getPageHTML(string pagename);

        [XmlRpcMethod("wiki.getAllPages")]
        string[] getAllPages();

        [XmlRpcMethod("wiki.getPageInfo")]
        PageInfo getPageInfo(string pagename, int version);

        [XmlRpcMethod("wiki.getPageInfo")]
        PageInfo getPageInfo(string pagename);

        [XmlRpcMethod("wiki.getPageInfoVersion")]
        PageInfo getPageInfoVersion(string pagename, int version);

        [XmlRpcMethod("wiki.getPageInfoVersion")]
        PageInfo getPageInfoVersion(string pagename);

        //[XmlRpcMethod("wiki.putPage")]
        //bool wiki.putPage(string pagename, string content, struct attributes);

        [XmlRpcMethod("wiki.listAttachments")]
        string[] listAttachments(string pagename);

        [XmlRpcMethod("wiki.getAttachment")]
        Byte[] getAttachment(string path);

        [XmlRpcMethod("wiki.putAttachment")]
        bool putAttachment(string path, byte[] data);

        [XmlRpcMethod("wiki.putAttachmentEx")]
        bool putAttachmentEx(string pagename, string filename, string description, byte[] data, bool replace);

        [XmlRpcMethod("wiki.putAttachmentEx")]
        bool putAttachmentEx(string pagename, string filename, string description, byte[] data); // replace = true

        [XmlRpcMethod("wiki.listLinks")]
        string[] listLinks(string pagename);

        [XmlRpcMethod("wiki.wikiToHtml")]
        string wikiToHtml(string text);
        #endregion

        #region Search
        [XmlRpcMethod("search.getSearchFilters")]
        object[] getSearchFilters();

        [XmlRpcMethod("search.performSearch")] // results: array(array(href, title, date, author, excerpt))
        object[] performSearch(string query, string[] filters);

        [XmlRpcMethod("search.performSearch")]
        object[] performSearch(string query);
        #endregion

        #region Ticket Milestones
        [XmlRpcMethod("ticket.milestone.getAll")]
        string[] getAllMilestones();

        [XmlRpcMethod("ticket.milestone.get")]
        object getMilestone(string name);
        #endregion

        #region Ticket Severity
        [XmlRpcMethod("ticket.severity.getAll")]
        string[] getAllSeverities();

        [XmlRpcMethod("ticket.severity.get")]
        string getSeverity(string name);
        #endregion

        #region Ticket Type
        [XmlRpcMethod("ticket.type.getAll")]
        string[] getAllTicketTypes();

        [XmlRpcMethod("ticket.type.get")]
        string getTicketType(string name);
        #endregion

        #region Ticket Resolution
        [XmlRpcMethod("ticket.resolution.getAll")]
        string[] getAllResolutions();

        [XmlRpcMethod("ticket.resolution.get")]
        string getTicketResolution(string name);
        #endregion

        #region Ticket Priority
        [XmlRpcMethod("ticket.priority.getAll")]
        string[] getAllPriorities();

        [XmlRpcMethod("ticket.priority.get")]
        string getPriority(string name);
        #endregion

        #region Ticket Component
        [XmlRpcMethod("ticket.component.getAll")]
        string[] getAllComponents();

        [XmlRpcMethod("ticket.component.get")]
        ComponentInfo getComponent(string name);
        #endregion

        #region Tickets
        [XmlRpcMethod("ticket.query")]//array ticket.query(string qstr="status!=closed")
        int[] queryTickets(string queryString);

        [XmlRpcMethod("ticket.get")]
        object[] getTicket(int id);
        #endregion

        #region System
        [XmlRpcMethod("system.multicall")]
        object[] multicall(MulticallItem[] signatures);

        [XmlRpcMethod("system.listMethods")]
        string[] listMethods();

        [XmlRpcMethod("system.methodHelp")]
        string methodHelp(string method);

        [XmlRpcMethod("system.methodSignature")]
        object[] methodSignature(string method);

        [XmlRpcMethod("system.getAPIVersion")]
        object[] getAPIVersion();
        #endregion

        #region Async
        [XmlRpcBegin("system.getAPIVersion")]
        IAsyncResult BeginGetAPIVersion();

        [XmlRpcEnd]
        object[] EndGetAPIVersion(IAsyncResult iasr);
        #endregion
    }
}
