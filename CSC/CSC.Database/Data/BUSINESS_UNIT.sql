/**********************************************************************/
-- Description:		初始化 BUSINESS_UNIT 数据
---------------------------------------------------------------------
-- Action		Date			Staff		Version		Remarks
-- Create	 	2013-4-7		xj			1.00.00
/**********************************************************************/
CREATE PROCEDURE INIT_BUSINESS_UNIT_DATA
(
	@SYS_CODE			nvarchar(4),
	@SYS_NAME			nvarchar(50),
	@CUR_DATE			datetime,		-- 初始化日期
	@CUR_USER_ID		bigint,			-- 初始化用户
	@ResultType			int				OUTPUT,
	@ResultMessage		nvarchar(1000)	OUTPUT
)
AS
BEGIN
	INSERT [BUSINESS_UNIT] ([BU_CODE], [BU_NAME], [CREATED_BY], [CREATION_DATE], [LAST_UPDATED_BY], [LAST_UPDATE_DATE])
		VALUES (@SYS_CODE, @SYS_NAME, @CUR_USER_ID, @CUR_DATE, @CUR_USER_ID, @CUR_DATE)

	SET @ResultType		= 0;
	SET @ResultMessage	= N'';
END