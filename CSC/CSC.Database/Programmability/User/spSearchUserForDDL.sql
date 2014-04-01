CREATE PROCEDURE [dbo].[spSearchUserForDDL]  
(  
 @SYS_CODE NVARCHAR(4),
 @USER_TYPE	nvarchar(2),
 @SHOP_CODE	nvarchar(4),
 @ResultType  INT        OUTPUT,  
 @ResultMessage NVARCHAR(1000)     OUTPUT  
)  
AS   
 BEGIN
  IF(@USER_TYPE = N'SA')
  BEGIN
	  SELECT   
	   [USER_ID],  
	   USER_CODE,  
	   [USER_NAME]
	  FROM SY_USER
	  where SYSTEM_SCOPE= @SYS_CODE
	  order by [USER_NAME]
	  ;
  END
  ELSE
  BEGIN
	  SELECT   
	   [USER_ID],  
	   USER_CODE,  
	   [USER_NAME]
	  FROM SY_USER
	  WHERE  SYSTEM_SCOPE= @SYS_CODE
		  and SY_USER.USER_TYPE = N'NS' 
		  --只筛选当前店的用户
		  and (SY_USER.[USER_ID] in (select SY_USER_SHOP.[USER_ID] from SY_USER_SHOP where SY_USER_SHOP.SHOP_CODE = @SHOP_CODE and SY_USER_SHOP.ACTIVE_FLAG = N'Y'))
	  order by [USER_NAME]
	  ;
  END
    
  

  SET @ResultType = 0;  
  SET @ResultMessage = ''  
    
  RETURN @ResultType;  
 END
 GO