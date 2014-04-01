/**********************************************************************/
-- Description:	
---------------------------------------------------------------------
-- Action		Date			Staff		Version		Remarks
-- Create	 	2011-09-20		XJ			1.00.00
---------------------------------------------------------------------
CREATE PROCEDURE [dbo].[spDeleteUserRoleNotInUser]
(
	 @USER_ID INT
	,@ResultType INT OUTPUT
	,@ResultMessage NVARCHAR(1000) OUTPUT  
)
AS 
BEGIN  
 
    DELETE FROM
        dbo.SY_USER_ROLE
    WHERE
        SY_USER_ROLE.[USER_ID] = @USER_ID
        AND SY_USER_ROLE.ROLE_ID NOT IN (SELECT DISTINCT
                                            ROLE_ID
                                         FROM
                                            SY_USER_ROLE_SHOP
                                         WHERE
                                            SY_USER_ROLE_SHOP.[USER_ID] = @USER_ID) ;
    
    SET @ResultType = 0 ;  
    SET @ResultMessage = 'DELETE SUCCEED';
    
    RETURN @ResultType ;  
END