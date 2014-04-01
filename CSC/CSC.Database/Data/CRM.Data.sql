/**********************************************************************/
-- Description:		初始化系统数据
---------------------------------------------------------------------
-- Action		Date			Staff		Version		Remarks
-- Create	 	2011-12-27		Zhangbo		1.00.00
/**********************************************************************/
--===========================================================================================================
DECLARE @SYS_CODE		nvarchar(4)		= N'CSC';
DECLARE @SYS_NAME		nvarchar(50)	= N'CSC';
DECLARE @CUR_USER_ID	bigint			= 0;
DECLARE @CUR_DATE		datetime		= GETDATE();
DECLARE @ResultType		int;
DECLARE @ResultMessage	nvarchar(1000);

BEGIN TRANSACTION

BEGIN TRY
	PRINT N'开始初始化 SY_SYSTEM 数据';
	EXEC INIT_SY_SYSTEM_DATA @SYS_CODE, @SYS_NAME, @CUR_DATE, @CUR_USER_ID, @ResultType OUTPUT, @ResultMessage OUTPUT;
	PRINT N'SY_SYSTEM 数据初始化完毕';

	PRINT N'开始初始化 BUSINESS_UNIT 数据';
	EXEC INIT_BUSINESS_UNIT_DATA @SYS_CODE, @SYS_NAME, @CUR_DATE, @CUR_USER_ID, @ResultType OUTPUT, @ResultMessage OUTPUT;
	PRINT N'SY_SYSTEM 数据初始化完毕';

	PRINT N'开始初始化 SY_FUNCTION 数据';
	EXEC INIT_SY_FUNCTION_DATA @SYS_CODE, @CUR_DATE, @CUR_USER_ID, @ResultType OUTPUT, @ResultMessage OUTPUT;
	PRINT N'SY_FUNCTION 数据初始化完毕';

	PRINT N'开始初始化 MASTER_CODE_TYPE 数据';
	EXEC INIT_MASTER_CODE_TYPE_DATA @SYS_CODE, @CUR_DATE, @CUR_USER_ID, @ResultType OUTPUT, @ResultMessage OUTPUT;
	PRINT N'MASTER_CODE_TYPE 数据初始化完毕';

	PRINT N'开始初始化 INIT_SY_DOC_NO_SEQ_DATA 数据';
	EXEC INIT_SY_DOC_NO_SEQ_DATA @CUR_DATE, @CUR_USER_ID, @ResultType OUTPUT, @ResultMessage OUTPUT;
	PRINT N'INIT_SY_DOC_NO_SEQ_DATA 数据初始化完毕';

	PRINT N'开始初始化 SY_TRANSACTION_DATE 数据';
	EXEC INIT_SY_TRANSACTION_DATE_DATA @CUR_DATE, @CUR_USER_ID, @ResultType OUTPUT, @ResultMessage OUTPUT;
	PRINT N'SY_TRANSACTION_DATE 数据初始化完毕';

	PRINT N'开始初始化 初始化 SY_PARAMETER,SY_PARAMETER_SHOP 数据 数据';
	EXEC INIT_SY_PARAMETER_DATA @SYS_CODE, @CUR_DATE, @CUR_USER_ID, @ResultType OUTPUT, @ResultMessage OUTPUT;
	PRINT N'初始化 SY_PARAMETER,SY_PARAMETER_SHOP 数据 数据初始化完毕';

	DROP PROCEDURE INIT_SY_SYSTEM_DATA;
	DROP PROCEDURE INIT_BUSINESS_UNIT_DATA;
	DROP PROCEDURE INIT_SY_FUNCTION_DATA;
	DROP PROCEDURE INIT_MASTER_CODE_TYPE_DATA;
	DROP PROCEDURE INIT_SY_DOC_NO_SEQ_DATA;
	DROP PROCEDURE INIT_SY_TRANSACTION_DATE_DATA;
	DROP PROCEDURE INIT_SY_PARAMETER_DATA;
	
END TRY
BEGIN CATCH
	IF @@TRANCOUNT > 0
		ROLLBACK TRANSACTION;
	PRINT ERROR_MESSAGE();
END CATCH

IF @@TRANCOUNT > 0
	COMMIT TRANSACTION;
GO