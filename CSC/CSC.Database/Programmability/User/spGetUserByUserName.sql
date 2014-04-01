/**********************************************************************/    
-- Description:	根据用户名称获取用户信息，用于登录
---------------------------------------------------------------------    
-- Action	Date		Staff	Version		Remarks
-- Modify	2013-01-10	ZB		1.01.00		加入SYS_CODE条件
-- Create	????-??-??	XH		1.00.00
---------------------------------------------------------------------    
-- Field Description：    
    
/**********************************************************************/    
CREATE PROCEDURE [dbo].[spGetUserByUserName]
(
	@UserName		nvarchar(20),
	@SYSTEM_SCOPE	nvarchar(4),
	@ShopCode		nvarchar(20)	= '',
	@ResultType		int				OUTPUT,
	@ResultMessage	nvarchar(1000)	OUTPUT
)
AS
BEGIN
	SELECT
		[SY_USER].[USER_ID]
		,[USER_CODE]
		,[USER_NAME]
		,[USER_TYPE]
		,[SYSTEM_SCOPE]
		,dbo.fnConvertNvarcharToBit(FROZEN_FLAG) AS FROZEN_FLAG
		,[FROZEN_DATE]
		,dbo.fnConvertNvarcharToBit([SY_USER].SUSPEND_FLAG) AS SUSPEND_FLAG
		,[SUSPEND_DATE]
		,[LOGIN_PWD]
		,[AUTH_PWD]
		,[PWD_EXPIRY_DATE]
		,[SY_USER].[LAST_UPDATE_DATE]
		,[SY_USER].[LAST_UPDATED_BY]
		,[SY_USER].[CREATION_DATE]
		,[SY_USER].[CREATED_BY]
		,[SY_USER].[LAST_UPDATE_LOGIN]
		,[REPORT_SERVER_CODE]
		,[CHAR_REPORT_SERVER_CODE]
		,[ROWID]
	FROM
		[SY_USER]
		LEFT JOIN SY_USER_SHOP ON SY_USER.[USER_ID] = SY_USER_SHOP.[USER_ID]
	WHERE
		SY_USER.USER_CODE=@UserName
		AND SY_USER.SYSTEM_SCOPE=@SYSTEM_SCOPE
		AND (@ShopCode IS NULL OR @ShopCode = N'' OR SY_USER_SHOP.SHOP_CODE=@ShopCode)

	SET @ResultType = 0;
	SET @ResultMessage = ''
	RETURN @ResultType;
END