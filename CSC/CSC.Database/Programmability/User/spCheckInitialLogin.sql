-- =============================================
-- Author:		XJ
-- Create date: 2013-9-11
-- Description: 是否初次登录
-- =============================================
CREATE PROCEDURE spCheckInitialLogin
(                        
	@UserID          bigint, 
	@ResultType		 INT			OUTPUT,
	@ResultMessage	 NVARCHAR(1000)	OUTPUT
)
AS
BEGIN  
	
	IF EXISTS(SELECT 1 FROM SY_PASSWORD_LOG WHERE PWD_TYPE = N'P' AND [USER_ID] = @UserID and CREATED_BY = @UserID)
	BEGIN
		SET @ResultType = 0; 
	END
	ELSE
	BEGIN
		SET @ResultType = 1;
	END
	
	RETURN @ResultType;
END
