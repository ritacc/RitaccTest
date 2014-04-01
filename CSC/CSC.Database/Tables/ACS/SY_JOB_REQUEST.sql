/**********************************************************************/
-- Description:		Job Request
-- Remarks:			Create sequence request_id_seq ;
---------------------------------------------------------------------
-- Action		Date			Staff		Version		Remarks
-- Create	 	2013-05-14		Zhangbo		1.00.00
/**********************************************************************/
CREATE TABLE SY_JOB_REQUEST
(
	REQUEST_ID					bigint			NOT NULL	IDENTITY(1,1)
	,JOB_ID						bigint			NOT NULL
	,ROLE_ID					bigint			NULL
	,QUEUE_ID					bigint			NOT NULL
	,ROLE_CODE					nvarchar(30)	NULL
	,RUN_MODE					nvarchar(4)		NOT NULL	--ASAP, ONCE, PERD,'SPEC'(On Specific Days) 
	,FIRST_RUN_TIME				datetime		NULL		-- for ONCE and PERD and SPEC
	,PERD_RUN_FREQ				nvarchar(1)		NULL		-- for PERD  H(hr), D(Day), W(eek), M(onth), Y(ear)
	,PERD_RUN_UNIT				int				NULL		-- for PERD, eg. freq=H, unit=3, => run every 3 hours
	,END_RUN_TIME				datetime		NULL		-- for PERD and SPEC
	,SPEC_WEEK_DAYS				nvarchar(7)		NOT NULL	DEFAULT(N'')	-- each character represent one day of the week e.g. YYYNNNN (job run on Sun, Mon and Tue
	,SPEC_MONTH_DAYS			nvarchar(32)	NOT NULL	DEFAULT(N'')	-- each character represent one day of the month --first character means the 'Last Day' of the month -- e.g. YYYNNNN-----NNN (job run on Last day of the month and on 1 and 2 of the month)
	,LAST_RUN_TIME				datetime		NULL
	,NEXT_RUN_TIME				datetime		NULL
	,NEXT_RUN_TIME_BATCH_ID		bigint			NULL		-- for RUN_MODE=PERD, SPEC, the times the job Is run
	--,PRINT_QUEUE_ID				bigint			NULL		-- Add(2013-05-24,CSC_Job_scheduler_DB_Changes.rtf)
	--,COPIES						int				NULL		-- Add(2013-05-24,CSC_Job_scheduler_DB_Changes.rtf)
	,INCREMENT_DATE_PARAM_IND	nvarchar(1)		NOT NULL	-- Add(2013-05-24,CSC_Job_scheduler_DB_Changes.rtf)
	,REQUEST_STATUS				nvarchar(4)		NOT NULL	--  overall status of Job Set
	,BU_CODE					nvarchar(10)	NOT NULL
	,SYS_CODE					nvarchar(4)	NOT NULL
	,CREATED_BY					bigint			NOT NULL
	,CREATION_DATE				datetime		NOT NULL
	,LAST_UPDATED_BY			bigint			NOT NULL
	,LAST_UPDATE_DATE			datetime		NOT NULL
	,CONSTRAINT PK_SY_JOB_REQUEST PRIMARY KEY (REQUEST_ID) ON [PRIMARY]
	,CONSTRAINT FK_SY_JOB_REQUEST_JOB FOREIGN KEY(JOB_ID) REFERENCES SY_JOB(JOB_ID)
	,CONSTRAINT FK_SY_JOB_REQUEST_ROLE FOREIGN KEY (JOB_ID,ROLE_ID) REFERENCES SY_JOB_ROLE(JOB_ID,ROLE_ID)
	,CONSTRAINT FK_SY_JOB_REQUEST_QUEUE FOREIGN KEY (QUEUE_ID) REFERENCES SY_JOB_QUEUE(QUEUE_ID)
	--,CONSTRAINT FK_SY_JOB_REQUEST_PRINT_QUEUE FOREIGN KEY (PRINT_QUEUE_ID) REFERENCES SY_PRINT_QUEUE(PRINT_QUEUE_ID)
	,CONSTRAINT FK_SY_JOB_REQUEST_BU FOREIGN KEY (BU_CODE) REFERENCES BUSINESS_UNIT(BU_CODE)
	,CONSTRAINT CHK_SY_JOB_REQUEST_PERD_RUN_FREQ CHECK(PERD_RUN_FREQ IN ('H','D','M','W','Y'))
	,CONSTRAINT CHK_SY_JOB_REQUEST_STATUS CHECK(REQUEST_STATUS IN ('NEW','REJ','RUN','COMP','FAIL','ABRT','CNCL'))
	,CONSTRAINT CHK_SY_JOB_REQUEST_INCREMENT_DATE_PARAM CHECK(INCREMENT_DATE_PARAM_IND=N'N' OR INCREMENT_DATE_PARAM_IND=N'Y')
) ON [PRIMARY]
GO