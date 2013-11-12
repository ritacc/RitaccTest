using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Linq;
using MonitorSystem.MonitorSystemGlobal;
using MonitorSystem.Web.Moldes;
using System.ComponentModel;
using System.Windows.Media.Imaging;
using MonitorSystem.Controls;
using MonitorSystem.ItMonitor;
using System.Collections.Generic;

namespace MonitorSystem.ItMonitor
{
    /// <summary>
    ///网络设备
    /// </summary>
    public class NetDevice : ItMonitorBase
    {
        Canvas _grid = new Canvas();
        Label _Txt = new Label();
        Image _img = new Image();
        Border _rect = new Border();
        Rectangle _connect = new Rectangle() { Fill = new SolidColorBrush(Colors.Red),StrokeThickness = 0, Height=5.0d, Width = 5.0d, HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center };

        public NetDevice()
        {
            this.Content = _grid;
            this.Height = this.Width = 100;

            _Txt.SetValue(Grid.RowProperty, 1);
            _Txt.FontSize = 12d;

            _grid.Children.Add(_Txt);
            _grid.Children.Add(_img);
            _grid.Background = new SolidColorBrush();
            SetImg();

            //_rect.BorderThickness = new Thickness(2);
            //_rect.BorderBrush = new SolidColorBrush(Colors.Blue);
            _grid.Children.Add(_rect);
            _rect.Visibility = Visibility.Collapsed;

            _rect.Child = _connect;
        }

        #region 关联处理
        public void ShowRect()
        {
            _rect.Visibility = Visibility.Visible;

        }

        public void HideRect()
        {
            _rect.Visibility = Visibility.Collapsed;
        }        

        public override void SetChannelValue(float fValue)
        {
            SetStringValue(fValue.ToString());
        }

        #endregion

        #region 重载
        public override event EventHandler Selected;

        public override event EventHandler Unselected;
        public override void DesignMode()
        {
            if (!IsDesignMode)
            {
                AdornerLayer = new Adorner(this);
                AdornerLayer.Selected += OnSelected;
                AdornerLayer.Unselected += OnUnselected;

                var menu = new ContextMenu();
                var menuItem = new MenuItem() { Header = "属性" };
                menuItem.Click += PropertyMenuItem_Click;
                menu.Items.Add(menuItem);
                AdornerLayer.SetValue(ContextMenuService.ContextMenuProperty, menu);
                //AdornerLayer.IsLockScale = true;
            }
        }

