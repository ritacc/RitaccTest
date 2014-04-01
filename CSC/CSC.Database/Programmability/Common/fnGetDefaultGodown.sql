/**********************************************************************/
-- Description:	 Get Default Godown
---------------------------------------------------------------------
-- Action		Date			Staff		Version		Remarks
-- Create	 	2013-07-10		LM		1.00.00
---------------------------------------------------------------------
/**********************************************************************/
CREATE Function fnGetDefaultGodown(
	@SYS_CODE nvarchar(4) = N'',
	@BU_CODE  nvarchar(10) = N'',
	@WH_CODE  nvarchar(8) = N''
)
RETURNS BIGINT
AS
BEGIN
	DECLARE @RESULT NVARCHAR(30),@TO_GODOWN_ID BIGINT
	EXEC @RESULT =  fnGetParameterValue @SYS_CODE,@WH_CODE,@BU_CODE,'DFT_GODOWN_CSC'

	declare @index int
	set @index=charindex('.',@RESULT)

	IF @index<=0
		return -1;

	DECLARE @SETWH NVARCHAR(20),@SETGODOWN NVARCHAR(20)

	SET @SETWH= substring(@RESULT,0,@index)
	SET @SETGODOWN= substring(@RESULT,@index+1,len(@RESULT))
	
	IF NOT EXISTS(SELECT 1 FROM GODOWN WHERE GODOWN.GODOWN_CODE= @SETGODOWN AND WAREHOUSE_CODE = @SETWH)
	BEGIN		
		RETURN -1
	END

	SELECT @TO_GODOWN_ID= GODOWN_ID FROM GODOWN WHERE GODOWN.GODOWN_CODE= @SETGODOWN AND WAREHOUSE_CODE = @SETWH

	RETURN @TO_GODOWN_ID;

END

