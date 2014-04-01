/**********************************************************************/
-- Description: Search Shop Code
---------------------------------------------------------------------
-- Action		Date			Staff		Version		Remarks
-- Create	 	2013-5-9    	LM		1.00.00
---------------------------------------------------------------------
/**********************************************************************/
CREATE PROCEDURE spSearchShopCodeForDDL
(
	@CODE				NVARCHAR(4) = N'',
	@SHOP_TYPE			NVARCHAR(10)= N'',
	@USER_ID		BIGINT,
	@SYS_CODE		NVARCHAR(4),
	@ResultType				INT			OUTPUT
	,@ResultMessage		NVARCHAR(1000)	OUTPUT
)
AS
BEGIN
	SELECT
		CODE,SHOP_TYPE
	FROM
		SY_SHOP
		INNER JOIN SY_USER_SHOP ON SY_SHOP.CODE = SY_USER_SHOP.SHOP_CODE AND SY_SHOP.SYS_CODE = SY_USER_SHOP.SYS_CODE
	WHERE 
		(@CODE=N'' or CharIndex(@CODE,CODE)>0) AND
		(@SHOP_TYPE=N'' or CharIndex(@SHOP_TYPE,SHOP_TYPE)>0)
		AND SY_SHOP.SYS_CODE = @SYS_CODE
		AND SY_USER_SHOP.[USER_ID] = @USER_ID
		AND SY_USER_SHOP.ACTIVE_FLAG = N'Y';
	SET @ResultType = 0;
	SET @ResultMessage = 'SEARCH SUCCEED';
	
END