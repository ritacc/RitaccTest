
/**********************************************************************/
-- Description:		GET Transaction Date
---------------------------------------------------------------------
-- Action		Date			Staff		Version		Remarks
-- Create	 	2011-12-03		XJ			1.00.00
/**********************************************************************/
CREATE FUNCTION fnGetTransactionDate
(
	@BU_CODE		nvarchar(10)
)
RETURNS datetime
AS
BEGIN
	DECLARE	@RetVar	datetime
	
	SELECT 
		@RetVar = CONVERT(DATETIME,CONVERT(nvarchar(10),TRAN_DATE,112),112)
	FROM 
		SY_TRANSACTION_DATE
	WHERE 
		 BU_CODE = @BU_CODE;
	
	RETURN ISNULL(@RetVar, getdate());
END
