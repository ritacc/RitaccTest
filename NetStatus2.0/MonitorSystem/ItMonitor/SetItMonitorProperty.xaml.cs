using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using MonitorSystem.Web.Servers;
using System.Collections.ObjectModel;
using MonitorSystem.Web.Moldes;
using System.ServiceModel.DomainServices.Client;
using System.ComponentModel;

namespace MonitorSystem.ItMonitor
{
    public partial class SetItMonitorProperty : ChildWindow
    {
        public SetItMonitorProperty()
        {
            InitializeComponent();
            _DataContext = LoadScreen._DataContext;
            
            Devices = new ObservableCollection<t_Device>(_DataContext.t_Devices);
            Channels = new ObservableCollection<t_Channel>();
            PropertyS = new ObservableCollection<t_SelectProperty>();

            cbDeviceID.DisplayMemberPath = "DeviceName";
            //查询
            this.DataContext = this;
        }

        #region 属性
        private int _DeviceID = 0;
        /// <summary>
        /// 设备ID
        /// </summary>
        public int DeviceID
        {
            get { return _DeviceID; }
            set { _DeviceID = value; }
        }

        private int _ChanncelID = 0;
        /// <summary>
        /// 通道ID
        /// </summary>
        public int ChanncelID
        {
            get { return _ChanncelID; }
            set { _ChanncelID = value; }
        }


        


        private bool _IsOK = false;
        /// <summary>
        /// 是否点击的OK按钮
        /// </summary>
        public bool IsOK
        {
            get { return _IsOK; }
        }
        #endregion
        MonitorServers _DataContext = new MonitorServers();


        private ObservableCollection<t_SelectProperty> _PropertyS;
        /// <summary>
        /// 设备所有属性
        /// </summary>
        public ObservableCollection<t_SelectProperty> PropertyS
        {
            get { return _PropertyS; }
            set { _PropertyS = value; RaisePropertyChanged("PropertyS"); }
        }

        private ObservableCollection<t_Device> _devices;
        public ObservableCollection<t_Device> Devices
        {
            get{return _devices;}
            set{_devices = value;RaisePropertyChanged("Devices");}
        }

        private t_Device _selectedDevices;
        public t_Device SelectedDevices
        {
            get{return _selectedDevices;}
            set{_selectedDevices = value;RaisePropertyChanged("SelectedDevices");}
        }

        private ObservableCollection<t_Channel> _channels;
        public ObservableCollection<t_Channel> Channels
        {
            get{return _channels;}
            set{_channels = value;RaisePropertyChanged("Channels");}
        }

        /// <summary>
        /// 选择了的属性项     使用#号分隔
        /// </summary>
        public string PropertyItems { get; set; }

        public void Init()
        {   
            var v = _devices.FirstOrDefault(a => a.DeviceID == _DeviceID);
            if (null != v)
            {
                SelectedDevices = v;
            }
        }

        private void cbDeviceID_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            t_Device d=(t_Device)cbDeviceID.Items[cbDeviceID.SelectedIndex];
            LoadChanncel(d.DeviceID);
        }

        private void LoadChanncel(int deviceid)
        {
            _DataContext.Load(_DataContext.GetT_ChannelQuery().Where(a => a.DeviceID == deviceid),
                LoadChanncelCommplete, deviceid);
            Channels.Clear();
            PropertyS.Clear();
        }

        private void LoadChanncelCommplete(LoadOperation<t_Channel> result)
        {
            if (result.HasError)
            {
                MessageBox.Show(result.Error.Message, "出错啦！", MessageBoxButton.OK);
                return;
            }
            var v = result.Entities;
            if (v.Count() > 0)
            {
                foreach (var obj in v)
                {
                    Channels.Add(obj);

                    var pro = new t_SelectProperty();
                    pro.ChannelNO= obj.ChannelNo;
                    pro.ChannelName= obj.ChannelName;
                    pro.ISSelect=false;
                    PropertyS.Add(pro);
                }
                //选中通道
                var vc = v.Where(a => a.ChannelNo == _ChanncelID);
                if (vc.Count() > 0)
                {
                    cbChanncel.SelectedItem = vc.First();
                }
                //选择中，鼠标提示，属性
                SelectPropertyItems();
            }
        }

        #region 提示属性，选中 和组合
        public void SelectPropertyItems()
        {
            if (string.IsNullOrEmpty(PropertyItems))
                return;
            string[] strArr=PropertyItems.Split('#');
            if(strArr.Length> 0)
            {
                foreach (string str in strArr)
                {
                    if(string.IsNullOrEmpty(str))continue;
                    int ChanncelNo = Convert.ToInt32(str);
                    foreach (var obj in PropertyS)
                    {
                        if (obj.ChannelNO == ChanncelNo)
                        {
                            obj.ISSelect = true;
                            break;
                        }
                    }
                }
            }
            gvPropertys.ItemsSource = PropertyS;
            this.DataContext = this;
        }
        /// <summary>
        /// 获取选中项
        /// </summary>
        /// <returns></returns>
        private string GetSelectProperty()
        {
            string result = "";
            foreach (var obj in PropertyS)
            {
                if (obj.ISSelect)
                {
                    if (string.IsNullOrEmpty(result))
                        result = obj.ChannelNO.ToString();
                    else
                        result += "#" + obj.ChannelNO.ToString();
                }
            }
            return result;

        }
        #endregion
        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            _IsOK = true;
            if (cbDeviceID.SelectedValue == null)
            {
                MessageBox.Show("请选择设备！", "温馨提示！", MessageBoxButton.OK);
                return;
            }
            if (cbChanncel.SelectedValue == null)
            {

                MessageBox.Show("请选择通道！", "温馨提示！", MessageBoxButton.OK);
                return;
            }

            _DeviceID = ((t_Device)cbDeviceID.SelectedValue).DeviceID;
            _ChanncelID = ((t_Channel)cbChanncel.SelectedValue).ChannelNo;
            PropertyItems = GetSelectProperty();
            this.DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            _IsOK = false;
            this.DialogResult = false;
        }

    
        private void RaisePropertyChanged(string propertyName)
        {
            if(null != PropertyChanged)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangedEventHandler  PropertyChanged;
    }

    //public class SelectProperty
    //{
    //    public int ChannelNO { get; set; }

    //    public string ChannelName { get; set; }

    //    private bool _ISSelect;
    //    public bool ISSelect { get { return _ISSelect; }
    //        set { _ISSelect = value; RaisePropertyChanged("ISSelect"); } 
    //    }

    //    private void RaisePropertyChanged(string propertyName)
    //    {
    //        if (null != PropertyChanged)
    //        {
    //            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
    //        }
    //    }

    //    public event PropertyChangedEventHandler PropertyChanged;
    //}
}

