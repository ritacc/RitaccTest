CREATE TABLE [SY_FUNCTION]
(
	[FUNC_ID]				bigint			NOT NULL,
	[SYS_CODE]				nvarchar(4)		NULL,
	[SYSTEM_SCOPE]			nvarchar(4)		NOT NULL,	-- 系统范围
	[FUNC_CODE]				nvarchar(20)	NULL,
	[FUNC_TYPE]				nvarchar(10)	NOT NULL,	-- 可选项:GODOWN/CS, 同SY_SHOP.SHOP_TYPE
	[DSC]					nvarchar(100)	NULL,
	[EXECUTABLE]			nvarchar(50)	NULL,
	[ADMIN_FLAG]			nvarchar(1)		NULL		DEFAULT ('N'),
	[CREATED_BY]			bigint			NOT NULL,
	[CREATION_DATE]			datetime		NOT NULL,
	[LAST_UPDATED_BY]		bigint			NOT NULL,
	[LAST_UPDATE_DATE]		datetime		NOT NULL,
	[LAST_UPDATE_LOGIN]		bigint			NULL,	
	CONSTRAINT [FUNC_PK] PRIMARY KEY CLUSTERED ([FUNC_ID] ASC)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO