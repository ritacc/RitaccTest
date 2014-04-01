-- =============================================  
-- Author:  JuLuDe  
-- Create date: 2011-9-16  
-- Description: 保存用户  
-- =============================================  
CREATE PROCEDURE spSaveUser  
(  
 @USER_ID         BIGINT	OUTPUT,            --用户ID  
 @USER_CODE       NVARCHAR(20),     --用户编码  
 @USER_NAME       NVARCHAR(50),     --用户名称  
 @USER_TYPE       NVARCHAR(2),   --用户类型  
 @SYSTEM_SCOPE    NVARCHAR(4),     --使用系统   
 @LOGIN_PWD    NVARCHAR(60),     --使用系统 
 @PWD_EXPIRY_DAYS   BIGINT,    
 @LAST_UPDATE_DATE	DATETIME,
 @LAST_UPDATED_BY	BIGINT,
 @CURRENT_USER_ID	BIGINT ,
 @CURRENT_USER_TYPE	NVARCHAR(2),
 @ResultType		INT				OUTPUT,
 @ResultMessage		NVARCHAR(1000)	OUTPUT
)  
AS  
BEGIN
	SET NOCOUNT ON

	--数据重复
	if exists(select 1 from SY_USER where [USER_ID]<>@USER_ID and upper(USER_CODE)=upper(@USER_CODE) and SYSTEM_SCOPE=@SYSTEM_SCOPE) 
	BEGIN
		SET @ResultType = -1001;
		SET @ResultMessage = 'RECORD CONTAINS DUPLICATE DATA.';
		RETURN @ResultType;
	END  

	IF (@USER_ID = 0)
	BEGIN
		if(@CURRENT_USER_TYPE = N'NS')
		BEGIN
			SET @USER_TYPE = N'NS';
		END	

		 --获取ID值 
		DECLARE @Sequence      BIGINT;     
		DECLARE @RowId         UNIQUEIDENTIFIER;   
		EXEC spGetNextSequence  @Sequence output, @RowId output;     

		INSERT INTO SY_USER
		(
			[USER_ID]
			,USER_CODE
			,[USER_NAME]
			,USER_TYPE
			,SYSTEM_SCOPE
			,FROZEN_FLAG
			,LOGIN_PWD
			,PWD_EXPIRY_DATE
			,LAST_UPDATE_DATE
			,LAST_UPDATED_BY
			,CREATION_DATE
			,CREATED_BY
			,LAST_UPDATE_LOGIN		
		)
		VALUES
		(
			@Sequence
			,upper(@USER_CODE)
			,upper(@USER_NAME)
			,@USER_TYPE
			,@SYSTEM_SCOPE
			,N'N'
			,@LOGIN_PWD
			,GETDATE() + @PWD_EXPIRY_DAYS
			,GETDATE()--Convert(datetime, Convert(varchar(20), GETDATE(), 120), 120)
			,@CURRENT_USER_ID
			,GETDATE()--Convert(datetime, Convert(varchar(20), GETDATE(), 120), 120)
			,@CURRENT_USER_ID
			,@CURRENT_USER_ID
		);

		--将function数据写入SY_ROLE_FUNC
		--insert into SY_ROLE_FUNC
		--select 
		--	FUNC_ID
		--	,@Sequence
		--	,'N'
		--	,GETDATE()
		--	,@CURRENT_USER_ID
		--	,GETDATE()
		--	,@CURRENT_USER_ID
		--	,@CURRENT_USER_ID
		--	,'N'
		--	,'N'
		--from SY_FUNCTION
		--where SY_FUNCTION.SYS_CODE = @SYS_CODE
		--	and SY_FUNCTION.ADMIN_FLAG = dbo.fnConvertBitToNvarchar(@ADMIN_FLAG);

		SET @USER_ID = @Sequence; 
		SET @ResultType = 0;
		SET @ResultMessage = 'INERT SUCCEED';
		RETURN @ResultType;
	END
	ELSE
	BEGIN
		IF NOT EXISTS(SELECT 1 FROM SY_USER WHERE [USER_ID] = @USER_ID AND SYSTEM_SCOPE = @SYSTEM_SCOPE  AND (datediff(S,LAST_UPDATE_DATE,@LAST_UPDATE_DATE)=0  OR LAST_UPDATED_BY = @CURRENT_USER_ID))
		BEGIN
			SET @ResultType = -1003;
			SET @ResultMessage = 'RECORD IS CHANGED';
			RETURN @ResultType;
		END
		
		--如果ADMIN_FLAG发生变化，重新将function数据写入SY_ROLE_FUNC
		--declare @OLD_ADMIN_FLAG nvarchar(1);
		--select @OLD_ADMIN_FLAG = ADMIN_FLAG
		--from SY_ROLE
		--where ROLE_ID = @ROLE_ID;

		--if(@OLD_ADMIN_FLAG <> dbo.fnConvertBitToNvarchar(@ADMIN_FLAG))
		--begin
		--	delete from SY_ROLE_FUNC where ROLE_ID = @ROLE_ID;

		--	--将function数据写入SY_ROLE_FUNC
		--	insert into SY_ROLE_FUNC
		--	select 
		--		FUNC_ID
		--		,@ROLE_ID
		--		,'N'
		--		,GETDATE()
		--		,@CURRENT_USER_ID
		--		,GETDATE()
		--		,@CURRENT_USER_ID
		--		,@CURRENT_USER_ID
		--		,'N'
		--		,'N'
		--	from SY_FUNCTION
		--	where SY_FUNCTION.SYS_CODE = @SYS_CODE
		--		and SY_FUNCTION.ADMIN_FLAG = dbo.fnConvertBitToNvarchar(@ADMIN_FLAG);
		--end

		UPDATE 
			SY_USER
		SET 
			[USER_NAME] = upper(@USER_NAME)
			,USER_TYPE = (case when @CURRENT_USER_TYPE = N'NS' then USER_TYPE else @USER_TYPE end)
			,LAST_UPDATE_DATE = GETDATE()--Convert(datetime, Convert(varchar(20), GETDATE(), 120), 120)
			,LAST_UPDATED_BY = @CURRENT_USER_ID
			,LAST_UPDATE_LOGIN = @CURRENT_USER_ID
		WHERE
			[USER_ID] = @USER_ID;
			
		SET @ResultType = 0;
		SET @ResultMessage = 'UPDATE SUCCEED';
		RETURN @ResultType;
	END

	

	
END 