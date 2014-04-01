/**********************************************************************/
-- Description:		Job Request Param
---------------------------------------------------------------------
-- Action		Date			Staff		Version		Remarks
-- Create	 	2013-05-14		Zhangbo			1.00.00
/**********************************************************************/
CREATE TABLE SY_JOB_REQUEST_PARAM
(
	REQUEST_PARAM_ID	bigint			NOT NULL	IDENTITY(1,1)
	,REQUEST_ID			bigint			NOT NULL
	,SEQ_NBR			bigint			NOT NULL
	,PARAM_NAME			nvarchar(30)	NOT NULL
	,PARAM_DESC			nvarchar(200)	NOT NULL	DEFAULT(N'')	-- Add(2013-05-24,CSC_Job_scheduler_DB_Changes.rtf)
	,PARAM_VALUE		nvarchar(50)	NOT NULL
	,MULTI_VALUE_IND	nvarchar(1)		NOT NULL
	,CREATED_BY			bigint			NOT NULL
	,CREATION_DATE		datetime		NOT NULL
	,LAST_UPDATED_BY	bigint			NOT NULL
	,LAST_UPDATE_DATE	datetime		NOT NULL
	,CONSTRAINT PK_SY_JOB_REQUEST_PARAM PRIMARY KEY(REQUEST_PARAM_ID) ON [PRIMARY]
	,CONSTRAINT FK_SY_JOB_REQUEST_PARAM_REQ FOREIGN KEY (REQUEST_ID) REFERENCES SY_JOB_REQUEST(REQUEST_ID)
	,CONSTRAINT CHK_SY_JOB_REQUEST_PARAM_MULTI_VALUE_IND CHECK (MULTI_VALUE_IND IN (N'Y',N'N'))
	,CONSTRAINT UK_SY_JOB_REQUEST_PARAM_SEQ_NBR UNIQUE(REQUEST_ID,SEQ_NBR)
) ON [PRIMARY]
GO
