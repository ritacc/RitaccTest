/**********************************************************************/
-- Description:	
---------------------------------------------------------------------
-- Action		Date			Staff		Version		Remarks
-- Create	 	2011-09-20		XJ			1.00.00
--------------------------------------------------------------------- 
CREATE PROCEDURE [dbo].[spDeleteUserRoleShop]
(
	 @SYS_CODE NVARCHAR(4)
	,@USER_ID INT
	,@CURRENT_USER_ID BIGINT
	,@ResultType INT OUTPUT
	,@ResultMessage NVARCHAR(1000) OUTPUT  
)
AS 
BEGIN  
 
    DELETE FROM
        dbo.SY_USER_ROLE_SHOP
    WHERE
        [USER_ID] = @USER_ID
        AND SY_USER_ROLE_SHOP.SHOP_CODE IN (SELECT DISTINCT
                                                SHOP_CODE
                                            FROM
                                                SY_USER_SHOP
                                            WHERE
                                                SY_USER_SHOP.[USER_ID] = @CURRENT_USER_ID
                                                AND SY_USER_SHOP.ACTIVE_FLAG = N'Y'
                                                AND SY_USER_SHOP.SYS_CODE = @SYS_CODE) ;
                                                
    SET @ResultType = 0 ;  
    SET @ResultMessage = 'DELETE SUCCEED';
    
    RETURN @ResultType ;  
END