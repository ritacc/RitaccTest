﻿CREATE PROCEDURE spGetParameter
	@SYS_CODE		NVARCHAR(4)
	,@SHOP_CODE		NVARCHAR(4)
	,@BU_CODE		nvarchar(10)
	,@CODE			NVARCHAR(30)
	,@ResultType	INT				OUTPUT
	,@ResultMessage	NVARCHAR(1000)	OUTPUT
AS
BEGIN
	DECLARE @RESULT NVARCHAR(30)
	EXEC @RESULT = dbo.[fnGetParameterValue] @SYS_CODE,@SHOP_CODE,@BU_CODE,@CODE
	SELECT @RESULT as RESULT
	SET @ResultType = 0;
	SET @ResultMessage = N'SEARCH SUCCEED';
END