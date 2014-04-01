/**********************************************************************/
-- Description: The Operation of Edit Password For shop
-- Remarks:	
---------------------------------------------------------------------
-- Action		Date			Staff		Version		Remarks
-- Create	 	2013/8/15		LM			1.00.00
/**********************************************************************/

Create Proc dbo.spEditPassword
(
    @SYS_CODE nvarchar(4) = Null,
    @CODE nvarchar(4) = Null,
	@BU_CODE nvarchar(10) = Null,
    @AUTH_PWD nvarchar(60) = Null,
    @LAST_UPDATED_BY bigint = Null,
    @ResultType		INT				OUTPUT,
    @ResultMessage	NVARCHAR(1000)	OUTPUT
)
AS
BEGIN
begin try
		UPDATE [SY_SHOP] SET
            AUTH_PWD = @AUTH_PWD,
            LAST_UPDATED_BY = @LAST_UPDATED_BY,
            LAST_UPDATE_DATE = GETDATE()
		WHERE 
            SYS_CODE=@SYS_CODE and CODE = @CODE and BU_CODE = @BU_CODE
		SET @ResultType=0;
		SET @ResultMessage = N'Update Success'
end try
begin catch
		SET @ResultType=-2000;
		SET @ResultMessage = error_message() 
end catch
END