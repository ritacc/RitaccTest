/**********************************************************************/
-- Description:		用户所属店的信息
---------------------------------------------------------------------
-- Action		Date			Staff		Version		Remarks
-- Create	 	2011-09-01		Zhangbo			1.00.00
/**********************************************************************/
CREATE TABLE [SY_USER_SHOP]
(
	[SYS_CODE]				nvarchar(4)		NOT NULL,
	[SHOP_CODE]				nvarchar(4)		NOT NULL,
	[USER_ID]				bigint			NOT NULL,
	[SUSPEND_FLAG]			nvarchar(1)		NULL		DEFAULT ('N'),
	[ACTIVE_FLAG]			nvarchar(1)		NULL		DEFAULT ('Y'),
	[CREATED_BY]			bigint			NOT NULL,
	[CREATION_DATE]			datetime		NOT NULL,
	[LAST_UPDATED_BY]		bigint			NOT NULL,
	[LAST_UPDATE_DATE]		datetime		NOT NULL,
	[LAST_UPDATE_LOGIN]		bigint			NULL,
	CONSTRAINT PK_USER_SHOP PRIMARY KEY CLUSTERED ([USER_ID] ASC, [SYS_CODE] ASC, [SHOP_CODE] ASC)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
	CONSTRAINT FK_USER_SHOP_SHOP FOREIGN KEY([SYS_CODE], [SHOP_CODE]) REFERENCES [SY_SHOP] ([SYS_CODE], [CODE]),
	CONSTRAINT FK_USER_SHOP_USER FOREIGN KEY([USER_ID]) REFERENCES [SY_USER] ([USER_ID]),
	CONSTRAINT CHK_USER_SHOP_SUSPEND_FLAG CHECK ([SUSPEND_FLAG]=N'N' OR [SUSPEND_FLAG]=N'Y'),
	CONSTRAINT CHK_USER_SHOP_ACTIVE_FLAG CHECK ([ACTIVE_FLAG]=N'N' OR [ACTIVE_FLAG]=N'Y')
) ON [PRIMARY]
GO