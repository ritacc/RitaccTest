-- =============================================
-- Script Template
-- =============================================
-- =============================================
-- Description:		根据店代码查找店
---------------------------------------------------------------------
-- Action		Date			Staff		Version		Remarks
-- Create	 	2012-1-13		LHL		1.00.00 

---------------------------------------------------------------------
-- =============================================


CREATE PROCEDURE [dbo].[spLoadShopView]
( 
	@SYS_CODE		NVARCHAR(4),     --系统代码
	@CODE           NVARCHAR(4),     --店代码
	@ResultType	    INT				OUTPUT,
	@ResultMessage	NVARCHAR(1000)	OUTPUT
)
AS
BEGIN
	SELECT 
		SYS_CODE
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
		,(select FULL_NAME  from  SY_PROVINCE Where SY_PROVINCE.CODE=SY_SHOP.PROV) As PROVNAME
		,CITY
		,(select FULL_NAME from  SY_CITY WHERE CODE=SY_SHOP.CITY) As CITYNAME
		,AREA
		,(
		     SELECT FULL_NAME FROM SY_AREA WHERE 
		    --(ISNULL(SY_SHOP.PROV,'') ='' OR CITY_PROV_CODE = SY_SHOP.PROV)
		    --(ISNULL(SY_SHOP.CITY_CODE,'') ='' OR CITY_PROV_CODE = SY_SHOP.CITY_CODE)
		    --AND 
		     CODE=SY_SHOP.AREA		
		) AS AREANAME
		,POSTAL_CODE
		,PHONE_AREA_CODE
		,LAST_UPDATE_DATE
		,LAST_UPDATED_BY
		,CREATION_DATE
		,CREATED_BY
		,LAST_UPDATE_LOGIN
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
		,BU_CODE	
		,SHOP_TYPE
		,AUTH_PWD
    FROM 
		SY_SHOP
	WHERE 
		CODE = @CODE
		AND  SYS_CODE = @SYS_CODE;
		   
	SET @ResultType = 0;
	SET @ResultMessage = '';
	
	RETURN @ResultType;
END

GO

