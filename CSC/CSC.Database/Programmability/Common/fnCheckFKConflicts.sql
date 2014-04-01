/**********************************************************************/
-- Description:		Check foreign key conflicts
---------------------------------------------------------------------
-- Action		Date			Staff		Version		Remarks
-- Create	 	2011-07-22		Zhangbo		1.00.00		仅用于外键设计完备的前提下
/**********************************************************************/
CREATE FUNCTION fnCheckFKConflicts
(
	@TABLE_NAME		sysname,
	@ID				int
) RETURNS bit
AS
BEGIN
	DECLARE @Result	bit = 0;

	DECLARE fks_cursor CURSOR FOR
		SELECT 
			FKTable.name	AS FKTableName
			,FKColumn.name	AS FKColName
		FROM 
			sysforeignkeys
			INNER JOIN sysobjects FKTable ON sysforeignkeys.fkeyid = FKTable.id 
			INNER JOIN syscolumns FKColumn ON sysforeignkeys.fkeyid = FKColumn.id and sysforeignkeys.fkey = FKColumn.colid
			INNER JOIN sysobjects PKTable ON sysforeignkeys.rkeyid = PKTable.id 
		WHERE
			PKTable.name=@TABLE_NAME;

	DECLARE @FKTableName sysname;
	DECLARE @FKColName sysname;

	OPEN fks_cursor;
	FETCH NEXT FROM fks_cursor INTO @FKTableName, @FKColName;
	WHILE (@@FETCH_STATUS <> -1)
	BEGIN;
		DECLARE @SQLCommand nvarchar(4000);
		SET @SQLCommand = 'IF EXISTS(SELECT 1 FROM ' + @FKTableName + ' WHERE ' + @FKColName + ' = ' + CONVERT(nvarchar(20), @ID) + ') SET @Result=1;'
		EXECUTE sp_executesql @SQLCommand, N'@Result bit output ', @Result OUTPUT;
		IF (@Result=0)
			FETCH NEXT FROM fks_cursor INTO @FKTableName, @FKColName;
		ELSE
			BREAK;
	END;
	CLOSE fks_cursor;
	DEALLOCATE fks_cursor;

	RETURN @Result;
END