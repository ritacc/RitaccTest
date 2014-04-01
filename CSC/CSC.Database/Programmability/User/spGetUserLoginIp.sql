-- =============================================
-- 获得用户登录IP
-- =============================================
CREATE PROCEDURE spGetUserLoginIp
@USER_ID NVARCHAR(30) OUTPUT,
@LOGIN_IP NVARCHAR(30) OUTPUT,
@ResultType  INT        OUTPUT,  
@ResultMessage NVARCHAR(1000)     OUTPUT  
AS
BEGIN
	 
	SELECT @LOGIN_IP= LOGIN_IP FROM SY_USER_LOCK WHERE [USER_ID] = @USER_ID
	SET @ResultType = 0
	SET @ResultMessage = N''
END
