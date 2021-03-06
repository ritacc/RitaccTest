/**********************************************************************/
-- Description:		密码历史记录
---------------------------------------------------------------------
-- Action		Date			Staff		Version		Remarks
-- Create	 	2011-09-01		Zhangbo			1.00.00
/**********************************************************************/
CREATE TABLE [SY_PASSWORD_LOG]
(
	[USER_ID]			bigint			NOT NULL,
	[PWD_TYPE]			nvarchar(1)		NOT NULL,
	[SEQ]				bigint			NOT NULL,
	[LOGIN_PWD]			nvarchar(60)	NULL,
	[AUTH_PWD]			nvarchar(60)	NULL,
	-- 数据信息
	CREATED_BY			bigint			NOT NULL,
	CREATION_DATE		datetime		NOT NULL,
	LAST_UPDATED_BY		bigint			NOT NULL,
	LAST_UPDATE_DATE	datetime		NOT NULL,
	LAST_UPDATE_LOGIN	bigint			NULL,
	CONSTRAINT CHK_PASSWORD_LOG_PWD_TYPE CHECK ([PWD_TYPE]=N'P' OR [PWD_TYPE]=N'A')
) ON [PRIMARY]
GO