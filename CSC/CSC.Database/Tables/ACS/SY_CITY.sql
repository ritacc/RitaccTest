CREATE TABLE [SY_CITY]
(
	[PROV_CODE]				nvarchar(10)	NOT NULL,
	[CODE]					nvarchar(10)	NOT NULL,
	[FULL_NAME]				nvarchar(50)	NULL,
	[CREATED_BY]			bigint			NOT NULL,
	[CREATION_DATE]			datetime		NOT NULL,
	[LAST_UPDATED_BY]		bigint			NOT NULL,
	[LAST_UPDATE_DATE]		datetime		NOT NULL,
	[LAST_UPDATE_LOGIN]		bigint			NULL,
	CONSTRAINT [PK_CITY] PRIMARY KEY CLUSTERED ([PROV_CODE] ASC, [CODE] ASC)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
	CONSTRAINT [FK_CITY_PROV] FOREIGN KEY([PROV_CODE])REFERENCES [SY_PROVINCE] ([CODE])
) ON [PRIMARY]
GO