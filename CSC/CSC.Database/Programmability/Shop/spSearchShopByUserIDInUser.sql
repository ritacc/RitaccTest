/**********************************************************************/
-- Description:查找当前用户所属店 
---------------------------------------------------------------------
-- Action		Date			Staff		Version		Remarks
-- Create	 	2011-9-16   	JuLuDe		1.00.00
---------------------------------------------------------------------
/**********************************************************************/
create PROCEDURE spSearchShopByUserIDInUser
( 
	@SYS_CODE NVARCHAR(4),  
	@USER_ID         INT = 0,   --用户ID  
	@CURRENT_USER_ID BIGINT,   
	@ResultType  INT        OUTPUT,  
	@ResultMessage NVARCHAR(1000)     OUTPUT  
)  
AS   
BEGIN   
	SELECT 
		SY_SHOP.CODE,
		SY_SHOP.NAME,
		(SY_SHOP.PROV+SY_SHOP.CITY+SY_SHOP.AREA)AS AREA 
	FROM 
		SY_SHOP   
		INNER JOIN SY_USER_SHOP ON SY_USER_SHOP.SHOP_CODE = SY_SHOP.CODE  
	WHERE 
		SY_USER_SHOP.[USER_ID] = @USER_ID   
	AND SY_USER_SHOP.ACTIVE_FLAG = N'Y'
	AND SY_SHOP.CODE IN (SELECT DISTINCT SHOP_CODE FROM SY_USER_SHOP WHERE SY_USER_SHOP.[USER_ID]=@CURRENT_USER_ID and SY_USER_SHOP.ACTIVE_FLAG = N'Y' AND SY_USER_SHOP.SYS_CODE = @SYS_CODE)  	


	SET @ResultType = 0;  
	SET @ResultMessage = ''  

	RETURN @ResultType;  
END