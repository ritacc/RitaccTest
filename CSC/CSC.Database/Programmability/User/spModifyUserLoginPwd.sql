-- =============================================  
-- Author:  JuLuDe  
-- Create date: 2011-9-6  
-- Description: 修改用户登录密码  
-- =============================================  
CREATE PROCEDURE [dbo].[spModifyUserLoginPwd]  
(                          
	@UserID          NUMERIC(10,0),  
    @NewLoginPwd     NVARCHAR(60) = N'',   
    @PwdExpiryDate   BIGINT,   
    @LastUpdatedBy   BIGINT,  
	@ResultType      INT   OUTPUT,  
	@ResultMessage   NVARCHAR(1000) OUTPUT  
)  
AS  
BEGIN      
	IF(@NewLoginPwd = '')  
	BEGIN  
		SET @ResultType = 0;  
		SET @ResultMessage = ''  
		RETURN  
	END   
	ELSE  
	BEGIN  
		--修改密码
		UPDATE   
			SY_USER   
		SET   
			LOGIN_PWD = @NewLoginPwd,  
			LAST_UPDATE_DATE = GETDATE(),  
			PWD_EXPIRY_DATE = GETDATE()+@PwdExpiryDate,  
			LAST_UPDATED_BY = @LastUpdatedBy  
		WHERE   
			[USER_ID] = @UserID  
			
		--记录修改密码日志
		DECLARE @SEQ_VALUE BIGINT
		SELECT @SEQ_VALUE = MAX(SEQ)+1 FROM SY_PASSWORD_LOG WHERE [USER_ID]= @UserID
		IF(@SEQ_VALUE IS NULL)
		BEGIN
			SET @SEQ_VALUE = 1
		END
		
		INSERT INTO SY_PASSWORD_LOG
		(
			[USER_ID],
			[PWD_TYPE],
			[SEQ],
			[LOGIN_PWD],
			[AUTH_PWD],
			[LAST_UPDATE_DATE],
			[LAST_UPDATED_BY],
			[CREATION_DATE],
			[CREATED_BY],
			[LAST_UPDATE_LOGIN]
		)
		VALUES
		(
			@UserID,
			'P',
			@SEQ_VALUE,
			@NewLoginPwd,
			NULL,
			GETDATE(),
			@LastUpdatedBy,
			GETDATE(),
			@LastUpdatedBy,
			@LastUpdatedBy
		)
	END  

	SET @ResultType = 0;  
	SET @ResultMessage = ''  
	RETURN @ResultType;  
END
