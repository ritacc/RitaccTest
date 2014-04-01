/**********************************************************************/
-- Description:		get next sequence to insert
---------------------------------------------------------------------
-- Action		Date			Staff		Version		Remarks
-- Modify	 	2011-09-02		Zhangbo		1.01.00
-- Create	 	2011-09-01		xj			1.00.00
/**********************************************************************/
CREATE PROCEDURE [dbo].[spGetNextSequence]
(
	@Sequence   bigint				OUTPUT,
	@RowId		uniqueidentifier	OUTPUT
)
AS
BEGIN
	SET NOCOUNT ON
	SET @RowId = NEWID();
	INSERT INTO SY_SEQUENCE VALUES(@RowId);
	SET @Sequence = SCOPE_IDENTITY();
END
