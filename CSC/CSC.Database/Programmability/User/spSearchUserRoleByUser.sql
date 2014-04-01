/**********************************************************************/
-- Description:		查找角色-用户
---------------------------------------------------------------------
-- Action		Date			Staff		Version		Remarks
-- Create	 	2011-09-20		XJ			1.00.00
-- Modify		2012-03-06		JASON		1.01.00		根据类型查询角色-用户
---------------------------------------------------------------------
/**********************************************************************/
CREATE PROCEDURE [dbo].[spSearchUserRoleByUser]
(
	 @USER_ID INT = 0--角色ID  
	,@ResultType INT OUTPUT
	,@ROLE_TYPE NVARCHAR(2) --为空则查询所有，否则则查询相关类型的(SY,SH)
	,@ResultMessage NVARCHAR(1000) OUTPUT  
)
AS 
BEGIN  
    SELECT
        SY_USER_ROLE.ROLE_ID
       ,SY_USER_ROLE.[USER_ID]
    FROM
        dbo.SY_USER_ROLE
        LEFT JOIN dbo.SY_ROLE ON dbo.SY_USER_ROLE.ROLE_ID = dbo.SY_ROLE.ROLE_ID
    WHERE
        SY_USER_ROLE.ACTIVE_FLAG = 'Y'
        AND SY_USER_ROLE.[USER_ID] = @USER_ID
        AND (@ROLE_TYPE = N'' OR (@ROLE_TYPE <> N'' AND dbo.SY_ROLE.ROLE_TYPE = @ROLE_TYPE))
    
    SET @ResultType = 0 ;  
    SET @ResultMessage = N'SEARCH SUCCEED';
    
    RETURN @ResultType ;  
END