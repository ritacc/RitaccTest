/**********************************************************************/
-- Description:		初始化 SY_PARAMETER,SY_PARAMETER_SHOP 数据
---------------------------------------------------------------------
-- Action		Date			Staff		Version		Remarks
-- Create	 	2011-11-26		Zhangbo		1.00.00
/**********************************************************************/
CREATE PROCEDURE INIT_SY_PARAMETER_DATA
(
	@SYS_CODE			nvarchar(4),
	@CUR_DATE			datetime,		-- 初始化日期
	@CUR_USER_ID		bigint,			-- 初始化用户
	@ResultType			int				OUTPUT,
	@ResultMessage		nvarchar(1000)	OUTPUT
)
AS 
BEGIN
	DECLARE @BU_CODE NVARCHAR(10) = N'CSC';

	INSERT [SY_PARAMETER] ([PARA_ID],[PARA_CODE], [PARA_DSC],[REMARKS], [PARA_TYPE], [PARA_VALUE], [SYS_CODE],BU_CODE, [CREATED_BY], [CREATION_DATE], [LAST_UPDATED_BY], [LAST_UPDATE_DATE]) 
		SELECT 1 , N'DFT_GODOWN_CSC'		, N'Default Godown to this Shop'	, N'Default Godown to this Shop'	,N'C'	,N'MAIN'	,@SYS_CODE,@BU_CODE, @CUR_USER_ID, @CUR_DATE, @CUR_USER_ID, @CUR_DATE UNION
		SELECT 2 , N'PRICE_INTERNAL_FACTOR'	, N'PRICE_INTERNAL_FACTOR'	, N'PRICE_INTERNAL_FACTOR'	,N'N'	,N'2'	,@SYS_CODE,@BU_CODE, @CUR_USER_ID, @CUR_DATE, @CUR_USER_ID, @CUR_DATE UNION
		SELECT 3 , N'PRICE_ASSESSORY_FACTOR'	, N'PRICE_ASSESSORY_FACTOR'	, N'PRICE_ASSESSORY_FACTOR'	,N'N'	,N'2'	,@SYS_CODE,@BU_CODE, @CUR_USER_ID, @CUR_DATE, @CUR_USER_ID, @CUR_DATE;

	INSERT [SY_PARAMETER_SHOP] ([SHOP_CODE],[PARA_S_CODE], [PARA_TYPE],[PARA_S_VALUE], [SYS_CODE],BU_CODE, [CREATED_BY], [CREATION_DATE], [LAST_UPDATED_BY], [LAST_UPDATE_DATE]) 
		SELECT SY_SHOP.CODE, N'DFT_GODOWN_CSC'	,N'C'	,N'MAIN'	,@SYS_CODE,@BU_CODE, @CUR_USER_ID, @CUR_DATE, @CUR_USER_ID, @CUR_DATE
		FROM SY_SHOP
		WHERE SY_SHOP.SYS_CODE = @SYS_CODE
			AND SY_SHOP.BU_CODE = @BU_CODE;
		

	SET @ResultType		= 0;
	SET @ResultMessage	= N'';
END
GO