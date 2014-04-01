/**********************************************************************/
-- Description:		查找角色
---------------------------------------------------------------------
-- Action		Date			Staff		Version		Remarks
-- Create	 	2011-9-13  		JuLuDe		1.00.00
-- Modify	 	2012-04-05  	Jason		1.01.00
---------------------------------------------------------------------
/**********************************************************************/
CREATE PROCEDURE [dbo].[spSearchRole]
(
	 @SYS_CODE NVARCHAR(4)
	,@ROLE_ID INT = 0	--角色ID
	,--@ROLE_DSC_FROM	nvarchar(50),
	 --@ROLE_DSC_TO	nvarchar(50),
	 @ROLE_CODE NVARCHAR(20)
	,@ROLE_TYPE NVARCHAR(2)
	,@ADMIN_FLAG BIT = NULL
	,@USER_TYPE NVARCHAR(2)
	,@SHOP_CODE NVARCHAR(4)
	,@ResultType INT OUTPUT
	,@ResultMessage NVARCHAR(1000) OUTPUT  
)
AS 
BEGIN 
  --如果当前用户是系统管理员，选择全部角色
    IF (@USER_TYPE = N'SA') 
        BEGIN	 
            SELECT
                SY_ROLE.ROLE_ID
               ,upper(SY_ROLE.ROLE_CODE) as ROLE_CODE
               ,SY_ROLE.ROLE_SDSC
               ,SY_ROLE.ROLE_DSC
               ,SY_ROLE.ROLE_TYPE
               ,SY_ROLE.SHOP_CODE
               ,SY_ROLE.SYSTEM_SCOPE
               ,dbo.fnConvertNvarcharToBit(SY_ROLE.ADMIN_FLAG) AS ADMIN_FLAG
               ,SY_ROLE.CREATION_DATE
               ,dbo.fnConvertNvarcharToBit(SY_ROLE.FROZEN_FLAG) AS FROZEN_FLAG
               ,SY_ROLE.FROZEN_DATE
               ,--case when ROLE_TYPE = N'SY' then '' else (SELECT NAME FROM SY_SHOP where SY_SHOP.CODE = SY_ROLE.SHOP_CODE) end as SHOP_NAME
                SY_SHOP.NAME AS SHOP_NAME
               ,@USER_TYPE AS USER_TYPE
               ,SY_ROLE.LAST_UPDATE_DATE
               ,SY_ROLE.LAST_UPDATED_BY
			   ,SY_ROLE.BU_CODE
            FROM
                SY_ROLE
                LEFT OUTER JOIN SY_SHOP ON SY_SHOP.CODE = SY_ROLE.SHOP_CODE
            WHERE
                SY_ROLE.SYS_CODE = @SYS_CODE
                AND SY_ROLE.ROLE_ID = @ROLE_ID
                OR @ROLE_ID = 0
                --and (@ROLE_DSC_FROM = N'' or SY_ROLE.ROLE_DSC >=  @ROLE_DSC_FROM)
                --and (@ROLE_DSC_TO = N'' or SY_ROLE.ROLE_DSC <=  @ROLE_DSC_TO)
                AND (@ROLE_CODE = N'' OR upper(SY_ROLE.ROLE_CODE) LIKE '%' + upper(@ROLE_CODE) + '%')
                --commented by jason in 2012-04-05
                --AND (@ROLE_TYPE = N'' OR SY_ROLE.ROLE_TYPE = @ROLE_TYPE )
                --end commented by jason in 2012-04-05
                --added by jason in 2012-04-05
                AND (
						(@ROLE_TYPE = N'' AND (SY_ROLE.ROLE_TYPE = N'SY' OR SY_ROLE.SHOP_CODE = @SHOP_CODE))
						OR (@ROLE_TYPE = N'SY' AND SY_ROLE.ROLE_TYPE = N'SY')
						OR (@ROLE_TYPE = N'SH' AND SY_ROLE.SHOP_CODE = @SHOP_CODE)
                    )
				--end added by jason in 2012-04-05
                AND (@ADMIN_FLAG IS NULL OR dbo.fnConvertNvarcharToBit(SY_ROLE.ADMIN_FLAG) = @ADMIN_FLAG)
        END
    ELSE--如果当前用户是公司用户，选择系统层面角色+当前店对应的角色
        BEGIN
            SELECT
                SY_ROLE.ROLE_ID
               ,upper(SY_ROLE.ROLE_CODE) as ROLE_CODE
               ,SY_ROLE.ROLE_SDSC
               ,SY_ROLE.ROLE_DSC
               ,SY_ROLE.ROLE_TYPE
               ,SY_ROLE.SHOP_CODE
               ,SY_ROLE.SYSTEM_SCOPE
               ,dbo.fnConvertNvarcharToBit(SY_ROLE.ADMIN_FLAG) AS ADMIN_FLAG
               ,SY_ROLE.CREATION_DATE
               ,dbo.fnConvertNvarcharToBit(SY_ROLE.FROZEN_FLAG) AS FROZEN_FLAG
               ,SY_ROLE.FROZEN_DATE
               ,--case when ROLE_TYPE = N'SY' then '' else (SELECT NAME FROM SY_SHOP where SY_SHOP.CODE = SY_ROLE.SHOP_CODE) end as SHOP_NAME
                SY_SHOP.NAME AS SHOP_NAME
               ,@USER_TYPE AS USER_TYPE
               ,SY_ROLE.LAST_UPDATE_DATE
               ,SY_ROLE.LAST_UPDATED_BY
			   ,SY_ROLE.BU_CODE
            FROM
                SY_ROLE
                LEFT OUTER JOIN SY_SHOP ON SY_SHOP.CODE = SY_ROLE.SHOP_CODE
            WHERE
                SY_ROLE.SYS_CODE = @SYS_CODE
                AND SY_ROLE.ROLE_ID = @ROLE_ID
                OR @ROLE_ID = 0
                --and (@ROLE_DSC_FROM = N'' or SY_ROLE.ROLE_DSC >=  @ROLE_DSC_FROM)
                --and (@ROLE_DSC_TO = N'' or SY_ROLE.ROLE_DSC <=  @ROLE_DSC_TO)
                AND (@ROLE_CODE = N'' OR upper(SY_ROLE.ROLE_CODE) LIKE '%' + upper(@ROLE_CODE) + '%')
                AND (
						(@ROLE_TYPE = N'' AND (SY_ROLE.ROLE_TYPE = N'SY' OR SY_ROLE.SHOP_CODE = @SHOP_CODE))
						OR (@ROLE_TYPE = N'SY' AND SY_ROLE.ROLE_TYPE = N'SY')
						OR (@ROLE_TYPE = N'SH' AND SY_ROLE.SHOP_CODE = @SHOP_CODE)
                    )
                AND (@ADMIN_FLAG IS NULL OR dbo.fnConvertNvarcharToBit(SY_ROLE.ADMIN_FLAG) = @ADMIN_FLAG)
	   
        END

    SET @ResultType = 0 ;  
    SET @ResultMessage = ''  
    
    RETURN @ResultType ;  
END