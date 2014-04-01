/**********************************************************************/
-- Description:		初始化 SY_TRANSACTION_DATE 数据
---------------------------------------------------------------------
-- Action		Date			Staff		Version		Remarks
-- Create	 	2013-05-31		Xj			1.00.00
/**********************************************************************/
CREATE PROCEDURE INIT_SY_TRANSACTION_DATE_DATA
(
	@CUR_DATE			datetime,		-- 初始化日期
	@CUR_USER_ID		bigint,			-- 初始化用户
	@ResultType			int				OUTPUT,
	@ResultMessage		nvarchar(1000)	OUTPUT
)
AS
BEGIN
	DECLARE @BU_CODE	nvarchar(10)= N'CSC';

	--暂时不确定文档类型
	INSERT SY_TRANSACTION_DATE (BU_CODE, TRAN_DATE, [CREATED_BY], [CREATION_DATE], [LAST_UPDATED_BY], [LAST_UPDATE_DATE])
		SELECT @BU_CODE,N'2013-05-01', @CUR_USER_ID, @CUR_DATE, @CUR_USER_ID, @CUR_DATE 
		

	SET @ResultType		= 0;
	SET @ResultMessage	= N'';
END