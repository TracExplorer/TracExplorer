@echo off
rem Get the latest version from the source code management and replace it inside a file
rem More Information inside of the TortoiseSVN OnlineHelp under "SubWCRev"
rem Param %1: Directory to analyse
rem Param %2: File INPUT
rem Param %3: File OUTPUT

rem Search for subwcrev.exe from TortoiseSVN
if not exist "%ProgramFiles%\TortoiseSVN\bin\SubWCRev.exe" goto try_64bit

"%ProgramFiles%\TortoiseSVN\bin\SubWCRev.exe" %1 %2 %3 -f

goto end

:try_64bit

if not exist "%ProgramW6432%\TortoiseSVN\bin\SubWCRev.exe" goto error_no_subwcrev

"%ProgramW6432%\TortoiseSVN\bin\SubWCRev.exe" %1 %2 %3 -f

goto end

:error_no_subwcrev
echo No TortoiseSVN-Client (SubWCRev.exe) detected!
exit /b 1

:end
exit %errorlevel%