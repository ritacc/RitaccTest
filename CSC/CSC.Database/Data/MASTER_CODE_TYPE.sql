/**********************************************************************/
-- Description:		初始化 MASTER_CODE_TYPE 数据
---------------------------------------------------------------------
-- Action		Date			Staff		Version		Remarks
-- Create	 	2013-04-23		xj		1.00.00
/**********************************************************************/
CREATE PROCEDURE INIT_MASTER_CODE_TYPE_DATA
(
	@BU_CODE			nvarchar(10),
	@CUR_DATE			datetime,		-- 初始化日期
	@CUR_USER_ID		bigint,			-- 初始化用户
	@ResultType			int				OUTPUT,
	@ResultMessage		nvarchar(1000)	OUTPUT
)
AS
BEGIN
	
	INSERT MASTER_CODE_TYPE (MASTER_TYPE_CODE, MASTER_TYPE_DESC, SORT_SEQ, BU_CODE, [CREATED_BY], [CREATION_DATE], [LAST_UPDATED_BY], [LAST_UPDATE_DATE])
		SELECT N'ACCT_COMP'			, N'Company'									, 1		,@BU_CODE, @CUR_USER_ID, @CUR_DATE, @CUR_USER_ID, @CUR_DATE UNION
		SELECT N'ACCT_DEPT'			, N'Department / SubLocation'					, 2		,@BU_CODE, @CUR_USER_ID, @CUR_DATE, @CUR_USER_ID, @CUR_DATE UNION
		SELECT N'ACCT_CODE'			, N'Account Code'								, 3		,@BU_CODE, @CUR_USER_ID, @CUR_DATE, @CUR_USER_ID, @CUR_DATE UNION
		SELECT N'ACCT_CNT_PARTY'	, N'Counterparty'								, 4		,@BU_CODE, @CUR_USER_ID, @CUR_DATE, @CUR_USER_ID, @CUR_DATE UNION
		SELECT N'ACCT_BUS_ACT'		, N'Business Activities'						, 5		,@BU_CODE, @CUR_USER_ID, @CUR_DATE, @CUR_USER_ID, @CUR_DATE UNION
		SELECT N'ACCT_SALE_DES'		, N'Sales Destination'							, 6		,@BU_CODE, @CUR_USER_ID, @CUR_DATE, @CUR_USER_ID, @CUR_DATE ;


	SET @ResultType		= 0;
	SET @ResultMessage	= N'';
END