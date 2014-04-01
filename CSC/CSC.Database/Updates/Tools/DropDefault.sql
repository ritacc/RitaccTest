/**********************************************************************/
-- Description:	删除指定表和指定字段上的默认值约束
---------------------------------------------------------------------
-- Action		Date			Staff		Version		Remarks
-- Create	 	2013-10-09		Zhangbo		1.00.00
/**********************************************************************/
DECLARE @TABLE_NAME		sysname	= N'*****';
DECLARE @FIELD_NAME		sysname	= N'*****';
DECLARE @DEFAULT_NAME	sysname;
SELECT @DEFAULT_NAME=name FROM sysobjects WHERE id = (SELECT syscolumns.cdefault FROM sysobjects INNER JOIN syscolumns ON sysobjects.Id=syscolumns.Id WHERE sysobjects.name=@TABLE_NAME AND syscolumns.name=@FIELD_NAME)
IF (@DEFAULT_NAME IS NOT NULL)
BEGIN
	DECLARE @command nvarchar(max)=N'ALTER TABLE ['+@TABLE_NAME+N'] DROP CONSTRAINT '+@DEFAULT_NAME;
	EXEC(@command);
	PRINT N'DROP CONSTRAINT '+@DEFAULT_NAME
END
GO