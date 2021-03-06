/**********************************************************************/
-- Description:		用户所属角色的信息
---------------------------------------------------------------------
-- Action		Date			Staff		Version		Remarks
-- Create	 	2011-09-01		Zhangbo			1.00.00
/**********************************************************************/
CREATE TABLE SY_USER_ROLE
(
	[ROLE_ID]				bigint		NOT NULL,
	[USER_ID]				bigint		NOT NULL,
	[ACTIVE_FLAG]			nvarchar(1)	NULL		DEFAULT ('Y'),
	[CREATED_BY]			bigint		NOT NULL,
	[CREATION_DATE]			datetime	NOT NULL,
	[LAST_UPDATED_BY]		bigint		NOT NULL,
	[LAST_UPDATE_DATE]		datetime	NOT NULL,
	[LAST_UPDATE_LOGIN]		bigint		NULL,
	CONSTRAINT PK_USER_ROLE PRIMARY KEY CLUSTERED ([ROLE_ID] ASC,[USER_ID] ASC)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
	CONSTRAINT FK_USER_ROLE_ROLE FOREIGN KEY([ROLE_ID]) REFERENCES [SY_ROLE] ([ROLE_ID]),
	CONSTRAINT FK_USER_ROLE_USER FOREIGN KEY([USER_ID]) REFERENCES [SY_USER] ([USER_ID]),
	CONSTRAINT CHK_USER_ROLE_ACTIVE_FLAG CHECK ([ACTIVE_FLAG]=N'N' OR [ACTIVE_FLAG]=N'Y')
) ON [PRIMARY]
GO