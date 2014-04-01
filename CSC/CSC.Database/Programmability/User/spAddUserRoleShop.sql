/**********************************************************************/
-- Description:	
---------------------------------------------------------------------
-- Action		Date			Staff		Version		Remarks
-- Create	 	2011-09-20		XJ			1.00.00
-- Modify		2012-03-06		ZJX			1.01.00
---------------------------------------------------------------------  
CREATE PROCEDURE [dbo].[spAddUserRoleShop]
(
 @SYS_CODE NVARCHAR(4)
,@SHOP_CODE NVARCHAR(4)
,@ROLE_ID INT--功能ID
,@USER_ID INT--功能ID
,@CURRENT_USER_ID BIGINT
,@ResultType INT OUTPUT
,@ResultMessage NVARCHAR(1000) OUTPUT  
)
AS 
BEGIN  
 
    INSERT  INTO dbo.SY_USER_ROLE_SHOP
            (ROLE_ID
            ,[USER_ID]
            ,SHOP_CODE
            ,SYS_CODE
            ,GRANT_BY
            ,GRANT_DATE
            ,ACTIVE_FLAG
            ,SHOP_ADM_FLAG
            ,LAST_UPDATE_DATE
            ,LAST_UPDATED_BY
            ,CREATION_DATE
            ,CREATED_BY
            ,LAST_UPDATE_LOGIN
            )
            SELECT
                @ROLE_ID
               ,@USER_ID
               ,@SHOP_CODE
               ,@SYS_CODE
               ,@CURRENT_USER_ID
               ,GETDATE()
               ,N'Y'
               ,N'N'
               ,GETDATE()
               ,@CURRENT_USER_ID
               ,GETDATE()
               ,@CURRENT_USER_ID
               ,@CURRENT_USER_ID
            FROM
                dbo.SY_ROLE
            WHERE
				dbo.SY_ROLE.ROLE_ID = @ROLE_ID 
				AND (dbo.SY_ROLE.ROLE_TYPE = N'SY' OR dbo.SY_ROLE.SHOP_CODE = @SHOP_CODE) ;
		
    
    SET @ResultType = 0 ;  
    SET @ResultMessage = 'ADD SUCCEED' ;
    
    RETURN @ResultType ;  
END