
function CreateMenuItems(m) {
    var item0, item1;

    item0 = m.Item('系统及权限');
    item0.Link('系统讯息-SYF09010', '', false);
    item0.Link('审计查询-SYF09020', 'SystemLog.htm', false);
    item0.Link('店管理-SYF01010', 'StoreManagement.htm', false);
    item0.Link('用户管理-SYF02010', 'UserManagement.htm', false);
    item0.Link('角色管理-SYF02020', 'RolesManagement.htm', false);
    item0.Link('密码变更-SYF02030', 'PasswordChange.htm', false);
    item0.Link('同步会员号码-SYF09040', '', false);
    item0.Link('车辆系列维护-SYF03090', 'MaintenanceVehicleSeries.htm', false);
    item0.Link('参数管理-SYF03010', 'ParameterManagement.htm', false);
    item0.Link('代码管理下载-SYF03025', '', false);
    item0.Link('代码管理-SYF03020 (车主会)', '', false);
    item0.Link('代码管理-SYF03020', 'CodeManagement.htm', false);
    item0.Link('假日管理-SYF03030', 'HolidaySet.htm', false);
    item0.Link('省管理-SYF03040', 'ProvinceCityArea.htm', false);
    item0.Link('市管理-SYF03051', 'ProvinceCityArea.htm', false);
    item0.Link('区管理-SYF03052', 'ProvinceCityArea.htm', false);
    item0.Link('招揽/原因管理-SYF03080', '', false);
    item0.Link('省/市/区下载-SYF03055', '', false);
    item0.Link('车辆型号维护-SYF03060', 'MaintenanceVehicleModel.htm', false);
    item0.Link('功能管理-SYF03070', '', false);
//    item0.Link('功能管理-SYF03070', 'FunctionManagement.htm', false);
    item0.Link('电话号码前缀管理-SYF02040', 'PhonePrefixManagement.htm', false);
    item0.Link('电话号码前缀对照表-SYF02050', 'PhonePrefixComparison.htm', false);

    item0 = m.Item('工作台');
    item0.Link('招揽控制台', 'RecruitmentPool.htm', false);
    item0.Link('招揽设定-WPF02010', 'CustomerCanvassIndex.htm', false);
    item0.Link('招揽分配-WPF02050', 'CanvassAssign.htm', false);
    item0.Link('招揽设定(商用车)-WPF02015', '', false);
    item0.Link('招揽的业务日期设置-WPF02040', 'CanvassBussDate.htm', false);
    item0.Link('工作台-WPF01010', 'Workspace.html', false);
    item0.Link('招揽历史查询-WPF02020', '', false);
    item0.Link('联络历史查询-WPF03010', '', false);
    item0.Link('高级招揽-WPF02030', 'CustomerSeniorCanvass.htm', false);
    item0.Link('WPF01110-Socket Testing Form 1', '', false);
    item0.Link('WPF01120-Socket Testing Form 1', '', false);
    item0.Link('CTI-Simulation-WPF01015', '', false);
    item0.Link('Maintain SMS Msg-WPF03030', '', false);
    item0.Link('规则设定', 'BussRuleSet.htm', false);

    item0 = m.Item('客户及车辆');
    item0.Link('客户管理(个人)-CVF01010', 'PersonalCustomerManagementIndex.htm', false);
    item0.Link('客户管理(公司)-CVF01010', 'CompanyCustomerManagementIndex.htm', false);
    item0.Link('车队管理-CVF02020', 'FleetManagementIndex.htm', false);
    item0.Link('CVF01015-客戶管理(商用车)', '', false);
    item0.Link('車輛管理-CVF02010', 'VehicleManagementIndex.htm', false);
    item0.Link('查找客戶-CVF03010', '', false);
    item0.Link('保有客戶分析报表-CVR09010', '', false);
    item0.Link('客户分类报表-CVR09020', '', false);
    item0.Link('客户流失报警-CVR09110', '', false);
    item0.Link('客户流失分析-CVR09120', '', false);
    item0.Link('ABCD 类型-CVF05010', 'ABCDType.htm', false);

    item0 = m.Item('服务');
    item0.Link('座席招揽分析报表-SCR01010', '', false);
    item0.Link('保养招揽成功分析报表-SCR02010', '', false);
    item0.Link('招揽失败原因分析报表-SCR03010', '', false);
    item0.Link('招揽分析报表-SCR04010', '', false);
    item0.Link('区域季审模式管理-SCF01020', 'SeasonReviewModeSet.htm', false);
    item0.Link('保养设定-SCF01010', 'MaintainSet.htm', false);

    item0 = m.Item('预约');
    item0.Link('预约-APF01010', 'BespeakIndex.htm', false);

    item0 = m.Item('保险');
    item0.Link('保险公司-INF01010', '', false);
    item0.Link('保险夹-INF02010', '', false);
    item0.Link('INR01010-Download Insurance detail by ve', '', false);
    item0.Link('INR01020-Insurance outbound call analysi', '', false);
    item0.Link('INR01030-Insurance renewal analysis repo', '', false);

    item0 = m.Item('投诉');
    item0.Link('投诉-CPF01010', 'ComplaintsManagementIndex.htm', false);
    item0.Link('投诉表-CPR01010', '', false);
    item0.Link('投诉分析报表-CPR02010', '', false);
    item0.Link('导出投诉分析-CPR03020', '', false);
    item0.Link('导出投诉明细-CPR03010', '', false);

    item0 = m.Item('调查');
    item0.Link('调查设定-SVF01010', 'SurveySetIndex.htm', false);
    item0.Link('进行调查-SVF02010', '', false);
    item0.Link('问卷调查(即时)-SVF02050', '', false);
    item0.Link('调查答案设定-SVF03010', '', false);
    item0.Link('问卷统计-SVR02010', '', false);
    item0.Link('汇出调查答案-SVR01010', '', false);

    item0 = m.Item('DMS介面');
    item0.Link('UIF02010-Data Capture of Customer, Vehic', '', false);
    item0.Link('UIF02020-Data Capture of Customer, Ver. ', '', false);
    item0.Link('UIF02020-Data Capture of Customer, Vehic', '', false);
    item0.Link('UIF03010-Enquire Import History', '', false);
    item0.Link('UIF03020-对照表批量上传/下载', '', false);
    item0.Link('UIF02040-Data Capture of Spending', '', false);
    item0.Link('UIF05010-会员资料消费积分上传', '', false);

    item0 = m.Item('车主会');
    item1 = item0.Item('车主会客户信息管理');
    item1.Link('车主会客户管理-MCF01010', '', false);
    item1.Link('车主会会员管理-MCF01020', '', false);
    item1.Link('特别会籍管理-MCF01030', '', false);
    item1.Link('车主会会员分析-MCF04120', '', false);
    item1.Link('会员卡模板设定-MCF06100', '', false);
    item1.Link('促销信息设定-MCF06110', '', false);
    item1.Link('会员卡打印信息-MCF06120', '', false);
    item0.Link('批处理工作管理-SYF99010', '', false);
    item0.Link('批处理工作执行-SYF99020', '', false);
    item1 = item0.Item('车主会积分管理');
    item1.Link('积分项目管理-MCF03010', '', false);
    item1.Link('消费积分管理-MCF03020', '', false);
    item1.Link('消费积分审核-MCF03030', '', false);
    item1.Link('会员积分调整管理-MCF03040', '', false);
    item1.Link('会员积分历史查询-MCF04010', '', false);
    item1.Link('会员积分调整查询-MCF04240', '', false);
    item1.Link('消费积分查询报表-MCF04250', '', false);
    item1.Link('车主会积分摘要-MCF04020', '', false);
    item1.Link('消费积分管理报表-MCF04260', '', false);
    item1 = item0.Item('车主会积分兑换管理');
    item1.Link('礼品管理-MCF02010', '', false);
    item1.Link('礼品换领地点管理-MCF02020', '', false);
    item1.Link('礼品兑换申请管理-MCF02030', '', false);
    item1.Link('礼品兑换申请分析-MCF04030', '', false);
    item1.Link('礼品兑换结算-MCF02040', '', false);
    item1.Link('礼品兑换结算分析-MCF04040', '', false);
    item1.Link('地区管理-MCF05010', '', false);
    item1.Link('服务项目管理-MCF06020', '', false);
    item1.Link('现金券管理-MCF06010', '', false);
    item1.Link('使用现金券管理 -MCF06040', '', false);
    item1.Link('套餐管理-MCF06030', '', false);
    item1.Link('使用套餐管理-MCF06050', '', false);

    item0 = m.Item('车主会报表及数据下载');
    item1 = item0.Item('车主会报表');
    item1.Link('到期会籍报表-MCR01050', '', false);
    item1.Link('打印会员申请表-MCR01060', '', false);
    item1.Link('积分审核记录报表-MCR01010', '', false);
    item1.Link('会员积分调整明细报表-MCR01110', '', false);
    item1.Link('打印会员积分概览-MCR01020', '', false);
    item1.Link('打印礼品兑换通知信-MCR01080', '', false);
    item1.Link('礼品兑换状态报表-MCR01090', '', false);
    item1 = item0.Item('车主会数据下载');
    item1.Link('下载会籍资料明细-MCR09120', '', false);
    item1.Link('下载会籍消费统计报表-MCR09180', '', false);
    item1.Link('下载续期会籍明细-MCR01100', '', false);
    item1.Link('下载会员卡修改信息-MCR09220', '', false);
    item1.Link('下载绩分对换报表(店)-MCR09250', '', false);

}

