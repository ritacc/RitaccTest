CREATE PROCEDURE [dbo].[spSearchShopInUser]
(
	 @SYS_CODE NVARCHAR(4)
	,@USER_ID INT = 0
	,@USER_TYPE NVARCHAR(2)
	,@SHOP_CODE NVARCHAR(4)
	,@CURRENT_USER_ID BIGINT
	,@ResultType INT OUTPUT
	,@ResultMessage NVARCHAR(1000) OUTPUT    
)
AS 
BEGIN   
  --如果当前用户是系统管理员，选择全部店  
    IF (@USER_TYPE = N'SA') 
        BEGIN    
            SELECT
                SY_SHOP.CODE
               ,SY_SHOP.NAME
               --added by jason in 20121219 for TSYF02010#06.doc
               ,SY_SHOP.FULL_NAME
               --end added by jason in 20121219 for TSYF02010#06.doc
               
               --commented by jason in 20121219 for TSYF02010#06.doc
               --,SY_AREA.FULL_NAME
               --end commented by jason in 20121219 for TSYF02010#06.doc
               ,dbo.fnConvertNvarcharToBit(SY_USER_SHOP.ACTIVE_FLAG) AS ACTIVE_FLAG
               --,CASE WHEN EXISTS ( SELECT
               --                     1
               --                    FROM
               --                     WP_ASSIGNMENT
               --                    WHERE
               --                     WP_ASSIGNMENT.SYS_CODE = @SYS_CODE
               --                     AND WP_ASSIGNMENT.SHOP_CODE = SY_SHOP.CODE
               --                     AND WP_ASSIGNMENT.[USER_ID] = @USER_ID ) THEN N'Y'
               --      ELSE N'N'
               -- END AS ASSIGNMENT_FLAG
			   ,SY_SHOP.BU_CODE
            FROM
                SY_SHOP
                LEFT OUTER JOIN SY_USER_SHOP ON SY_USER_SHOP.SHOP_CODE = SY_SHOP.CODE
                                                AND SY_USER_SHOP.[USER_ID] = @USER_ID
                                                AND SY_USER_SHOP.ACTIVE_FLAG = N'Y'
                                                AND SY_USER_SHOP.SYS_CODE = @SYS_CODE
                --commented by jason in 20121219 for TSYF02010#06.doc
                --LEFT OUTER JOIN SY_AREA ON SY_AREA.CITY_PROV_CODE = SY_SHOP.PROV
                --                           AND SY_AREA.CITY_CODE = SY_SHOP.CITY
                --                           AND SY_AREA.CODE = SY_SHOP.AREA
				--end commented by jason in 20121219 for TSYF02010#06.doc
            WHERE
                SY_SHOP.SYS_CODE = @SYS_CODE
                AND SY_SHOP.CODE IN (SELECT DISTINCT
                                        SHOP_CODE
                                     FROM
                                        SY_USER_SHOP
                                     WHERE
                                        SY_USER_SHOP.[USER_ID] = @CURRENT_USER_ID
                                        AND SY_USER_SHOP.ACTIVE_FLAG = N'Y'
                                        AND SY_USER_SHOP.SYS_CODE = @SYS_CODE)
            ORDER BY
                SY_USER_SHOP.ACTIVE_FLAG DESC
               ,SY_SHOP.CODE ASC  
      
        END  
    ELSE
		--如果当前用户是公司用户，选择当前店  
        BEGIN  
            SELECT
                SY_SHOP.CODE
               ,SY_SHOP.NAME
                --added by jason in 20121219 for TSYF02010#06.doc
               ,SY_SHOP.FULL_NAME
               --end added by jason in 20121219 for TSYF02010#06.doc
               
               --commented by jason in 20121219 for TSYF02010#06.doc
               --,SY_AREA.FULL_NAME
               --end commented by jason in 20121219 for TSYF02010#06.doc
               ,dbo.fnConvertNvarcharToBit(SY_USER_SHOP.ACTIVE_FLAG) AS ACTIVE_FLAG
               --,CASE WHEN EXISTS ( SELECT
               --                     1
               --                    FROM
               --                     WP_ASSIGNMENT
               --                    WHERE
               --                     WP_ASSIGNMENT.SYS_CODE = @SYS_CODE
               --                     AND WP_ASSIGNMENT.SHOP_CODE = SY_SHOP.CODE
               --                     AND WP_ASSIGNMENT.[USER_ID] = @USER_ID ) THEN N'Y'
               --      ELSE N'N'
               -- END AS ASSIGNMENT_FLAG
			   ,SY_SHOP.BU_CODE
            FROM
                SY_SHOP
                LEFT OUTER JOIN SY_USER_SHOP ON SY_USER_SHOP.SHOP_CODE = SY_SHOP.CODE
                                                AND SY_USER_SHOP.[USER_ID] = @USER_ID
                                                AND SY_USER_SHOP.ACTIVE_FLAG = N'Y'
                                                AND SY_USER_SHOP.SYS_CODE = @SYS_CODE
				--commented by jason in 20121219 for TSYF02010#06.doc
                --LEFT OUTER JOIN SY_AREA ON SY_AREA.CITY_PROV_CODE = SY_SHOP.PROV
                --                           AND SY_AREA.CITY_CODE = SY_SHOP.CITY
                --                           AND SY_AREA.CODE = SY_SHOP.AREA
				--end commented by jason in 20121219 for TSYF02010#06.doc
            WHERE
                SY_SHOP.SYS_CODE = @SYS_CODE
                AND SY_SHOP.CODE = @SHOP_CODE
            ORDER BY
                SY_USER_SHOP.ACTIVE_FLAG DESC
               ,SY_SHOP.CODE ASC  
        END  
 
    SET @ResultType = 0 ;    
    SET @ResultMessage = ''    
      
    RETURN @ResultType ;    
END  