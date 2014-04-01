/**********************************************************************/
-- Description: 处理由数据库程序产生的异常，用于 BEGIN CATCH...END CATCH 语句中。
---------------------------------------------------------------------
-- Action		Date			Staff		Version		Remarks
-- Create		2013-01-03		Zhangbo		1.00.00
/**********************************************************************/
CREATE PROCEDURE spExceptionHandle
	@THROW_IND		bit		= 1		-- 是否继续抛出异常，默认继续抛出。
AS
BEGIN
	DECLARE @EVENT_ID			int				= ERROR_NUMBER();
	DECLARE @EVENT_TIME			datetime		= GETDATE();
	DECLARE @PRIORITY			int				= ERROR_STATE();
	DECLARE @SEVERITY			int				= ERROR_SEVERITY();
	DECLARE @MACHINE_NAME		nvarchar(512)	= CAST(SERVERPROPERTY(N'MachineName') AS nvarchar);
	DECLARE @SERVER_NAME		nvarchar(512)	= CAST(SERVERPROPERTY(N'ServerName') AS nvarchar);
	DECLARE @PROCESS_ID			int				= CAST(SERVERPROPERTY(N'ProcessID') AS int);
	DECLARE @PROCESS_NAME		nvarchar(512)	= DB_NAME();
	DECLARE @THREAD_ID			int				= OBJECT_ID(ERROR_PROCEDURE());
	DECLARE @THREAD_NAME		nvarchar(512)	= ERROR_PROCEDURE();
	DECLARE @THREAD_LINE		int				= ERROR_LINE();
	DECLARE @MESSAGE			nvarchar(2046)	= ERROR_MESSAGE();
	DECLARE @FOMATTED_MESSAGE	nvarchar(max)	= ERROR_MESSAGE();

	BEGIN TRY
		INSERT INTO SYS_LOG
		(
			EVENT_ID
			,EVENT_TIME
			,[PRIORITY]
			,SEVERITY
			,MACHINE_NAME
			,SERVER_NAME
			,PROCESS_ID
			,PROCESS_NAME
			,THREAD_ID
			,THREAD_NAME
			,THREAD_LINE
			,[MESSAGE]
			,FOMATTED_MESSAGE
		)
		VALUES
		(
			@EVENT_ID
			,@EVENT_TIME
			,@PRIORITY
			,@SEVERITY
			,@MACHINE_NAME
			,@SERVER_NAME
			,@PROCESS_ID
			,@PROCESS_NAME
			,@THREAD_ID
			,@THREAD_NAME
			,@THREAD_LINE
			,@MESSAGE
			,@FOMATTED_MESSAGE
		);
	END TRY
	BEGIN CATCH
	END CATCH
	IF (@THROW_IND=1)
	BEGIN
		RAISERROR (@MESSAGE,@SEVERITY,@PRIORITY);
	END
END