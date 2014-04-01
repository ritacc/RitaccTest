/**********************************************************************/
-- Description: Provide the split function
-- Remarks:	
---------------------------------------------------------------------
-- Action		Date			Staff		Version		Remarks
-- Create	 	2013/6/24		LM			1.00.00
/**********************************************************************/

CREATE FUNCTION dbo.fnSplit(@String varchar(8000), @Delimiter char(1))
returns @temptable TABLE (items varchar(8000))
as
begin
    declare @idx int
    declare @slice varchar(8000)
 
    select @idx = 1
          if len(@String)<1 or @String is null return
       
     while @idx!= 0
     begin
         set @idx = charindex(@Delimiter,@String)
         if @idx!=0
             set @slice = left(@String,@idx - 1)
         else
             set @slice = @String
  
         if(len(@slice)>0)
             insert into @temptable(items) values(@slice)
   
         set @String = right(@String,len(@String) - @idx)
         if len(@String) = 0 break
     end
 return
 end


 GO

 CREATE function dbo.fnGetArrayLength
( 
	@str varchar(1024), --要分割的字符串 
	@split varchar(10) --分隔符号 
) 
returns int 
as 
begin 
	declare @location int 
	declare @start int 
	declare @length int 

	set @str=ltrim(rtrim(@str)) 
	set @location=charindex(@split,@str) 
	set @length=1 
	while @location<>0 
	begin 
		set @start=@location+1 
		set @location=charindex(@split,@str,@start) 
		set @length=@length+1 
	end 
	return @length 
end 

GO

CREATE function dbo.fnGetStrArrayStrOfIndex 
( 
@str varchar(1024), --要分割的字符串 
@split varchar(10), --分隔符号 
@index int --取第几个元素 
) 
returns varchar(1024) 
as 
begin 
	declare @location int 
	declare @start int 
	declare @next int 
	declare @seed int 

	set @str=ltrim(rtrim(@str)) 
	set @start=1 
	set @next=1 
	set @seed=len(@split) 

	set @location=charindex(@split,@str) 
	while @location<>0 and @index>@next 
	begin 
	set @start=@location+@seed 
	set @location=charindex(@split,@str,@start) 
	set @next=@next+1 
	end 
	if @location =0 select @location =len(@str)+1 
	--这儿存在两种情况：1、字符串不存在分隔符号 2、字符串中存在分隔符号，跳出while循环后，@location为0，那默认为字符串后边有一个分隔符号。 

	return substring(@str,@start,@location-@start) 
end 
