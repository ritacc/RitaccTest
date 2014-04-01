/**********************************************************************/  
-- Description:Shearch shop Godown  
---------------------------------------------------------------------  
-- Action  Date   Staff  Version  Remarks  
-- Create   2013-4-26    zcs   1.00.00  
---------------------------------------------------------------------  
/**********************************************************************/  
  
CREATE PROCEDURE spShearchShopBySysCodeAndBuCode  
(  
	@USER_ID		BIGINT,
	@BU_CODE  NVarChar(20),  
	@SYS_CODE  NVarChar(8),  
	@ResultType     INT OUTPUT,  
	@ResultMessage NVARCHAR(1000) OUTPUT  
)  
AS  
BEGIN  
 select VW_GODOWN.* from VW_GODOWN 
 INNER JOIN SY_USER_SHOP ON VW_GODOWN.CODE = SY_USER_SHOP.SHOP_CODE AND VW_GODOWN.SYS_CODE = SY_USER_SHOP.SYS_CODE
 where VW_GODOWN.SYS_CODE=@SYS_CODE and VW_GODOWN.BU_CODE=@BU_CODE  
		AND SY_USER_SHOP.[USER_ID] = @USER_ID
		AND SY_USER_SHOP.ACTIVE_FLAG = N'Y';
  
SET @ResultType = 0;  
SET @ResultMessage = ''  
END