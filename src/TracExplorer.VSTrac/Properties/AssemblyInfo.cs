#region GPL Licence
/**********************************************************************
 TracExplorer - Trac Integration for Visual Studio and TortoiseSvn
 Copyright (C) 2008 Mladen Mihajlovic
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

using System.Reflection;
using System.Runtime.CompilerServices;

//
// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
//
[assembly: AssemblyTitle("VSTrac")]
[assembly: AssemblyCulture("")]

//
// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Revision
//      Build Number
//
// You can specify all the value or you can default the Revision and Build Numbers 
// by using the '*' as shown below:

//
// In order to sign your assembly you must specify a key to use. Refer to the 
// Microsoft .NET Framework documentation for more information on assembly signing.
//
// Use the attributes below to control which key is used for signing. 
//
// Notes: 
//   (*) If no key is specified - the assembly cannot be signed.
//   (*) KeyName refers to a key that has been installed in the Crypto Service
//       Provider (CSP) on your machine. 
//   (*) If the key file and a key name attributes are both specified, the 
//       following processing occurs:
//       (1) If the KeyName can be found in the CSP - that key is used.
//       (2) If the KeyName does not exist and the KeyFile does exist, the key 
//           in the file is installed into the CSP and used.
//   (*) Delay Signing is an advanced option - see the Microsoft .NET Framework
//       documentation for more information on this.
//
[assembly: AssemblyDelaySign(false)]
[assembly: AssemblyKeyFile("")]
[assembly: AssemblyKeyName("")]
