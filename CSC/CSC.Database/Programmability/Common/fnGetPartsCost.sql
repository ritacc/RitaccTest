/**********************************************************************/
-- Description:	GET COST FROM PARTS_COST
---------------------------------------------------------------------
-- Action		Date			Staff		Version		Remarks
-- Create	 	2013-06-13		zcs			1.00.00
---------------------------------------------------------------------
/**********************************************************************/
CREATE PROCEDURE spGetPartsCost
(
	 @BU_CODE	nvarchar(10),
	 @PARTS_ID	bigint,
	 @DATE		date,--trans_date
	 @Cost			bigint output
)
AS
BEGIN
	SET NOCOUNT ON;	
	
	IF EXISTS(select top 1 UNIT_COST from PARTS_DAYEND_QTY_COST where bu_code = @BU_CODE and parts_id = @PARTS_ID and effective_date <=@DATE)
	BEGIN
		select top 1 @Cost=UNIT_COST from PARTS_DAYEND_QTY_COST where bu_code = @BU_CODE and parts_id = @PARTS_ID and effective_date <=@DATE ORDER BY effective_date DESC
	END
	ELSE
	BEGIN
		SET @Cost=0
	END
END


go

CREATE FUNCTION fnGetPartsCost
(
	 @BU_CODE	nvarchar(10),
	 @PARTS_ID	bigint,
	 @DATE		datetime
)
RETURNS  Decimal(14,2)
AS
BEGIN
	DECLARE @Cost			bigint 

	IF EXISTS(select top 1 UNIT_COST from PARTS_DAYEND_QTY_COST where bu_code = @BU_CODE and parts_id = @PARTS_ID and effective_date <=@DATE)
	BEGIN
		select top  1 @Cost=UNIT_COST from PARTS_DAYEND_QTY_COST where bu_code = @BU_CODE and parts_id = @PARTS_ID and effective_date <=@DATE ORDER BY effective_date DESC
	END
	ELSE
	BEGIN
		SET @Cost=0
	END
	RETURN @Cost
END