        /// <summary>
        /// 设备关联的线
        /// </summary>
        public List<NetLine> DeviceOnLine { get; set; }
        protected void OnSelected(object sender, EventArgs e)
        {
            if (null != Selected)
            {
                Selected(this, RoutedEventArgs.Empty);
            }


            ShowRect();
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

            HideRect();
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

        public void SetDeviceValue(string Value)
        {
            CurenntValue = Value;
        }

        public override void SetStringValue(string mValue)
        {
            if (string.IsNullOrEmpty(mValue))
                return;
            if (!string.IsNullOrEmpty(Paser1) && !string.IsNullOrEmpty(Img1))
            {
                if (base.IsMatch(Paser1, mValue))
                {
                    SetImg(Img1);
                }
            }
            if (!string.IsNullOrEmpty(Paser2) && !string.IsNullOrEmpty(Img2))
            {
                if (base.IsMatch(Paser2, mValue))
                {
                    SetImg(Img2);
                }
            }

            if (!string.IsNullOrEmpty(Paser3) && !string.IsNullOrEmpty(Img3))
            {
                if (base.IsMatch(Paser3, mValue))
                {
                    SetImg(Img3);
                }
            }

            if (!string.IsNullOrEmpty(Paser4) && !string.IsNullOrEmpty(Img4))
            {
                if (base.IsMatch(Paser4, mValue))
                {
                    SetImg(Img4);
                }
            }

            if (!string.IsNullOrEmpty(Paser5) && !string.IsNullOrEmpty(Img5))
            {
                if (base.IsMatch(Paser5, mValue))
                {
                    SetImg(Img5);
                }
            }

            if (!string.IsNullOrEmpty(Paser6) && !string.IsNullOrEmpty(Img6))
            {
                if (base.IsMatch(Paser6, mValue))
                {
                    SetImg(Img6);
                }
            }

            if (!string.IsNullOrEmpty(Paser7) && !string.IsNullOrEmpty(Img7))
            {
                if (base.IsMatch(Paser7, mValue))
                {
                    SetImg(Img7);
                }
            }
        }
        
        public void Paint()
        {
            _img.Height = this.Height - 20;
            _img.Width = this.Width - 20;
            _img.SetValue(Canvas.LeftProperty, 10d);
            _Txt.SetValue(Canvas.TopProperty, this.Height - 18);
            SetText();

            _rect.Width = this.Width;
            _rect.Height = this.Height;
            
        }

        public void SetText()
        {
            if (_Txt != null)
            {
                _Txt.Content = DeviceName;
                string txtval = DeviceName;
                int len = Common.GetLength(txtval);
                _Txt.SetValue(Canvas.LeftProperty, this.Width / 2 - len * 6 / 2);
            }
        }

        string _CurenntValue = "正常";
        public string CurenntValue
        {
            set { 
                _CurenntValue = value;
                SetImg();
            }
            get { return _CurenntValue; }
        }
        
        protected void SetImg()
        {
           string gbUrl = string.Format("{0}/Upload/Pic/{1}", Common.TopUrl(), DefultImg);
            _img.Source = new BitmapImage(new Uri(gbUrl, UriKind.Absolute));
        }

        protected void SetImg(string imgName)
        {
            string gbUrl = string.Format("{0}/Upload/Pic/{1}", Common.TopUrl(), imgName);
            _img.Source = new BitmapImage(new Uri(gbUrl, UriKind.Absolute));
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            if (Width < Height)
                this.Width = this.Height = availableSize.Width;
            else
                this.Height = this.Height = availableSize.Height;

            Paint();
            return base.MeasureOverride(availableSize);
        }

        #region 重载

        public override void SetPropertyValue()
        {
            foreach (t_ElementProperty pro in ListElementProp)
            {
                string name = pro.PropertyName.ToUpper();
                string value = pro.PropertyValue;
                switch (name.ToLower())
                {
                    case "devicename":
                        DeviceName = value;
                        break;
                    case "paser1":
                        Paser1 = value;
                        break;
                    case "img1":
                        Img1 = value;
                        break;
                    case "paser2":
                        Paser2 = value;
                        break;
                    case "img2":
                        Img2 = value;
                        break;
                    case "paser3":
                        Paser3 = value;
                        break;
                    case "img3":
                        Img3 = value;
                        break;

                    case "paser4":
                        Paser4 = value;
                        break;
                    case "img4":
                        Img4 = value;
                        break;

                    case "paser5":
                        Paser5 = value;
                        break;
                    case "img5":
                        Img5 = value;
                        break;
                    case "paser6":
                        Paser6 = value;
                        break;
                    case "img6":
                        Img6 = value;
                        break;
                    case "paser7":
                        Paser7 = value;
                        break;
                    case "img7":
                        Img7 = value;
                        break;
                    case "defultimg":
                        DefultImg = value;
                        SetImg();
                        break;
					case "portnumber":
						if (!string.IsNullOrEmpty(value))
							PortNumber = Convert.ToInt32(value);
						break;
                }
            }
        }

        public override void SetCommonPropertyValue()
        {
            this.SetValue(Canvas.LeftProperty, (double)ScreenElement.ScreenX);
            this.SetValue(Canvas.TopProperty, (double)ScreenElement.ScreenY);
            ForeColor = Common.StringToColor(ScreenElement.ForeColor);
            this.Width = (double)ScreenElement.Width;
            this.Height = (double)ScreenElement.Height;
        }

        private string[] m_BrowsableProperties = new string[] { "Left", "Top", "Width", "Height"
            , "PointsSave", "Stroke", "StrokeThickness", "ForeColor" ,"DeviceName","DefultImg"
        ,"Paser1","Img1"        ,"Paser2","Img2"       ,"Paser3","Img3"     ,"Paser4","Img4" 
        ,"Paser5","Img5"        ,"Paser6","Img6"        ,"Paser7","Img7"
        ,"PortNumber"};
        public override string[] BrowsableProperties
        {
            get{return m_BrowsableProperties;}
            set{m_BrowsableProperties = value;}
        }

        #endregion
        
        #region 属性设置
        private static readonly DependencyProperty PortNumberProperty = DependencyProperty.Register("PortNumber", typeof(int), typeof(NetDevice), new PropertyMetadata(16));
        [DefaultValue(""), Description("端口数量"), Category("基本属性")]
         public int PortNumber
        {
            get { return (int)GetValue(PortNumberProperty); }
            set
            {
                SetValue(PortNumberProperty, value);
                SetAttrByName("PortNumber", value);
            }
        }

        private static readonly DependencyProperty ForeColorProperty =DependencyProperty.Register("ForeColor",typeof(Color), typeof(NetDevice), new PropertyMetadata(Colors.Black));
        [DefaultValue(0), Description("名称颜色"), Category("基本属性")]
        public Color ForeColor
        {
            get { return (Color)this.GetValue(ForeColorProperty); }
            set
            {
                this.SetValue(ForeColorProperty, value);
                if (ScreenElement != null)
                    ScreenElement.ForeColor = value.ToString();
                _Txt.Foreground = new SolidColorBrush(value);
            }
        }

        private static readonly DependencyProperty DefultImgProperty = DependencyProperty.Register("DefultImg", typeof(string), typeof(NetDevice), new PropertyMetadata(""));
        [ImageAttribute("PIC")]
        [DefaultValue(""), Description("默认图片"), Category("基本属性")]
        public string DefultImg
        {
            get { return (string)GetValue(DefultImgProperty); }
            set
            {
                SetValue(DefultImgProperty, value);
                SetAttrByName("DefultImg", value);
            }
        }

        private static readonly DependencyProperty DeviceNameProperty =DependencyProperty.Register("DeviceName",typeof(string), typeof(NetDevice), new PropertyMetadata(""));

        [DefaultValue(""), Description("设备名称"), Category("基本属性")]
        public string DeviceName
        {
            get { return (string)GetValue(DeviceNameProperty); }
            set
            {
                SetValue(DeviceNameProperty, value);
                SetAttrByName("DeviceName", value);
                SetText();
            }
        }


        #region 表达式、颜色1     ,"Paser1","Img1"
        private static readonly DependencyProperty Paser1Property =
           DependencyProperty.Register("Paser1",
           typeof(string), typeof(NetDevice), new PropertyMetadata(""));

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

        private static readonly DependencyProperty Img1Property =
           DependencyProperty.Register("Img1",
           typeof(string), typeof(NetDevice), new PropertyMetadata(""));

        [ImageAttribute("PIC")]
        [DefaultValue(""), Description("表达式1，显示图片"), Category("我的属性")]
        public string Img1
        {
            get { return (string)GetValue(Img1Property); }
            set
            {
                SetValue(Img1Property, value);
                SetAttrByName("Img1", value);
            }
        }
        #endregion

        #region 表达式、颜色2     ,"Paser2","Img2"
        private static readonly DependencyProperty Paser2Property =
           DependencyProperty.Register("Paser2",
           typeof(string), typeof(NetDevice), new PropertyMetadata(""));

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

        private static readonly DependencyProperty Img2Property =
           DependencyProperty.Register("Img2",
           typeof(string), typeof(NetDevice), new PropertyMetadata(""));

        [ImageAttribute("PIC")]
        [DefaultValue(""), Description("表达式2，显示图片"), Category("我的属性")]
        public string Img2
        {
            get { return (string)GetValue(Img2Property); }
            set
            {
                SetValue(Img2Property, value);
                SetAttrByName("Img2", value);
            }
        }
        #endregion

        #region 表达式、颜色3     ,"Paser3","Img3"
        private static readonly DependencyProperty Paser3Property =
           DependencyProperty.Register("Paser3",
           typeof(string), typeof(NetDevice), new PropertyMetadata(""));

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

        private static readonly DependencyProperty Img3Property =
           DependencyProperty.Register("Img3",
           typeof(string), typeof(NetDevice), new PropertyMetadata(""));

        [ImageAttribute("PIC")]
        [DefaultValue(""), Description("表达式3，显示图片"), Category("我的属性")]
        public string Img3
        {
            get { return (string)GetValue(Img3Property); }
            set
            {
                SetValue(Img3Property, value);
                SetAttrByName("Img3", value);
            }
        }
        #endregion

        #region 表达式、颜色4     ,"Paser4","Img4"
        private static readonly DependencyProperty Paser4Property =
           DependencyProperty.Register("Paser4",
           typeof(string), typeof(NetDevice), new PropertyMetadata(""));

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

        private static readonly DependencyProperty Img4Property =
           DependencyProperty.Register("Img4",
           typeof(string), typeof(NetDevice), new PropertyMetadata(""));

        [ImageAttribute("PIC")]
        [DefaultValue(""), Description("表达式4，显示图片"), Category("我的属性")]
        public string Img4
        {
            get { return (string)GetValue(Img4Property); }
            set
            {
                SetValue(Img4Property, value);
                SetAttrByName("Img4", value);
            }
        }
        #endregion

        #region 表达式、颜色5     ,"Paser5","Img5"
        private static readonly DependencyProperty Paser5Property =
           DependencyProperty.Register("Paser5",
           typeof(string), typeof(NetDevice), new PropertyMetadata(""));

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

        private static readonly DependencyProperty Img5Property =
           DependencyProperty.Register("Img5",
           typeof(string), typeof(NetDevice), new PropertyMetadata(""));

        [ImageAttribute("PIC")]
        [DefaultValue(""), Description("表达式5，显示图片"), Category("我的属性")]
        public string Img5
        {
            get { return (string)GetValue(Img5Property); }
            set
            {
                SetValue(Img5Property, value);
                SetAttrByName("Img5", value);
            }
        }
        #endregion

        #region 表达式、颜色6     ,"Paser6","Img6"
        private static readonly DependencyProperty Paser6Property =
           DependencyProperty.Register("Paser6",
           typeof(string), typeof(NetDevice), new PropertyMetadata(""));

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

        private static readonly DependencyProperty Img6Property =
           DependencyProperty.Register("Img6",
           typeof(string), typeof(NetDevice), new PropertyMetadata(""));

        [ImageAttribute("PIC")]
        [DefaultValue(""), Description("表达式6，显示图片"), Category("我的属性")]
        public string Img6
        {
            get { return (string)GetValue(Img6Property); }
            set
            {
                SetValue(Img6Property, value);
                SetAttrByName("Img6", value);
            }
        }
        #endregion

        #region 表达式、颜色7     ,"Paser7","Img7"
        private static readonly DependencyProperty Paser7Property =
           DependencyProperty.Register("Paser7",
           typeof(string), typeof(NetDevice), new PropertyMetadata(""));

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

        private static readonly DependencyProperty Img7Property =
           DependencyProperty.Register("Img7",
           typeof(string), typeof(NetDevice), new PropertyMetadata(""));

        [ImageAttribute("PIC")]
        [DefaultValue(""), Description("表达式7，显示图片"), Category("我的属性")]
        public string Img7
        {
            get { return (string)GetValue(Img7Property); }
            set
            {
                SetValue(Img7Property, value);
                SetAttrByName("Img7", value);
            }
        }
        #endregion

        #endregion
    }
}
