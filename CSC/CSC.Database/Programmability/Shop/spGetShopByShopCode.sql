/**********************************************************************/
-- Description:根据店代码查找店
---------------------------------------------------------------------
-- Action		Date			Staff		Version		Remarks
-- Create	 	2011-9-1    	JuLuDe		1.00.00
---------------------------------------------------------------------
/**********************************************************************/
CREATE PROCEDURE spGetShopByShopCode
(                        
	@Code           nvarchar(20) ,  --店代码
	@ResultType		INT				OUTPUT,
	@ResultMessage	nvarchar(1000)	OUTPUT
)
AS
BEGIN
	SELECT 
			CODE 
    FROM 
			SY_SHOP
	WHERE 
		   CODE=@Code  
		   
	SET @ResultType = 0;
	SET @ResultMessage = ''
	
	RETURN @ResultType;
END