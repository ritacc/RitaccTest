/**********************************************************************/
-- Description:		PRINT QUEUE
---------------------------------------------------------------------
-- Action		Date			Staff		Version		Remarks
-- Create	 	2013-05-22		Zhangbo		1.00.00
/**********************************************************************/
CREATE TABLE SY_PRINT_QUEUE
(
	PRINT_QUEUE_ID		bigint			NOT NULL		IDENTITY(1,1)
	,PRINT_QUEUE_NAME	nvarchar(30)	NOT NULL
	,PRINT_QUEUE_DESC	nvarchar(100)	NOT NULL
	,PRINTER_TYPE		nvarchar(5)		NOT NULL
	,CREATED_BY			bigint			NOT NULL
	,CREATION_DATE		datetime		NOT NULL
	,LAST_UPDATED_BY	bigint			NOT NULL
	,LAST_UPDATE_DATE	datetime		NOT NULL
	,CONSTRAINT PK_PRINT_QUEUE PRIMARY KEY(PRINT_QUEUE_ID) ON [PRIMARY]
) ON [PRIMARY]
GO