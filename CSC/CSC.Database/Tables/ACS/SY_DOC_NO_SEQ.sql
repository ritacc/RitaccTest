/**********************************************************************/
-- Description:		文档编号
---------------------------------------------------------------------
-- Action		Date			Staff		Version		Remarks
-- Create	 	2013-05-28		XJ			1.00.00
-- Modify	 	2013-06-03		XJ			1.00.01     len(NO_TYPE)由4变成10
/**********************************************************************/
CREATE TABLE [SY_DOC_NO_SEQ]
(
	[SEQ_ID]				[bigint]		NOT NULL	IDENTITY(1,1),
	[BU_CODE]				[nvarchar](4)	NOT NULL,
	[NO_TYPE]				[nvarchar](30)	NOT NULL,
	[PREFIX]				[nvarchar](4)	NOT NULL,
	[NEXT_AVAIL_SEQ]		[bigint]		NOT NULL,
	[YEAR_LGH]				[bigint]		NOT NULL,	-- 日期-年的位数（[0,4]）
	[MONTH_LGH]				[bigint]		NOT NULL,	-- 日期-月的位数（[0,2]）
	[DAY_LGH]				[bigint]		NOT NULL,	-- 日期-日的位数（[0,2]）
	[NO_LGH]				[bigint]		NOT NULL,	-- 流水号的位数
	[RESET_PERIOD]			[nvarchar](1)	NOT NULL,	-- 重设周期：Y-年；M-月；D-日；N'' - 不重置
	[LAST_UPDATE_DATE]		[datetime]		NOT NULL,
	[LAST_UPDATED_BY]		[bigint]		NOT NULL,
	[CREATION_DATE]			[datetime]		NOT NULL,
	[CREATED_BY]			[bigint]		NOT NULL,
	[LAST_UPDATE_LOGIN]		[bigint]		NULL,
	CONSTRAINT [PK_MC_NO_SEQ] PRIMARY KEY CLUSTERED ([SEQ_ID] ASC)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
	CONSTRAINT [UK_NSEQ_NO_TYPE] UNIQUE ([BU_CODE],[NO_TYPE]),
	CONSTRAINT [CK_NSEQ_RESET_PERIOD] CHECK ([RESET_PERIOD]=N'' OR [RESET_PERIOD]=N'D' OR [RESET_PERIOD]=N'M' OR [RESET_PERIOD]=N'Y'),
	CONSTRAINT [CK_NSEQ_YEAR_LGH] CHECK ([YEAR_LGH] <= 4 AND [YEAR_LGH] >= 0),
	CONSTRAINT [CK_NSEQ_MONTH_LGH] CHECK ([MONTH_LGH] <= 2 AND [MONTH_LGH] >= 0),
	CONSTRAINT [CK_NSEQ_DAY_LGH] CHECK ([DAY_LGH] <= 2 AND [DAY_LGH] >= 0)
) ON [PRIMARY]
GO