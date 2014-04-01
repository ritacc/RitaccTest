/**********************************************************************/
-- Description:	 去掉右边的指定字符，有多少个就删多少个
---------------------------------------------------------------------
-- Action		Date			Staff		Version		Remarks
-- Create	 	2014-01-14		LM		1.00.00
---------------------------------------------------------------------
/**********************************************************************/
CREATE FUNCTION fnRtrimStr
(@string NVARCHAR(4000),
@trimStr NVARCHAR(50))
RETURNS NVARCHAR(4000)
AS
BEGIN
    set @string =isnull(@string ,'')
    WHILE (Len(@string) > 0)
      BEGIN
        IF RIGHT(@string,Len(@trimStr)) = @trimStr
          BEGIN
            SET @string = LEFT(@string,Len(@string) - Len(@trimStr))
          END
        ELSE
          BREAK
      END
    RETURN @string
END