/**********************************************************************/
-- Description:		Job
---------------------------------------------------------------------
-- Action		Date			Staff		Version		Remarks
-- Create	 	2013-05-14		Zhangbo			1.00.00
/**********************************************************************/
CREATE TABLE SY_JOB
(
	JOB_ID				bigint			NOT NULL	IDENTITY(1,1)
	,JOB_CODE			nvarchar(20)	NOT NULL	-- Add(2013-05-24,CSC_Job_scheduler_DB_Changes.rtf)
	,JOB_DESC			nvarchar(60)	NOT NULL
	,JOB_TYPE			nvarchar(4)		NOT NULL	-- 'OS' - command line, 'PKG - DB Package. 'RPT'- Report Services  , 'JOB' - multi-step
	,JOB_SET_IND		nvarchar(1)		NOT NULL	-- must be , 'N'
	,ACTIVE_IND			nvarchar(1)		NOT NULL
	,INACTIVE_DATE		datetime		NULL
	,SYS_CODE			nvarchar(4)		NOT NULL	-- CSC
	,JOB_METHOD_NAME	nvarchar(60)	NULL	
	,OTHER_SET_IND		nvarchar(1)		NULL		DEFAULT(N'Y')
	,CREATED_BY			bigint			NOT NULL
	,CREATION_DATE		datetime		NOT NULL
	,LAST_UPDATED_BY	bigint			NOT NULL
	,LAST_UPDATE_DATE	datetime		NOT NULL
	,CONSTRAINT PK_SY_JOB PRIMARY KEY(JOB_ID) ON [PRIMARY]
	,CONSTRAINT UK_SY_JOB_CODE UNIQUE(SYS_CODE,JOB_CODE)
	,CONSTRAINT CHK_SY_JOB_JOB_TYPE CHECK (JOB_TYPE=N'JOB' OR JOB_TYPE=N'OS' OR JOB_TYPE=N'PKG' OR JOB_TYPE=N'RPT')
	,CONSTRAINT CHK_SY_JOB_JOB_SET_IND CHECK (JOB_SET_IND=N'N' OR JOB_SET_IND=N'Y')
	,CONSTRAINT CHK_SY_JOB_ACTIVE_IND CHECK (ACTIVE_IND=N'N' OR ACTIVE_IND=N'Y')
	,CONSTRAINT CHK_SY_OTHER_SET_IND CHECK (OTHER_SET_IND=N'N' OR OTHER_SET_IND=N'Y')
) ON [PRIMARY]
GO