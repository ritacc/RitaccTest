-- =============================================
-- 更新IP
-- =============================================
CREATE PROCEDURE spUpdateUserIp
@USER_ID NVARCHAR(50),
@IP NVARCHAR(50),
@ResultType  INT OUTPUT,  
@ResultMessage NVARCHAR(1000)   OUTPUT 
AS
BEGIN
	IF EXISTS(SELECT 1 FROM SY_USER_LOCK WHERE [USER_ID] = @USER_ID)
	BEGIN
		DECLARE @OLD_IP nvarchar(20)    
		SELECT @OLD_IP =LOGIN_IP from SY_USER_LOCK where [USER_ID] = @USER_ID;

		if(@OLD_IP = @IP)
		begin
			SET @ResultType = -2001;  
			SET @ResultMessage = N'已经解锁登录';
			RETURN @ResultType; 
		end

		UPDATE SY_USER_LOCK SET LOGIN_IP = @IP WHERE [USER_ID] = @USER_ID
	END
	ELSE
	BEGIN
		INSERT INTO SY_USER_LOCK([USER_ID],LOGIN_IP) VALUES(@USER_ID,@IP)
	END
	SET @ResultType = 0
	SET @ResultMessage = N''
END