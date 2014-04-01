﻿CREATE VIEW VW_CUSTOMER_SERVICE
AS
	SELECT SYS_CODE
		  ,CODE
		  ,NAME
		  ,FULL_NAME
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
		  ,MON_WK_DAY_FLAG
		  ,TUE_WK_DAY_FLAG
		  ,WED_WK_DAY_FLAG
		  ,THU_WK_DAY_FLAG
		  ,FRI_WK_DAY_FLAG
		  ,SAT_WK_DAY_FLAG
		  ,SUN_WK_DAY_FLAG
		  ,APPT_DAILY_QUOTA
		  ,BU_CODE
		  ,SHOP_TYPE
		  ,CREATED_BY
		  ,CREATION_DATE
		  ,LAST_UPDATED_BY
		  ,LAST_UPDATE_DATE
		  ,LAST_UPDATE_LOGIN
		  ,ROWID
		  ,AUTH_PWD
	FROM
		SY_SHOP
	WHERE
		UPPER(SHOP_TYPE) = N'CS';
	