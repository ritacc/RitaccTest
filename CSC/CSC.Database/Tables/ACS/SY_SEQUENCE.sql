/**********************************************************************/
-- Description:		SEQUENCE ID
---------------------------------------------------------------------
-- Action		Date			Staff		Version		Remarks
-- Modify	 	2011-09-02		Zhangbo		1.01.00		[SEQUENCE]: int -> bigint
-- Create	 	2011-09-01		XJ			1.00.00
/**********************************************************************/
CREATE TABLE [SY_SEQUENCE]
(
	SEQUENCE	bigint				NOT NULL	IDENTITY(1,1),
	ROW_ID		uniqueidentifier	NOT NULL	ROWGUIDCOL,
	CONSTRAINT [PK_SY_SEQUENCE] PRIMARY KEY CLUSTERED ([SEQUENCE] ASC)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO