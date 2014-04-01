/**********************************************************************/
-- Description:	 
---------------------------------------------------------------------
-- Action		Date			Staff		Version		Remarks
-- Create	 	2013-08-27		zcs			1.00.00
---------------------------------------------------------------------
/**********************************************************************/
Create Procedure  spPartsTransDailyUnitCostUpdate
(
	@BU_CODE				NVARCHAR(30),
	@TRANSACTION_DATE		DATETIME,	 

	@ResultType				INT				OUTPUT,
	@ResultMessage			NVARCHAR(1000)	OUTPUT
)
AS	
BEGIN
	DECLARE @PARTS_ID BIGINT
	DECLARE @M_STOCK_QTY BIGINT,@M_TOTAL_COST DECIMAL(14,2),@M_UNIT_COST DECIMAL(7,2)

	DECLARE @TRANSACTION_DATE_Add_Day DATETIME

	SET @TRANSACTION_DATE_Add_Day=DATEADD(DAY,1,@TRANSACTION_DATE)

	DECLARE curParts CURSOR FOR SELECT DISTINCT PARTS_ID FROM PARTS_GODOWN WHERE BU_CODE=@BU_CODE

	OPEN curParts
	FETCH curParts INTO @PARTS_ID	
	WHILE @@FETCH_STATUS=0 
	BEGIN
		
		EXEC dbo.spGetPartsLatestQtyUnitCost @BU_CODE,@PARTS_ID,@TRANSACTION_DATE_Add_Day,'DAYEND',@M_STOCK_QTY OUTPUT,@M_TOTAL_COST  OUTPUT,@M_UNIT_COST OUTPUT
												,@ResultType  OUTPUT,@ResultMessage OUTPUT
		UPDATE 
			PARTS_GODOWN_TRANS 
		SET 
			UNIT_COST_DAYEND = @M_UNIT_COST
		WHERE 
			BU_CODE = @BU_CODE
			And PARTS_GODOWN_ID IN ( SELECT PARTS_GODOWN_ID FROM PARTS_GODOWN WHERE PARTS_ID=@PARTS_ID)
			And TXN_TYPE_ID in (SELECT TXN_TYPE_ID FROM PARTS_GODOWN_TRANS_TYPE Where UPDATE_COST_IND ='Y' AND TXN_DATE=@TRANSACTION_DATE)
		FETCH curParts INTO @PARTS_ID
	END
	CLOSE curParts
	DEALLOCATE curParts


	SET @ResultType=0;
	SET @ResultMessage = N'Success'
END