/**********************************************************************/
-- Description:		用户信息
---------------------------------------------------------------------
-- Action		Date			Staff		Version		Remarks
-- Create	 	2011-09-01		Zhangbo			1.00.00
/**********************************************************************/
CREATE TABLE SY_USER
(
	[USER_ID]					bigint				NOT NULL,                               -- 用户ID
	USER_CODE					nvarchar(20)		NOT NULL,								-- 代码
	[USER_NAME]					nvarchar(50)		NULL,									-- 姓名		--modify on 20131012
	USER_TYPE					nvarchar(2)			NOT NULL,								-- 类型
	SYSTEM_SCOPE				nvarchar(4)			NOT NULL,								-- 系统范围
	FROZEN_FLAG					nvarchar(1)			NOT NULL		DEFAULT ('N'),			-- 是否冷冻
	FROZEN_DATE					datetime			NULL,									-- 冷冻日期
	SUSPEND_FLAG				nvarchar(1)			NULL			DEFAULT ('N'),			-- 是否暂停
	SUSPEND_DATE				datetime			NULL,									-- 暂停日期
	LOGIN_PWD					nvarchar(60)		NOT NULL,								-- 登录密码
	AUTH_PWD					nvarchar(60)		NULL,									-- 特批密码
	PWD_EXPIRY_DATE				datetime			NOT NULL,								-- 密码到期时间
	REPORT_SERVER_CODE			nvarchar(10)		NULL,
	CHAR_REPORT_SERVER_CODE		nvarchar(10)		NULL,
	CREATED_BY					bigint				NOT NULL,
	CREATION_DATE				datetime			NOT NULL,
	LAST_UPDATED_BY				bigint				NOT NULL,
	LAST_UPDATE_DATE			datetime			NOT NULL,
	LAST_UPDATE_LOGIN			bigint				NULL,
	ROWID						uniqueidentifier	NOT NULL		DEFAULT (NEWID()),
	CONSTRAINT PK_USER PRIMARY KEY CLUSTERED ([USER_ID] ASC)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
	CONSTRAINT CHK_USER_USER_TYPE CHECK (USER_TYPE='NS' OR USER_TYPE='SA'),
	CONSTRAINT CHK_USER_FROZEN_FLAG CHECK (FROZEN_FLAG=N'N' OR FROZEN_FLAG=N'Y'),
	CONSTRAINT CHK_USER_SUSPEND_FLAG CHECK (SUSPEND_FLAG=N'N' OR SUSPEND_FLAG=N'Y')
) ON [PRIMARY]
GO