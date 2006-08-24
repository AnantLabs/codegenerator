	MySQLDriverCS: An C# driver for MySQL.
	Copyright (c) 2002 Manuel Lucas Viñas Livschitz.

	This file is part of MySQLDriverCS.

    MySQLDriverCS is free software; you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation; either version 2 of the License, or
    (at your option) any later version.

    MySQLDriverCS is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with MySQLDriverCS; if not, write to the Free Software
    Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA

ABOUT
-----
This project was developed because the only ADO.NET compliant driver is 
commercial. This driver is free (GPL).
There were another solutions as OLEDB driver and ODBC.NET but the first 
is very limited and was abandoned by the owner in the year 2001, the 
second solution ODBC.NET doesn't works fine because ODBC.NET is a very 
advanced driver not very compatible with the GPL ODBC driver of MySQL.

OBJECTIVES
----------
-	Support all SQL basic functions.
-	Additional features.


INSTALLATION/DEVELOPMENT
------------------------
You must add a reference to MySQLDriverCS.DLL in your project. 
MySQLDriverCS.DLL calls libMYSQL.dll but this file produces errors when 
MySQLDriverCS is used in ASP.NET projects so put it in windows\system 
folder to avoid errors and remove it from ASP.NET bin folder of your 
project. Because of this, the package includes a install option to make
it automatically (for deployments).


USAGE
-----
Use it as OleDb driver or Sql driver but taking care of unsupported members.
This driver works now with text-only queries.

RELEASE VERSION 3.0.13 NOTES 
----------------------------
Date: 2004-26-02
Features:
-MySQLDataReader: Now IsDBNull is supported.
-Now supports MySQL 4.x versions: (libMySQLd.dll upgraded).
-MySQL documentation updated


RELEASE VERSION 3.0.12 NOTES 
----------------------------
Date: 2004-18-01
Features:
MySQLCommand:
Items update by Omar del Valle Rodríguez (01/18/2004)
- Method ExecuteScalar is now support.
- ExecuteReader now is declare protected and support CloseConnection parameter
- Created method public ExecuteReader() with CloseConnection in false
- Updated method ExecuteReader(CommandBehavior behavior) in order support
CommandBehavior.CloseConnection

MySQLDataReader:
Items update by Omar del Valle Rodríguez (01/18/2004)
- Constrctor MySQLDataReader now support bool parameter to close connection
after close DataReader.
- Update method Close in order support CommandBehavior.CloseConnection

RELEASE VERSION 3.0.11 NOTES 
----------------------------
Date: 2003-12-05
Features:
	Blob, Text and TimeStamp support (by "Christophe Ravier" <c.ravier@laposte.net>).
	Explicit conection errors (by "Christophe Ravier" <c.ravier@laposte.net>). 


RELEASE VERSION 3.0.10 NOTES 
---------------------------
Date: 2003-08-31
Features:
	Parameter usage added (by Omar del Valle Rodríguez and William Reinoso). 
BugFixes:
	MySQLCommand changes in order to support parameters.

RELEASE VERSION 3.0.8 NOTES 
---------------------------
Date: 2003-05-24
Features:
	MySQLDataAdapter added (by Omar del Valle Rodríguez). 
BugFixes:
	DataReader changes in order to support MySQLDataAdapter.
	
RELEASE VERSION 3.0.7 NOTES 
---------------------------
Date: 2003-02-16
Features:
	Strong Name Added.
	GAC.bat -> GAC registering for MySQLDriverCS 
BugFixes:
	none

RELEASE VERSION 3.0.6 NOTES 
---------------------------
Date: 2003-01-28
BugFixes:
	MySQLCommand.ExecuteReader() and ExecuteNonQuery() bugfixed while checking connection state.
	Connection property in MySQLCommand


RELEASE VERSION 3.0.5 NOTES 
---------------------------
Date: 2002-11-27
Features:
	Transactions bugfixed [Vincent (vdaron) 2002-11-23 and Me 2002-11-27]
	GetDouble and GetFloat decimation bugfixed for Languages with comma decimation like German
	
RELEASE VERSION 3.0.4 NOTES 
---------------------------
Date: 2002-11-14
Features:
	VB.NET sample added

RELEASE VERSION 3.0.3 NOTES 
---------------------------
Date: 2002-11-04
BugFixes:
	 MySQLSelectCommand.Exec: Bugfixed by Yann Sénécheau 2002-10-28
	 MySQLCommand.CommandText: setting hadn't any effect
Features:
	 Port added as property of connection and as part of connection string

RELEASE VERSION 3.0.2 NOTES 
---------------------------
Date: 2002-10-28
- Now supports: Transactions. 
		all related functions.
		MySQLTransaction class
- New Functions:
		MySQLConnection
			ChangeDatabase
			CreateCommand
		MySQLCommand
			Cancel
- New MySQLSelectCommand function (a select that can do anything)


RELEASE VERSION 3.0.1 NOTES 
---------------------------
Date: 2002-10-05
-	Small bugfix in Easy-Query-Tools where clauses.

RELEASE VERSION 3.0.0 NOTES 
---------------------------
Date: 2002-10-02
-	MySQL Easy-Query-Tools to make query creation and database connection easier.
-	More installer options to avoid default mysqllib.dll installation.
-	Internal enhacements.
-	More examples and more clearer.
-	Examples in help.

RELEASE VERSION 2.1.2 NOTES 
---------------------------
Date: 2002-10-01
-	Installer fixed (samples doesn't appear correct with SDK-only option).

RELEASE VERSION 2.1.1 NOTES
---------------------------
-	MySQL documentation added.

RELEASE VERSION 2.1 NOTES
-------------------------
-	Example added.

RELEASE VERSION 2.0 NOTES
-------------------------
-	Due problems with MySQL++ MySQLDriver is now executed over 
	mylsqlLib.dll only.
-	Fixed errors with null fields.
-	All runs ok!!!!
-	Managed C++ code was removed.

RELEASE VERSION 1.01 NOTES
--------------------------
-	Minor fixes in documentation

RELEASE VERSION 1.00 NOTES
--------------------------
-	Basic funcionality.
-	NULL value is returned as string "NULL" instead of a null pointer.
-	Retrieving values as integers or strings may be ok but with other types
	would be unpredictable results.


