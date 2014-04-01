-- =============================================  
-- Author:  XJ  
-- Create date: 2011-9-20  
-- Description: 查找角色-用户 
-- =============================================  
CREATE PROCEDURE [dbo].[spSearchUserRole]  
(   
 @SYS_CODE NVARCHAR(4),
 @ROLE_ID         INT = 0,   --角色ID  
 @ResultType  INT        OUTPUT,  
 @ResultMessage NVARCHAR(1000)     OUTPUT  
)  
AS   
 BEGIN  
  SELECT   
   SY_USER_ROLE_SHOP.SHOP_CODE,
   SY_SHOP.NAME AS SHOP_NAME,
   SY_USER.USER_TYPE,  
   SY_USER.[USER_CODE],
   SY_USER.[USER_NAME],
   SY_SHOP.BU_CODE
  FROM SY_USER_ROLE_SHOP  
  left outer join SY_USER on SY_USER_ROLE_SHOP.[USER_ID] = SY_USER.[USER_ID]
  left outer join SY_SHOP ON SY_SHOP.CODE = SY_USER_ROLE_SHOP.SHOP_CODE
  WHERE 
	SY_USER_ROLE_SHOP.SYS_CODE = @SYS_CODE
	AND	SY_USER_ROLE_SHOP.ACTIVE_FLAG= 'Y' 
   AND SY_USER_ROLE_SHOP.ROLE_ID = @ROLE_ID 
   order by SY_USER_ROLE_SHOP.SHOP_CODE, SY_USER.[USER_CODE]
    
  SET @ResultType = 0;  
  SET @ResultMessage = ''  
    
  RETURN @ResultType;  
 END
 GO