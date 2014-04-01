-- =============================================  
-- Author:  JuLuDe  
-- Create date: 2011-9-13  
-- Description: 查找角色  
-- =============================================  
CREATE PROCEDURE [dbo].[spSearchRoleForDDL]
(
	 @SYS_CODE			NVARCHAR(4)
	,@USER_TYPE			NVARCHAR(2)
	,@SHOP_CODE			NVARCHAR(4)
	,@ResultType		INT				OUTPUT
	,@ResultMessage		NVARCHAR(1000)	OUTPUT  
)
AS 
BEGIN 
    SELECT
        ROLE_CODE
       ,ROLE_DSC
       ,ROLE_ID
    FROM
        SY_ROLE
    WHERE
        SYS_CODE = @SYS_CODE
        --added by jason in 20121219 for TSYF02010#06.doc
        AND SY_ROLE.ROLE_TYPE = N'SY'
        --end added by jason in 20121219 for TSYF02010#06.doc
        
        --commented by jason in 20121219 for TSYF02010#06.doc
		--AND (@USER_TYPE = N'SA' OR (@USER_TYPE = N'NS' AND (SY_ROLE.ROLE_TYPE = N'SY' OR SY_ROLE.SHOP_CODE = @SHOP_CODE)))
		--end commented by jason in 20121219 for TSYF02010#06.doc
    ORDER BY
        ROLE_DSC;

    SET @ResultType = 0 ;  
    SET @ResultMessage = '';
    
    RETURN @ResultType ;  
END
 GO