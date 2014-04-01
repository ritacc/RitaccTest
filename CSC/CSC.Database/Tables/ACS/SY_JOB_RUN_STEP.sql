/**********************************************************************/
-- Description:		Job Run Step
---------------------------------------------------------------------
-- Action		Date			Staff		Version		Remarks
-- Create	 	2013-05-14		Zhangbo			1.00.00
/**********************************************************************/
CREATE TABLE SY_JOB_RUN_STEP
(
	RUN_ID					bigint			NOT NULL	IDENTITY(1,1)
	,REQUEST_ID				bigint			NOT NULL
	,RUN_TIME_BATCH_ID		bigint			NULL
	,RUN_TIME				datetime		NOT NULL
	,JOB_ID					bigint			NOT NULL
	,JOB_TYPE				nvarchar(4)		NOT NULL	-- Add(2013-05-24,CSC_Job_scheduler_DB_Changes.rtf)
	,RUN_STEP_DESC			nvarchar(350)	NOT NULL	-- Add(2013-05-24,CSC_Job_scheduler_DB_Changes.rtf)
	,INSTANCE_ID			bigint			NOT NULL	-- Add(2013-05-24,CSC_Job_scheduler_DB_Changes.rtf)
	,PRINT_QUEUE_ID			bigint			NULL		-- Add(2013-05-24,CSC_Job_scheduler_DB_Changes.rtf)
	,COPIES					int				NULL		-- Add(2013-05-24,CSC_Job_scheduler_DB_Changes.rtf)
	,SEQ_NBR				bigint			NOT NULL
	,INPUT_PARAM_VALUE		nvarchar(2000)	NOT NULL	DEFAULT(N'')
	,OUTPUT_PARAM_VALUE		nvarchar(2000)	NOT NULL	DEFAULT(N'')
	,START_TIME				datetime		NULL
	,END_TIME				datetime		NULL
	,RUN_STATUS				nvarchar(4)		NOT NULL
	,OUTPUT_LOG_FILE		nvarchar(200)	NOT NULL	DEFAULT(N'')
	,OUTPUT_FILE			nvarchar(200)	NOT NULL	DEFAULT(N'')
	,OUTPUT_FORMAT			nvarchar(10)	NOT NULL	DEFAULT(N'')	-- Add(2013-05-24,CSC_Job_scheduler_DB_Changes.rtf)
	,ERROR_MSG				nvarchar(2000)	NOT NULL	DEFAULT(N'')
	,SYS_CODE				nvarchar (4)	NOT NULL
	,CREATED_BY				bigint			NOT NULL
	,CREATION_DATE			datetime		NOT NULL
	,LAST_UPDATED_BY		bigint			NOT NULL
	,LAST_UPDATE_DATE		datetime		NOT NULL
	,CONSTRAINT PK_SY_JOB_RUN_STEP PRIMARY KEY(RUN_ID) ON [PRIMARY]
	,CONSTRAINT FK_SY_JOB_RUN_STEP_JOB FOREIGN KEY(JOB_ID) REFERENCES SY_JOB(JOB_ID)
	,CONSTRAINT FK_SY_JOB_RUN_STEP_PRINT_QUEUE FOREIGN KEY (PRINT_QUEUE_ID) REFERENCES SY_PRINT_QUEUE(PRINT_QUEUE_ID)
	,CONSTRAINT CHK_SY_JOB_RUN_STEP_STATUS CHECK(RUN_STATUS=N'COMP' OR RUN_STATUS='FAIL' OR RUN_STATUS='NEW')
) ON [PRIMARY]
GO