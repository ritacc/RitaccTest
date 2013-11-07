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
using System.ServiceModel.DomainServices.Client;
using MonitorSystem.Web.Moldes;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace MonitorSystem.ItMonitor
{
    public partial class PropertyView : UserControl
    {
        public PropertyView()
        {
            InitializeComponent();
            ListChanns = new ObservableCollection<t_Channel>();
            this.DataContext = this;
        }

        private ObservableCollection<t_Channel> _ListChanns;
        public ObservableCollection<t_Channel> ListChanns
        {
            get { return _ListChanns; }
            set { _ListChanns = value; RaisePropertyChanged("Devices"); }
        }

       // public ObservableCollection<t_Channel>  { get; set; }

        string LastItems = string.Empty;
        int DeviceID = -1;
        public void View(string properts, int mDeviceID)
        {
                LastItems = properts;

                EntityQuery<t_Channel> v = LoadScreen._DataContext.GetMonitorSelectChannelQuery(properts, mDeviceID);
                LoadScreen._DataContext.Load(v, ValueLoadComplete, null);
                DeviceID = mDeviceID;

                this.Height = 25;
                ListChanns.Clear();
        }

        public void ValueLoadComplete(LoadOperation<t_Channel> result)
        {
            if (result.HasError)
                return;
            ListChanns.Clear();
            foreach (t_Channel obj in result.Entities)
            {
                //if (obj.DeviceID != DeviceID)
                //    return;
                ListChanns.Add(obj);
            }
            if (ListChanns.Count < 10)
            {
                this.Height = ListChanns.Count * 25+ 25;
            }
            else
            {
                this.Height = 275;
            }
            this.DataContext = this;
            if (ListChanns.Count == 0)
            {

            }
        }


        private void RaisePropertyChanged(string propertyName)
        {
            if (null != PropertyChanged)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

    }
}
