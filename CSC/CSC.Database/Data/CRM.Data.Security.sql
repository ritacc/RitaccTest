/**********************************************************************/
-- Description:		初始化管理员用户数据
---------------------------------------------------------------------
-- Action		Date			Staff		Version		Remarks
-- Create	 	2011-12-27		Zhangbo		1.00.00
/**********************************************************************/
DECLARE @SYS_CODE	nvarchar(4);
DECLARE @CUR_USER	bigint;
DECLARE @CUR_DATE	datetime;
DECLARE @ROLE_ID	bigint;
DECLARE @USER_ID	bigint;
DECLARE @ROW_ID		uniqueidentifier;
DECLARE @BU_CODE	nvarchar(10);

SET @SYS_CODE	= N'CSC';
SET @BU_CODE	= N'CSC';
SET @CUR_USER	= 0;
SET @CUR_DATE	= GETDATE();

BEGIN TRANSACTION

BEGIN TRY
	PRINT N'开始创建系统管理员用户 - SA';
	BEGIN
		-- 创建系统管理员角色
		PRINT N'创建系统管理员角色 - Administrators';
		EXEC dbo.spGetNextSequence @ROLE_ID OUTPUT, @ROW_ID OUTPUT;
		INSERT [dbo].[SY_ROLE] 
		(
			[ROLE_ID], 
			[ROLE_CODE], 
			[ROLE_TYPE], 
			[ROLE_SDSC], 
			[ROLE_DSC], 
			[ADMIN_FLAG], 
			[SHOP_CODE], 
			[SYSTEM_SCOPE], 
			[SYS_CODE], 
			BU_CODE,
			[CREATED_BY], 
			[CREATION_DATE], 
			[LAST_UPDATED_BY], 
			[LAST_UPDATE_DATE]
		)
		VALUES 
		(
			@ROLE_ID, 
			N'Administrators',
			N'SY',
			N'Administrators',
			N'System Administrators',
			N'Y', 
			N'*', 
			@SYS_CODE, 
			@SYS_CODE, 
			@BU_CODE,
			@CUR_USER, 
			@CUR_DATE, 
			@CUR_USER, 
			@CUR_DATE
		);

		--给此角色添加管理类型的功能
		PRINT N'将管理类型的功能授权给角色 - Administrators';
		INSERT [dbo].[SY_ROLE_FUNC] ([FUNC_ID], [ROLE_ID], [INSERTABLE_FLAG], [UPDATABLE_FLAG], [CREATED_BY], [CREATION_DATE], [LAST_UPDATED_BY], [LAST_UPDATE_DATE])
			SELECT FUNC_ID, @ROLE_ID, N'Y', N'Y', @CUR_USER, @CUR_DATE, @CUR_USER, @CUR_DATE
			FROM SY_FUNCTION 
			WHERE SY_FUNCTION.SYS_CODE = @SYS_CODE AND ADMIN_FLAG = N'Y';

		-- 创建系统管理员用户
		PRINT N'创建系统管理员用户 - SA';
		EXEC dbo.spGetNextSequence @USER_ID OUTPUT, @ROW_ID OUTPUT;
		INSERT [dbo].[SY_USER] 
		(
			[USER_ID], 
			[USER_CODE], 
			[USER_NAME], 
			[USER_TYPE], 
			[LOGIN_PWD], 
			[AUTH_PWD], 
			[PWD_EXPIRY_DATE], 
			[SYSTEM_SCOPE], 
			[CREATED_BY], 
			[CREATION_DATE], 
			[LAST_UPDATED_BY], 
			[LAST_UPDATE_DATE]
		)
		VALUES 
		(
			@USER_ID,
			N'SA',
			N'System Administrator',
			N'SA',
			N'e2S7K60BAgY=',
			N'e2S7K60BAgY=',
			DATEADD(DAY, 180, @CUR_DATE),
			@SYS_CODE,
			@CUR_USER,
			@CUR_DATE,
			@CUR_USER,
			@CUR_DATE
		);

		--关联用户和角色
		PRINT N'将角色 Administrators 赋予用户 SA';
		INSERT [dbo].[SY_USER_ROLE] ([ROLE_ID], [USER_ID], [ACTIVE_FLAG], [CREATED_BY], [CREATION_DATE], [LAST_UPDATED_BY], [LAST_UPDATE_DATE]) VALUES (@ROLE_ID, @USER_ID, N'Y', @CUR_USER, @CUR_DATE, @CUR_USER, @CUR_DATE);
	END
	
	PRINT N'开始创建系统管理员用户 - SU';
	BEGIN
		-- 创建系统管理员角色
		PRINT N'创建系统管理员角色 - System';
		EXEC dbo.spGetNextSequence @ROLE_ID OUTPUT, @ROW_ID OUTPUT;
		INSERT [dbo].[SY_ROLE] 
		(
			[ROLE_ID], 
			[ROLE_CODE], 
			[ROLE_TYPE], 
			[ROLE_SDSC], 
			[ROLE_DSC], 
			[ADMIN_FLAG], 
			[SHOP_CODE], 
			[SYSTEM_SCOPE], 
			[SYS_CODE], 
			BU_CODE,
			[CREATED_BY], 
			[CREATION_DATE], 
			[LAST_UPDATED_BY], 
			[LAST_UPDATE_DATE]
		)
		VALUES 
		(
			@ROLE_ID, 
			N'System',
			N'SY',
			N'System',
			N'System Users',
			N'N', 
			N'*', 
			@SYS_CODE, 
			@SYS_CODE, 
			@BU_CODE,
			@CUR_USER, 
			@CUR_DATE, 
			@CUR_USER, 
			@CUR_DATE
		);

		--给此角色添加非管理类型的功能
		PRINT N'将非管理类型的功能授权给角色 - System';
		INSERT [dbo].[SY_ROLE_FUNC] ([FUNC_ID], [ROLE_ID], [INSERTABLE_FLAG], [UPDATABLE_FLAG], [CREATED_BY], [CREATION_DATE], [LAST_UPDATED_BY], [LAST_UPDATE_DATE])
			SELECT FUNC_ID, @ROLE_ID, N'Y', N'Y', @CUR_USER, @CUR_DATE, @CUR_USER, @CUR_DATE
			FROM SY_FUNCTION 
			WHERE SY_FUNCTION.SYS_CODE = @SYS_CODE AND ADMIN_FLAG = N'N';

		-- 创建系统管理员用户
		PRINT N'创建系统管理员用户 - SU';
		EXEC dbo.spGetNextSequence @USER_ID OUTPUT, @ROW_ID OUTPUT;
		INSERT [dbo].[SY_USER] 
		(
			[USER_ID], 
			[USER_CODE], 
			[USER_NAME], 
			[USER_TYPE], 
			[LOGIN_PWD], 
			[AUTH_PWD], 
			[PWD_EXPIRY_DATE], 
			[SYSTEM_SCOPE], 
			[CREATED_BY], 
			[CREATION_DATE], 
			[LAST_UPDATED_BY], 
			[LAST_UPDATE_DATE]
		)
		VALUES 
		(
			@USER_ID,
			N'SU',
			N'System User',
			N'SA',
			N'e2S7K60BAgY=',
			N'e2S7K60BAgY=',
			DATEADD(DAY, 180, @CUR_DATE),
			@SYS_CODE,
			@CUR_USER,
			@CUR_DATE,
			@CUR_USER,
			@CUR_DATE
		);

		--关联用户和角色
		PRINT N'将角色 System 赋予用户 SU';
		INSERT [dbo].[SY_USER_ROLE] ([ROLE_ID], [USER_ID], [ACTIVE_FLAG], [CREATED_BY], [CREATION_DATE], [LAST_UPDATED_BY], [LAST_UPDATE_DATE]) VALUES (@ROLE_ID, @USER_ID, N'Y', @CUR_USER, @CUR_DATE, @CUR_USER, @CUR_DATE);
	END

	PRINT N'开始创建超级用户 - ADMIN';
	BEGIN
		-- 创建超级用户
		PRINT N'创建超级用户 - ADMIN';
		EXEC dbo.spGetNextSequence @USER_ID OUTPUT, @ROW_ID OUTPUT;
		INSERT [dbo].[SY_USER] 
		(
			[USER_ID], 
			[USER_CODE], 
			[USER_NAME], 
			[USER_TYPE], 
			[LOGIN_PWD], 
			[AUTH_PWD], 
			[PWD_EXPIRY_DATE], 
			[SYSTEM_SCOPE], 
			[CREATED_BY], 
			[CREATION_DATE], 
			[LAST_UPDATED_BY], 
			[LAST_UPDATE_DATE]
		)
		VALUES 
		(
			@USER_ID,
			N'ADMIN',
			N'Super User',
			N'SA',
			N'e2S7K60BAgY=',
			N'e2S7K60BAgY=',
			DATEADD(DAY, 180, @CUR_DATE),
			@SYS_CODE,
			@CUR_USER,
			@CUR_DATE,
			@CUR_USER,
			@CUR_DATE
		);

		--关联用户和角色
		PRINT N'将角色 Administrators, System 赋予用户 ADMIN';
		INSERT [dbo].[SY_USER_ROLE] ([ROLE_ID], [USER_ID], [ACTIVE_FLAG], [CREATED_BY], [CREATION_DATE], [LAST_UPDATED_BY], [LAST_UPDATE_DATE]) 
			SELECT ROLE_ID, @USER_ID, N'Y', @CUR_USER, @CUR_DATE, @CUR_USER, @CUR_DATE
			FROM SY_ROLE
			WHERE ROLE_CODE IN (N'Administrators', N'System');
	END
END TRY
BEGIN CATCH
	IF @@TRANCOUNT > 0
		ROLLBACK TRANSACTION;
	PRINT ERROR_MESSAGE();
END CATCH

IF @@TRANCOUNT > 0
	COMMIT TRANSACTION;
GO