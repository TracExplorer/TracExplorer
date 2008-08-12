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
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TracExplorer.Common;
using CookComputing.XmlRpc;
using System.Net;
using System.IO;
using System.Diagnostics;
using System.Drawing;
using System.Security.Cryptography.X509Certificates;
using System.Reflection;

namespace TracExplorer.CommonTest
{
    [TestClass]
    public class XmlRpcTests
    {
        public static bool SetAllowUnsafeHeaderParsing()
        {
            //Get the assembly that contains the internal class
            Assembly aNetAssembly = Assembly.GetAssembly(
              typeof(System.Net.Configuration.SettingsSection));
            if (aNetAssembly != null)
            {
                //Use the assembly in order to get the internal type for 
                // the internal class
                Type aSettingsType = aNetAssembly.GetType(
                  "System.Net.Configuration.SettingsSectionInternal");
                if (aSettingsType != null)
                {
                    //Use the internal static property to get an instance 
                    // of the internal settings class. If the static instance 
                    // isn't created allready the property will create it for us.
                    object anInstance = aSettingsType.InvokeMember("Section",
                      BindingFlags.Static | BindingFlags.GetProperty
                      | BindingFlags.NonPublic, null, null, new object[] { });
                    if (anInstance != null)
                    {
                        //Locate the private bool field that tells the 
                        // framework is unsafe header parsing should be 
                        // allowed or not
                        FieldInfo aUseUnsafeHeaderParsing = aSettingsType.GetField(
                          "useUnsafeHeaderParsing",
                          BindingFlags.NonPublic | BindingFlags.Instance);
                        if (aUseUnsafeHeaderParsing != null)
                        {
                            aUseUnsafeHeaderParsing.SetValue(anInstance, true);
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        #region Skeleton Code
        private static ITrac trac = null;

        [ClassInitialize]
        public static void Init(TestContext context)
        {
            ServicePointManager.ServerCertificateValidationCallback += delegate(object sender, X509Certificate cert, X509Chain chain, System.Net.Security.SslPolicyErrors error)
            {
                // just accept any ssl connection for the unit tests
                return true;
            };

            //List<ServerDetails> servers = ServerDetails.LoadAll();

            SetAllowUnsafeHeaderParsing();
            //trac = TracCommon.GetTrac(servers[1]); // get the first server from the registry
            trac = TracCommon.GetTrac(new ServerDetails("http://vstrac.devjavu.com/", "vstrac.devjavu@gmail.com", "testing"));

            xmlrpc.Tracer tracer = new xmlrpc.Tracer();
            tracer.Attach(trac);

            //call a method just to get exception here is there is a problem. Then no tests will run.
            object result = trac.getAPIVersion();
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }
        #endregion

        #region Wiki.* Tests
        [TestMethod]
        [Description("Test the wiki getRecentChanges method")]
        public void TestGetRecentChanges()
        {
            object result = trac.getRecentChanges(DateTime.Now.AddDays(-2));

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestGetPage()
        {
            string pageText = trac.getPage("WikiStart");

            Assert.IsFalse(string.IsNullOrEmpty(pageText));

            string pageText2 = trac.getPage("WikiStart", 1);

            Assert.IsFalse(string.IsNullOrEmpty(pageText2));

            Assert.AreNotEqual<string>(pageText2, pageText);
        }

        [TestMethod]
        public void TestGetPageHtml()
        {
            string pageText = trac.getPageHTML("WikiStart");

            Assert.IsFalse(string.IsNullOrEmpty(pageText));

            string pageText2 = trac.getPageHTML("WikiStart", 1);

            Assert.IsFalse(string.IsNullOrEmpty(pageText2));

            Assert.AreNotEqual<string>(pageText2, pageText);
        }

        [TestMethod]
        [Description("Test GetAllPages")]
        public void TestGetAllPages()
        {
            string[] pages = trac.getAllPages();

            Assert.IsNotNull(pages);
            Assert.AreNotEqual<int>(0, pages.Length);
        }


        [TestMethod]
        [Description("Test GetPageInfo")]
        public void TestGetPageInfo()
        {
            PageInfo result = trac.getPageInfo("WikiStart");

            Assert.IsNotNull(result);

            PageInfo result2 = trac.getPageInfo("WikiStart", 1);

            Assert.IsNotNull(result2);

            Assert.AreNotEqual<int>(result2.version, result.version);
            Assert.AreNotEqual<DateTime>(result2.lastModified, result.lastModified);
        }


        [TestMethod]
        [Description("Test for getPageInfoVersion")]
        public void TestgetPageInfoVersion()
        {
            PageInfo result = trac.getPageInfoVersion("WikiStart");

            Assert.IsNotNull(result);

            PageInfo result2 = trac.getPageInfoVersion("WikiStart", 1);

            Assert.IsNotNull(result2);

            Assert.AreNotEqual<int>(result2.version, result.version);
            Assert.AreNotEqual<DateTime>(result2.lastModified, result.lastModified);
        }


        [TestMethod]
        [Description("Test listAttachments")]
        public void TestListAttachments()
        {
            string[] attachments = trac.listAttachments("WikiStart");

            Assert.IsNotNull(attachments);
        }


        [TestMethod]
        [Description("Test getAttachment")]
        public void TestGetAttachment()
        {
            byte[] avatar = trac.getAttachment("WikiStart/avatar.gif");

            Assert.IsNotNull(avatar);
            Assert.AreNotEqual<int>(0, avatar.Length);

            MemoryStream s = new MemoryStream(avatar);
            byte[] actual = new byte[(int)s.Length];
            s.Read(actual, 0, (int)s.Length);
            s.Close();

            CollectionAssert.AreEqual(avatar, actual);
        }


        [TestMethod]
        [Description("Test listLinks")]
        [Ignore] // not implemented yet on Trac's XMLRPC
        public void TestListLinks() 
        {
            string[] links = trac.listLinks("WikiStart");

            Assert.IsNotNull(links);
            Assert.AreNotEqual<int>(0, links.Length);
        }


        [TestMethod]
        [Description("Test Wiki to HTML")]
        public void TestWikiToHtml()
        {
            string text = "= test_header =";
            string result = trac.wikiToHtml(text);

            Assert.AreEqual<string>("<h1 id=\"test_header\">test_header</h1>", result.TrimEnd());
        }
        #endregion

        #region Search.* Tests
        [TestMethod]
        [Description("Test getSearchFilters")]
        public void TestGetSearchFilters()
        {
            object[] results = trac.getSearchFilters();

            Assert.IsNotNull(results);
        }

        [TestMethod]
        [Description("Test performSearch")]
        public void TestPerformSearch()
        {
            object[] results = trac.performSearch("mika");

            Assert.IsNotNull(results, "Without filter");

            results = trac.performSearch("trac", new string[] { "wiki" });

            Assert.IsNotNull(results, "With filter");
            //filters: ticket, wiki, changeset
        }
        #endregion

        #region Ticket.* Tests
        [TestMethod]
        [Description("Test the trac.get function")]
        public void TestTicketGet()
        {
            object result = trac.getTicket(1);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        [Description("Test the trac.query function")]
        public void TestTicketQuery()
        {
            int[] result = trac.queryTickets("status=new");

            Assert.IsNotNull(result);
        }
        #endregion

        #region Ticket.Milestone.*
        [TestMethod]
        [Description("Test getAllMilestones")]
        public void TestGetAllMilestones()
        {
            string[] results = trac.getAllMilestones();

            Assert.IsNotNull(results);
        }


        [TestMethod]
        [Description("Test Ticket Milestone get")]
        public void TestGetMilestone()
        {
            //object = result = trac.getMilestone("");

            Assert.Inconclusive();
        }
        #endregion

        #region Ticket.Severity.*
        [TestMethod]
        [Description("Test GetAllSeverities")]
        public void TestGetAllSeverities()
        {
            string[] results = trac.getAllSeverities();

            Assert.IsNotNull(results);
        }

        [TestMethod]
        [Description("Test GetSeverity")]
        public void TestGetSeverity()
        {
            string result = trac.getSeverity("critical");

            Assert.IsFalse(string.IsNullOrEmpty(result));
        }
        #endregion

        #region Ticket.Type.*
        [TestMethod]
        [Description("Test GetTicketTypes")]
        public void TestGetAllTicketTypes()
        {
            string[] results = trac.getAllTicketTypes();

            Assert.IsNotNull(results);
        }

        [TestMethod]
        [Description("Test GetTicketType")]
        public void TestGetTicketType()
        {
            string result = trac.getTicketType("enhancement");

            Assert.IsFalse(string.IsNullOrEmpty(result));
        }
        #endregion

        #region Ticket.Resolution.*
        [TestMethod]
        [Description("Test GetAllResolutions")]
        public void TestGetAllResolutions()
        {
            string[] results = trac.getAllResolutions();

            Assert.IsNotNull(results);
        }

        [TestMethod]
        [Description("Test GetTicketResolution")]
        public void TestGetTicketResolution()
        {
            string result = trac.getTicketResolution("wontfix");

            Assert.IsFalse(string.IsNullOrEmpty(result));
        }
        #endregion

        #region Ticket.Priority.*
        [TestMethod]
        [Description("Test GetAllPriorities")]
        public void TestGetAllPriorities()
        {
            string[] results = trac.getAllPriorities();

            Assert.IsNotNull(results);
        }

        [TestMethod]
        [Description("Test GetPriority")]
        public void TestGetPriority()
        {
            string result = trac.getPriority("high");

            Assert.IsFalse(string.IsNullOrEmpty(result));
        }
        #endregion

        #region Ticket.Component.*
        [TestMethod]
        [Description("Test GetAllComponents")]
        public void TestGetAllComponents()
        {
            string[] results = trac.getAllComponents();

            Assert.IsNotNull(results);
        }

        [TestMethod]
        [Description("Test GetComponent")]
        public void TestGetComponent()
        {
            ComponentInfo result = trac.getComponent("General");

            Assert.IsNotNull(result);
        }
        #endregion

        #region System.* Tests
        [TestMethod]
        [Description("Test multicall")]
        public void TestMulticall()
        {
            int[] ticketIds = trac.queryTickets("status=new");

            List<MulticallItem> signatures = new List<MulticallItem>();

            foreach (int i in ticketIds)
                signatures.Add(new MulticallItem("ticket.get", new string[] { i.ToString() }));

            object[] results = trac.multicall(signatures.ToArray());

            Assert.IsNotNull(results);
            Assert.AreNotEqual<int>(0, results.Length);
        }

        [TestMethod]
        public void TestSystemList()
        {
            string[] methods = trac.listMethods();

            Assert.IsNotNull(methods);
            Assert.AreNotEqual<int>(0, methods.Length);
        }

        [TestMethod]
        public void TestMethodHelp()
        {
            string sig = trac.methodHelp("ticket.query");

            Assert.IsFalse(string.IsNullOrEmpty(sig));
        }

        [TestMethod]
        public void TestGetApiVersion()
        {
            object[] result = trac.getAPIVersion();

            Assert.IsNotNull(result);
            Assert.AreEqual<int>(2, result.Length);
        }
        #endregion
    }
}
