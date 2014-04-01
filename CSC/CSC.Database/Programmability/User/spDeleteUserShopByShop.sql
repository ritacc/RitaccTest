-- =============================================  
-- Author		: Jason  
-- Create date	: 2012-09-13  
-- Description	: added by Jason in 20120913 for remove user role list in add/edit page 
-- =============================================  
CREATE PROCEDURE [dbo].[spDeleteUserShopByShop]
(
	 @USER_ID BIGINT
	,@SHOP_CODE NVARCHAR(4)
	,@ResultType INT OUTPUT
	,@ResultMessage NVARCHAR(1000) OUTPUT  
)
AS 
BEGIN  
 
    DELETE FROM
        dbo.SY_USER_SHOP
    WHERE
        [USER_ID] = @USER_ID
        AND dbo.SY_USER_SHOP.SHOP_CODE = @SHOP_CODE;
    
    SET @ResultType = 0 ;  
    SET @ResultMessage = '';
    
    RETURN @ResultType ;  
END