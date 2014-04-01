CREATE PROCEDURE spGetTransactionDate
(
	@BU_CODE		nvarchar(10)
	,@ResultType	INT				OUTPUT
	,@ResultMessage	NVARCHAR(1000)	OUTPUT
)
AS
BEGIN
	
	SELECT dbo.fnGetTransactionDate(@BU_CODE) as RESULT

	SET @ResultType = 0;
	SET @ResultMessage = N'SEARCH SUCCEED';
END