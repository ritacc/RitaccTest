/**********************************************************************/
-- Description:		载入功能信息
---------------------------------------------------------------------
-- Action		Date			Staff		Version		Remarks
-- Create	 	2011-09-23		ZJX			1.00.00
---------------------------------------------------------------------
/**********************************************************************/
CREATE PROCEDURE [dbo].[spLoadFunction]
(                        
	@SYS_CODE			NVARCHAR(4)
	,@FUNC_ID			BIGINT
	,@ResultType		INT				OUTPUT
	,@ResultMessage		NVARCHAR(1000)	OUTPUT
)
AS
BEGIN
	SET NOCOUNT ON;
	
	SELECT 
		FUNC_ID --功能编号
		,FUNC_CODE --功能代码
		,DSC --功能名称
		,EXECUTABLE --地址
		,SYSTEM_SCOPE --系统
		,dbo.fnConvertNvarcharToBit(ADMIN_FLAG) AS ADMIN_FLAG --管理
		,FUNC_TYPE --类型
		,LAST_UPDATE_DATE --最后更新时间
		,LAST_UPDATED_BY --最后更新人
	FROM 
		SY_FUNCTION
	WHERE 
		SYS_CODE = @SYS_CODE
		AND FUNC_ID = @FUNC_ID;

	SET @ResultType = 0;
	SET @ResultMessage = N'LOAD SUCCEED';

	RETURN @ResultType;
END

