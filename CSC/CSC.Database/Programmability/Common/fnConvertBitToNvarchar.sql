/**********************************************************************/
-- Description:		Convert Bit To Nvarchar(1)
---------------------------------------------------------------------
-- Action		Date			Staff		Version		Remarks
-- Create	 	2011-08-30		ZJX			1.00.00
/**********************************************************************/
CREATE FUNCTION [dbo].[fnConvertBitToNvarchar]
(
	@BitVar		BIT
)
RETURNS NVARCHAR(1)
AS
BEGIN
	DECLARE	@RetVar	NVARCHAR(1);
	
	IF (@BitVar = 1)
		BEGIN
			SET @RetVar = N'Y';
		END
	ELSE IF (@BitVar = 0)
		BEGIN
			SET @RetVar = N'N';
		END
	ELSE
		BEGIN
			SET @RetVar = N'N';
		END	
		
	RETURN @RetVar;
END