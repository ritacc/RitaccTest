
/**********************************************************************/
-- Description:		GET PARAMETER VALUE
---------------------------------------------------------------------
-- Action		Date			Staff		Version		Remarks
-- Create	 	2011-12-03		KING			1.00.00
/**********************************************************************/
CREATE FUNCTION [fnGetParameterValue]
(
	@SYS_CODE		nvarchar(4)
	,@SHOP_CODE		nvarchar(4)
	,@BU_CODE		nvarchar(10)
	,@CODE			nvarchar(30)
)
RETURNS nvarchar(30)
AS
BEGIN
	DECLARE	@RetVar	nvarchar(30);
	
	SELECT 
		@RetVar = PARA_S_VALUE
	FROM 
		SY_PARAMETER_SHOP
	WHERE 
		SYS_CODE = @SYS_CODE
		AND BU_CODE = @BU_CODE
		AND SHOP_CODE = @SHOP_CODE
		AND PARA_S_CODE = @CODE;

	--IF @RetVar IS NULL
	--	SELECT 
	--		@RetVar = PARA_VALUE
	--	FROM 
	--		SY_PARAMETER
	--	WHERE 
	--		SYS_CODE = @SYS_CODE
	--		AND BU_CODE = @BU_CODE
	--		AND PARA_CODE = @CODE;
			
	RETURN LTRIM(RTRIM(ISNULL(@RetVar, N'')));
END

GO

CREATE FUNCTION [fnGetParaValue]
(
	 @SYS_CODE		nvarchar(4)	 
	,@BU_CODE		nvarchar(10)
	,@CODE			nvarchar(30)
)
RETURNS nvarchar(30)
AS
BEGIN
	DECLARE	@RetVar	nvarchar(30);
	
	SELECT 
		@RetVar = PARA_VALUE
	FROM 
		SY_PARAMETER
	WHERE 
		SYS_CODE = @SYS_CODE
		AND BU_CODE = @BU_CODE
		AND PARA_CODE = @CODE;
			
	RETURN LTRIM(RTRIM(ISNULL(@RetVar, N'')));
END
