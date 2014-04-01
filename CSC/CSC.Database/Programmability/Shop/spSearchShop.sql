/**********************************************************************/  
-- Description:  根据店代码查找店  
---------------------------------------------------------------------  
-- Action  Date   Staff  Version  Remarks  
-- Create   2011-9-1  JuLuDe	1.00.00  
-- Alter   2011-9-23  zjx			1.00.01  
-- Alter   2011-9-23  Kevin			1.00.01  
---------------------------------------------------------------------  
-- Field Description：  
  
/**********************************************************************/  
CREATE PROCEDURE [dbo].[spSearchShop]  
(   
	@USER_ID		BIGINT
	,@SYS_CODE		NVARCHAR(4)
	,@CODE          NVARCHAR(4)	
	,@ResultType	    INT				OUTPUT
	,@ResultMessage	NVARCHAR(1000)	OUTPUT 
)  
AS  
BEGIN  
 SELECT 
		SY_SHOP.SYS_CODE
		,CODE
		,NAME
		,ADDR_LINE1
		,ADDR_LINE2
		,PHONE_NO1
		,PHONE_NO2
		,FAX
		,EMAIL
		,WEB_URL
		,PROV
		,CITY
		,AREA
		,POSTAL_CODE
		,PHONE_AREA_CODE
		,SY_SHOP.LAST_UPDATE_DATE
		,SY_SHOP.LAST_UPDATED_BY
		,SY_SHOP.CREATION_DATE
		,SY_SHOP.CREATED_BY
		,SY_SHOP.LAST_UPDATE_LOGIN
		,MON_WK_DAY_FLAG
		,TUE_WK_DAY_FLAG
		,WED_WK_DAY_FLAG
		,THU_WK_DAY_FLAG
		,FRI_WK_DAY_FLAG
		,SAT_WK_DAY_FLAG
		,SUN_WK_DAY_FLAG
		,APPT_DAILY_QUOTA
		,FULL_NAME
		,ROWID
		,SY_SHOP.BU_CODE	
		,SHOP_TYPE
		,SY_PARAMETER_SHOP.PARA_S_VALUE AS DEFUALT_GODOWN
    FROM 
		SY_SHOP
		INNER JOIN SY_USER_SHOP ON SY_SHOP.CODE = SY_USER_SHOP.SHOP_CODE AND SY_SHOP.SYS_CODE = SY_USER_SHOP.SYS_CODE
		LEFT JOIN  SY_PARAMETER_SHOP ON SY_PARAMETER_SHOP.SHOP_CODE= SY_SHOP.CODE AND PARA_S_CODE='DFT_GODOWN_CSC'
	WHERE 
		(@CODE = N'' OR (@CODE <> N'' AND CODE LIKE N'%' + @CODE + N'%'))
		AND SY_SHOP.SYS_CODE = @SYS_CODE
		AND SY_USER_SHOP.[USER_ID] = @USER_ID
		AND SY_USER_SHOP.ACTIVE_FLAG = N'Y';
	SET @ResultType = 0;
	SET @ResultMessage = '';
	
	RETURN @ResultType;
END