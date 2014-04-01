/**********************************************************************/
-- Description:		保存功能信息
---------------------------------------------------------------------
-- Action		Date			Staff		Version		Remarks
-- Create	 	2011-09-06		ZJX			1.00.00
---------------------------------------------------------------------
/**********************************************************************/
CREATE PROCEDURE [dbo].[spSaveFunction]
(
	@FUNC_ID			BIGINT
	,@SYS_CODE			NVARCHAR(4)
	,@DSC				NVARCHAR(50)
	,@ADMIN_FLAG		BIT
	,@FUNC_TYPE			NVARCHAR(4)
	--其他
	,@LAST_UPDATE_DATE	DATETIME
	,@LAST_UPDATED_BY	BIGINT
	,@CURRENT_USER_ID	BIGINT
	,@RecordStatus		NVARCHAR(5)
	,@ResultType		INT				OUTPUT
	,@ResultMessage		NVARCHAR(1000)	OUTPUT
)
AS
BEGIN
	SET NOCOUNT ON;
	
	BEGIN TRY
		DECLARE @CURRENT_DATE DATETIME;
		SET @CURRENT_DATE = GETDATE();

		IF (@RecordStatus = N'EDIT')
			BEGIN
				IF EXISTS(SELECT 1 FROM SY_FUNCTION WHERE FUNC_ID = @FUNC_ID AND SYS_CODE = @SYS_CODE AND (DATEDIFF(S,LAST_UPDATE_DATE,@LAST_UPDATE_DATE) = 0 OR LAST_UPDATED_BY = @CURRENT_USER_ID))
					BEGIN
						UPDATE 
							SY_FUNCTION
						SET 
							DSC = @DSC --名称
							,ADMIN_FLAG = dbo.fnConvertBitToNvarchar(@ADMIN_FLAG) --管理
							,FUNC_TYPE = @FUNC_TYPE --类型
							,LAST_UPDATE_DATE = @CURRENT_DATE --最后更新日
							,LAST_UPDATED_BY = @CURRENT_USER_ID --最后更新人
							,LAST_UPDATE_LOGIN = @CURRENT_USER_ID --最后更新人
						WHERE
							FUNC_ID = @FUNC_ID
							AND SYS_CODE = @SYS_CODE;

						SET @ResultType = 0;
						SET @ResultMessage = N'UPDATE SUCCEED';
					END
				ELSE
					BEGIN
						SET @ResultType = -1003;
						SET @ResultMessage = N'RECORD IS CHANGED';
					END
			END  
		RETURN @ResultType;
	END TRY
	BEGIN CATCH
		SET @ResultType = -9999;
		SET @ResultMessage = ERROR_MESSAGE();
		
		RETURN @ResultType;
	END CATCH
END
