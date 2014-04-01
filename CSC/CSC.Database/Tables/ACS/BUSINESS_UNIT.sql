/**********************************************************************/
-- Description:		Business Unit
---------------------------------------------------------------------
-- Action		Date			Staff		Version		Remarks
-- Create	 	2013-03-29		Zhangbo		1.00.00
/**********************************************************************/
CREATE TABLE BUSINESS_UNIT
(
	[BU_CODE]			nvarchar(10)	NOT NULL,
	[BU_NAME]			nvarchar(30)	NOT NULL,
	[LOGO_FILE]			nvarchar(500)	NULL,
	[CREATED_BY]		bigint			NOT NULL,
	[CREATION_DATE]		datetime		NOT NULL,
	[LAST_UPDATED_BY]	bigint			NOT NULL,
	[LAST_UPDATE_DATE]	datetime		NOT NULL,
	CONSTRAINT [PK_BUSINESS_UNIT] PRIMARY KEY ([BU_CODE]) ON [PRIMARY]
) ON [PRIMARY]
GO