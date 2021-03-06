/**********************************************************************/
-- Description:		角色信息
---------------------------------------------------------------------
-- Action		Date			Staff		Version		Remarks
-- Create	 	2011-09-01		Zhangbo			1.00.00
/**********************************************************************/
CREATE TABLE [SY_ROLE]
(
	ROLE_ID				bigint			NOT NULL						--角色ID
	,SYS_CODE			nvarchar(4)		NOT NULL						-- 系统代码
	,SHOP_CODE			nvarchar(4)		NOT NULL	DEFAULT ('*')		-- 店代码
	,BU_CODE			nvarchar(10)	NOT NULL						-- FK:BUSINESS_UNIT.BU_CODE
	,SYSTEM_SCOPE		nvarchar(4)		NOT NULL						-- 系统范围
	,ROLE_CODE			nvarchar(20)	NOT NULL						-- 角色代码
	,ROLE_TYPE			nvarchar(2)		NOT NULL						-- 角色类型(系统 店)
	,ROLE_SDSC			nvarchar(20)	NULL							-- 简述
	,ROLE_DSC			nvarchar(50)	NULL							-- 描述
	,ADMIN_FLAG			nvarchar(1)		NULL		DEFAULT ('N')		-- 是否具有管理权限
	,FROZEN_FLAG		nvarchar(1)		NULL		DEFAULT ('N')		-- 是否被冻结
	,FROZEN_DATE		datetime		NULL							-- 冻结日期
	,CREATED_BY			bigint			NOT NULL
	,CREATION_DATE		datetime		NOT NULL
	,LAST_UPDATED_BY	bigint			NOT NULL
	,LAST_UPDATE_DATE	datetime		NOT NULL
	,LAST_UPDATE_LOGIN	bigint			NULL
	,CONSTRAINT PK_ROLE PRIMARY KEY CLUSTERED (ROLE_ID) ON [PRIMARY]
	,CONSTRAINT CHK_ROLE_ROLE_TYPE CHECK (ROLE_TYPE=N'SY' OR [ROLE_TYPE]=N'SH')
	,CONSTRAINT CHK_ROLE_ADMIN_FLAG CHECK (ADMIN_FLAG=N'N' OR [ADMIN_FLAG]=N'Y')
	,CONSTRAINT CHK_ROLE_FROZEN_FLAG CHECK (FROZEN_FLAG=N'N' OR [FROZEN_FLAG]=N'Y')
) ON [PRIMARY]
GO