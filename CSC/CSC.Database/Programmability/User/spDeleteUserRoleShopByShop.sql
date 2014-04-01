/**********************************************************************/
-- Description:		删除用户所属店角色信息
---------------------------------------------------------------------
-- Action		Date			Staff		Version		Remarks
-- Create	 	2011-09-20		XJ			1.00.00
-- Modify		2012-03-06		JASON		1.01.00		根据类型删除店角色
---------------------------------------------------------------------
/**********************************************************************/
CREATE PROCEDURE [dbo].[spDeleteUserRoleShopByShop]
(
	 @USER_ID INT
	,@SHOP_CODE NVARCHAR(4)
	,@ROLE_TYPE NVARCHAR(2) --为空则删除所有，否则则删除相关类型的(SY,SH)
	,@ResultType INT OUTPUT
	,@ResultMessage NVARCHAR(1000) OUTPUT  
)
AS 
BEGIN  
    
    DELETE
		dbo.SY_USER_ROLE_SHOP
    FROM
        dbo.SY_USER_ROLE_SHOP
        LEFT JOIN dbo.SY_ROLE ON dbo.SY_USER_ROLE_SHOP.ROLE_ID = dbo.SY_ROLE.ROLE_ID
    WHERE
        dbo.SY_USER_ROLE_SHOP.[USER_ID] = @USER_ID
        AND dbo.SY_USER_ROLE_SHOP.SHOP_CODE = @SHOP_CODE
        AND (@ROLE_TYPE = N'' OR (@ROLE_TYPE <> N'' AND dbo.SY_ROLE.ROLE_TYPE = @ROLE_TYPE));
    
    SET @ResultType = 0 ;  
    SET @ResultMessage = N'DELETE SUCCEED'  
    
    RETURN @ResultType ;  
END