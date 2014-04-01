-- =============================================  
-- Author:  XJ  
-- Create date: 2011-9-20  
-- Description:  
-- =============================================  
CREATE PROCEDURE [dbo].[spUpdateUserForzen]  
(                
 @USER_ID    bigint, 
 @LAST_UPDATED_BY   INT,           --修改  
 @ResultType   INT   OUTPUT,  
 @ResultMessage  NVARCHAR(1000) OUTPUT  
)  
AS  
BEGIN    
 UPDATE  
   SY_USER  
  SET  
   FROZEN_FLAG = N'N',  
   FROZEN_DATE = null,  
   LAST_UPDATED_BY = @LAST_UPDATED_BY,  
   LAST_UPDATE_DATE = GETDATE()  
  WHERE  
   [USER_ID] = @USER_ID;  
    
 SET @ResultType = 0;  
 SET @ResultMessage = '';  
   
 RETURN @ResultType;  
END