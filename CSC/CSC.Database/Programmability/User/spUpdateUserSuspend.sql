-- =============================================  
-- Author:  XJ  
-- Create date: 2011-9-20  
-- Description:  
-- =============================================  
CREATE PROCEDURE [dbo].[spUpdateUserSuspend]  
(                
 @USER_ID    bigint,  --角色ID  
 @SUSPEND_FLAG      BIT,           --冷冻状态  
 @LAST_UPDATED_BY   INT,           --修改  
 @ResultType   INT   OUTPUT,  
 @ResultMessage  NVARCHAR(1000) OUTPUT  
)  
AS  
BEGIN    
 UPDATE  
   SY_USER 
  SET  
   SUSPEND_FLAG = dbo.fnConvertBitToNvarchar(@SUSPEND_FLAG),  
   SUSPEND_DATE =  case when @SUSPEND_FLAG = 1 then GETDATE() else NULL end,  
   LAST_UPDATED_BY = @LAST_UPDATED_BY,  
   LAST_UPDATE_DATE = GETDATE()  
  WHERE  
   [USER_ID] = @USER_ID;  
    
 SET @ResultType = 0;  
 SET @ResultMessage = '';  
   
 RETURN @ResultType;  
END