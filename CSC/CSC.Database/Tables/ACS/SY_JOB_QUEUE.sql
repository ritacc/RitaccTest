/**********************************************************************/
-- Description:		Job Queue
---------------------------------------------------------------------
-- Action		Date			Staff		Version		Remarks
-- Create	 	2013-05-14		Zhangbo		1.00.00
-- Modify	 	2013-05-22		Zhangbo		1.01.00
/**********************************************************************/
CREATE TABLE SY_JOB_QUEUE
(
	QUEUE_ID					bigint			NOT NULL	IDENTITY(1,1)
	,QUEUE_NAME					nvarchar(10)	NOT NULL
	--,JOB_TYPE					nvarchar(4)		NOT NULL	-- = JOB_TYPE and ALL
	,QUEUE_STATUS				nvarchar(1)		NOT NULL	-- N-Normal, T-Terminate, D-Deactivated
	,STATUS_CHANGE_DATE			datetime		NULL
	,ENGINE_PROCESS_ID			nvarchar(30)	NULL		-- job execution engine ID in the OS
	,ENGINE_LAST_UPDATE_DATE	datetime		NULL		-- the last time the job execution engine update this record
	,BU_CODE					nvarchar(10)	NOT NULL	-- ADD(2013-05-22)
	,CREATED_BY					bigint			NOT NULL
	,CREATION_DATE				datetime		NOT NULL
	,LAST_UPDATED_BY			bigint			NOT NULL
	,LAST_UPDATE_DATE			datetime		NOT NULL
	,CONSTRAINT PK_SY_JOB_QUEUE PRIMARY KEY(QUEUE_ID) ON [PRIMARY]
	,CONSTRAINT FK_SY_JOB_QUEUE_BU_CODE FOREIGN KEY(BU_CODE) REFERENCES BUSINESS_UNIT(BU_CODE)
	,CONSTRAINT CHK_SY_JOB_QUEUE_STATUS CHECK(QUEUE_STATUS=N'D' OR QUEUE_STATUS=N'N' OR QUEUE_STATUS=N'T')
) ON [PRIMARY]
GO