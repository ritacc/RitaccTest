CREATE PROCEDURE [dbo].[spGetUserPassword]
(                        
	@UserName   nvarchar(20) ,
	@ShopCode nvarchar(20),
	@ResultType		int				OUTPUT,
	@ResultMessage	nvarchar(1000)	OUTPUT
)
AS
BEGIN
	SELECT 
      [LOGIN_PWD]
     
  FROM [SY_USER]

	WHERE
		 
		   USER_CODE = @UserName
		
		
	SET @ResultType = 0;
	SET @ResultMessage = ''
	RETURN @ResultType;
END