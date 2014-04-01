-- =============================================  
-- Author:  XJ  
-- Create date: 2011-9-20  
-- Description:  
-- =============================================  
CREATE PROCEDURE [dbo].[spUpdateBatchRoleFunc]  
(  
 @ROLE_ID         INT,   --角色ID 
 @ResultType  INT        OUTPUT,  
 @ResultMessage NVARCHAR(1000)     OUTPUT  
)  
AS   
 BEGIN  
 -- UPDATE SY_ROLE_FUNC
 -- SET ACTIVE_FLAG = 'N'
	--,INSERTABLE_FLAG = 'N'
	--,UPDATABLE_FLAG = 'N'
 -- WHERE SY_ROLE_FUNC.ROLE_ID = @ROLE_ID;

  DELETE FROM SY_ROLE_FUNC WHERE SY_ROLE_FUNC.ROLE_ID = @ROLE_ID;

  SET @ResultType = 0;  
  SET @ResultMessage = 'UPDATE ROLE FUNCTION SUCCEED'  
    
  RETURN @ResultType;  
 END
 GO