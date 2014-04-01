/**********************************************************************/
-- Description:	 
---------------------------------------------------------------------
-- Action		Date			Staff		Version		Remarks
-- Create	 	2013-08-27		zcs			1.00.00
---------------------------------------------------------------------
/**********************************************************************/
Create Procedure  spPartsDailyUnitCostUpdate
(
	@BU_CODE				NVARCHAR(30),
	@TRANSACTION_DATE		DATETIME,	 

	@ResultType				INT				OUTPUT,
	@ResultMessage			NVARCHAR(1000)	OUTPUT
)
AS	
BEGIN
	DECLARE @PARTS_ID BIGINT
	DECLARE @M_STOCK_QTY BIGINT,
			@M_TOTAL_COST DECIMAL(14,2),
			@M_UNIT_COST DECIMAL(7,2)
	DECLARE curParts CURSOR FOR SELECT DISTINCT PARTS_ID FROM PARTS_GODOWN WHERE BU_CODE=@BU_CODE
	DECLARE @CREATED_BY BIGINT

	SELECT TOP 1 @CREATED_BY =SY_USER.USER_ID FROM SY_USER 

	OPEN curParts
	FETCH curParts INTO @PARTS_ID	
	WHILE @@FETCH_STATUS=0 
	BEGIN
		
		EXEC dbo.spGetPartsLatestQtyUnitCost @BU_CODE,@PARTS_ID,@TRANSACTION_DATE,'REALTIME',@M_STOCK_QTY OUTPUT,@M_TOTAL_COST  OUTPUT,@M_UNIT_COST OUTPUT
												,@ResultType  OUTPUT,@ResultMessage OUTPUT
		INSERT INTO PARTS_DAYEND_QTY_COST
			(BU_CODE,PARTS_ID,EFFECTIVE_DATE,CLOSE_STOCK_QTY, TOTAL_COST, UNIT_COST,CREATED_BY,CREATION_DATE,LAST_UPDATED_BY,LAST_UPDATE_DATE)
		VALUES 
			(@BU_CODE, @PARTS_ID, @TRANSACTION_DATE,@M_STOCK_QTY, @M_TOTAL_COST, @M_UNIT_COST,@CREATED_BY,GETDATE(),@CREATED_BY,GETDATE())
		
		FETCH curParts INTO @PARTS_ID
	END
	CLOSE curParts
	DEALLOCATE curParts
	
	SET @ResultType=0;
	SET @ResultMessage = N'Success'
END