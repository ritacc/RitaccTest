
PRINT N'==================================================';
PRINT N'Drop all old programs';
PRINT N'==================================================';
/**********************************************************************/
-- Description:		Drop all store procedures
--------------------------------------------------------------------
-- Version:				1.00.00
-- CreateBy: 			Phoenix
-- CreateDate:		2008-01-21
/**********************************************************************/
PRINT N'Drop all store procedures starting...';
PRINT N'-----------------------------------------';
GO
DECLARE @command nvarchar(max);
WHILE EXISTS(SELECT name FROM sysobjects WHERE TYPE='P' AND name NOT LIKE N'sp[_]%' AND (name LIKE N'sp%' or name LIKE N'rsp%' OR name LIKE N'INIT[_]%') AND status>=0)
BEGIN
	SELECT TOP 1 
		@command = N'DROP PROCEDURE ' + name
	FROM
		sysobjects
	WHERE
		TYPE='P'
		AND name NOT LIKE N'sp[_]%'
		AND (name LIKE N'sp%' or name LIKE N'rsp%' OR name LIKE N'INIT[_]%')
		AND status>=0;
	EXEC(@command);
	PRINT @command;
END
GO
PRINT N'Drop store procedures succeed!';
PRINT N'-----------------------------------------';
GO
/**********************************************************************/
-- Description:		Drop all table functions
--------------------------------------------------------------------
-- Version:				1.00.00
-- CreateBy: 			Phoenix
-- CreateDate:		2008-01-21
/**********************************************************************/
PRINT N'Drop all table functions starting...';
PRINT N'-----------------------------------------';
GO
DECLARE @command nvarchar(max);
WHILE EXISTS(SELECT name FROM sysobjects WHERE TYPE=N'TF' AND name LIKE N'fn%' AND name NOT LIKE N'fn[_]%' AND status>=0)
BEGIN
	SELECT TOP 1 
		@command = N'DROP FUNCTION ' + name
	FROM   
		sysobjects    
	WHERE   
		TYPE=N'TF'
		AND name NOT LIKE N'fn[_]%'
		AND name LIKE N'fn%'
		AND status>=0;
	EXEC(@command);
	PRINT @command;
END
GO
PRINT N'Drop table functions succeed!';
PRINT N'-----------------------------------------';
GO
/**********************************************************************/
-- Description:		Drop all functions
--------------------------------------------------------------------
-- Version:				1.00.00
-- CreateBy: 			Phoenix
-- CreateDate:		2008-01-21
/**********************************************************************/
PRINT N'Drop all functions starting...';
PRINT N'-----------------------------------------';
GO
DECLARE @command nvarchar(max);
WHILE EXISTS(SELECT name FROM sysobjects WHERE TYPE='FN' AND name LIKE N'fn%' AND name NOT LIKE N'fn[_]%' AND status>=0)
BEGIN
	SELECT TOP 1 
		@command = N'DROP FUNCTION ' + name
	FROM   
		sysobjects    
	WHERE   
		TYPE = 'FN'
		AND name NOT LIKE N'fn[_]%'
		AND name LIKE N'fn%'
		AND status>=0;
	EXEC(@command);
	PRINT @command;
END
GO
PRINT N'Drop all functions succeed!';
PRINT N'-----------------------------------------';
GO
/**********************************************************************/
-- Description:		Drop all functions
--------------------------------------------------------------------
-- Version:				1.00.00
-- CreateBy: 			Phoenix
-- CreateDate:		2008-01-21
/**********************************************************************/
PRINT N'Drop all trigger starting...';
PRINT N'-----------------------------------------';
GO
DECLARE @command nvarchar(max);
SET @command = N'';
SELECT @command = @command+N'DROP TRIGGER '+name+N';
' FROM sys.triggers WHERE name LIKE N'TRG[_]%';
EXEC(@command);
PRINT @command;
GO
PRINT N'Drop all trigger succeed!';
PRINT N'-----------------------------------------';
GO
/**********************************************************************/
-- Description:		Drop all views
--------------------------------------------------------------------
-- Version:				1.00.00
-- CreateBy: 			Phoenix
-- CreateDate:		2012-07-18
/**********************************************************************/
PRINT N'Drop all view starting...';
PRINT N'-----------------------------------------';
GO
DECLARE @command nvarchar(max);
SET @command = N'';
SELECT @command = @command+N'DROP VIEW '+name+N';
' FROM sys.views WHERE name LIKE N'VW[_]%';
EXEC(@command);
PRINT @command;
GO
PRINT N'Drop all view succeed!';
GO

PRINT N'==================================================';
PRINT N'Create new programs';
PRINT N'==================================================';
GO