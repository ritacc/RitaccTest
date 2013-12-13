declare @ControlNum int;--查询控件数量，用于判断是否已经添加
set @ControlNum=0;
declare @AddControl varchar(50);--添加控件名称

--添加“NetLine”
set @AddControl='NetLine'
declare @ControlID int;
select @ControlID=ControlID from t_control where controlName=@AddControl
if @ControlID >1
begin
	delete t_control where ControlID=@ControlID
	delete [t_ControlProperty] where ControlID=@ControlID
end

select @ControlNum=count(*) from t_control where controlname=@AddControl
if @ControlNum = 0
begin
		print '添加控件'
		print @AddControl
		insert into t_control (controlname,controltype,imageUrl,controltypeName,controlCaption)
		values(@AddControl,'10','FoldLine.jpg','ItMonitor','连接线');
		 
		set @ControlID=0;
		select @ControlID=max(controlid)  from t_control
		if @ControlID > 0
		begin
			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 1,'StrokeThickness','1.5','边线粗细');
			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 2,'Stroke','#5694BA','线颜色');
			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 3,'PointsSave','','位置集');

			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 4,'Paser1','','表达式1');
			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 5,'ShowColor1','#FFC6EBC2','颜色1');
			
			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 6,'Paser2','','表达式2');
			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 7,'ShowColor2','#FF3EB74D','颜色2');

			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 8,'Paser3','','表达式3');
			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 9,'ShowColor3','#FF1DE5DA','颜色3');

			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 10,'Paser4','','表达式4');
			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 11,'ShowColor4','#FF0B80B7','颜色4');

			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 12,'Paser5','','表达式5');
			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 13,'ShowColor5','#FF2340EB','颜色5');

			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 14,'Paser6','','表达式6');
			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 15,'ShowColor6','#FFDAE517','颜色6');

			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 16,'Paser7','','表达式7');
			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 17,'ShowColor7','#FFE52517','颜色7');

			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 20,'UpLinePort','','上联设备端口');
			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 21,'UpLineDeviceID','','上联设备ID');
			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 22,'DownLinePort','1','下联设备端口');
			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 23,'DownLineDeviceID','','下联设备ID');

		end
end

--添加“NetDevice”
set @AddControl='NetDevice'
select @ControlID=ControlID from t_control where controlName=@AddControl
if @ControlID >1
begin
	delete t_control where ControlID=@ControlID
	delete [t_ControlProperty] where ControlID=@ControlID
end

select @ControlNum=count(*) from t_control where controlname=@AddControl
if @ControlNum = 0
begin
		print '添加设备'
		print @AddControl
		insert into t_control (controlname,controltype,imageUrl,controltypeName,controlCaption)
		values(@AddControl,'10','Server.jpg','ItMonitor','设备');


		set @ControlID=0;
		select @ControlID=max(controlid)  from t_control
		if @ControlID > 0
		begin			
			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 1,'DeviceName','','设备名称');

			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 2,'Paser1','','表达式1');
			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 3,'Img1','','图片1');
			
			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 4,'Paser2','','表达式2');
			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 5,'Img2','','图片2');

			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 6,'Paser3','','表达式3');
			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 7,'Img3','','图片3');

			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 8,'Paser4','','表达式4');
			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 9,'Img4','','图片4');

			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 10,'Paser5','','表达式5');
			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 11,'Img5','','图片5');

			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 12,'Paser6','','表达式6');
			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 13,'Img6','','图片6');

			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 14,'Paser7','','表达式7');
			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 15,'Img7','','图片7');

			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 16,'DefultImg','route0.png','图片7');

			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				VALUES(@ControlID, 20,'PortNumber','16','端口数量');

		end
end

--添加“NetDevice”
set @AddControl='ViewCallout'
select @ControlID=ControlID from t_control where controlName=@AddControl
if @ControlID >1
begin
	delete t_control where ControlID=@ControlID
	delete [t_ControlProperty] where ControlID=@ControlID
end

select @ControlNum=count(*) from t_control where controlname=@AddControl
if @ControlNum = 0
begin
		print '添加ViewCallout'
		print @AddControl
		insert into t_control (controlname,controltype,imageUrl,controltypeName,controlCaption)
		values(@AddControl,'10','SubNet.jpg','ItMonitor','云图');


		set @ControlID=0;
		select @ControlID=max(controlid)  from t_control
		if @ControlID > 0
		begin
			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 1,'Radius','20','边框圆角半径');

			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 1,'BorderColor','#FFF2C4C4','边框颜色');

			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 1,'BorderSize','1','边框线粗细');
		end
end 
