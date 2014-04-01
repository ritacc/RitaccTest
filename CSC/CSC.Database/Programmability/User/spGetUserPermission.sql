-- =============================================  
-- Author:  xj  
-- Create date: 2011-10-26  
-- Description: 根据用户ID,SHOPCODE获取权限列表  
-- =============================================  
CREATE PROCEDURE [dbo].[spGetUserPermission]
(                          
	@SYS_CODE NVARCHAR(4),
	@USER_ID         INT = 0,   --角色ID 
	@SHOP_CODE	nvarchar(4),   
	@ResultType   INT   OUTPUT,  
	@ResultMessage  NVARCHAR(1000) OUTPUT  
)  
AS  
BEGIN
	IF EXISTS(SELECT 1 FROM SY_USER WHERE (FROZEN_FLAG='Y' OR SUSPEND_FLAG = N'Y') AND [USER_ID] = @USER_ID)
	BEGIN
		SET @ResultType = 0;  
		SET @ResultMessage = ''  
		RETURN;
	END
	select 
	SY_FUNCTION.FUNC_CODE,
	convert(bit,(case when sum(case when SY_ROLE_FUNC.INSERTABLE_FLAG = N'Y' THEN 1 when SY_ROLE_FUNC.INSERTABLE_FLAG = N'N' THEN 0  ELSE 0 END) = 0 then 0 else 1 end)) as INSERTABLE_FLAG,
	convert(bit,(case when sum(case when SY_ROLE_FUNC.UPDATABLE_FLAG = N'Y' THEN 1 when SY_ROLE_FUNC.UPDATABLE_FLAG = N'N' THEN 0  ELSE 0 END) = 0 then 0 else 1 end)) as UPDATABLE_FLAG
	from SY_ROLE_FUNC  
	left outer join SY_FUNCTION on SY_ROLE_FUNC.FUNC_ID = SY_FUNCTION.FUNC_ID
	where SY_ROLE_FUNC.ROLE_ID in
	(
		select SY_USER_ROLE_SHOP.ROLE_ID
		from SY_USER_ROLE_SHOP
			LEFT JOIN dbo.SY_ROLE ON SY_USER_ROLE_SHOP.ROLE_ID = dbo.SY_ROLE.ROLE_ID
		where 
			SY_USER_ROLE_SHOP.SYS_CODE = @SYS_CODE
			and SY_USER_ROLE_SHOP.[USER_ID] = @USER_ID
			and SY_USER_ROLE_SHOP.SHOP_CODE = @SHOP_CODE
			and SY_USER_ROLE_SHOP.ACTIVE_FLAG = N'Y'
			AND dbo.SY_ROLE.FROZEN_FLAG = N'N'
	)
	and SY_ROLE_FUNC.ACTIVE_FLAG = N'Y'
	--add by xj on 2013-4-11
	--and SY_FUNCTION.FUNC_TYPE in (select SY_SHOP.SHOP_TYPE from SY_SHOP where SY_SHOP.CODE = @SHOP_CODE)
	group by SY_FUNCTION.FUNC_CODE;

	 SET @ResultType = 0;  
	 SET @ResultMessage = '';
	 RETURN @ResultType;  
END
