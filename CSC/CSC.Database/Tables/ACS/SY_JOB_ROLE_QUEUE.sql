/**********************************************************************/
-- Description:		Job Role Queue
---------------------------------------------------------------------
-- Action		Date			Staff		Version		Remarks
-- Create		2013-05-24		Zhangbo		1.00.00
/**********************************************************************/
CREATE TABLE SY_JOB_ROLE_QUEUE
(
	JOB_ID				bigint			NOT NULL
	,ROLE_ID			bigint			NOT NULL
	,QUEUE_ID			bigint			NOT NULL
	,BU_CODE			nvarchar(10)	NOT NULL
	,SYS_CODE			nvarchar(4)		NOT NULL
	,CREATED_BY			bigint			NOT NULL
	,CREATION_DATE		datetime		NOT NULL
	,LAST_UPDATED_BY	bigint			NOT NULL
	,LAST_UPDATE_DATE	datetime		NOT NULL
	,CONSTRAINT PK_JOB_ROLE_QUEUE PRIMARY KEY(JOB_ID,QUEUE_ID) ON [PRIMARY]
	,CONSTRAINT FK_JOB_ROLE_QUEUE_ROLE FOREIGN KEY(JOB_ID,ROLE_ID) REFERENCES SY_JOB_ROLE(JOB_ID,ROLE_ID)
	,CONSTRAINT FK_JOB_ROLE_QUEUE_QUEUE FOREIGN KEY(QUEUE_ID) REFERENCES SY_JOB_QUEUE(QUEUE_ID)
) ON [PRIMARY]
GO