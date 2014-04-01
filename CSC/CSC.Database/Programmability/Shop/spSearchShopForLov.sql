/**********************************************************************/    
-- Description:  查找店-LOV    
---------------------------------------------------------------------    
-- Action  Date   Staff  Version  Remarks    
-- Create   2011-12-3  XJ 1.00.00    
---------------------------------------------------------------------    
-- Field Description：    
    
/**********************************************************************/    
CREATE PROCEDURE spSearchShopForLov
( 
@SYS_CODE NVARCHAR(4),
@ResultType     INT OUTPUT,
@ResultMessage NVARCHAR(1000) OUTPUT
)
AS
BEGIN
	SELECT 
	SYS_CODE,
	CODE,
	NAME,
	ADDR_LINE1,
	ADDR_LINE2,
	PHONE_NO1,
	PHONE_NO2,
	FAX,
	EMAIL,
	WEB_URL,
	PROV,
	CITY,
	AREA,
	POSTAL_CODE,
	PHONE_AREA_CODE,
	LAST_UPDATE_DATE,
	LAST_UPDATED_BY,
	CREATION_DATE,
	CREATED_BY,
	LAST_UPDATE_LOGIN,
	MON_WK_DAY_FLAG,
	TUE_WK_DAY_FLAG,
	WED_WK_DAY_FLAG,
	THU_WK_DAY_FLAG,
	FRI_WK_DAY_FLAG,
	SAT_WK_DAY_FLAG,
	SUN_WK_DAY_FLAG,
	APPT_DAILY_QUOTA,
	FULL_NAME,
	ROWID,
	BU_CODE,	
	SHOP_TYPE
		FROM 
	SY_SHOP
	WHERE 
	 SYS_CODE = @SYS_CODE
   
SET @ResultType = 0;
SET @ResultMessage = '';

RETURN @ResultType;
END
