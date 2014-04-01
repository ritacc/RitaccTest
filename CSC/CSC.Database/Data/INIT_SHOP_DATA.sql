/**********************************************************************/
-- Description:		初始化 SY_SHOP 数据
---------------------------------------------------------------------
-- Action		Date			Staff		Version		Remarks
-- Create	 	2011-11-26		Zhangbo		1.00.00
/**********************************************************************/
CREATE PROCEDURE INIT_SHOP_DATA
(
	@SYS_CODE			nvarchar(4),
	@SHOP_CODE			nvarchar(4),
	@SHOP_NAME			nvarchar(60),
	@BU_CODE			nvarchar(10),
	@SHOP_TYPE			nvarchar(10),
	@ResultType			int				OUTPUT,
	@ResultMessage		nvarchar(1000)	OUTPUT
)
AS
BEGIN
	DECLARE @CUR_USER	bigint		= 0;
	DECLARE @CUR_DATE	datetime	= GETDATE();

	-- 创建店
	PRINT N'初始化 SY_SHOP';
	INSERT [SY_SHOP] ([CODE], [NAME],BU_CODE,SHOP_TYPE, [SYS_CODE], [CREATED_BY], [CREATION_DATE], [LAST_UPDATED_BY], [LAST_UPDATE_DATE]) 
		VALUES (@SHOP_CODE, @SHOP_NAME,@BU_CODE,@SHOP_TYPE, @SYS_CODE, @CUR_USER, @CUR_DATE, @CUR_USER, @CUR_DATE)

	-- 将店授权给系统管理员和系统用户
	PRINT N'将店授权给用户 SA，SU，ADMIN'
	BEGIN
		PRINT N'将店授权给用户 SA，SU，ADMIN - 初始化 SY_USER_SHOP';
		INSERT [SY_USER_SHOP] ([SHOP_CODE], [USER_ID], [SYS_CODE], [CREATED_BY], [CREATION_DATE], [LAST_UPDATED_BY], [LAST_UPDATE_DATE])
			SELECT  @SHOP_CODE, [USER_ID], @SYS_CODE, @CUR_USER, @CUR_DATE, @CUR_USER, @CUR_DATE
			FROM SY_USER
			WHERE SYSTEM_SCOPE = @SYS_CODE AND USER_CODE IN ('SA', 'SU', 'ADMIN');

		PRINT N'将店授权给用户 SA，SU，ADMIN - 初始化 SY_USER_ROLE_SHOP';
		INSERT [SY_USER_ROLE_SHOP] ([ROLE_ID], [USER_ID], [SHOP_CODE], [SHOP_ADM_FLAG], [GRANT_BY], [GRANT_DATE], [SYS_CODE], [CREATED_BY], [CREATION_DATE], [LAST_UPDATED_BY], [LAST_UPDATE_DATE])
			SELECT SY_USER_ROLE.[ROLE_ID], SY_USER_ROLE.[USER_ID], @SHOP_CODE, N'Y', @CUR_USER, @CUR_DATE, @SYS_CODE, @CUR_USER, @CUR_DATE, @CUR_USER, @CUR_DATE
			FROM SY_USER_ROLE INNER JOIN SY_USER ON SY_USER_ROLE.[USER_ID]=SY_USER.[USER_ID]
			WHERE SY_USER.SYSTEM_SCOPE = @SYS_CODE AND SY_USER.USER_CODE IN ('SA', 'SU', 'ADMIN');
	END

	PRINT N'SY_SHOP 数据初始化完毕';

	SET @ResultType		= 0;
	SET @ResultMessage	= N'';
END