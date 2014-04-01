-- =============================================
-- Author:		XJ
-- Create date: 2013-9-11
-- Description: 最近修改过的密码不能被使用
-- =============================================
create PROCEDURE spCheckLastPwd
(                        
	@UserID          bigint, 
    @NewPassword	 NVARCHAR(60) = N'', 
    @ChangePwdCnt    bigint, 
	@ResultType		 INT			OUTPUT,
	@ResultMessage	 NVARCHAR(1000)	OUTPUT
)
AS
BEGIN  
	IF(@ChangePwdCnt = 0)
	BEGIN
		SET @ResultType = 1;
		SET @ResultMessage = ''
		RETURN
	END 
	ELSE
	BEGIN
		IF EXISTS
		(
			SELECT 1
			FROM
			(
				SELECT TOP(@ChangePwdCnt) * 
				FROM SY_PASSWORD_LOG
				WHERE PWD_TYPE = N'P'
				AND [USER_ID] = @UserID
				ORDER BY SEQ DESC
			) LIST
			WHERE LIST.LOGIN_PWD = @NewPassword
		)
		BEGIN
			SET @ResultType = 0; 
		END
		ELSE
		BEGIN
			SET @ResultType = 1;
		END
	END

	RETURN @ResultType;
END
