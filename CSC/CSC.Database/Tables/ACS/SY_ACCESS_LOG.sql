CREATE TABLE SY_ACCESS_LOG
(
	[ACLG_ID]			bigint			NOT NULL	IDENTITY(1,1)
	,[FUNC_ID]			bigint			NULL
	,[FUNC_CODE]		nvarchar(20)	NULL
	,[USER_ID]			bigint			NOT NULL
	,[ACTION_TYPE]		nvarchar(10)	NOT NULL
	,[ACTION_DTIME]		datetime		NOT NULL
	,[LOGIN_IP]			nvarchar(30)	NOT NULL	-- IP
	,[SHOP_CODE]		nvarchar(4)		NOT NULL
	,[BU_CODE]			nvarchar(10)	NOT NULL
	,CONSTRAINT PK_ACCESS_LOG PRIMARY KEY ([ACLG_ID]) ON [PRIMARY]
) ON [PRIMARY]
GO