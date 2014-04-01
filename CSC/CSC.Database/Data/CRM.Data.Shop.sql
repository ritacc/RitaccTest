/**********************************************************************/
-- Description:		初始化系统数据
---------------------------------------------------------------------
-- Action		Date			Staff		Version		Remarks
-- Create	 	2011-12-27		Zhangbo		1.00.00
/**********************************************************************/
--===========================================================================================================
DECLARE @SYS_CODE		nvarchar(4);
DECLARE @BU_CODE	nvarchar(10);
DECLARE @ResultType		int;
DECLARE @ResultMessage	nvarchar(1000);


SET @SYS_CODE = N'CSC'
SET @BU_CODE= N'CSC';

BEGIN TRANSACTION

BEGIN TRY
	PRINT N'开始初始化店数据';
	EXEC INIT_SHOP_DATA @SYS_CODE, N'HK'	,N'Services HK Services Centre'		,@BU_CODE,	N'CS'		, @ResultType OUTPUT, @ResultMessage OUTPUT;
	EXEC INIT_SHOP_DATA @SYS_CODE, N'MAIN'	,N'Parts Main Godown'				,@BU_CODE,	N'GODOWN'	, @ResultType OUTPUT, @ResultMessage OUTPUT;
	EXEC INIT_SHOP_DATA @SYS_CODE, N'MC'	,N'Parts Macau Godown'				,@BU_CODE,	N'GODOWN'	, @ResultType OUTPUT, @ResultMessage OUTPUT;
	EXEC INIT_SHOP_DATA @SYS_CODE, N'PRC'	,N'Parts China Godown '				,@BU_CODE,	N'GODOWN'	, @ResultType OUTPUT, @ResultMessage OUTPUT;
	PRINT N'店数据初始化完毕';

	DROP PROCEDURE INIT_SHOP_DATA;
END TRY
BEGIN CATCH
	IF @@TRANCOUNT > 0
		ROLLBACK TRANSACTION;
	PRINT ERROR_MESSAGE();
END CATCH

IF @@TRANCOUNT > 0
	COMMIT TRANSACTION;
GO