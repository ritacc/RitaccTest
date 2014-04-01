/**********************************************************************/  
-- Description:  查找店  
---------------------------------------------------------------------  
-- Action  Date   Staff  Version  Remarks  
-- 2013-10-23	  JLD
---------------------------------------------------------------------  
-- Field Description：  
  
/**********************************************************************/  
CREATE PROCEDURE spSearchShopFromTechnician
(   
	@SYS_CODE		NVARCHAR(4)		,
	@SHOP_TYPE		NVARCHAR(20)	= NULL,
	@BU_CODE		NVARCHAR(20)	= NULL,
	@ResultType	    INT				OUTPUT,
	@ResultMessage	NVARCHAR(1000)	OUTPUT 
)  
AS  
BEGIN  
	SELECT
		TOP 1 *
	FROM
		SY_SHOP
	WHERE
		SYS_CODE = @SYS_CODE
		AND SHOP_TYPE = @SHOP_TYPE
		AND BU_CODE = @BU_CODE
		
	SET @ResultType = 0
	SET @ResultMessage = ''
	
	RETURN @ResultType;
END