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
CREATE PROCEDURE [dbo].[spSearchCityForDDL]
(       
	@PROV_CODE		NVARCHAR(10)            
	,@ResultType			INT				OUTPUT
	,@ResultMessage	NVARCHAR(1000)	OUTPUT
)
AS
BEGIN
	SELECT
		CODE
		,FULL_NAME		
	FROM
		SY_CITY
	WHERE
		SY_CITY.PROV_CODE = @PROV_CODE;	
	SET @ResultType = 0;
	SET @ResultMessage = 'SEARCH SUCCEED';
	RETURN @ResultType;
END

