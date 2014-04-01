/**********************************************************************/
-- Description:查找当前用户所属店 
---------------------------------------------------------------------
-- Action		Date			Staff		Version		Remarks
-- Create	 	2011-9-16   	JuLuDe		1.00.00
---------------------------------------------------------------------
/**********************************************************************/
CREATE PROCEDURE spSearchShopByUserID
(
	 @UserID INT = 0--用户ID 
	,@ResultType INT OUTPUT
	,@ResultMessage NVARCHAR(1000) OUTPUT  
)
AS 
BEGIN   
    SELECT
        SY_SHOP.CODE
       ,SY_SHOP.NAME
       ,(SY_SHOP.PROV + SY_SHOP.CITY + SY_SHOP.AREA) AS AREA
    FROM
        SY_SHOP
        INNER JOIN SY_USER_SHOP ON SY_USER_SHOP.SHOP_CODE = SY_SHOP.CODE
    WHERE
        SY_USER_SHOP.[USER_ID] = @UserID
        AND SY_USER_SHOP.ACTIVE_FLAG = N'Y' ;

    SET @ResultType = 0 ;  
    SET @ResultMessage = ''  

    RETURN @ResultType ;  
END