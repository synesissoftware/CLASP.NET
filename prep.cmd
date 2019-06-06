@ECHO OFF

REM #########################################################################
REM File:		prep.cmd (for CLASP.NET)
REM
REM Created:	6th June 2019
REM Updated:	6th June 2019
REM
REM Copyright (c) 2009-2019, Matthew Wilson and Synesis Software. All rights
REM reserved.
REM
REM #########################################################################


REM ##############################################
REM compatibility

SETLOCAL ENABLEEXTENSIONS ENABLEDELAYEDEXPANSION

IF ERRORLEVEL 1 (

	ECHO This script requires extensions and delayed expansion

	EXIT /B 1
)

REM ##############################################
REM constants

SET dir_script=%~dp0
SET dir_project_root=%dir_script%

REM ##############################################
REM command-line parsing

REM ##############################################
REM main


REM hide directories/files we don't care to look at

IF EXIST %dir_project_root%\CLASP.NET.suo ATTRIB +h %dir_project_root%\CLASP.NET.suo 
IF EXIST %dir_project_root%\TestResults ATTRIB +h %dir_project_root%\TestResults 
IF EXIST %dir_project_root%\packages ATTRIB +h %dir_project_root%\packages 

IF EXIST C:\bin\batch\binobj.cmd CALL C:\bin\batch\binobj.cmd

FOR /R %dir_project_root% %%f IN (*.user) DO @ATTRIB +h %%f


REM clear out the TestResults

IF EXIST %dir_project_root%\TestResults (

	FOR /R %dir_project_root%\TestResults %%f IN (*) DO @DEL /Q "%%f"
)


GOTO :EOF

REM ############################ end of file ############################# #

