/**********************************************************************/
-- Description:格式化整数为等宽数字文本，前面使用 0 补齐。例如：2=>002
---------------------------------------------------------------------
-- Action		Date			Staff		Version		Remarks
-- Modify		2013-01-03		Zhangbo		2.00.00		仅用于正整数或者0
-- Create		2012-11-08		xj			1.00.00
/**********************************************************************/
CREATE FUNCTION fnToNeatInteger
(
	@VALUE		bigint
	,@LENGTH	int
)
RETURNS nvarchar(20)
AS
BEGIN
	IF(@VALUE IS NULL)
	BEGIN
		RETURN NULL;
	END
	IF(@LENGTH<=0 OR @VALUE<0)
	BEGIN
		RETURN LTRIM(RTRIM(CAST(@VALUE AS nvarchar)));
	END
	RETURN RIGHT(N'00000000000000000000'+LTRIM(RTRIM(CAST(@VALUE AS nvarchar))),@LENGTH);
END