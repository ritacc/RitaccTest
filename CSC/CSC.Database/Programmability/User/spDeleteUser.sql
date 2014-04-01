/**********************************************************************/
-- Description:	Delete Brand Code Info
---------------------------------------------------------------------
-- Action		Date			Staff		Version		Remarks
-- Create	 	2013-04-07		Jason		1.00.00
---------------------------------------------------------------------
/**********************************************************************/
CREATE PROCEDURE spDeleteUser
(
	 @USER_ID				BIGINT
	,@ResultType			INT				OUTPUT
	,@ResultMessage			NVARCHAR(1000)	OUTPUT
)
AS
BEGIN
	
	BEGIN TRANSACTION;
	BEGIN TRY
		
		DELETE FROM SY_USER_SHOP WHERE [USER_ID] = @USER_ID;

		DECLARE @FKConflictsResult AS BIT;
		EXEC dbo.spCheckFKConflicts 
			@TABLE_NAME = N'SY_USER'
			,@ID = @USER_ID
			,@Result = @FKConflictsResult OUTPUT;
			
		IF (@FKConflictsResult = 1)
		BEGIN
			SET @ResultType = -1004;
			SET @ResultMessage = N'Foreign key conflicts.';
			RAISERROR (@ResultMessage, 11/* 必须>10才能转到Catch */, 1, 5); /* 抛出异常，并转到 Catch */
		END

		DELETE FROM SY_USER WHERE [USER_ID] = @USER_ID;

		
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION;

		--返回信息
		IF (ISNULL(@ResultType, 0)=0)
		BEGIN
			SET @ResultType = -9999;
			SET @ResultMessage = ERROR_MESSAGE();
		END	
		RETURN @ResultType;
		
	END CATCH

	IF @@TRANCOUNT > 0
		COMMIT TRANSACTION;
	
	SET @ResultType = 0;
	SET @ResultMessage = N'SUCCEED';
	RETURN @ResultType;
END