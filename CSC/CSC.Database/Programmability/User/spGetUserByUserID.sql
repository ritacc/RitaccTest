-- =============================================  
-- Author:  JuLuDe  
-- Create date: 2011-9-6  
-- Description: 根据ID查找用户  
-- =============================================  
CREATE PROCEDURE [dbo].[spGetUserByUserID]  
(                          
 @UserID          NUMERIC(10,0),   
 @ResultType   INT   OUTPUT,  
 @ResultMessage  NVARCHAR(1000) OUTPUT  
)  
AS  
BEGIN    
 SELECT   
    [USER_ID]  
      ,[USER_CODE]  
      ,[USER_NAME]  
      ,[USER_TYPE]  
      ,[SYSTEM_SCOPE]  
      ,dbo.fnConvertNvarcharToBit(FROZEN_FLAG) AS FROZEN_FLAG  
      ,[FROZEN_DATE]  
      ,dbo.fnConvertNvarcharToBit(SUSPEND_FLAG) AS SUSPEND_FLAG  
      ,[SUSPEND_DATE]  
      ,[LOGIN_PWD]  
      ,[AUTH_PWD]  
      ,[PWD_EXPIRY_DATE]  
      ,[LAST_UPDATE_DATE]  
      ,[LAST_UPDATED_BY]  
      ,[CREATION_DATE]  
      ,[CREATED_BY]  
      ,[LAST_UPDATE_LOGIN]  
      ,[REPORT_SERVER_CODE]  
      ,[CHAR_REPORT_SERVER_CODE]  
      ,[ROWID]  
    FROM   
  SY_USER   
 WHERE   
  [USER_ID] = @UserID   
   
 SET @ResultType = 0;  
 SET @ResultMessage = ''  
 RETURN @ResultType;  
END