/**********************************************************************/
-- Description:拆分字符串，返回一个表。
---------------------------------------------------------------------
-- Action		Date			Staff		Version		Remarks
-- Create	 	2011-11-14		xj			1.00.00
/**********************************************************************/
CREATE FUNCTION fnSplitString
(
	@String		nvarchar(max),		--待分拆的字符串
	@Split		nvarchar(10)		--数据分隔符
) RETURNS @return TABLE(value nvarchar(100))
AS
BEGIN
	DECLARE @splitlen int;
	SET @splitlen=LEN(@Split+'a')-2;

	WHILE CHARINDEX(@Split,@String)>0
	BEGIN
		INSERT @return VALUES(LEFT(@String,CHARINDEX(@Split,@String)-1));
		--删除前面的字符
		SET @String=STUFF(@String,1,CHARINDEX(@Split,@String)+@splitlen,'');
	END
	INSERT @return VALUES(@String);
	RETURN
END
GO