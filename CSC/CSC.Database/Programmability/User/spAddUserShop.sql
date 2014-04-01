-- =============================================  
-- Author:  XJ  
-- Create date: 2011-9-20  
-- Description:  
-- =============================================  
create PROCEDURE [dbo].[spAddUserShop]  
(  
 @SYS_CODE NVARCHAR(4), 
 @SHOP_CODE NVARCHAR(4), 
 @USER_ID         INT,   --功能ID  	
 @CURRENT_USER_ID	BIGINT, 
 @ResultType  INT        OUTPUT,  
 @ResultMessage NVARCHAR(1000)     OUTPUT  
)  
AS   
 BEGIN  
 
  insert into SY_USER_SHOP
  (
	SYS_CODE,
	SHOP_CODE,
	[USER_ID],
	ACTIVE_FLAG,
	LAST_UPDATE_DATE,
	LAST_UPDATED_BY,
	CREATION_DATE,
	CREATED_BY,
	LAST_UPDATE_LOGIN,
	SUSPEND_FLAG
  ) 
  values
  (
	@SYS_CODE,
	@SHOP_CODE,
	@USER_ID,
	N'Y',
	GETDATE(),
	@CURRENT_USER_ID,
	GETDATE(),
	@CURRENT_USER_ID,
	@CURRENT_USER_ID,
	NULL
  );
		
    
  SET @ResultType = 0;  
  SET @ResultMessage = ''  
    
  RETURN @ResultType;  
 END
 GO