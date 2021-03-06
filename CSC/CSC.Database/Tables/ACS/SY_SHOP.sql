/**********************************************************************/
-- Description:		店信息
---------------------------------------------------------------------
-- Action		Date			Staff		Version		Remarks
-- Create	 	2011-09-01		Zhangbo			1.00.00
/**********************************************************************/
CREATE TABLE [SY_SHOP]
(
	SYS_CODE			nvarchar(4)			NOT NULL	-- 系统代码
	,CODE				nvarchar(4)			NOT NULL	-- 店代码
	,NAME				nvarchar(60)		NOT NULL	-- 店名称
	,FULL_NAME			nvarchar(240)		NULL		-- 店全称
	,ADDR_LINE1			nvarchar(300)		NULL		-- 地址1
	,ADDR_LINE2			nvarchar(300)		NULL		-- 地址2
	,PHONE_NO1			nvarchar(16)		NULL		-- 联系电话1
	,PHONE_NO2			nvarchar(16)		NULL		-- 联系电话2
	,FAX				nvarchar(16)		NULL		-- 传真
	,EMAIL				nvarchar(60)		NULL		-- 邮件
	,WEB_URL			nvarchar(150)		NULL		-- 网站地址
	,PROV				nvarchar(10)		NULL		-- 省
	,CITY				nvarchar(10)		NULL		-- 市
	,AREA				nvarchar(10)		NULL		-- 区域
	,POSTAL_CODE		nvarchar(10)		NULL		-- 邮政编码
	,PHONE_AREA_CODE	nvarchar(4)			NULL		-- 区域电话号码
	,MON_WK_DAY_FLAG	nvarchar(1)			NOT NULL	DEFAULT (N'Y')		-- 工作日设置：周一
	,TUE_WK_DAY_FLAG	nvarchar(1)			NOT NULL	DEFAULT (N'Y')		-- 工作日设置：周二
	,WED_WK_DAY_FLAG	nvarchar(1)			NOT NULL	DEFAULT (N'Y')		-- 工作日设置：周三
	,THU_WK_DAY_FLAG	nvarchar(1)			NOT NULL	DEFAULT (N'Y')		-- 工作日设置：周四
	,FRI_WK_DAY_FLAG	nvarchar(1)			NOT NULL	DEFAULT (N'Y')		-- 工作日设置：周五
	,SAT_WK_DAY_FLAG	nvarchar(1)			NOT NULL	DEFAULT (N'Y')		-- 工作日设置：周六
	,SUN_WK_DAY_FLAG	nvarchar(1)			NOT NULL	DEFAULT (N'N')		-- 工作日设置：周日
	,APPT_DAILY_QUOTA	bigint				NULL
	,BU_CODE			nvarchar(10)		NOT NULL	-- FK:BUSINESS_UNIT.BU_CODE
	,SHOP_TYPE			nvarchar(10)		NOT NULL	-- 可选项:GODOWN/CS
	,AUTH_PWD			nvarchar(60)		NULL		-- 特批密码			-- modify on 20130815
	,CREATED_BY			bigint				NOT NULL
	,CREATION_DATE		datetime			NOT NULL
	,LAST_UPDATED_BY	bigint				NOT NULL
	,LAST_UPDATE_DATE	datetime			NOT NULL
	,LAST_UPDATE_LOGIN	bigint				NULL
	,ROWID				uniqueidentifier	NOT NULL	DEFAULT (NEWID())
	,CONSTRAINT [PK_SHOP] PRIMARY KEY ([SYS_CODE],[CODE]) ON [PRIMARY]
	,CONSTRAINT [FK_SHOP_BU] FOREIGN KEY (BU_CODE) REFERENCES BUSINESS_UNIT(BU_CODE)
	,CONSTRAINT [CHK_SHOP_MON_WK_DAY_FLAG] CHECK ([MON_WK_DAY_FLAG]=N'N' OR [MON_WK_DAY_FLAG]=N'Y')
	,CONSTRAINT [CHK_SHOP_TUE_WK_DAY_FLAG] CHECK ([TUE_WK_DAY_FLAG]=N'N' OR [TUE_WK_DAY_FLAG]=N'Y')
	,CONSTRAINT [CHK_SHOP_WED_WK_DAY_FLAG] CHECK ([WED_WK_DAY_FLAG]=N'N' OR [WED_WK_DAY_FLAG]=N'Y')
	,CONSTRAINT [CHK_SHOP_THU_WK_DAY_FLAG] CHECK ([THU_WK_DAY_FLAG]=N'N' OR [THU_WK_DAY_FLAG]=N'Y')
	,CONSTRAINT [CHK_SHOP_FRI_WK_DAY_FLAG] CHECK ([FRI_WK_DAY_FLAG]=N'N' OR [FRI_WK_DAY_FLAG]=N'Y')
	,CONSTRAINT [CHK_SHOP_SAT_WK_DAY_FLAG] CHECK ([SAT_WK_DAY_FLAG]=N'N' OR [SAT_WK_DAY_FLAG]=N'Y')
	,CONSTRAINT [CHK_SHOP_SUN_WK_DAY_FLAG] CHECK ([SUN_WK_DAY_FLAG]=N'N' OR [SUN_WK_DAY_FLAG]=N'Y')
	,CONSTRAINT [CHK_SHOP_SHOP_TYPE] CHECK (SHOP_TYPE=N'GODOWN' OR SHOP_TYPE=N'CS')
) ON [PRIMARY]
GO