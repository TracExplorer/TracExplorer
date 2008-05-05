@echo off

rem Create release folder
md release
cd release

rem delete any old files from it
del . /q

rem create temp folder
md temp
cd temp

rem delete any old files from it
del . /q

rem copy all deployable files to temp folder
copy ..\..\VSTrac\bin\CookComputing.XmlRpcV2.dll .
copy ..\..\VSTrac\bin\VSTrac.dll .
copy ..\..\VSTrac\vstrac.vscontent .
copy ..\..\VSTrac\vstrac.addin .
copy ..\..\VSTrac\LICENCE.TXT .
copy ..\..\VSTrac\README.TXT .

rem create vsi file (glorified zip)
..\..\external\zip.exe vstrac.vsi *.* -9 -z<LICENCE.TXT
move vstrac.vsi ..\vstrac.vsi

rem removed the vscontent file - not needed for manual install file
del vstrac.vscontent

rem zip again
..\..\external\zip.exe vstrac.zip *.* -9 -z<LICENCE.TXT
move vstrac.zip ..\vstrac.zip

rem last clean-up
cd ..
rd temp /S /Q

pause