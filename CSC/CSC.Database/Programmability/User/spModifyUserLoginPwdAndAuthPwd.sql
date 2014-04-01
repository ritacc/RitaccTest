-- =============================================
-- Author:		JuLuDe
-- Create date: 2011-9-6
-- Description: 修改用户密码
-- =============================================
CREATE PROCEDURE [dbo].[spModifyUserLoginPwdAndAuthPwd]
(                        
	@UserID          NUMERIC(10,0),
    @NewLoginPwd     NVARCHAR(60) = N'', 
    @NewAuthPwd      NVARCHAR(60) = N'', 
    @LastUpdatedBy   BIGINT,
	@ResultType		 INT			OUTPUT,
	@ResultMessage	 NVARCHAR(1000)	OUTPUT
)
AS
BEGIN  
	IF(@NewLoginPwd = '' AND @NewAuthPwd = '')
	BEGIN
		RETURN
	END
	ELSE IF(@NewLoginPwd = '' AND @NewAuthPwd <> '')
	BEGIN
		UPDATE SY_USER SET AUTH_PWD = @NewAuthPwd,LAST_UPDATE_DATE=GETDATE(),LAST_UPDATED_BY=@LastUpdatedBy WHERE [USER_ID] = @UserID
	END
	ELSE IF(@NewAuthPwd = '' AND @NewLoginPwd <> '')
	BEGIN
		UPDATE SY_USER SET LOGIN_PWD = @NewLoginPwd,LAST_UPDATE_DATE=GETDATE(),LAST_UPDATED_BY=@LastUpdatedBy WHERE [USER_ID] = @UserID
	END
	ELSE
	BEGIN
		UPDATE SY_USER SET LOGIN_PWD = @NewLoginPwd,AUTH_PWD = @NewAuthPwd,LAST_UPDATE_DATE=GETDATE(),LAST_UPDATED_BY=@LastUpdatedBy WHERE [USER_ID] = @UserID
	END
	
	SET @ResultType = 0;
	SET @ResultMessage = ''
	RETURN @ResultType;
END