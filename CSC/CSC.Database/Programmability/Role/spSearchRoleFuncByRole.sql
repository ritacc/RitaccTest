-- =============================================  
-- Author:  XJ  
-- Create date: 2011-9-20  
-- Description: 查找角色-功能  
-- =============================================  
CREATE PROCEDURE [dbo].[spSearchRoleFuncByRole]  
(   
 @SYS_CODE NVARCHAR(4),
 @ROLE_ID         INT = 0,   --角色ID  
 @ResultType  INT        OUTPUT,  
 @ResultMessage NVARCHAR(1000)     OUTPUT  
)  
AS   
 BEGIN  
  SELECT   
   SY_FUNCTION.FUNC_CODE,  
   SY_FUNCTION.FUNC_TYPE,
   SY_FUNCTION.DSC,
   SY_FUNCTION.SYSTEM_SCOPE,
   dbo.fnConvertNvarcharToBit(SY_FUNCTION.ADMIN_FLAG)  AS ADMIN_FLAG,
   SY_ROLE_FUNC.FUNC_ID,
   SY_ROLE_FUNC.ROLE_ID,
   dbo.fnConvertNvarcharToBit(SY_ROLE_FUNC.INSERTABLE_FLAG)  AS INSERTABLE_FLAG, 
   dbo.fnConvertNvarcharToBit(SY_ROLE_FUNC.UPDATABLE_FLAG) AS UPDATABLE_FLAG,
   dbo.fnConvertNvarcharToBit(SY_ROLE_FUNC.ACTIVE_FLAG) AS ACTIVE_FLAG
  FROM SY_ROLE_FUNC  
  left outer join SY_FUNCTION on SY_ROLE_FUNC.FUNC_ID = SY_FUNCTION.FUNC_ID
  WHERE SY_ROLE_FUNC.ROLE_ID = @ROLE_ID
  and SY_ROLE_FUNC.ACTIVE_FLAG = N'Y'
  order by SY_FUNCTION.FUNC_CODE ASC ; 
    
  SET @ResultType = 0;  
  SET @ResultMessage = ''  
    
  RETURN @ResultType;  
 END
 GO