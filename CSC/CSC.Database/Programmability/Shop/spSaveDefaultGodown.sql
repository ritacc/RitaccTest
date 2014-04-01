-- Description:Shearch shop Godown
---------------------------------------------------------------------
-- Action		Date			Staff		Version		Remarks
-- Create	 	2013-8-09   	zcs			1.00.00
---------------------------------------------------------------------
/**********************************************************************/

CREATE PROCEDURE spSaveDefaultGodown
(
	@GodownID		BIGINT,
	@CODE			NVarChar(10),

	@ResultType     INT OUTPUT,
	@ResultMessage NVARCHAR(1000) OUTPUT
)
AS
BEGIN
	
	DECLARE @GODWONCODE  NVARCHAR(50)

	SELECT @GODWONCODE= GODOWN.WAREHOUSE_CODE+'.' + GODOWN.GODOWN_CODE  FROM GODOWN WHERE GODOWN.GODOWN_ID=@GodownID

	UPDATE 
		SY_PARAMETER_SHOP
	SET
		PARA_S_VALUE=@GODWONCODE
	WHERE 
		SHOP_CODE=@CODE


SET @ResultType = 0;
SET @ResultMessage = ''
END