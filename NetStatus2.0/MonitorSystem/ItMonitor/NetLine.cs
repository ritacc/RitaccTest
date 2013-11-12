using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MonitorSystem.Controls;
 
using MonitorSystem.Web.Moldes;

namespace MonitorSystem.ItMonitor
{
    public partial class NetLine : ItMonitorBase
    {
        Polyline PL = new Polyline();
        Canvas _Canvas = new Canvas();

        Rectangle _connect = new Rectangle() { Fill = new SolidColorBrush(Colors.Red), StrokeThickness = 0, Height = 5.0d, Width = 5.0d, HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center, Visibility = Visibility.Collapsed };

        public NetLine()
        {
            Content = _Canvas;
            _Canvas.Children.Add(PL);
            PL.Stroke = new SolidColorBrush(this.Stroke);
            PL.StrokeThickness = this.StrokeThickness;
            PL.MouseLeftButtonUp += new MouseButtonEventHandler(PL_MouseLeftButtonUp);

            this.SizeChanged += new SizeChangedEventHandler(NetLine_SizeChanged);

            Canvas.SetZIndex(this, 9999);
            _Canvas.Children.Add(_connect);
        }

        void NetLine_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.Width = e.NewSize.Width;
            this.Height = e.NewSize.Height;
        }

        void PL_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            DesignMode();
        }

        #region 与设备关联处理
		public void UpdateDeviceID()
		{
			this.UpLineDevice = _UpNetDevice;
			this.DownLineDevice = _DownNetDevice;
		} 

        public void MovePoint(NetDevice _netDev, double offsetX, double offsetY)
        {
            int index = 0;
            if (_netDev == UpLineDevice)
                index = 0;
            else
                index = PointColl.Count - 1;

            Point newPoint = new Point(PL.Points[index].X + offsetX, PL.Points[index].Y + offsetY);
            PL.Points.RemoveAt(index);
            PL.Points.Insert(index, newPoint);

            PointColl = PL.Points;
            GlobalPoints = PL.Points;
            PointsConvertStr(PL.Points);

            (AdornerLayer as LineAdorner).UpdatePoints(index, offsetX, offsetY);
        }

        public void SetOnDeviceColor()
        {
            PL.Stroke = new SolidColorBrush(Colors.Blue);
        }
        #endregion

        #region 重载
        public override event EventHandler Selected;
        public override event EventHandler Unselected;
       
        protected void OnSelected(object sender, EventArgs e)
        {
            if (null != Selected)
            {
                Selected(this, RoutedEventArgs.Empty);
            }
        }

        public override FrameworkElement GetRootControl()
        {
            return this;
        }



        protected void OnUnselected(object sender, EventArgs e)
        {
            if (null != Unselected)
            {
                Unselected(this, RoutedEventArgs.Empty);
            }
        }


        public override void UnDesignMode()
        {
            if (IsDesignMode)
            {
                AdornerLayer.Selected -= OnSelected;
                AdornerLayer.ClearValue(ContextMenuService.ContextMenuProperty);
                AdornerLayer.Dispose();
                AdornerLayer = null;
            }
        }

        #endregion

        #region SetValue

        public override void SetChannelValue(float fValue)
        {
            SetStringValue(fValue.ToString());
        }

        public override void SetStringValue(string mValue)
        {
            if (string.IsNullOrEmpty(mValue))
                return;
            Color Col = this.Stroke;
            bool IsMatch = false;
            if (!string.IsNullOrEmpty(Paser1))
            {
                if (base.IsMatch(Paser1, mValue))
                {
                    Col = ShowColor1;
                    IsMatch = true;
                }
            }

            if (!string.IsNullOrEmpty(Paser2))
            {
                if (base.IsMatch(Paser2, mValue))
                {
                    Col = ShowColor2;
                    IsMatch = true;
                }
            }

            if (!string.IsNullOrEmpty(Paser3))
            {
                if (base.IsMatch(Paser3, mValue))
                {
                    Col = ShowColor3;
                    IsMatch = true;
                }
            }

            if (!string.IsNullOrEmpty(Paser4))
            {
                if (base.IsMatch(Paser4, mValue))
                {
                    Col = ShowColor4;
                    IsMatch = true;
                }
            }

            if (!string.IsNullOrEmpty(Paser5))
            {
                if (base.IsMatch(Paser5, mValue))
                {
                    Col = ShowColor5;
                    IsMatch = true;
                }
            }
            if (!string.IsNullOrEmpty(Paser6))
            {
                if (base.IsMatch(Paser6, mValue))
                {
                    Col = ShowColor6;
                    IsMatch = true;
                }
            }
            if (!string.IsNullOrEmpty(Paser7))
            {
                if (base.IsMatch(Paser7, mValue))
                {
                    Col = ShowColor7;
                    IsMatch = true;
                }
            }
            if (IsMatch)
                SetPLColor(Col);

        }

        public void SetPLColor(Color color)
        {
            PL.Stroke = new SolidColorBrush(color);
        }
        #endregion

        #region 重载
        public override void SetPropertyValue()
        {
            foreach (t_ElementProperty pro in ListElementProp)
            {
                string name = pro.PropertyName.ToUpper();
                string value = pro.PropertyValue;

                switch (name.ToLower())
                {
                    case "stroke":
                        Stroke = Common.StringToColor(value);
                        break;
                    case "strokethickness":
                        StrokeThickness = Convert.ToDouble(value);
                        break;
                    case "pointssave":
                        StrConverttToPoints(value);
                        break;
                    case "paser1":
                        Paser1 = value;
                        break;
                    case "showcolor1":
                        if (!string.IsNullOrEmpty(value))
                            ShowColor1 = Common.StringToColor(value);
                        break;

                    case "paser2":
                        Paser2 = value;
                        break;
                    case "showcolor2":
                        if (!string.IsNullOrEmpty(value))
                            ShowColor2 = Common.StringToColor(value);
                        break;

                    case "paser3":
                        Paser3 = value;
                        break;
                    case "showcolor3":
                        if (!string.IsNullOrEmpty(value))
                            ShowColor3 = Common.StringToColor(value);
                        break;

                    case "paser4":
                        Paser4 = value;
                        break;
                    case "showcolor4":
                        if (!string.IsNullOrEmpty(value))
                            ShowColor4 = Common.StringToColor(value);
                        break;

                    case "paser5":
                        Paser5 = value;
                        break;
                    case "showcolor5":
                        if (!string.IsNullOrEmpty(value))
                            ShowColor5 = Common.StringToColor(value);
                        break;

                    case "paser6":
                        Paser6 = value;
                        break;
                    case "showcolor6":
                        if (!string.IsNullOrEmpty(value))
                            ShowColor6 = Common.StringToColor(value);
                        break;
					case "paser7":
						Paser7 = value;
						break;
					case "showcolor7":
						if (!string.IsNullOrEmpty(value))
							ShowColor7 = Common.StringToColor(value);
						break;

					case "uplineport":
						if (!string.IsNullOrEmpty(value))
							UpLinePort = Convert.ToInt32(value);
						break;

					case "uplinedeviceid":
						if (!string.IsNullOrEmpty(value))
						{
							UpLineDeviceID = Convert.ToInt32(value);
							UpLineDevice = DeviceLineHeadle.GetDeviceByElementID(UpLineDeviceID.Value);
						}
						break;
					case "downlineport":
						if (!string.IsNullOrEmpty(value))
							DownLinePort = Convert.ToInt32(value);
						break;
					case "downlinedeviceid":
						if (!string.IsNullOrEmpty(value))
						{
							DownLineDeviceID = Convert.ToInt32(value);
							DownLineDevice = DeviceLineHeadle.GetDeviceByElementID(DownLineDeviceID.Value);
						}
						break;

                }			
            }
        }

        public override void SetCommonPropertyValue()
        {
            this.SetValue(Canvas.LeftProperty, (double)ScreenElement.ScreenX);
            this.SetValue(Canvas.TopProperty, (double)ScreenElement.ScreenY);

            this.Width = (double)ScreenElement.Width;
            this.Height = (double)ScreenElement.Height;
        }

        private string[] m_BrowsableProperties = new string[] { "Width", "Height",  "Stroke", "StrokeThickness", "ForeColor" 
        ,"Paser1","ShowColor1"  ,"Paser2","ShowColor2"  ,"Paser3","ShowColor3"  ,"Paser1","ShowColor1"
        ,"Paser5","ShowColor5" ,"Paser6","ShowColor6"   ,"Paser7","ShowColor7"
        ,"UpLinePort","UpLineDeviceName","DownLineDeviceName"	,"DownLinePort"	};
        public override string[] BrowsableProperties
        {
            get
            {
                return m_BrowsableProperties;
            }
            set
            {
                m_BrowsableProperties = value;
            }
        }

        #endregion

        #region  线的点处理

        public void Connection(NetDevice netDevice, Rect rect)
        {
            var center = new Point(rect.Left + rect.Width / 2d - Left, rect.Top + rect.Height / 2d - Top);
            int endIndex = PL.Points.Count - 1;
            var offsetX = 0d;
            var offsetY = 0d;
            if ((this.UpLineDevice == netDevice || this.UpLineDevice == null)
                && this.DownLineDevice != netDevice
                && rect.Contains(new Point(PL.Points[0].X + Left, PL.Points[0].Y + Top)))
            {
                this.UpLineDevice = netDevice;
                offsetX = center.X - PL.Points[0].X;
                offsetY = center.Y - PL.Points[0].Y;
                MovePoint(netDevice, offsetX, offsetY);
            }
            else if ((this.DownLineDevice == netDevice || this.DownLineDevice == null)
                 && this.UpLineDevice != netDevice
                && rect.Contains(new Point(PL.Points[endIndex].X + Left, PL.Points[endIndex].Y + Top)))
            {
                this.DownLineDevice = netDevice;
                offsetX = center.X - PL.Points[endIndex].X;
                offsetY = center.Y - PL.Points[endIndex].Y;
                MovePoint(netDevice, offsetX, offsetY);
            }
        }

        public void UpdateHighlight(Rect rect)
        {
            int endIndex = PL.Points.Count - 1;
            if (null == this.UpLineDevice && rect.Contains(new Point(PL.Points[0].X + Left, PL.Points[0].Y + Top)))
            {
                Canvas.SetLeft(_connect, PL.Points[0].X - _connect.Width / 2d);
                Canvas.SetTop(_connect, PL.Points[0].Y - _connect.Height / 2d);
                _connect.Visibility = Visibility.Visible;
            }
            else if (null == this.DownLineDevice && rect.Contains(new Point(PL.Points[endIndex].X + Left, PL.Points[endIndex].Y + Top)))
            {
                Canvas.SetLeft(_connect, PL.Points[endIndex].X - _connect.Width / 2d);
                Canvas.SetTop(_connect, PL.Points[endIndex].Y - _connect.Height / 2d);
                _connect.Visibility = Visibility.Visible;
            }
            else
            {
                _connect.Visibility = Visibility.Collapsed;
            }
        }

        public void CancelHightlight()
        {
            _connect.Visibility = Visibility.Collapsed;
        }

        private string _PointsSave;
        /// <summary>
        /// 
        /// </summary>
        public string PointsSave
        {
            get { return _PointsSave; }
            set
            {
                _PointsSave = value;
                SetAttrByName("PointsSave", value);
            }
        }
        /// <summary>
        /// 处理过的点，直接用于 PL
        /// </summary>
        public PointCollection PointColl
        {
            get { return PL.Points; }
            set
            {
                PL.Points = value;
            }
        }

        /// <summary>
        /// 将点转换成为字符串
        /// </summary>
        /// <param name="pc"></param>
        private void PointsConvertStr(PointCollection pc)
        {
            if (pc == null)
                return;
            StringBuilder sb = new StringBuilder();
            int index = 0;
            foreach (Point p in pc)
            {
                if (index > 0)
                    sb.Append("|");
                sb.Append(string.Format("{0},{1}", Math.Round(p.X, 3), Math.Round(p.Y, 3)));
                index++;
            }
            PointsSave = sb.ToString();
        }

        private void StrConverttToPoints(string strpoints)
        {
            if (string.IsNullOrEmpty(strpoints))
                return;
            string[] strArr = strpoints.Split('|');

            PointCollection pc = new PointCollection();
            foreach (string str in strArr)
            {
                if (string.IsNullOrEmpty(str))
                    return;
                string[] arrPoint = str.Split(',');
                if (arrPoint.Length == 2)
                {
                    pc.Add(new Point(Convert.ToDouble(arrPoint[0]), Convert.ToDouble(arrPoint[1])));
                }
            }
            PointColl = pc;
            GlobalPoints = pc;
        }

        public void ConvertPoints(PointCollection mpc)
        {
            if (mpc == null)
                return;
            if (mpc.Count == 0)
                return;

            Point firstPoint = mpc[0];

            if (mpc[0].X > mpc[mpc.Count - 1].X)
                firstPoint = mpc[mpc.Count - 1];

            PointCollection pc = new PointCollection();
            for (int i = 0; i < mpc.Count; i++)
            {
                pc.Add(new Point(mpc[i].X - firstPoint.X, mpc[i].Y - firstPoint.Y));
            }
            PL.Points = pc;
            PointsConvertStr(pc);

            this.SetValue(Canvas.LeftProperty, firstPoint.X);
            this.SetValue(Canvas.TopProperty, firstPoint.Y);

            GlobalPoints = mpc;
        }
        #endregion

        #region 属性
        /// <summary>
        /// 线段的绝对坐标
        /// </summary>
        public PointCollection GlobalPoints
        {
            get;
            private set;
        }
        #region 上联设备、端口
        private static readonly DependencyProperty UpLinePortProperty =DependencyProperty.Register("UpLinePort",typeof(int?), typeof(NetLine), new PropertyMetadata(null));
        [DefaultValue(null), Description("上联设备端口"), Category("连接设备")]
        public int? UpLinePort
        {
            get { return (int?)GetValue(UpLinePortProperty); }
            set
            {
                SetValue(UpLinePortProperty, value);
                SetAttrByName("UpLinePort", value);
            }
        }

        private int? _UpLineDeviceID = null;
        public int? UpLineDeviceID
        {
            get { return _UpLineDeviceID; }
            set
            {
                SetAttrByName("UpLineDeviceID", value);
                _UpLineDeviceID = value;
            }
        }

        private NetDevice _UpNetDevice = null;
        public NetDevice UpLineDevice
        {
            get { return _UpNetDevice; }
            set
            {
                _UpNetDevice = value;
                if (value != null)
                {
                    UpLineDeviceID = value.ScreenElement.ElementID;
                    UpLineDeviceName = value.DeviceName;
                }
                else
                {
                    UpLineDeviceID = null;
                    UpLineDeviceName = "";
                }
            }
        }

        private static readonly DependencyProperty UpLineDeviceNameProperty = DependencyProperty.Register("UpLineDeviceName", typeof(string), typeof(NetLine), new PropertyMetadata(""));
        [DefaultValue(""), Description("上联设备名称"), Category("连接设备")]
        public string UpLineDeviceName
        {
            get { return (string)GetValue(UpLineDeviceNameProperty); }
            set
            {
                SetValue(UpLineDeviceNameProperty, value);
            }
        }
        #endregion

        #region 下联设备、端口
        private static readonly DependencyProperty DownLinePortProperty =DependencyProperty.Register("DownLinePort",typeof(int), typeof(NetLine), new PropertyMetadata(null));
        [DefaultValue(null), Description("下联设备端口"), Category("连接设备")]
        public int DownLinePort
        {
            get { return (int)GetValue(DownLinePortProperty); }
            set
            {
                SetValue(DownLinePortProperty, value);
                SetAttrByName("DownLinePort", value);
            }
        }

        private int? _DownLineDeviceID = null;
        public int? DownLineDeviceID
        {
            get { return _DownLineDeviceID; }
            set
            {
                SetAttrByName("DownLineDeviceID", value);
                _DownLineDeviceID = value;
            }
        }
        private NetDevice _DownNetDevice=null;
        public NetDevice DownLineDevice
        {
            get { return _DownNetDevice; }
            set
            {
                _DownNetDevice = value;
                if (value != null)
                {
                    DownLineDeviceID = value.ScreenElement.ElementID;
                    DownLineDeviceName = value.DeviceName;
                }
                else
                {
                    DownLineDeviceID = null;
                    DownLineDeviceName = "";
                }
            }
        }

        private static readonly DependencyProperty DownLineDeviceNameProperty = DependencyProperty.Register("DownLineDeviceName", typeof(string), typeof(NetLine), new PropertyMetadata(""));
        [DefaultValue(""), Description("下联设备名称"), Category("连接设备")]
        public string DownLineDeviceName
        {
            get { return (string)GetValue(DownLineDeviceNameProperty); }
            set
            {
                SetValue(DownLineDeviceNameProperty, value);
            }
        }
        		 
        #endregion

        #region 边线颜色

        private static readonly DependencyProperty StrokeProperty =
           DependencyProperty.Register("Stroke",
           typeof(Color), typeof(NetLine), new PropertyMetadata(Common.StringToColor("#5694BA")));

        [DefaultValue(""), Description("线颜色"), Category("杂项")]
        public Color Stroke
        {
            get { return (Color)GetValue(StrokeProperty); }
            set
            {
                SetValue(StrokeProperty, value);
                SetAttrByName("Stroke", value.ToString());
                PL.Stroke = new SolidColorBrush(value);
            }
        }
        #endregion

        #region 边线粗细
        private static readonly DependencyProperty StrokeThicknessProperty =
           DependencyProperty.Register("StrokeThickness",
           typeof(double), typeof(NetLine), new PropertyMetadata(1.5d));

        [DefaultValue(""), Description("线粗细"), Category("杂项")]
        public double StrokeThickness
        {
            get { return (double)GetValue(StrokeThicknessProperty); }
            set
            {
                SetValue(StrokeThicknessProperty, value);
                SetAttrByName("StrokeThickness", value.ToString());
                PL.StrokeThickness = value;
            }
        }
        #endregion

        #region 表达式、颜色1     ,Paser1,ShowColor1
        private static readonly DependencyProperty Paser1Property =
           DependencyProperty.Register("Paser1",
           typeof(string), typeof(NetLine), new PropertyMetadata(""));

        [DefaultValue(""), Description("表达式"), Category("我的属性")]
        public string Paser1
        {
            get { return (string)GetValue(Paser1Property); }
            set
            {
                SetValue(Paser1Property, value);
                SetAttrByName("Paser1", value);
            }
        }

        private static readonly DependencyProperty ShowColor1Property =
           DependencyProperty.Register("ShowColor1",
           typeof(Color), typeof(NetLine), new PropertyMetadata(Common.StringToColor("#FFC6EBC1")));

        [DefaultValue(""), Description("显示颜色"), Category("我的属性")]
        public Color ShowColor1
        {
            get { return (Color)GetValue(ShowColor1Property); }
            set
            {
                SetValue(ShowColor1Property, value);
                SetAttrByName("ShowColor1", value);
            }
        }
        #endregion

        #region 表达式、颜色2     ,Paser2,ShowColor2
        private static readonly DependencyProperty Paser2Property =
           DependencyProperty.Register("Paser2",
           typeof(string), typeof(NetLine), new PropertyMetadata(""));

        [DefaultValue(""), Description("表达式"), Category("我的属性")]
        public string Paser2
        {
            get { return (string)GetValue(Paser2Property); }
            set
            {
                SetValue(Paser2Property, value);
                SetAttrByName("Paser2", value);
            }
        }

        private static readonly DependencyProperty ShowColor2Property =
           DependencyProperty.Register("ShowColor2",
           typeof(Color), typeof(NetLine), new PropertyMetadata(Common.StringToColor("#FF3EB74D")));

        [DefaultValue(""), Description("显示颜色"), Category("我的属性")]
        public Color ShowColor2
        {
            get { return (Color)GetValue(ShowColor2Property); }
            set
            {
                SetValue(ShowColor2Property, value);
                SetAttrByName("ShowColor2", value);
            }
        }
        #endregion

        #region 表达式、颜色3     ,Paser3,ShowColor3
        private static readonly DependencyProperty Paser3Property =
           DependencyProperty.Register("Paser3",
           typeof(string), typeof(NetLine), new PropertyMetadata(""));

        [DefaultValue(""), Description("表达式"), Category("我的属性")]
        public string Paser3
        {
            get { return (string)GetValue(Paser3Property); }
            set
            {
                SetValue(Paser3Property, value);
                SetAttrByName("Paser3", value);
            }
        }

        private static readonly DependencyProperty ShowColor3Property =
           DependencyProperty.Register("ShowColor3",
           typeof(Color), typeof(NetLine), new PropertyMetadata(Common.StringToColor("#FF1DE5DA")));

        [DefaultValue(""), Description("显示颜色"), Category("我的属性")]
        public Color ShowColor3
        {
            get { return (Color)GetValue(ShowColor3Property); }
            set
            {
                SetValue(ShowColor3Property, value);
                SetAttrByName("ShowColor3", value);
            }
        }
        #endregion

        #region 表达式、颜色4     ,Paser4,ShowColor4
        private static readonly DependencyProperty Paser4Property =
           DependencyProperty.Register("Paser4",
           typeof(string), typeof(NetLine), new PropertyMetadata(""));

        [DefaultValue(""), Description("表达式"), Category("我的属性")]
        public string Paser4
        {
            get { return (string)GetValue(Paser4Property); }
            set
            {
                SetValue(Paser4Property, value);
                SetAttrByName("Paser4", value);
            }
        }

        private static readonly DependencyProperty ShowColor4Property =
           DependencyProperty.Register("ShowColor4",
           typeof(Color), typeof(NetLine), new PropertyMetadata(Common.StringToColor("#FF0B80B7")));

        [DefaultValue(""), Description("显示颜色"), Category("我的属性")]
        public Color ShowColor4
        {
            get { return (Color)GetValue(ShowColor4Property); }
            set
            {
                SetValue(ShowColor4Property, value);
                SetAttrByName("ShowColor4", value);
            }
        }
        #endregion

        #region 表达式、颜色5     ,Paser5,ShowColor5
        private static readonly DependencyProperty Paser5Property =
           DependencyProperty.Register("Paser5",
           typeof(string), typeof(NetLine), new PropertyMetadata(""));

        [DefaultValue(""), Description("表达式"), Category("我的属性")]
        public string Paser5
        {
            get { return (string)GetValue(Paser5Property); }
            set
            {
                SetValue(Paser5Property, value);
                SetAttrByName("Paser5", value);
            }
        }

        private static readonly DependencyProperty ShowColor5Property =
           DependencyProperty.Register("ShowColor5",
           typeof(Color), typeof(NetLine), new PropertyMetadata(Common.StringToColor("#FF2340EB")));

        [DefaultValue(""), Description("显示颜色"), Category("我的属性")]
        public Color ShowColor5
        {
            get { return (Color)GetValue(ShowColor5Property); }
            set
            {
                SetValue(ShowColor5Property, value);
                SetAttrByName("ShowColor5", value);
            }
        }
        #endregion

        #region 表达式、颜色6     ,Paser6,ShowColor6
        private static readonly DependencyProperty Paser6Property =
           DependencyProperty.Register("Paser6",
           typeof(string), typeof(NetLine), new PropertyMetadata(""));

        [DefaultValue(""), Description("表达式"), Category("我的属性")]
        public string Paser6
        {
            get { return (string)GetValue(Paser6Property); }
            set
            {
                SetValue(Paser6Property, value);
                SetAttrByName("Paser6", value);
            }
        }

        private static readonly DependencyProperty ShowColor6Property =
           DependencyProperty.Register("ShowColor6",
           typeof(Color), typeof(NetLine), new PropertyMetadata(Common.StringToColor("#FFDAE517")));

        [DefaultValue(""), Description("显示颜色"), Category("我的属性")]
        public Color ShowColor6
        {
            get { return (Color)GetValue(ShowColor6Property); }
            set
            {
                SetValue(ShowColor6Property, value);
                SetAttrByName("ShowColor6", value);
            }
        }
        #endregion

        #region 表达式、颜色7     ,Paser7,ShowColor7
        private static readonly DependencyProperty Paser7Property =
           DependencyProperty.Register("Paser7",
           typeof(string), typeof(NetLine), new PropertyMetadata(""));

        [DefaultValue(""), Description("表达式"), Category("我的属性")]
        public string Paser7
        {
            get { return (string)GetValue(Paser7Property); }
            set
            {
                SetValue(Paser7Property, value);
                SetAttrByName("Paser7", value);
            }
        }

        private static readonly DependencyProperty ShowColor7Property =
           DependencyProperty.Register("ShowColor7",
           typeof(Color), typeof(NetLine), new PropertyMetadata(Common.StringToColor("#FFE52517")));

        [DefaultValue(""), Description("显示颜色"), Category("我的属性")]
        public Color ShowColor7
        {
            get { return (Color)GetValue(ShowColor7Property); }
            set
            {
                SetValue(ShowColor7Property, value);
                SetAttrByName("ShowColor7", value);
            }
        }
        #endregion

        #endregion

        public override void DesignMode()
        {
            if (!IsDesignMode)
            {
                AdornerLayer = new LineAdorner(this);
                AdornerLayer.Selected += OnSelected;

                var menu = new ContextMenu();
                var menuItem = new MenuItem() { Header = "属性" };
                menuItem.Click += PropertyMenuItem_Click;
                menu.Items.Add(menuItem);
                AdornerLayer.SetValue(ContextMenuService.ContextMenuProperty, menu);
            }
        }
    }
}
