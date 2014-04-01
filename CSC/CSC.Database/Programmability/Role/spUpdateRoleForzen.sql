-- =============================================  
-- Author:  XJ  
-- Create date: 2011-9-20  
-- Description:  
-- =============================================  
CREATE PROCEDURE [dbo].[spUpdateRoleForzen]  
(                
 @ROLE_ID    bigint,  --角色ID  
 @FROZEN_FLAG      BIT,           --冷冻状态  
 @LAST_UPDATED_BY   INT,           --修改  
 @ResultType   INT   OUTPUT,  
 @ResultMessage  NVARCHAR(1000) OUTPUT  
)  
AS  
BEGIN    
 UPDATE  
   SY_ROLE  
  SET  
   FROZEN_FLAG = dbo.fnConvertBitToNvarchar(@FROZEN_FLAG),  
   FROZEN_DATE =  case when @FROZEN_FLAG = 1 then GETDATE() else NULL end,  
   LAST_UPDATED_BY = @LAST_UPDATED_BY,  
   LAST_UPDATE_DATE = GETDATE()  
  WHERE  
   ROLE_ID = @ROLE_ID;  
    
 SET @ResultType = 0;  
 SET @ResultMessage = '';  
   
 RETURN @ResultType;  
END