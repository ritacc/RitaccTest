/**********************************************************************/
-- Description:		Job Step
---Remarks:			Note It is possible to have more than 1 steps for Job Sets, otherwise, the seq_nbr = 0 is the only step in the table
---------------------------------------------------------------------
-- Action		Date			Staff		Version		Remarks
-- Create	 	2013-05-14		Zhangbo			1.00.00
/**********************************************************************/
CREATE TABLE SY_JOB_STEP
(
	JOB_STEP_ID			bigint			NOT NULL	IDENTITY(1,1)
	,JOB_ID				bigint			NOT NULL
	,SEQ_NBR			bigint			NOT NULL
	,JOB_TYPE			nvarchar (4)	NOT NULL	-- 'OS' - command line, 'PKG - DB Package. 'RPT'- Oracle Report 
	,JOB_STEP_DESC		nvarchar (60)	NOT NULL
	,ERROR_CONTINUE		nvarchar (1)	NOT NULL
	,JOB_COMMAND		nvarchar (200)	NULL
	,COPIES				int				NULL		-- Add(2013-05-24,CSC_Job_scheduler_DB_Changes.rtf)
	,OUTPUT_FORMAT		nvarchar(10)	NULL		-- Add(2013-05-24,CSC_Job_scheduler_DB_Changes.rtf)
	,PRINT_QUEUE_ID		bigint			NULL		-- Add(2013-05-24,CSC_Job_scheduler_DB_Changes.rtf)
	,SYS_CODE			nvarchar (4)	NOT NULL
	,CREATED_BY			bigint			NOT NULL
	,CREATION_DATE		datetime		NOT NULL
	,LAST_UPDATED_BY	bigint			NOT NULL
	,LAST_UPDATE_DATE	datetime		NOT NULL
	,CONSTRAINT PK_SY_JOB_STEP PRIMARY KEY(JOB_STEP_ID) ON [PRIMARY]
	,CONSTRAINT FK_SY_JOB_STEP_JOB FOREIGN KEY(JOB_ID) REFERENCES SY_JOB(JOB_ID)
	,CONSTRAINT FK_SY_JOB_STEP_PRINT_QUEUE FOREIGN KEY(PRINT_QUEUE_ID) REFERENCES SY_PRINT_QUEUE(PRINT_QUEUE_ID)
	,CONSTRAINT CHK_SY_JOB_STEP_JOB_TYPE CHECK(JOB_TYPE=N'JOB' OR JOB_TYPE=N'OS' OR JOB_TYPE=N'PKG' OR JOB_TYPE=N'RPT')
	,CONSTRAINT UK_SY_JOB_STEP_SEQ_NBR UNIQUE(JOB_ID,SEQ_NBR)
) ON [PRIMARY]
GO