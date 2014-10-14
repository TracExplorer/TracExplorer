Trac Explorer
Copyright (C) 2007-2008 Mladen Mihajlovic, Jan Linnenkohl
http://github.com/TracExplorer/TracExplorer/


LICENCE
=======

This program comes with ABSOLUTELY NO WARRANTY; This is free software, and you are welcome to redistribute it under certain conditions. See LICENCE.TXT or go to http://tracexplorer.devjavu.com/wiki/Licence for more details.

Trac Explorer is developed and released under the GPL v3 licence.
XML-RPC.NET Copyright (c) 2006 Charles Cook
Wizard Control Copyright (c) 2004 Al Gardner http://www.codeproject.com/KB/miscctrl/DesignTimeWizard.aspx
SortableBindingList Copyright (c) 2007 Tim Van Wassenhove http://www.timvw.be/presenting-the-sortablebindinglistt/


DESCRIPTION
===========

TracExplorer combines an VSTrac, an Viusal Studio Trac integration, and TSVNTrac, an Trac-Bugtraqprovider for TortoiseSVN.  
 * VSTrac is an addin for Visual Studio 2005, 2008 and 2010.
 * TSVNTrac uses the bugtraq-interface provided by TortoiseSVN, the best userinterface for subversion on windows (http://www.tortoisesvn.net).
TracExplorer allows the integration with Trac (http://trac.edgewall.com/) systems using the XML-RPC plugin (http://trac-hacks.org/wiki/XmlRpcPlugin). It allows configuring many servers, querying, modifying and adding new tickets as well as editing wiki pages.

Please feel free to post any bugs or suggestions on the main website.


INSTALLATION
============

Please use the msi-package to install the software on your computer.

Requirements:
 * VSTrac uses Visual Studio 2005, 2008, 2010 (minimum Standard version)
 * TSVNTrac uses TortoiseSVN 1.5 or above
   
USAGE
=====

Please go to our main website for the documentation.

BUILDING PROCEDURE (REQUIREMENTS)
=================================
 * .NET 2 framework
 * Solution can be build by both VS2005, VS2008, VS2010. There are 3 solution files.
 * Needs SubWCRev from TortoiseSVN installation for autmatic version numbering
 * Uses the XMLRPC.NET library from http://www.xml-rpc.net/
 * Requires Trac sites with an enabled [http://trac-hacks.org/wiki/XmlRpcPlugin XMLRPC plugin]
 * Uses [http://wix.sourceforge.net/downloadv3.html WiX 3 beta] for the installation project 
     
RELEASE PROCEDURE
=================
   - Update version number in "GlobalAssemblyInfo.cs.in" & "VersionNumberInclude.wxi.in"
   - Tag the actual version
   - Switch to the tag
   - Rebuild application
   - Rename the msi-file to e.g. "TracExplorer 0.1.0.msi"
   - Upload file to SourceForge
   - Announce the new release on SourceForge
   - Close milestone and add new version (trac)

DEBUGGING
=========

VSTrac:
   - Select project properties / Debug
     - Start external program: "c:\Program Files\Microsoft Visual Studio 9.0\Common7\IDE\devenv.exe"
     - Command line arguments: "/resetaddin TracExlporer.VSTrac.Connect"
   - Change the addin file in your TracExplorer installation:
     - Modify the assembly path to your binary : e.g. <Assembly>T:\TracExplorer\trunk\bin\Debug\TracExplorer.VSTrac.dll</Assembly>
   
TSVNTrac:
   - Register your TracExplorer.TSVNTrac.dll
   - Choose debug "Attach to process.."
   
   
OPEN SOURCE / BUG REPORTING
===========================
TracExplorer is an open source project. If you would like to help out or just log a bug/suggestion, please visit the main site at http://github.com/TracExplorer/TracExplorer/
