/**********************************************************************/
-- Description:		Convert Nvarchar(1) To Bit
---------------------------------------------------------------------
-- Action		Date			Staff		Version		Remarks
-- Create	 	2011-08-30		ZJX			1.00.00
/**********************************************************************/
CREATE FUNCTION [dbo].[fnConvertNvarcharToBit]
(
	@NvarcharVar	NVARCHAR(1)
)
RETURNS BIT
AS
BEGIN
	DECLARE	@RetVar	BIT;

	IF (@NvarcharVar = N'Y')
		BEGIN
			SET @RetVar = 1;
		END
	ELSE IF (@NvarcharVar = N'N')
		BEGIN
			SET @RetVar = 0;
		END
	ELSE
		BEGIN
			SET @RetVar = 0;
		END	
	
	RETURN @RetVar;
END