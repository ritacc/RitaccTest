CREATE PROCEDURE [spAuthUser]
(                        
	@UserName   nvarchar(20) ,
	@LoginPwd nvarchar(60) ,
	@SystemScope nvarchar(4) ,
	@ResultType		int				OUTPUT,
	@ResultMessage	nvarchar(1000)	OUTPUT
)
AS
BEGIN
	SELECT [USER_ID]
      ,[USER_CODE]
      ,[USER_NAME]
      ,[USER_TYPE]
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
  FROM [SY_USER]

	WHERE
		 
		[USER_NAME] = @UserName
		and [LOGIN_PWD] = @LoginPwd
		AND SYSTEM_SCOPE=@SystemScope
		
	SET @ResultType = 0;
	SET @ResultMessage = ''
	RETURN @ResultType;
END