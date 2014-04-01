/**********************************************************************/
-- Description:		获取用户店的个数
---------------------------------------------------------------------
-- Action		Date			Staff		Version		Remarks
-- Create	 	2012-03-08		ZJX			1.00.00
---------------------------------------------------------------------
/**********************************************************************/
CREATE PROCEDURE [dbo].[spGetUserShopCount]
(
	 @SYS_CODE NVARCHAR(4)
	,@USER_ID BIGINT = 0
	,@USER_SHOP_COUNT INT OUTPUT
	,@ResultType INT OUTPUT
	,@ResultMessage NVARCHAR(1000) OUTPUT  
)
AS 
BEGIN
	
	SELECT 
		@USER_SHOP_COUNT = COUNT(1)
	FROM
		dbo.SY_USER_SHOP
	WHERE
		dbo.SY_USER_SHOP.SYS_CODE = @SYS_CODE
		AND dbo.SY_USER_SHOP.USER_ID = @USER_ID;
  
    SET @ResultType = 0 ;  
    SET @ResultMessage = 'GET SUCCEED';
    
    RETURN @ResultType ;  
END