CREATE PROCEDURE [dbo].[spSearchUserRoleShopByShop]
(
	 @SYS_CODE NVARCHAR(4)
	,@USER_ID INT = 0
	,@USER_TYPE NVARCHAR(2)
	,@SHOP_CODE NVARCHAR(4)
	,@CURRENT_SHOP_CODE NVARCHAR(4)
	,@CURRENT_USER_ID BIGINT
	,@ResultType INT OUTPUT
	,@ResultMessage NVARCHAR(1000) OUTPUT  
)
AS 
BEGIN 
	--如果当前用户是系统管理员，选择全部系统层面的角色
    IF (@USER_TYPE = N'SA') 
        BEGIN	 
            SELECT
                SY_ROLE.ROLE_CODE
               ,SY_ROLE.ROLE_DSC
               ,SY_ROLE.ROLE_SDSC
               ,SY_ROLE.ROLE_TYPE
               ,SY_ROLE.SHOP_CODE
               ,SY_SHOP.NAME AS SHOP_NAME
               ,SY_ROLE.SYSTEM_SCOPE
               ,dbo.fnConvertNvarcharToBit(SY_ROLE.ADMIN_FLAG) AS ADMIN_FLAG
               ,dbo.fnConvertNvarcharToBit(SY_USER_ROLE_SHOP.ACTIVE_FLAG) AS ACTIVE_FLAG
               ,SY_ROLE.ROLE_ID
			   ,SY_ROLE.BU_CODE
            FROM
                SY_ROLE
                LEFT OUTER JOIN SY_USER_ROLE_SHOP ON SY_USER_ROLE_SHOP.ROLE_ID = SY_ROLE.ROLE_ID
                                                     AND SY_USER_ROLE_SHOP.[USER_ID] = @USER_ID
                                                     AND SY_USER_ROLE_SHOP.ACTIVE_FLAG = N'Y'
                                                     AND SY_USER_ROLE_SHOP.SHOP_CODE = @SHOP_CODE
                LEFT OUTER JOIN SY_SHOP ON SY_SHOP.CODE = SY_ROLE.SHOP_CODE
            WHERE
                SY_ROLE.SYS_CODE = @SYS_CODE
                --AND SY_ROLE.ROLE_TYPE = N'SY'
                AND (SY_ROLE.ROLE_TYPE = N'SY'
					OR SY_SHOP.CODE IN (SELECT DISTINCT
											SHOP_CODE
										 FROM
											SY_USER_SHOP
										 WHERE
											SY_USER_SHOP.[USER_ID] = @CURRENT_USER_ID
											AND SY_USER_SHOP.ACTIVE_FLAG = N'Y'
											AND SY_USER_SHOP.SYS_CODE = @SYS_CODE
											AND dbo.SY_USER_SHOP.SHOP_CODE = @SHOP_CODE)
					)
                AND SY_ROLE.FROZEN_FLAG = N'N'
            ORDER BY
                SY_USER_ROLE_SHOP.ACTIVE_FLAG DESC
               ,SY_ROLE.ROLE_TYPE DESC
               ,SY_ROLE.ROLE_CODE ASC
	   
        END
    ELSE
		--如果当前用户是公司用户，选择系统层面角色+当前店对应的角色
        BEGIN
            SELECT
                SY_ROLE.ROLE_CODE
               ,SY_ROLE.ROLE_DSC
               ,SY_ROLE.ROLE_SDSC
               ,SY_ROLE.ROLE_TYPE
               ,SY_ROLE.SHOP_CODE
               ,SY_SHOP.NAME AS SHOP_NAME
               ,SY_ROLE.SYSTEM_SCOPE
               ,dbo.fnConvertNvarcharToBit(SY_ROLE.ADMIN_FLAG) AS ADMIN_FLAG
               ,dbo.fnConvertNvarcharToBit(SY_USER_ROLE_SHOP.ACTIVE_FLAG) AS ACTIVE_FLAG
               ,SY_ROLE.ROLE_ID
			   ,SY_ROLE.BU_CODE
            FROM
                SY_ROLE
                LEFT OUTER JOIN SY_USER_ROLE_SHOP ON SY_USER_ROLE_SHOP.ROLE_ID = SY_ROLE.ROLE_ID
                                                     AND SY_USER_ROLE_SHOP.[USER_ID] = @USER_ID
                                                     AND SY_USER_ROLE_SHOP.ACTIVE_FLAG = N'Y'
                                                     AND SY_USER_ROLE_SHOP.SHOP_CODE = @SHOP_CODE
                LEFT OUTER JOIN SY_SHOP ON SY_SHOP.CODE = SY_ROLE.SHOP_CODE
            WHERE
                SY_ROLE.SYS_CODE = @SYS_CODE
                AND SY_ROLE.FROZEN_FLAG = N'N'
                AND (SY_ROLE.ROLE_TYPE = N'SY'
                     OR SY_ROLE.SHOP_CODE = @CURRENT_SHOP_CODE
                    )
                --AND SY_ROLE.SHOP_CODE = @CURRENT_SHOP_CODE
            ORDER BY
                SY_USER_ROLE_SHOP.ACTIVE_FLAG DESC
               ,SY_ROLE.ROLE_TYPE DESC
               ,SY_ROLE.ROLE_CODE ASC
        END

    SET @ResultType = 0 ;  
    SET @ResultMessage = ''  
    
    RETURN @ResultType ;  
END
 GO