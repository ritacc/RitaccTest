/**********************************************************************/
-- Description:		系统参数
---------------------------------------------------------------------
-- Action		Date			Staff		Version		Remarks
-- Create	 	2011-09-01		Zhangbo			1.00.00
/**********************************************************************/
CREATE TABLE [SY_PARAMETER]
(
	[PARA_ID]			bigint			NOT NULL,
	[PARA_CODE]			nvarchar(30)	NOT NULL,
	[PARA_DSC]			nvarchar(100)	NOT NULL,
	[REMARKS]			nvarchar(500)	NOT NULL,
	[PARA_TYPE]			nvarchar(1)		NOT NULL	DEFAULT ('C'),
	[PARA_VALUE]		nvarchar(30)	NOT NULL,
	SYS_CODE			nvarchar(4)		NOT NULL,
	BU_CODE				nvarchar(10)	NOT NULL,
	-- 数据信息
	CREATED_BY			bigint			NOT NULL,
	CREATION_DATE		datetime		NOT NULL,
	LAST_UPDATED_BY		bigint			NOT NULL,
	LAST_UPDATE_DATE	datetime		NOT NULL,
	CONSTRAINT [SY_PARAMETER_PK] PRIMARY KEY CLUSTERED ([PARA_ID] ASC)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
	CONSTRAINT CHK_SY_PARAMETER_TYPE CHECK ([PARA_TYPE]=N'C' OR [PARA_TYPE]=N'N' OR [PARA_TYPE]=N'D')
) ON [PRIMARY]
GO
