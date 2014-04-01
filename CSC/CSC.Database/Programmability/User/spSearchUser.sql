CREATE PROCEDURE [dbo].[spSearchUser]    
(     
  @SYS_CODE  NVARCHAR(4),  
  @USER_ID       INT = 0,   --用户ID   
  @USER_CODE     nvarchar(20) = N'',   --用户代码  
  @USER_NAME  nvarchar(20),  
  @ROLE_ID       INT = 0,   --角色ID   
  @USER_TYPE  nvarchar(2),  
  @SHOP_CODE  nvarchar(4),  
  --added by jason in 20121219 for TSYF02010#06.doc  
  @SUSPEND_FLAG nvarchar(1) = N'',  
  --end added by jason in 20121219 for TSYF02010#06.doc  
  @SUSPEND		NVARCHAR(1) = N'',
  @ResultType INT        OUTPUT,    
  @ResultMessage NVARCHAR(1000)     OUTPUT    
)    
AS     
BEGIN  
if(@USER_TYPE = N'SA')  
 begin     
  SELECT     
   [USER_ID],    
   upper(USER_CODE) AS USER_CODE,    
   upper([USER_NAME]) AS [USER_NAME],    
   USER_TYPE,     
   SYSTEM_SCOPE,   
   dbo.fnConvertNvarcharToBit(FROZEN_FLAG)  AS FROZEN_FLAG,    
   FROZEN_DATE,      
   dbo.fnConvertNvarcharToBit(SUSPEND_FLAG) AS SUSPEND_FLAG,  
   SUSPEND_DATE,  
   CREATION_DATE,  
   LAST_UPDATE_DATE,  
   LAST_UPDATED_BY,  
   (select COUNT(1) from SY_USER_SHOP where SY_USER_SHOP.[USER_ID] = SY_USER.[USER_ID] AND SY_USER_SHOP.ACTIVE_FLAG=N'Y') as SHOP_COUNT  
  FROM   
   SY_USER  
  WHERE   
   SY_USER.SYSTEM_SCOPE= @SYS_CODE  
   and (@USER_ID = 0 OR SY_USER.[USER_ID] = @USER_ID)    
   and (@USER_CODE =N'' OR SY_USER.USER_CODE LIKE N'%' + @USER_CODE + N'%')  
   and (@USER_NAME = N'' or SY_USER.[USER_NAME] like '%' + @USER_NAME + '%')  
   and (@ROLE_ID = 0 OR SY_USER.[USER_ID] in (select SY_USER_ROLE.[USER_ID] from SY_USER_ROLE where SY_USER_ROLE.ROLE_ID = @ROLE_ID and SY_USER_ROLE.ACTIVE_FLAG = N'Y'))  
   --added by jason in 20121219 for TSYF02010#06.doc  
   --AND (@SUSPEND_FLAG = N'' OR dbo.SY_USER.SUSPEND_FLAG = @SUSPEND_FLAG)  
   --end added by jason in 20121219 for TSYF02010#06.doc  
   AND dbo.SY_USER.SUSPEND_FLAG = 
	CASE @SUSPEND
		WHEN N'N' THEN N'N' --正常
		WHEN N'S' THEN N'Y'	--暂停
		ELSE dbo.SY_USER.SUSPEND_FLAG
	END
 end  
else  
 begin  
  SELECT     
   [USER_ID],    
   USER_CODE,    
   [USER_NAME],    
   USER_TYPE,     
   SYSTEM_SCOPE,   
   dbo.fnConvertNvarcharToBit(FROZEN_FLAG)  AS FROZEN_FLAG,    
   FROZEN_DATE,      
   dbo.fnConvertNvarcharToBit(SUSPEND_FLAG) AS SUSPEND_FLAG,  
   SUSPEND_DATE,  
   CREATION_DATE,  
   LAST_UPDATE_DATE,  
   LAST_UPDATED_BY,  
   (select COUNT(1) from SY_USER_SHOP where SY_USER_SHOP.[USER_ID] = SY_USER.[USER_ID] AND SY_USER_SHOP.ACTIVE_FLAG=N'Y') as SHOP_COUNT  
  FROM   
   SY_USER  
  WHERE   
   SY_USER.SYSTEM_SCOPE= @SYS_CODE   
   and (@USER_ID = 0 OR SY_USER.[USER_ID] = @USER_ID)    
   and (@USER_CODE =N'' OR SY_USER.USER_CODE LIKE N'%' + @USER_CODE + N'%')  
   and (@USER_NAME = N'' or SY_USER.[USER_NAME] like '%' + @USER_NAME + '%')  
   and (@ROLE_ID = 0 OR SY_USER.[USER_ID] in (select SY_USER_ROLE.[USER_ID] from SY_USER_ROLE where SY_USER_ROLE.ROLE_ID = @ROLE_ID and SY_USER_ROLE.ACTIVE_FLAG = N'Y'))  
   --只筛选公司类型的用户  
   and SY_USER.USER_TYPE = N'NS'   
   --只筛选当前店的用户  
   and (SY_USER.[USER_ID] in (select SY_USER_SHOP.[USER_ID] from SY_USER_SHOP where SY_USER_SHOP.SHOP_CODE = @SHOP_CODE and SY_USER_SHOP.ACTIVE_FLAG = N'Y'))  
   --added by jason in 20121219 for TSYF02010#06.doc  
   --AND (@SUSPEND_FLAG IS NULL OR dbo.SY_USER.SUSPEND_FLAG = dbo.fnConvertBitToNvarchar(@SUSPEND_FLAG))  
   --AND (@SUSPEND_FLAG = N'' OR dbo.SY_USER.SUSPEND_FLAG = @SUSPEND_FLAG)  
   --end added by jason in 20121219 for TSYF02010#06.doc  
   AND dbo.SY_USER.SUSPEND_FLAG = 
	CASE @SUSPEND
		WHEN N'N' THEN N'N' --正常
		WHEN N'S' THEN N'Y'	--暂停
		ELSE dbo.SY_USER.SUSPEND_FLAG
	END
  END  
    
  SET @ResultType = 0;    
  SET @ResultMessage = '';  
      
  RETURN @ResultType;    
 END  
