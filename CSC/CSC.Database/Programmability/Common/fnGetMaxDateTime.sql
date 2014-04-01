/**********************************************************************/
-- Description: 获取与指定日期同一天的最大日期时间。
---------------------------------------------------------------------
-- Action		Date			Staff		Version		Remarks
-- Modify		2012-10-09		Zhangbo		2.00.00		支持NULL值，并采用ISO格式转换
-- Create		2011-11-14		xj			1.00.00
/**********************************************************************/
CREATE FUNCTION fnGetMaxDateTime
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
	RETURN CONVERT(datetime,CONVERT(nvarchar(10),@DATE,112)+' 23:59:59.998',112);
END

--2013-12-20 ZCS
GO	
CREATE FUNCTION fnGetMonthMaxDateTime
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
	DECLARE @NEWDATE DATETIME=DATEADD(DD,(DAY(@DATE)*-1 +1),@DATE)
	SET @NEWDATE=DATEADD(M, 1,@NEWDATE)
	SET @NEWDATE=DATEADD(S, -1,@NEWDATE)

	RETURN @NEWDATE
END

GO
CREATE FUNCTION fnGetMonthMinDateTime
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
	DECLARE @NEWDATE DATETIME=DATEADD(DD,(DAY(@DATE)*-1 +1),@DATE)
	SET @NEWDATE= CONVERT(DATETIME,CONVERT(nvarchar(10),@NEWDATE,112),112);
	RETURN @NEWDATE
END
