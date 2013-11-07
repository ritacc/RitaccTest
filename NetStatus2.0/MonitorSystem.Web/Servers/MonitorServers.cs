
namespace MonitorSystem.Web.Servers
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Data;
    using System.Linq;
    using System.ServiceModel.DomainServices.EntityFramework;
    using System.ServiceModel.DomainServices.Hosting;
    using System.ServiceModel.DomainServices.Server;
    using MonitorSystem.Web.Moldes;


    // 使用 MS 上下文实现应用程序逻辑。
    // TODO: 将应用程序逻辑添加到这些方法中或其他方法中。
    // TODO: 连接身份验证(Windows/ASP.NET Forms)并取消注释以下内容，以禁用匿名访问
    //还可考虑添加角色，以根据需要限制访问。
    // [RequiresAuthentication]
    [EnableClientAccess()]
    public partial class MonitorServers : LinqToEntitiesDomainService<MS>
    {

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“t_Channel”查询添加顺序。
        public IQueryable<t_Channel> GetT_Channel()
        {
            return this.ObjectContext.t_Channel;
        }

        public void InsertT_Channel(t_Channel t_Channel)
        {
            if ((t_Channel.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(t_Channel, EntityState.Added);
            }
            else
            {
                this.ObjectContext.t_Channel.AddObject(t_Channel);
            }
        }

        public void UpdateT_Channel(t_Channel currentt_Channel)
        {
            this.ObjectContext.t_Channel.AttachAsModified(currentt_Channel, this.ChangeSet.GetOriginal(currentt_Channel));
        }

        public void DeleteT_Channel(t_Channel t_Channel)
        {
            if ((t_Channel.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(t_Channel, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.t_Channel.Attach(t_Channel);
                this.ObjectContext.t_Channel.DeleteObject(t_Channel);
            }
        }

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“t_Control”查询添加顺序。
        public IQueryable<t_Control> GetT_Control()
        {
            return this.ObjectContext.t_Control;
        }

        public void InsertT_Control(t_Control t_Control)
        {
            if ((t_Control.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(t_Control, EntityState.Added);
            }
            else
            {
                this.ObjectContext.t_Control.AddObject(t_Control);
            }
        }

        public void UpdateT_Control(t_Control currentt_Control)
        {
            this.ObjectContext.t_Control.AttachAsModified(currentt_Control, this.ChangeSet.GetOriginal(currentt_Control));
        }

        public void DeleteT_Control(t_Control t_Control)
        {
            if ((t_Control.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(t_Control, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.t_Control.Attach(t_Control);
                this.ObjectContext.t_Control.DeleteObject(t_Control);
            }
        }

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“t_ControlProperty”查询添加顺序。
        public IQueryable<t_ControlProperty> GetT_ControlProperty()
        {
            return this.ObjectContext.t_ControlProperty;
        }

        public void InsertT_ControlProperty(t_ControlProperty t_ControlProperty)
        {
            if ((t_ControlProperty.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(t_ControlProperty, EntityState.Added);
            }
            else
            {
                this.ObjectContext.t_ControlProperty.AddObject(t_ControlProperty);
            }
        }

        public void UpdateT_ControlProperty(t_ControlProperty currentt_ControlProperty)
        {
            this.ObjectContext.t_ControlProperty.AttachAsModified(currentt_ControlProperty, this.ChangeSet.GetOriginal(currentt_ControlProperty));
        }

        public void DeleteT_ControlProperty(t_ControlProperty t_ControlProperty)
        {
            if ((t_ControlProperty.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(t_ControlProperty, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.t_ControlProperty.Attach(t_ControlProperty);
                this.ObjectContext.t_ControlProperty.DeleteObject(t_ControlProperty);
            }
        }

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“t_Device”查询添加顺序。
        public IQueryable<t_Device> GetT_Device()
        {
            return this.ObjectContext.t_Device;
        }

        public void InsertT_Device(t_Device t_Device)
        {
            if ((t_Device.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(t_Device, EntityState.Added);
            }
            else
            {
                this.ObjectContext.t_Device.AddObject(t_Device);
            }
        }

        public void UpdateT_Device(t_Device currentt_Device)
        {
            this.ObjectContext.t_Device.AttachAsModified(currentt_Device, this.ChangeSet.GetOriginal(currentt_Device));
        }

        public void DeleteT_Device(t_Device t_Device)
        {
            if ((t_Device.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(t_Device, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.t_Device.Attach(t_Device);
                this.ObjectContext.t_Device.DeleteObject(t_Device);
            }
        }

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“t_Element”查询添加顺序。
        public IQueryable<t_Element> GetT_Element()
        {
            return this.ObjectContext.t_Element;
        }

        public void InsertT_Element(t_Element t_Element)
        {
            if ((t_Element.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(t_Element, EntityState.Added);
            }
            else
            {
                this.ObjectContext.t_Element.AddObject(t_Element);
            }
        }

        public void UpdateT_Element(t_Element currentt_Element)
        {
            this.ObjectContext.t_Element.AttachAsModified(currentt_Element, this.ChangeSet.GetOriginal(currentt_Element));
        }

        public void DeleteT_Element(t_Element t_Element)
        {
            if ((t_Element.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(t_Element, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.t_Element.Attach(t_Element);
                this.ObjectContext.t_Element.DeleteObject(t_Element);
            }
        }

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“t_Element_RealTimeLine”查询添加顺序。
        public IQueryable<t_Element_RealTimeLine> GetT_Element_RealTimeLine()
        {
            return this.ObjectContext.t_Element_RealTimeLine;
        }

        public void InsertT_Element_RealTimeLine(t_Element_RealTimeLine t_Element_RealTimeLine)
        {
            if ((t_Element_RealTimeLine.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(t_Element_RealTimeLine, EntityState.Added);
            }
            else
            {
                this.ObjectContext.t_Element_RealTimeLine.AddObject(t_Element_RealTimeLine);
            }
        }

        public void UpdateT_Element_RealTimeLine(t_Element_RealTimeLine currentt_Element_RealTimeLine)
        {
            this.ObjectContext.t_Element_RealTimeLine.AttachAsModified(currentt_Element_RealTimeLine, this.ChangeSet.GetOriginal(currentt_Element_RealTimeLine));
        }

        public void DeleteT_Element_RealTimeLine(t_Element_RealTimeLine t_Element_RealTimeLine)
        {
            if ((t_Element_RealTimeLine.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(t_Element_RealTimeLine, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.t_Element_RealTimeLine.Attach(t_Element_RealTimeLine);
                this.ObjectContext.t_Element_RealTimeLine.DeleteObject(t_Element_RealTimeLine);
            }
        }

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“t_ElementProperty”查询添加顺序。
        public IQueryable<t_ElementProperty> GetT_ElementProperty()
        {
            return this.ObjectContext.t_ElementProperty;
        }

        public void InsertT_ElementProperty(t_ElementProperty t_ElementProperty)
        {
            if ((t_ElementProperty.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(t_ElementProperty, EntityState.Added);
            }
            else
            {
                this.ObjectContext.t_ElementProperty.AddObject(t_ElementProperty);
            }
        }

        public void UpdateT_ElementProperty(t_ElementProperty currentt_ElementProperty)
        {
            this.ObjectContext.t_ElementProperty.AttachAsModified(currentt_ElementProperty, this.ChangeSet.GetOriginal(currentt_ElementProperty));
        }

        public void DeleteT_ElementProperty(t_ElementProperty t_ElementProperty)
        {
            if ((t_ElementProperty.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(t_ElementProperty, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.t_ElementProperty.Attach(t_ElementProperty);
                this.ObjectContext.t_ElementProperty.DeleteObject(t_ElementProperty);
            }
        }

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“t_GalleryClassification”查询添加顺序。
        public IQueryable<t_GalleryClassification> GetT_GalleryClassification()
        {
            return this.ObjectContext.t_GalleryClassification;
        }

        public void InsertT_GalleryClassification(t_GalleryClassification t_GalleryClassification)
        {
            if ((t_GalleryClassification.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(t_GalleryClassification, EntityState.Added);
            }
            else
            {
                this.ObjectContext.t_GalleryClassification.AddObject(t_GalleryClassification);
            }
        }

        public void UpdateT_GalleryClassification(t_GalleryClassification currentt_GalleryClassification)
        {
            this.ObjectContext.t_GalleryClassification.AttachAsModified(currentt_GalleryClassification, this.ChangeSet.GetOriginal(currentt_GalleryClassification));
        }

        public void DeleteT_GalleryClassification(t_GalleryClassification t_GalleryClassification)
        {
            if ((t_GalleryClassification.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(t_GalleryClassification, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.t_GalleryClassification.Attach(t_GalleryClassification);
                this.ObjectContext.t_GalleryClassification.DeleteObject(t_GalleryClassification);
            }
        }

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“t_MonitorSystemParam”查询添加顺序。
        public IQueryable<t_MonitorSystemParam> GetT_MonitorSystemParam()
        {
            return this.ObjectContext.t_MonitorSystemParam;
        }

        public void InsertT_MonitorSystemParam(t_MonitorSystemParam t_MonitorSystemParam)
        {
            if ((t_MonitorSystemParam.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(t_MonitorSystemParam, EntityState.Added);
            }
            else
            {
                this.ObjectContext.t_MonitorSystemParam.AddObject(t_MonitorSystemParam);
            }
        }

        public void UpdateT_MonitorSystemParam(t_MonitorSystemParam currentt_MonitorSystemParam)
        {
            this.ObjectContext.t_MonitorSystemParam.AttachAsModified(currentt_MonitorSystemParam, this.ChangeSet.GetOriginal(currentt_MonitorSystemParam));
        }

        public void DeleteT_MonitorSystemParam(t_MonitorSystemParam t_MonitorSystemParam)
        {
            if ((t_MonitorSystemParam.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(t_MonitorSystemParam, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.t_MonitorSystemParam.Attach(t_MonitorSystemParam);
                this.ObjectContext.t_MonitorSystemParam.DeleteObject(t_MonitorSystemParam);
            }
        }

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“t_Screen”查询添加顺序。
        public IQueryable<t_Screen> GetT_Screen()
        {
            return this.ObjectContext.t_Screen;
        }

        public void InsertT_Screen(t_Screen t_Screen)
        {
            if ((t_Screen.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(t_Screen, EntityState.Added);
            }
            else
            {
                this.ObjectContext.t_Screen.AddObject(t_Screen);
            }
        }

        public void UpdateT_Screen(t_Screen currentt_Screen)
        {
            this.ObjectContext.t_Screen.AttachAsModified(currentt_Screen, this.ChangeSet.GetOriginal(currentt_Screen));
        }

        public void DeleteT_Screen(t_Screen t_Screen)
        {
            if ((t_Screen.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(t_Screen, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.t_Screen.Attach(t_Screen);
                this.ObjectContext.t_Screen.DeleteObject(t_Screen);
            }
        }

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“t_SelectProperty”查询添加顺序。
        public IQueryable<t_SelectProperty> GetT_SelectProperty()
        {
            return this.ObjectContext.t_SelectProperty;
        }

        public void InsertT_SelectProperty(t_SelectProperty t_SelectProperty)
        {
            if ((t_SelectProperty.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(t_SelectProperty, EntityState.Added);
            }
            else
            {
                this.ObjectContext.t_SelectProperty.AddObject(t_SelectProperty);
            }
        }

        public void UpdateT_SelectProperty(t_SelectProperty currentt_SelectProperty)
        {
            this.ObjectContext.t_SelectProperty.AttachAsModified(currentt_SelectProperty, this.ChangeSet.GetOriginal(currentt_SelectProperty));
        }

        public void DeleteT_SelectProperty(t_SelectProperty t_SelectProperty)
        {
            if ((t_SelectProperty.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(t_SelectProperty, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.t_SelectProperty.Attach(t_SelectProperty);
                this.ObjectContext.t_SelectProperty.DeleteObject(t_SelectProperty);
            }
        }

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“t_Station”查询添加顺序。
        public IQueryable<t_Station> GetT_Station()
        {
            return this.ObjectContext.t_Station;
        }

        public void InsertT_Station(t_Station t_Station)
        {
            if ((t_Station.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(t_Station, EntityState.Added);
            }
            else
            {
                this.ObjectContext.t_Station.AddObject(t_Station);
            }
        }

        public void UpdateT_Station(t_Station currentt_Station)
        {
            this.ObjectContext.t_Station.AttachAsModified(currentt_Station, this.ChangeSet.GetOriginal(currentt_Station));
        }

        public void DeleteT_Station(t_Station t_Station)
        {
            if ((t_Station.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(t_Station, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.t_Station.Attach(t_Station);
                this.ObjectContext.t_Station.DeleteObject(t_Station);
            }
        }

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“t_Sys_MainRealTimeSet”查询添加顺序。
        public IQueryable<t_Sys_MainRealTimeSet> GetT_Sys_MainRealTimeSet()
        {
            return this.ObjectContext.t_Sys_MainRealTimeSet;
        }

        public void InsertT_Sys_MainRealTimeSet(t_Sys_MainRealTimeSet t_Sys_MainRealTimeSet)
        {
            if ((t_Sys_MainRealTimeSet.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(t_Sys_MainRealTimeSet, EntityState.Added);
            }
            else
            {
                this.ObjectContext.t_Sys_MainRealTimeSet.AddObject(t_Sys_MainRealTimeSet);
            }
        }

        public void UpdateT_Sys_MainRealTimeSet(t_Sys_MainRealTimeSet currentt_Sys_MainRealTimeSet)
        {
            this.ObjectContext.t_Sys_MainRealTimeSet.AttachAsModified(currentt_Sys_MainRealTimeSet, this.ChangeSet.GetOriginal(currentt_Sys_MainRealTimeSet));
        }

        public void DeleteT_Sys_MainRealTimeSet(t_Sys_MainRealTimeSet t_Sys_MainRealTimeSet)
        {
            if ((t_Sys_MainRealTimeSet.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(t_Sys_MainRealTimeSet, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.t_Sys_MainRealTimeSet.Attach(t_Sys_MainRealTimeSet);
                this.ObjectContext.t_Sys_MainRealTimeSet.DeleteObject(t_Sys_MainRealTimeSet);
            }
        }

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“t_TmpValue”查询添加顺序。
        public IQueryable<t_TmpValue> GetT_TmpValue()
        {
            return this.ObjectContext.t_TmpValue;
        }

        public void InsertT_TmpValue(t_TmpValue t_TmpValue)
        {
            if ((t_TmpValue.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(t_TmpValue, EntityState.Added);
            }
            else
            {
                this.ObjectContext.t_TmpValue.AddObject(t_TmpValue);
            }
        }

        public void UpdateT_TmpValue(t_TmpValue currentt_TmpValue)
        {
            this.ObjectContext.t_TmpValue.AttachAsModified(currentt_TmpValue, this.ChangeSet.GetOriginal(currentt_TmpValue));
        }

        public void DeleteT_TmpValue(t_TmpValue t_TmpValue)
        {
            if ((t_TmpValue.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(t_TmpValue, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.t_TmpValue.Attach(t_TmpValue);
                this.ObjectContext.t_TmpValue.DeleteObject(t_TmpValue);
            }
        }

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“V_ScreenMonitorValue”查询添加顺序。
        public IQueryable<V_ScreenMonitorValue> GetV_ScreenMonitorValue()
        {
            return this.ObjectContext.V_ScreenMonitorValue;
        }

        public void InsertV_ScreenMonitorValue(V_ScreenMonitorValue v_ScreenMonitorValue)
        {
            if ((v_ScreenMonitorValue.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(v_ScreenMonitorValue, EntityState.Added);
            }
            else
            {
                this.ObjectContext.V_ScreenMonitorValue.AddObject(v_ScreenMonitorValue);
            }
        }

        public void UpdateV_ScreenMonitorValue(V_ScreenMonitorValue currentV_ScreenMonitorValue)
        {
            this.ObjectContext.V_ScreenMonitorValue.AttachAsModified(currentV_ScreenMonitorValue, this.ChangeSet.GetOriginal(currentV_ScreenMonitorValue));
        }

        public void DeleteV_ScreenMonitorValue(V_ScreenMonitorValue v_ScreenMonitorValue)
        {
            if ((v_ScreenMonitorValue.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(v_ScreenMonitorValue, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.V_ScreenMonitorValue.Attach(v_ScreenMonitorValue);
                this.ObjectContext.V_ScreenMonitorValue.DeleteObject(v_ScreenMonitorValue);
            }
        }
    }
}


