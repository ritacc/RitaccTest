/**********************************************************************/
-- Description:		验证修改某条记录时，该记录是否曾经被修改过
---------------------------------------------------------------------
-- Action		Date			Staff		Version		Remarks
-- Create	 	2013-04-07		LM			1.00.00
/**********************************************************************/
CREATE Proc spValidLastUpdate(
@tbname nvarchar(128),--表名
@IDNAME NVARCHAR(100),--表的ID字段名
@IDVALUE BIGINT,--ID的值
@LAST_UPDATE_DATE_NAME NVARCHAR(50) = N'LAST_UPDATE_DATE',--最后修改时间的字段名
@LAST_UPDATE_DATE datetime --最后修改时间的值
)
AS
BEGIN
	DECLARE @SQLSTR NVARCHAR(1000)
	DECLARE @PARAM NVARCHAR(400)
	DECLARE @RESULT INT = 0
	SET @SQLSTR = N'SELECT @RESULT = COUNT(1) FROM '+QUOTENAME(@tbname)+' WHERE '+CAST(@IDNAME AS NVARCHAR(100))+' = @IDVALUE AND (DATEDIFF(S,'+CAST(@LAST_UPDATE_DATE_NAME as nvarchar(50))+',@LAST_UPDATE_DATE) = 0)'
	SET @PARAM = N'@RESULT INT OUTPUT,@IDVALUE BIGINT,@LAST_UPDATE_DATE DATETIME'
	EXECUTE SP_EXECUTESQL @SQLSTR,@PARAM,@RESULT OUTPUT,@IDVALUE,@LAST_UPDATE_DATE
	RETURN @RESULT
END