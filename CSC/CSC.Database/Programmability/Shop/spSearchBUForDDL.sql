
CREATE PROCEDURE [dbo].[spSearchBUForDDL]
(
	@ResultType				INT				OUTPUT
	,@ResultMessage		NVARCHAR(1000)	OUTPUT
)
AS
BEGIN
	SELECT
		*
	FROM
		BUSINESS_UNIT

	SET @ResultType = 0;
	SET @ResultMessage = 'SEARCH SUCCEED';
	RETURN @ResultType;
END