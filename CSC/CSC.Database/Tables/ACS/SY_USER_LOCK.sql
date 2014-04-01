/**********************************************************************/
-- Description:		用户锁定信息
---------------------------------------------------------------------
-- Action		Date			Staff		Version		Remarks
-- Create	 	2011-09-01		Zhangbo			1.00.00
/**********************************************************************/
CREATE TABLE SY_USER_LOCK
(
	[USER_ID]	bigint				NOT NULL,		-- 用户ID
	LOGIN_IP	nvarchar(20)		NOT NULL	DEFAULT(N''),
	WP_KEY		nvarchar(30)		NOT NULL	DEFAULT(N''),
	ROWID		uniqueidentifier	NOT NULL	DEFAULT (NEWID()),
	CONSTRAINT PK_USER_LOCK PRIMARY KEY ([USER_ID] ASC) ON [PRIMARY]
) ON [PRIMARY]
GO