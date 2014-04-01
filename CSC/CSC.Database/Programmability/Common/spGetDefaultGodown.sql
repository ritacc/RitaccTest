/**********************************************************************/
-- Description:	 
-- Remarks:	
---------------------------------------------------------------------
-- Action		Date			Staff		Version		Remarks
-- Create	 	2013-08-09		ZCS			1.00.10
/**********************************************************************/
CREATE PROCEDURE spGetDefaultGodown
(
	@SYS_CODE		NVARCHAR(4) = N'',
	@BU_CODE		NVARCHAR(10) = N'',
	@WH_CODE		NVARCHAR(8) = N'',
	@ResultType		INT				OUTPUT,
	@ResultMessage	NVARCHAR(1000)	OUTPUT
)
AS
BEGIN
	
	SELECT dbo.fnGetDefaultGodown(@SYS_CODE,@BU_CODE,@WH_CODE) as RESULT

	SET @ResultType = 0;
	SET @ResultMessage = N'SEARCH SUCCEED';
END