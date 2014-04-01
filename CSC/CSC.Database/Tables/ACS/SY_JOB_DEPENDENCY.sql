/**********************************************************************/
-- Description:		Job Dependency
---------------------------------------------------------------------
-- Action		Date			Staff		Version		Remarks
-- Create	 	2013-05-14		Zhangbo			1.00.00
/**********************************************************************/
CREATE TABLE SY_JOB_DEPENDENCY
(
	JOB_ID				bigint			NOT NULL
	,DEPENDENT_JOB_ID	bigint			NOT NULL
	,SYS_CODE			nvarchar (4)	NOT NULL
	,CREATED_BY			bigint			NOT NULL
	,CREATION_DATE		datetime		NOT NULL
	,LAST_UPDATED_BY	bigint			NOT NULL
	,LAST_UPDATE_DATE	datetime		NOT NULL
	,CONSTRAINT PK_SY_JOB_DEPENDENCY PRIMARY KEY(JOB_ID,DEPENDENT_JOB_ID) ON [PRIMARY]
	,CONSTRAINT FK_SY_JOB_DEPEND_JOB FOREIGN KEY(JOB_ID) REFERENCES SY_JOB(JOB_ID)
	,CONSTRAINT FK_SY_JOB_DEPEND_DEPEND_JOB FOREIGN KEY (DEPENDENT_JOB_ID) REFERENCES SY_JOB(JOB_ID)
) ON [PRIMARY]
GO