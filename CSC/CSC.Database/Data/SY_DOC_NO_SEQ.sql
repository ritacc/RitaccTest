/**********************************************************************/
-- Description:		初始化 SY_DOC_NO_SEQ 数据
---------------------------------------------------------------------
-- Action		Date			Staff		Version		Remarks
-- Create	 	2011-11-26		Zhangbo		1.00.00
/**********************************************************************/
CREATE PROCEDURE INIT_SY_DOC_NO_SEQ_DATA
(
	@CUR_DATE			datetime,		-- 初始化日期
	@CUR_USER_ID		bigint,			-- 初始化用户
	@ResultType			int				OUTPUT,
	@ResultMessage		nvarchar(1000)	OUTPUT
)
AS
BEGIN
	DECLARE @BU_CODE	nvarchar(10)= N'CSC';

	
	INSERT SY_DOC_NO_SEQ ([NO_TYPE], [PREFIX], [NEXT_AVAIL_SEQ], [YEAR_LGH], [MONTH_LGH], [DAY_LGH], [NO_LGH],[RESET_PERIOD], [BU_CODE], [CREATED_BY], [CREATION_DATE], [LAST_UPDATED_BY], [LAST_UPDATE_DATE])
		--Purchases Order
		SELECT N'PURCHASE_ORDER'	, N'PO'	, 1, 0, 0, 0, 7, N''	, @BU_CODE, @CUR_USER_ID, @CUR_DATE, @CUR_USER_ID, @CUR_DATE UNION
		--Stock Arrival Maintenance
		SELECT N'RECEIVED'			, N'RC'	, 1, 0, 0, 0, 7, N''	, @BU_CODE, @CUR_USER_ID, @CUR_DATE, @CUR_USER_ID, @CUR_DATE UNION
		--Requisition
		SELECT N'REQUISITION'		, N'RA'	, 1, 0, 0, 0, 7, N''	, @BU_CODE, @CUR_USER_ID, @CUR_DATE, @CUR_USER_ID, @CUR_DATE UNION
		--Requisition Return
		SELECT N'REQ_RETURN'		, N'RR'	, 1, 0, 0, 0, 7, N''	, @BU_CODE, @CUR_USER_ID, @CUR_DATE, @CUR_USER_ID, @CUR_DATE UNION
		--Sales Memo (Payment type = C or B)
		SELECT N'SALES_MENO_CB'		, N'SM'	, 1, 0, 0, 0, 7, N''	, @BU_CODE, @CUR_USER_ID, @CUR_DATE, @CUR_USER_ID, @CUR_DATE UNION
		--Sales Memo (Payment type = A)
		SELECT N'SALES_MENO_A'		, N'SI'	, 1, 0, 0, 0, 7, N''	, @BU_CODE, @CUR_USER_ID, @CUR_DATE, @CUR_USER_ID, @CUR_DATE UNION
		--Sales Return
		SELECT N'SALES_RETURN'		, N'SR'	, 1, 0, 0, 0, 7, N''	, @BU_CODE, @CUR_USER_ID, @CUR_DATE, @CUR_USER_ID, @CUR_DATE UNION
		--Stock Transfer
		SELECT N'STOCK_TRF'			, N'ST'	, 1, 0, 0, 0, 7, N''	, @BU_CODE, @CUR_USER_ID, @CUR_DATE, @CUR_USER_ID, @CUR_DATE UNION
		--Stock Adjustment
		SELECT N'STOCK_ADJ'			, N'SJ'	, 1, 0, 0, 0, 7, N''	, @BU_CODE, @CUR_USER_ID, @CUR_DATE, @CUR_USER_ID, @CUR_DATE UNION
		--Stock Take
		SELECT N'STOCK_TAKE'		, N'SK'	, 1, 2, 0, 0, 5, N'Y'	, @BU_CODE, @CUR_USER_ID, @CUR_DATE, @CUR_USER_ID, @CUR_DATE UNION
		--Product No
		SELECT N'PRODUCT_NO'		, N''	, 1, 0, 0, 0, 7, N''	, @BU_CODE, @CUR_USER_ID, @CUR_DATE, @CUR_USER_ID, @CUR_DATE UNION
		--Job No.
		SELECT N'JOB_NO'			, N''	, 1, 2, 0, 0, 7, N'Y'	, @BU_CODE, @CUR_USER_ID, @CUR_DATE, @CUR_USER_ID, @CUR_DATE UNION
		--Quotation
		SELECT N'QUOTATION'			, N'Q'	, 1, 0, 0, 0, 7, N''	, @BU_CODE, @CUR_USER_ID, @CUR_DATE, @CUR_USER_ID, @CUR_DATE UNION
		--ID/OD
		SELECT N'IDOD'				, N''	, 1, 0, 0, 0, 7, N''	, @BU_CODE, @CUR_USER_ID, @CUR_DATE, @CUR_USER_ID, @CUR_DATE UNION
		--Ref No. (Maintenance renewal notice)
		--SELECT N'MAIN_RENEWAL'		, N''	, 1, 0, 0, 0, 7, N''	, @BU_CODE, @CUR_USER_ID, @CUR_DATE, @CUR_USER_ID, @CUR_DATE UNION
		--Policy No. (Maintenance policy)
		SELECT N'MAIN_POLICY'		, N'M'	, 1, 0, 0, 0, 7, N''	, @BU_CODE, @CUR_USER_ID, @CUR_DATE, @CUR_USER_ID, @CUR_DATE UNION
		--AR Invoice
		SELECT N'AR_INV'			, N'I'	, 1, 0, 0, 0, 7, N''	, @BU_CODE, @CUR_USER_ID, @CUR_DATE, @CUR_USER_ID, @CUR_DATE UNION
		--Payment Receipt
		SELECT N'PAYMENT_RECEIPT'	, N'R'	, 1, 0, 0, 0, 7, N''	, @BU_CODE, @CUR_USER_ID, @CUR_DATE, @CUR_USER_ID, @CUR_DATE UNION
		--Pint Job Sheet
		SELECT N'JO_PRINT_BATCH'	, N''	, 1, 2, 2, 2, 5, N'D'	, @BU_CODE, @CUR_USER_ID, @CUR_DATE, @CUR_USER_ID, @CUR_DATE UNION
		--MAIN_RENEWAL_NO
		SELECT N'MAIN_RENEWAL_NO'	, N''	, 1, 0, 0, 0, 7, N''	, @BU_CODE, @CUR_USER_ID, @CUR_DATE, @CUR_USER_ID, @CUR_DATE UNION
		--RENEW_LETTER_REF
		SELECT N'RENEW_LETTER_REF'	, N''	, 1, 0, 0, 0, 7, N''	, @BU_CODE, @CUR_USER_ID, @CUR_DATE, @CUR_USER_ID, @CUR_DATE UNION
		--PO_RECEIPT_CORRECTION
		SELECT N'RECEIVED_CORRECTION'	, N'CO'	, 1, 0, 0, 0, 7, N''	, @BU_CODE, @CUR_USER_ID, @CUR_DATE, @CUR_USER_ID, @CUR_DATE UNION
		--PO_RECEIPT_RETURN
		SELECT N'RECEIVED_RETURN'		, N'RN'	, 1, 0, 0, 0, 7, N''	, @BU_CODE, @CUR_USER_ID, @CUR_DATE, @CUR_USER_ID, @CUR_DATE UNION
		--JOB_ORDER_ADJ
		SELECT N'JOB_ADJ'				, N''	, 1, 2, 0, 0, 7, N'Y'	, @BU_CODE, @CUR_USER_ID, @CUR_DATE, @CUR_USER_ID, @CUR_DATE UNION
		--AR_DRNOTE
		SELECT N'AR_DRNOTE'				, N'DN'	, 1, 0, 0, 0, 7, N''	, @BU_CODE, @CUR_USER_ID, @CUR_DATE, @CUR_USER_ID, @CUR_DATE UNION
		--AR_CRNOTE
		SELECT N'AR_CRNOTE'				, N'CN'	, 1, 0, 0, 0, 7, N''	, @BU_CODE, @CUR_USER_ID, @CUR_DATE, @CUR_USER_ID, @CUR_DATE UNION
		--Bad Debt / Refund Maintenance
		SELECT N'VOUCHER'				, N'V'	, 1, 0, 0, 0, 7, N''	, @BU_CODE, @CUR_USER_ID, @CUR_DATE, @CUR_USER_ID, @CUR_DATE;

	SET @ResultType		= 0;
	SET @ResultMessage	= N'';
END


