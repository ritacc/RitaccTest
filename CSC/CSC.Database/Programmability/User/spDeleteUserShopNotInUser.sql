-- =============================================  
-- Author:  XJ  
-- Create date: 2011-9-20  
-- Description:  
-- =============================================  
CREATE PROCEDURE [dbo].[spDeleteUserShopNotInUser]  
(  
 @USER_ID         INT, 
 @ResultType  INT        OUTPUT,  
 @ResultMessage NVARCHAR(1000)     OUTPUT  
)  
AS   
 BEGIN  
 
  delete from dbo.SY_USER_SHOP 
  where SY_USER_SHOP.[USER_ID] = @USER_ID
	and SY_USER_SHOP.SHOP_CODE not in (select distinct SHOP_CODE from SY_USER_ROLE_SHOP where SY_USER_ROLE_SHOP.[USER_ID] = @USER_ID )
  ;
    
  SET @ResultType = 0;  
  SET @ResultMessage = ''  
    
  RETURN @ResultType;  
 END
 GO