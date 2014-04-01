-- =============================================  
-- Author:  XJ  
-- Create date: 2011-9-20  
-- Description:  
-- =============================================  
create PROCEDURE [dbo].[spUpdateRoleFunc]  
(   
 @FUNC_ID         INT,   --功能ID  	
 @ROLE_ID         INT,   --角色ID  
 @INSERTABLE_FLAG      BIT,    
 @UPDATABLE_FLAG      BIT,    
 @CURRENT_USER_ID	BIGINT, 
 @ResultType  INT        OUTPUT,  
 @ResultMessage NVARCHAR(1000)     OUTPUT  
)  
AS   
 BEGIN  
 -- UPDATE SY_ROLE_FUNC
 -- SET ACTIVE_FLAG = 'Y'
	--,INSERTABLE_FLAG = dbo.fnConvertBitToNvarchar(@INSERTABLE_FLAG)
	--,UPDATABLE_FLAG =  dbo.fnConvertBitToNvarchar(@UPDATABLE_FLAG)
 -- WHERE SY_ROLE_FUNC.ROLE_ID = @ROLE_ID
	--and SY_ROLE_FUNC.FUNC_ID = @FUNC_ID;
  
  insert into SY_ROLE_FUNC
  (
	FUNC_ID,
	ROLE_ID,
	ACTIVE_FLAG,
	LAST_UPDATE_DATE,
	LAST_UPDATED_BY,
	CREATION_DATE,
	CREATED_BY,
	LAST_UPDATE_LOGIN,
	INSERTABLE_FLAG,
	UPDATABLE_FLAG
  )  
  values
  (
	@FUNC_ID,
	@ROLE_ID,
	N'Y',
	GETDATE(),
	@CURRENT_USER_ID,
	GETDATE(),
	@CURRENT_USER_ID,
	@CURRENT_USER_ID,
	dbo.fnConvertBitToNvarchar(@INSERTABLE_FLAG),
	dbo.fnConvertBitToNvarchar(@UPDATABLE_FLAG)
  );
		
    
  SET @ResultType = 0;  
  SET @ResultMessage = 'UPDATE ROLE FUNCTION SUCCEED'  
    
  RETURN @ResultType;  
 END
 GO