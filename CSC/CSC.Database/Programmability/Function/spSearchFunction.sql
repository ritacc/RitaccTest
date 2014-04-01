/**********************************************************************/
-- Description:		搜寻功能信息
---------------------------------------------------------------------
-- Action		Date			Staff		Version		Remarks
-- Create	 	2011-09-23		ZJX			1.00.00
---------------------------------------------------------------------
/**********************************************************************/
CREATE PROCEDURE [dbo].[spSearchFunction]
(                        
	@SYS_CODE			NVARCHAR(4)
	,@ADMIN_FLAG		BIT	= NULL
	,@FunctionCode      NVARCHAR(20)
	,@FunctionDSC       NVARCHAR(50)
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
	FROM 
		SY_FUNCTION
	WHERE 
		SYS_CODE = @SYS_CODE
		AND (@ADMIN_FLAG IS NULL OR (@ADMIN_FLAG IS NOT NULL AND ADMIN_FLAG = dbo.fnConvertBitToNvarchar(@ADMIN_FLAG)))
		AND (@FunctionCode = N'' OR FUNC_CODE LIKE N'%'+ @FunctionCode+'%')
		AND (@FunctionDSC = N'' OR DSC LIKE N'%'+ @FunctionDSC+'%')

	SET @ResultType = 0;
	SET @ResultMessage = N'SEARCH SUCCEED';

	RETURN @ResultType;
END

