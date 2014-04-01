/**********************************************************************/
-- Description: 获取与指定日期同一天的最小日期时间。
---------------------------------------------------------------------
-- Action		Date			Staff		Version		Remarks
-- Modify		2012-10-09		Zhangbo		2.00.00		支持NULL值，并采用ISO格式转换
-- Create		2011-11-14		xj			1.00.00
/**********************************************************************/
CREATE FUNCTION fnGetMinDateTime
(
	@DATE datetime
)
RETURNS datetime
AS
BEGIN
	IF (@DATE IS NULL)
	BEGIN
		RETURN NULL;
	END
	-- 强制采用ISO格式(YYYYMMDD)进行转换，可忽略语言问题
	RETURN CONVERT(datetime,CONVERT(nvarchar(10),@DATE,112),112);
END