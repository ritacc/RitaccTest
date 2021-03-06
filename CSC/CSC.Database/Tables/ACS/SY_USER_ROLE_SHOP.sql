/**********************************************************************/
-- Description:		用户所属店角色的信息
---------------------------------------------------------------------
-- Action		Date			Staff		Version		Remarks
-- Create	 	2011-09-01		Zhangbo			1.00.00
/**********************************************************************/
CREATE TABLE [SY_USER_ROLE_SHOP]
(
	[ROLE_ID]				bigint			NOT NULL,
	[USER_ID]				bigint			NOT NULL,
	[SYS_CODE]				nvarchar(4)		NOT NULL,
	[SHOP_CODE]				nvarchar(4)		NOT NULL,
	[GRANT_BY]				bigint			NULL,
	[GRANT_DATE]			datetime		NULL,
	[REVOKE_BY]				bigint			NULL,
	[REVOKE_DATE]			datetime		NULL,
	[SHOP_ADM_FLAG]			nvarchar(1)		NULL		DEFAULT ('N'),
	[ACTIVE_FLAG]			nvarchar(1)		NULL		DEFAULT ('Y'),
	[CREATED_BY]			bigint			NOT NULL,
	[CREATION_DATE]			datetime		NOT NULL,
	[LAST_UPDATED_BY]		bigint			NOT NULL,
	[LAST_UPDATE_DATE]		datetime		NOT NULL,
	[LAST_UPDATE_LOGIN]		bigint			NULL,
	CONSTRAINT PK_USER_ROLE_SHOP PRIMARY KEY CLUSTERED ([SYS_CODE] ASC, [SHOP_CODE] ASC, [ROLE_ID] ASC, [USER_ID] ASC)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
	CONSTRAINT PK_USER_ROLE_SHOP_USER FOREIGN KEY([ROLE_ID], [USER_ID])REFERENCES [SY_USER_ROLE] ([ROLE_ID], [USER_ID]),
	CONSTRAINT PK_USER_ROLE_SHOP_SHOP FOREIGN KEY([SYS_CODE], [SHOP_CODE])REFERENCES [SY_SHOP] ([SYS_CODE], [CODE]),
	CONSTRAINT CHK_USER_ROLE_SHOP_ADM_FLAG CHECK ([SHOP_ADM_FLAG]=N'N' OR [SHOP_ADM_FLAG]=N'Y'),
	CONSTRAINT CHK_USER_ROLE_SHOP_ACTIVE_FLAG CHECK ([ACTIVE_FLAG]=N'N' OR [ACTIVE_FLAG]=N'Y')
) ON [PRIMARY]
GO