-- =============================================
-- Author:		JuLuDe
-- Create date: 2011-8-30
-- Description:	保存店信息
-- =============================================

/**********************************************************************/
-- Description:		保存店信息
---------------------------------------------------------------------
-- Action		Date			Staff		Version		Remarks
-- Create	 	2011-9-1			JuLuDe		1.00.00
-- Alter	 		2011-11-23		Kevin		1.00.01
---------------------------------------------------------------------
-- Field Description：

/**********************************************************************/

CREATE PROCEDURE spSaveShop
(
	@SYS_CODE				NVARCHAR(4),     --系统代码
	@CODE					NVARCHAR(4),     --店代码
	@NAME       			NVARCHAR(60),    --店名称
	@ADDR_LINE1 			NVARCHAR(300),    --店地址1
	@ADDR_LINE2  			NVARCHAR(300),    --店地址2
	@PHONE_NO1				NVARCHAR(20),    --电话号码1
	@PHONE_NO2				NVARCHAR(20),    --电话号码2
	@FAX					NVARCHAR(20),    --传真
	@EMAIL					NVARCHAR(60),	 --电子邮件
	@WEB_URL				NVARCHAR(150),   --网站地址
	@PROV					NVARCHAR(10),    --省
	@CITY					NVARCHAR(10),    --市
	@AREA					NVARCHAR(10),    --区域
	@POSTAL_CODE			NVARCHAR(10),    --邮政编码
	@PHONE_AREA_CODE		NVARCHAR(10),    --电话区域号码
	@LAST_UPDATED_BY		BIGINT, 
	@LAST_UPDATE_DATE		DATETIME,	
	@FULL_NAME				NVARCHAR(240),   --店全名
	@ROWID					UNIQUEIDENTIFIER,
	@CURRENT_USER_ID		BIGINT,
	@RecordStatus  			NVARCHAR(5),     --操作类型(添加-ADD 修改-EDIT)
	@BU_CODE				NVARCHAR(10),
	@SHOP_TYPE				NVARCHAR(10),
	@ResultType				INT				OUTPUT,
	@ResultMessage			NVARCHAR(1000)	OUTPUT
)
AS
BEGIN
	SET NOCOUNT ON 
	
	IF(@RecordStatus = N'EDIT')
	BEGIN
		IF EXISTS(SELECT 1 FROM SY_SHOP WHERE SYS_CODE = @SYS_CODE AND CODE = @CODE AND (DATEDIFF(S,LAST_UPDATE_DATE,@LAST_UPDATE_DATE) = 0 OR LAST_UPDATED_BY = @CURRENT_USER_ID))
		BEGIN
			UPDATE
				SY_SHOP
			SET 
				CODE=@CODE,
				NAME=@NAME ,
				ADDR_LINE1=@ADDR_LINE1,
				ADDR_LINE2=@ADDR_LINE2,
				PHONE_NO1=@PHONE_NO1 ,
				PHONE_NO2=@PHONE_NO2 ,
				FAX=@FAX,
				EMAIL=@EMAIL,		 
				WEB_URL=@WEB_URL, 
				PROV=@PROV,
				CITY=@CITY,
				AREA=@AREA,
				POSTAL_CODE=@POSTAL_CODE,
				PHONE_AREA_CODE=@PHONE_AREA_CODE ,
				LAST_UPDATE_DATE=GETDATE(),
				LAST_UPDATED_BY=@CURRENT_USER_ID, 
				FULL_NAME=@FULL_NAME,
				BU_CODE=@BU_CODE,
				SHOP_TYPE=@SHOP_TYPE
			WHERE
				CODE = @CODE
				AND SYS_CODE = @SYS_CODE;
					
			SET @ResultType = 0
			SET @ResultMessage = N'UPDATE SUCCEED'	
			RETURN @ResultType
		END
		ELSE
		BEGIN
			SET @ResultType = -1003;
			SET @ResultMessage = N'RECORD IS CHANGED';
			RETURN @ResultType
		END
	END 

	SET @ResultType = 0;
	SET @ResultMessage='';
	RETURN @ResultType;
END  