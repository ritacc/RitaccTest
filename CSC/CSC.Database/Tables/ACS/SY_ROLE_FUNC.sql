/**********************************************************************/
-- Description:		角色权限信息
---------------------------------------------------------------------
-- Action		Date			Staff		Version		Remarks
-- Create	 	2011-09-01		Zhangbo			1.00.00
/**********************************************************************/
CREATE TABLE [SY_ROLE_FUNC]
(
	[FUNC_ID]				bigint			NOT NULL,
	[ROLE_ID]				bigint			NOT NULL,
	[INSERTABLE_FLAG]		nvarchar(1)		NULL		DEFAULT ('Y'),
	[UPDATABLE_FLAG]		nvarchar(1)		NULL		DEFAULT ('Y'),
	[ACTIVE_FLAG]			nvarchar(1)		NULL		DEFAULT ('Y'),
	[CREATED_BY]			bigint			NOT NULL,
	[CREATION_DATE]			datetime		NOT NULL,
	[LAST_UPDATED_BY]		bigint			NOT NULL,
	[LAST_UPDATE_DATE]		datetime		NOT NULL,
	[LAST_UPDATE_LOGIN]		bigint			NULL,
	CONSTRAINT PK_ROLE_FUNC PRIMARY KEY CLUSTERED ([FUNC_ID] ASC, [ROLE_ID] ASC)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
	CONSTRAINT FK_ROLE_FUNC_FUNC FOREIGN KEY([FUNC_ID]) REFERENCES [SY_FUNCTION] ([FUNC_ID]),
	CONSTRAINT FK_ROLE_FUNC_ROLE FOREIGN KEY([ROLE_ID]) REFERENCES [SY_ROLE] ([ROLE_ID]),
	CONSTRAINT CHK_ROLE_FUNC_INSERTABLE_FLAG CHECK ([INSERTABLE_FLAG]=N'N' OR [INSERTABLE_FLAG]=N'Y'),
	CONSTRAINT CHK_ROLE_FUNC_UPDATABLE_FLAG CHECK ([UPDATABLE_FLAG]=N'N' OR [UPDATABLE_FLAG]=N'Y'),
	CONSTRAINT CHK_ROLE_FUNC_ACTIVE_FLAG CHECK ([ACTIVE_FLAG]=N'N' OR [ACTIVE_FLAG]=N'Y')
) ON [PRIMARY]
GO