-- =============================================  
-- Author:  XJ  
-- Create date: 2011-9-20  
-- Description:  
-- =============================================  
create PROCEDURE [dbo].[spAddUserRole]  
(  
 @ROLE_ID         INT,   --功能ID  	
 @USER_ID         INT,   --功能ID  	
 @CURRENT_USER_ID	BIGINT, 
 @ResultType  INT        OUTPUT,  
 @ResultMessage NVARCHAR(1000)     OUTPUT  
)  
AS   
 BEGIN  
 if not exists(
	select 1 from SY_USER_ROLE where ROLE_ID = @ROLE_ID and [USER_ID] = @USER_ID and ACTIVE_FLAG = N'Y'
 )
 begin
  insert into dbo.SY_USER_ROLE
  (
	ROLE_ID,
	[USER_ID],
	ACTIVE_FLAG,
	LAST_UPDATE_DATE,
	LAST_UPDATED_BY,
	CREATION_DATE,
	CREATED_BY,
	LAST_UPDATE_LOGIN
  ) 
  values
  (
	@ROLE_ID,
	@USER_ID,
	N'Y',
	GETDATE(),
	@CURRENT_USER_ID,
	GETDATE(),
	@CURRENT_USER_ID,
	@CURRENT_USER_ID
  );
end		
    
  SET @ResultType = 0;  
  SET @ResultMessage = ''  
    
  RETURN @ResultType;  
 END
 GO