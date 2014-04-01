-- =============================================  
-- Author:  JuLuDe  
-- Create date: 2011-9-13  
-- Description: 保存角色  
-- =============================================  
CREATE PROCEDURE [dbo].[spSaveRole]
(
	 @ROLE_ID NUMERIC(10, 0)    --角色ID  
	,@SYS_CODE NVARCHAR(4)   --系统代码 
	,@ROLE_CODE NVARCHAR(20)     --角色代码  
	,@ROLE_TYPE NVARCHAR(2)      --系统层面(系统 店)  
	,@ROLE_DSC NVARCHAR(50)     --说明
	 --,@SYSTEM_SCOPE    NVARCHAR(4)      --使用系统  
	,@ADMIN_FLAG BIT     --是否具有管理权限 
	,@ROLE_SDSC NVARCHAR(50)     --简述
	,@SHOP_CODE NVARCHAR(4)      --点代码  
	,@LAST_UPDATE_DATE DATETIME
	,@LAST_UPDATED_BY BIGINT
	,@CURRENT_USER_ID BIGINT
	,@USER_TYPE NVARCHAR(2)
	,@BU_CODE NVARCHAR(10)
	,@ResultType INT OUTPUT
	,@ResultMessage NVARCHAR(1000) OUTPUT
)
AS 
BEGIN
    SET NOCOUNT ON

	--ADD BY XJ ON 20130902
	SET @ROLE_TYPE = N'SY';
	SET @ADMIN_FLAG = 0;

    IF (@USER_TYPE = N'NS') 
        BEGIN
            SET @ROLE_TYPE = N'SH' ;
        END		
    IF (@ROLE_TYPE = N'SY')
		BEGIN
			SET @SHOP_CODE = N'*';
		END
	
	--数据重复
    IF EXISTS (SELECT 1 FROM SY_ROLE WHERE ROLE_ID <> @ROLE_ID AND upper(ROLE_CODE) = upper(@ROLE_CODE) AND SYS_CODE = @SYS_CODE AND SHOP_CODE = @SHOP_CODE) 
        BEGIN
            SET @ResultType = -1001 ;
            SET @ResultMessage = 'RECORD CONTAINS DUPLICATE DATA.' ;
            RETURN @ResultType ;
        END  

    IF (@ROLE_ID = 0) 
        BEGIN
		 --获取ID值 
            DECLARE @Sequence BIGINT ;     
            DECLARE @RowId UNIQUEIDENTIFIER ;   
            EXEC spGetNextSequence @Sequence OUTPUT, @RowId OUTPUT ;     

            INSERT INTO SY_ROLE
                    (ROLE_ID
                    ,SYS_CODE
                    ,ROLE_CODE
                    ,ROLE_TYPE
                    ,ROLE_DSC
                    ,SYSTEM_SCOPE
                    ,ADMIN_FLAG
                    ,LAST_UPDATE_DATE
                    ,LAST_UPDATED_BY
                    ,CREATION_DATE
                    ,CREATED_BY
                    ,LAST_UPDATE_LOGIN
                    ,ROLE_SDSC
                    ,SHOP_CODE
					,BU_CODE
		        )
            VALUES
                    (@Sequence
                    ,@SYS_CODE
                    ,upper(@ROLE_CODE)
                    ,@ROLE_TYPE
                    ,@ROLE_DSC
                    ,@SYS_CODE
                    ,dbo.fnConvertBitToNvarchar(@ADMIN_FLAG)
                    ,GETDATE()--Convert(datetime, Convert(varchar(20), GETDATE(), 120), 120)
                    ,@CURRENT_USER_ID
                    ,GETDATE()--Convert(datetime, Convert(varchar(20), GETDATE(), 120), 120)
                    ,@CURRENT_USER_ID
                    ,@CURRENT_USER_ID
                    ,@ROLE_SDSC
					--added by Jason in 20120913 for bug System Role's Shop Code Not Equals *
                    ,@SHOP_CODE
					--end added by Jason in 20120913 for bug System Role's Shop Code Not Equals *
					
					--commented by Jason in 20120913 for bug System Role's Shop Code Not Equals *
					--,CASE WHEN  @SHOP_CODE = N'' then N'*' else @SHOP_CODE end
					--end commented by Jason in 20120913 for bug System Role's Shop Code Not Equals *
					,@BU_CODE
		        ) ;

			--将function数据写入SY_ROLE_FUNC
			--insert into SY_ROLE_FUNC
			--select 
			--	FUNC_ID
			--	,@Sequence
			--	,'N'
			--	,GETDATE()
			--	,@CURRENT_USER_ID
			--	,GETDATE()
			--	,@CURRENT_USER_ID
			--	,@CURRENT_USER_ID
			--	,'N'
			--	,'N'
			--from SY_FUNCTION
			--where SY_FUNCTION.SYS_CODE = @SYS_CODE
			--	and SY_FUNCTION.ADMIN_FLAG = dbo.fnConvertBitToNvarchar(@ADMIN_FLAG);

            SET @ResultType = 0 ;
            SET @ResultMessage = 'INERT SUCCEED' ;
            RETURN @ResultType ;
        END
    ELSE 
        BEGIN
            IF NOT EXISTS (SELECT 1 FROM SY_ROLE WHERE ROLE_ID = @ROLE_ID AND SYS_CODE = @SYS_CODE AND (DATEDIFF(S, LAST_UPDATE_DATE, @LAST_UPDATE_DATE) = 0 OR LAST_UPDATED_BY = @CURRENT_USER_ID))
                BEGIN
                    SET @ResultType = -1003 ;
                    SET @ResultMessage = 'RECORD IS CHANGED' ;
                    RETURN @ResultType ;
                END
		
			--如果ADMIN_FLAG从false->true，删除之前写人的SY_ROLE_FUNC
            DECLARE @OLD_ADMIN_FLAG NVARCHAR(1) ;
            SELECT @OLD_ADMIN_FLAG = ADMIN_FLAG FROM SY_ROLE WHERE ROLE_ID = @ROLE_ID ;

            IF (dbo.fnConvertBitToNvarchar(@ADMIN_FLAG) = N'Y' AND @OLD_ADMIN_FLAG = N'N') 
                BEGIN
                    DELETE FROM SY_ROLE_FUNC WHERE ROLE_ID = @ROLE_ID ;

					--将function数据写入SY_ROLE_FUNC
					--insert into SY_ROLE_FUNC
					--select 
					--	FUNC_ID
					--	,@ROLE_ID
					--	,'N'
					--	,GETDATE()
					--	,@CURRENT_USER_ID
					--	,GETDATE()
					--	,@CURRENT_USER_ID
					--	,@CURRENT_USER_ID
					--	,'N'
					--	,'N'
					--from SY_FUNCTION
					--where SY_FUNCTION.SYS_CODE = @SYS_CODE
					--	and SY_FUNCTION.ADMIN_FLAG = dbo.fnConvertBitToNvarchar(@ADMIN_FLAG);
                END

            UPDATE
                SY_ROLE
            SET 
				--SYS_CODE = @SYS_CODE
				--,ROLE_TYPE = @ROLE_TYPE
                ROLE_DSC = @ROLE_DSC
               ,ADMIN_FLAG = dbo.fnConvertBitToNvarchar(@ADMIN_FLAG)
               ,ROLE_SDSC = @ROLE_SDSC
               ,LAST_UPDATE_DATE = GETDATE()--Convert(datetime, Convert(varchar(20), GETDATE(), 120), 120)
               ,LAST_UPDATED_BY = @CURRENT_USER_ID
               ,LAST_UPDATE_LOGIN = @CURRENT_USER_ID
            WHERE
                ROLE_ID = @ROLE_ID ;
	
            SET @ResultType = 0 ;
            SET @ResultMessage = 'UPDATE SUCCEED' ;
            RETURN @ResultType ;
        END
END 