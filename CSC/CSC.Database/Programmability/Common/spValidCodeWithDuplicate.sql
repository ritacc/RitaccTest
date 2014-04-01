/**********************************************************************/
-- Description:		Valid Duplicate code
---------------------------------------------------------------------
-- Action		Date			Staff		Version		Remarks
-- Create	 	2013-04-10		Lm			1.00.00
/**********************************************************************/
CREATE PROC spValidCodeWithDuplicate(  
@tbname nvarchar(400),  
@columnName nvarchar(100),  
@valName nvarchar(100),  
@otherCon nvarchar(400)=N'',--另外的附加条件  
@count int OUTPUT)  
AS  
BEGIN  
  
DECLARE @SQL NVARCHAR(1000)  
DECLARE @PARAM NVARCHAR(400)  
  
SET @SQL = N'SELECT @COUNT = COUNT(1) FROM '+QUOTENAME(@tbname)+' WHERE '+@columnName+' = @valName ' +@otherCon  
  
SET @PARAM = N'@COUNT INT output,@valName nvarchar(100)'  
exec sp_executesql @SQL,@PARAM,@count output,@valName  
  
END

GO

--带bu_code
CREATE PROC spValidCodeWithDuplicate2(  
@BU_CODE nvarchar(10),
@tbname nvarchar(400),
@columnName nvarchar(100),
@valName nvarchar(100),
@otherCon nvarchar(400)=N'',--另外的附加条件  
@count int OUTPUT)
AS  
BEGIN  
  
DECLARE @SQL NVARCHAR(1000)  
DECLARE @PARAM NVARCHAR(400)  
  
SET @SQL = N'SELECT @COUNT = COUNT(1) FROM '+QUOTENAME(@tbname)+' WHERE '+@columnName+' = @valName '+N' AND BU_CODE = '''+@BU_CODE+''' '+@otherCon  
  
SET @PARAM = N'@COUNT INT output,@valName nvarchar(100)'  
exec sp_executesql @SQL,@PARAM,@count output,@valName  
  
END