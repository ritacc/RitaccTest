-- =============================================  
-- Author:  xh  
-- Create date: 2012-2-22
-- Description:  
-- =============================================  
CREATE PROCEDURE spForzenUser
(                
 @USER_ID    BIGINT,  
 @ResultType   INT   OUTPUT,  
 @ResultMessage  NVARCHAR(1000) OUTPUT  
)  
AS  
BEGIN    
 UPDATE  
   SY_USER 
  SET  
   --SUSPEND_FLAG = N'Y',  
   FROZEN_FLAG = N'Y',
   --SUSPEND_DATE = GETDATE(),
   FROZEN_DATE = GETDATE(),
   LAST_UPDATED_BY =0,  
   LAST_UPDATE_DATE = GETDATE()  
  WHERE  
   [USER_ID] = @USER_ID
    
 SET @ResultType = 0;  
 SET @ResultMessage = '';  
   
 RETURN @ResultType;  
END