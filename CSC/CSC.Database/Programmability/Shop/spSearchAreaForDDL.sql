/**********************************************************************/
-- Description:		搜寻区信息
---------------------------------------------------------------------
-- Action		Date					Staff		Version		Remarks
-- Create	 	2011-11-21		Kevin	1.00.00
---------------------------------------------------------------------
-- Field Description：
-- CITY_PROV_CODE	省代码
-- CITY_CODE			市代码

/**********************************************************************/
CREATE PROCEDURE [dbo].[spSearchAreaForDDL]
(
	@CITY_PROV_CODE	NVARCHAR(10)
	,@CITY_CODE			NVARCHAR(10)
	,@ResultType				INT				OUTPUT
	,@ResultMessage		NVARCHAR(1000)	OUTPUT
)
AS
BEGIN
	SELECT
		CITY_PROV_CODE
		,CITY_CODE
		,CODE
		,FULL_NAME
		,LAST_UPDATE_DATE
		,LAST_UPDATED_BY
	FROM
		SY_AREA
	WHERE
		(@CITY_PROV_CODE = N'' OR (@CITY_PROV_CODE <> N'' AND CITY_PROV_CODE = @CITY_PROV_CODE))
		AND CITY_CODE = @CITY_CODE;

	SET @ResultType = 0;
	SET @ResultMessage = 'SEARCH SUCCEED';
	RETURN @ResultType;
END