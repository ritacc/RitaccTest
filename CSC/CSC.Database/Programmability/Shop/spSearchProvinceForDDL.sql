/**********************************************************************/
-- Description:		搜寻省信息
---------------------------------------------------------------------
-- Action		Date					Staff		Version		Remarks
-- Create	 	2011-11-21		Kevin	1.00.00
---------------------------------------------------------------------
-- Field Description：
-- CODE			省代码
-- FULL_NAME		省全称
/**********************************************************************/
CREATE PROCEDURE [dbo].[spSearchProvinceForDDL]
(                        
	@ResultType			INT				OUTPUT
	,@ResultMessage	NVARCHAR(1000)	OUTPUT
)
AS
BEGIN
	SELECT
		CODE
		,FULL_NAME		
	FROM
		SY_PROVINCE;
	
	SET @ResultType = 0;
	SET @ResultMessage = 'SEARCH SUCCEED';
	RETURN @ResultType;
END

