declare @ControlNum int;--��ѯ�ؼ������������ж��Ƿ��Ѿ����
set @ControlNum=0;
declare @AddControl varchar(50);--��ӿؼ�����

--��ӡ�NetLine��
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
		print '��ӿؼ�'
		print @AddControl
		insert into t_control (controlname,controltype,imageUrl,controltypeName,controlCaption)
		values(@AddControl,'10','FoldLine.jpg','ItMonitor','������');
		 
		set @ControlID=0;
		select @ControlID=max(controlid)  from t_control
		if @ControlID > 0
		begin
			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 1,'StrokeThickness','1.5','���ߴ�ϸ');
			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 2,'Stroke','#5694BA','����ɫ');
			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 3,'PointsSave','','λ�ü�');

			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 4,'Paser1','','���ʽ1');
			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 5,'ShowColor1','#FFC6EBC2','��ɫ1');
			
			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 6,'Paser2','','���ʽ2');
			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 7,'ShowColor2','#FF3EB74D','��ɫ2');

			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 8,'Paser3','','���ʽ3');
			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 9,'ShowColor3','#FF1DE5DA','��ɫ3');

			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 10,'Paser4','','���ʽ4');
			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 11,'ShowColor4','#FF0B80B7','��ɫ4');

			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 12,'Paser5','','���ʽ5');
			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 13,'ShowColor5','#FF2340EB','��ɫ5');

			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 14,'Paser6','','���ʽ6');
			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 15,'ShowColor6','#FFDAE517','��ɫ6');

			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 16,'Paser7','','���ʽ7');
			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 17,'ShowColor7','#FFE52517','��ɫ7');

			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 20,'UpLinePort','','�����豸�˿�');
			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 21,'UpLineDeviceID','','�����豸ID');
			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 22,'DownLinePort','1','�����豸�˿�');
			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 23,'DownLineDeviceID','','�����豸ID');

		end
end

--��ӡ�NetDevice��
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
		print '����豸'
		print @AddControl
		insert into t_control (controlname,controltype,imageUrl,controltypeName,controlCaption)
		values(@AddControl,'10','Server.jpg','ItMonitor','�豸');


		set @ControlID=0;
		select @ControlID=max(controlid)  from t_control
		if @ControlID > 0
		begin			
			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 1,'DeviceName','','�豸����');

			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 2,'Paser1','','���ʽ1');
			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 3,'Img1','','ͼƬ1');
			
			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 4,'Paser2','','���ʽ2');
			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 5,'Img2','','ͼƬ2');

			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 6,'Paser3','','���ʽ3');
			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 7,'Img3','','ͼƬ3');

			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 8,'Paser4','','���ʽ4');
			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 9,'Img4','','ͼƬ4');

			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 10,'Paser5','','���ʽ5');
			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 11,'Img5','','ͼƬ5');

			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 12,'Paser6','','���ʽ6');
			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 13,'Img6','','ͼƬ6');

			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 14,'Paser7','','���ʽ7');
			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 15,'Img7','','ͼƬ7');

			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 16,'DefultImg','route0.png','ͼƬ7');

			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				VALUES(@ControlID, 20,'PortNumber','16','�˿�����');

		end
end

--��ӡ�NetDevice��
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
		print '���ViewCallout'
		print @AddControl
		insert into t_control (controlname,controltype,imageUrl,controltypeName,controlCaption)
		values(@AddControl,'10','SubNet.jpg','ItMonitor','��ͼ');


		set @ControlID=0;
		select @ControlID=max(controlid)  from t_control
		if @ControlID > 0
		begin
			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 1,'Radius','20','�߿�Բ�ǰ뾶');

			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 1,'BorderColor','#FFF2C4C4','�߿���ɫ');

			INSERT INTO [t_ControlProperty]([ControlID],[PropertyNo],[PropertyName],[DefaultValue],[Caption])
				 VALUES(@ControlID, 1,'BorderSize','1','�߿��ߴ�ϸ');
		end
end 
