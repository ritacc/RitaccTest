/**********************************************************************/
-- Description:		店参数
---------------------------------------------------------------------
-- Action		Date			Staff		Version		Remarks
-- Create	 	2011-09-01		Zhangbo			1.00.00
/**********************************************************************/
CREATE TABLE [SY_PARAMETER_SHOP]
(
	[PARA_S_ID]			bigint			NOT NULL	IDENTITY(1,1),
	[SHOP_CODE]			nvarchar(4)		NOT NULL,
	[PARA_S_CODE]		nvarchar(20)	NOT NULL,
	[PARA_TYPE]			nvarchar(1)		NOT NULL,
	[PARA_S_VALUE]		nvarchar(30)	NOT NULL,
	SYS_CODE			nvarchar(4)		NOT NULL,
	BU_CODE				nvarchar(10)	NOT NULL,
	-- 数据信息
	CREATED_BY			bigint			NOT NULL,
	CREATION_DATE		datetime		NOT NULL,
	LAST_UPDATED_BY		bigint			NOT NULL,
	LAST_UPDATE_DATE	datetime		NOT NULL,
	CONSTRAINT PK_SY_PARAMETER_SHOP PRIMARY KEY CLUSTERED ([PARA_S_ID] ASC) ON [PRIMARY],
	CONSTRAINT UK_SY_PARAMETER_SHOP_CODE UNIQUE ([SHOP_CODE], [PARA_S_CODE]),
	CONSTRAINT CHK_SY_PARAMETER_SHOP_TYPE CHECK ([PARA_TYPE]=N'C' OR [PARA_TYPE]=N'N' OR [PARA_TYPE]=N'D')
) ON [PRIMARY]
GO