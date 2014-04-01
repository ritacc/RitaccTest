/**********************************************************************/
-- Description:		Job Role
---------------------------------------------------------------------
-- Action		Date			Staff		Version		Remarks
-- Create	 	2013-05-14		Zhangbo			1.00.00
/**********************************************************************/
CREATE TABLE SY_JOB_ROLE
(
	JOB_ID				bigint			NOT NULL
	,ROLE_ID			bigint			NOT NULL
	--,QUEUE_ID			bigint			NOT NULL	-- DEFAULT QUEUE DELETE(2013-05-24,CSC_Job_scheduler_DB_Changes.rtf)
	,SYS_CODE			nvarchar(4)		NOT NULL	-- ERP
	,CREATED_BY			bigint			NOT NULL
	,CREATION_DATE		datetime		NOT NULL
	,LAST_UPDATED_BY	bigint			NOT NULL
	,LAST_UPDATE_DATE	datetime		NOT NULL
	,CONSTRAINT PK_SY_JOB_ROLE PRIMARY KEY(JOB_ID,ROLE_ID) ON [PRIMARY]
	,CONSTRAINT FK_SY_JOB_ROLE_ROLE FOREIGN KEY(ROLE_ID) REFERENCES SY_ROLE(ROLE_ID)
	,CONSTRAINT FK_SY_JOB_ROLE_JOB FOREIGN KEY(JOB_ID) REFERENCES SY_JOB(JOB_ID)
	--,CONSTRAINT FK_SY_JOB_ROLE_QUEUE FOREIGN KEY(QUEUE_ID) REFERENCES SY_JOB_QUEUE(QUEUE_ID)
) ON [PRIMARY]
GO