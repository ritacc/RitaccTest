/**********************************************************************/
-- Description:	查找角色-功能
---------------------------------------------------------------------
-- Action	Date			Staff		Version		Remarks
-- Modify	2012-11-20		Zhangbo		1.01.00		加入是否开启车主会的筛选
-- Create	2011-09-20		XJ			1.00.00
---------------------------------------------------------------------
/**********************************************************************/
CREATE PROCEDURE spSearchRoleFunc
(
	@SYS_CODE		nvarchar(4),
	@ROLE_ID		bigint			= 0,	--角色ID
	@ADMIN_FLAG		bit,
	@ResultType		int				OUTPUT,
	@ResultMessage	nvarchar(1000)	OUTPUT
)
AS
BEGIN
	
	SELECT
		SY_FUNCTION.FUNC_CODE,
		SY_FUNCTION.FUNC_TYPE,
		SY_FUNCTION.DSC,
		SY_FUNCTION.SYSTEM_SCOPE,
		dbo.fnConvertNvarcharToBit(SY_FUNCTION.ADMIN_FLAG)  AS ADMIN_FLAG,
		SY_FUNCTION.FUNC_ID,
		--SY_ROLE_FUNC.ROLE_ID,
		dbo.fnConvertNvarcharToBit(SY_ROLE_FUNC.INSERTABLE_FLAG)  AS INSERTABLE_FLAG, 
		dbo.fnConvertNvarcharToBit(SY_ROLE_FUNC.UPDATABLE_FLAG) AS UPDATABLE_FLAG,
		dbo.fnConvertNvarcharToBit(SY_ROLE_FUNC.ACTIVE_FLAG) AS ACTIVE_FLAG
	FROM
		SY_FUNCTION
		LEFT OUTER JOIN SY_ROLE_FUNC on SY_ROLE_FUNC.FUNC_ID=SY_FUNCTION.FUNC_ID AND SY_ROLE_FUNC.ROLE_ID=@ROLE_ID AND SY_ROLE_FUNC.ACTIVE_FLAG=N'Y'
	WHERE
		SY_FUNCTION.SYS_CODE = @SYS_CODE
		AND SY_FUNCTION.ADMIN_FLAG = dbo.fnConvertBitToNvarchar(@ADMIN_FLAG)
	ORDER BY
		SY_ROLE_FUNC.ACTIVE_FLAG DESC
		,SY_FUNCTION.FUNC_CODE ASC;

	SET @ResultType		= 0;
	SET @ResultMessage	= ''
	RETURN @ResultType;
END
GO