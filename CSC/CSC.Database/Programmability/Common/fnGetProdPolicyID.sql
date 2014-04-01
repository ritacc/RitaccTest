
/**********************************************************************/
-- Description:	 Get CustProd PolicyID
---------------------------------------------------------------------
-- Action		Date			Staff		Version		Remarks
-- Create	 	2013-09-02		TuJiang		1.00.00
---------------------------------------------------------------------
/**********************************************************************/
CREATE FUNCTION fnGetProdPolicyID
(
	@POLICY_NO		NVARCHAR(10),
	@BU_CODE		NVARCHAR(10),
	@DN_DATE		DATETIME
)
RETURNS BIGINT
AS
BEGIN
	DECLARE @POLICY_ID BIGINT, @TranDate DATETIME, @AgeYear INT
	SET @AgeYear = 0;

	SELECT @TranDate = CAST(CONVERT(VARCHAR(10), dbo.fnGetTransactionDate(@BU_CODE), 120) AS DATETIME);
	--SET @DN_DATE = CAST(CONVERT(VARCHAR(10), DATEADD(YEAR, 1, @DN_DATE), 120) AS DATETIME);  OR  @DN_DATE > @TranDate

	IF (ISNULL(@POLICY_NO, N'') = N'') BEGIN
		RETURN -1;
	END

	WHILE (@DN_DATE <= @TranDate)
	BEGIN
		SET @AgeYear = @AgeYear + 1;
		SET @DN_DATE = DATEADD(YEAR, 1, @DN_DATE);
	END

	SELECT 
		@POLICY_ID = dbo.POLICY.POLICY_ID 
	From 
		dbo.POLICY 
	Where 
		dbo.POLICY.POLICY_NO = @POLICY_NO
		AND @AgeYear >= dbo.POLICY.AGE_YEAR_FROM
		AND @AgeYear <= dbo.POLICY.AGE_YEAR_TO

	RETURN ISNULL(@POLICY_ID, -1);

END