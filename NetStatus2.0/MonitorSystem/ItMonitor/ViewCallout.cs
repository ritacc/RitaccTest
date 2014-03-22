using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using MonitorSystem.Web.Moldes;
using System.Windows.Media.Imaging;
using MonitorSystem.MonitorSystemGlobal;
using MonitorSystem.ZTControls;
using MonitorSystem.ItMonitor;
using System.ComponentModel;
using System.ServiceModel.DomainServices.Client;
using System.Windows.Input;
using System.Windows.Shapes;
using System.Windows.Threading;
using MonitorSystem.Other;
using MonitorSystem.Web.Servers;

namespace MonitorSystem.ItMonitor
{
	public class ViewCallout : MonitorControl
	{
		Canvas csScreen = new Canvas();
        Canvas _mainCan = new Canvas();
        Border _border = new Border();

		Rectangle _ContentRect = new Rectangle();
        Rectangle _connect = new Rectangle() { Fill = new SolidColorBrush(Colors.Red), StrokeThickness = 0, Height = 5.0d, Width = 5.0d, 
            HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center };


		public ViewCallout()
		{
            _mainCan.Children.Add(csScreen);

            Content = _mainCan;
			if (LoadScreen._instance.ScaleValue < 2)
			{
				_mainCan.Background = new SolidColorBrush(Colors.Blue);
				csScreen.Visibility = Visibility.Collapsed;
			}
			else
			{
				csScreen.Visibility = Visibility.Visible;
				_mainCan.Background = new SolidColorBrush();
			}

			this.SizeChanged += new SizeChangedEventHandler(ViewCallout_SizeChanged);

			this.MouseLeftButtonUp += new MouseButtonEventHandler(TP_MouseLeftButtonUp);
			this.MouseLeftButtonDown += new MouseButtonEventHandler(ViewCallout_MouseLeftButtonDown);
            
			_border.Visibility = Visibility.Collapsed;            
            _border.Child = _connect;
            _mainCan.Children.Add(_border);

			//_ContentBorder
			_ContentRect.StrokeThickness = 1.0;
			_ContentRect.Stroke = new SolidColorBrush(Colors.Red);
			_mainCan.Children.Add(_ContentRect);

			timerRefrshValue.Interval = new TimeSpan(0, 0, 30);
			timerRefrshValue.Tick += new EventHandler(timer_Tick);
			timerRefrshValue.Start();

			_DataContext = LoadScreen._DataContext;
		}
		
		void ViewCallout_SizeChanged(object sender, SizeChangedEventArgs e)
		{
			this.Width = e.NewSize.Width;
			this.Height = e.NewSize.Height;
            _border.SetValue(Canvas.LeftProperty, (this.Width-5.0d) / 2);
            _border.SetValue(Canvas.TopProperty, (this.Height-5.0d) / 2);

			_ContentRect.Width = this.Width;
			_ContentRect.Height = this.Height;

            if (e.NewSize.Width > 10 && e.NewSize.Height > 10)
            {
                ScreenInit();
				SetRect();
            }
		}
		DateTime dtStart = DateTime.Now;
		void ViewCallout_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			dtStart = DateTime.Now;
		}
		public void TP_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			TimeSpan t = DateTime.Now - dtStart;
			if (t.Seconds == 0 && t.Minutes < 2 && t.Milliseconds < 999)
			{
				t_Screen ts = GetChildScreenID();
				if (ts != null)
				{
					LoadScreen.Load(ts);
				}
			}
		}
		
        #region 关联处理
        /// <summary>
        /// 设备关联的线
        /// </summary>
        public List<NetLine> DeviceOnLine { get; set; }
        public void ShowRect()
        {
            _border.Visibility = Visibility.Visible;
        }

        public void HideRect()
        {
            _border.Visibility = Visibility.Collapsed;
        }
        #endregion
		#region 背景处理
		public Color _NowColor = Colors.Blue;
		private double _NowSize = 1;
		public override void SetChannelValue(float fValue)
		{

			//

			//RectangleGeometry r = new RectangleGeometry();
			//Rect rect = new Rect();
			//rect.Width = this.Width;
			//rect.Height = this.Height;

			//r.Rect = rect;
			//this.Clip = r;

			PathFigureCollection pfc = new PathFigureCollection();
			PathFigure pf = new PathFigure();
			PathSegmentCollection psc = new PathSegmentCollection();
			pfc.Add(pf);
			pf.Segments = psc;

            psc.Clear();
			pf.StartPoint = new Point(this.Width * 0.1, this.Height* 0.5);
			
			//直线
			ArcSegment arcs = new ArcSegment();
			arcs.Point = new Point(0, this.Height * 0.4);
            arcs.SweepDirection = SweepDirection.Clockwise;
            arcs.Size = new Size() { Height = this.Height / 2, Width = this.Width * 0.25 };
			psc.Add(arcs);

            arcs = new ArcSegment();
            arcs.Point = new Point(this.Width * 0.3, this.Height * 0.01);
            arcs.SweepDirection = SweepDirection.Clockwise;
            psc.Add(arcs);


			arcs = new ArcSegment();
			arcs.Point = new Point(this.Width, this.Height / 2);
			arcs.Size = new Size();

			if (fValue == 1)
			{
				_NowColor = this.BackColor;				
			}
			else
			{
				_NowColor = this.ForeColor;		 
			}
			SetBackInfo();
		}
		public void SetBackInfo()
		{
			if (_NowSize >= 2)
			{
				_mainCan.Background = new SolidColorBrush();
				csScreen.Visibility = Visibility.Visible;
			}
			else
			{
				_mainCan.Background = new SolidColorBrush(_NowColor);
				csScreen.Visibility = Visibility.Collapsed;
			}
		}
		public void SetCallout(double m)
		{
			_NowSize = m;
			if (!IsDesignMode)
			{
				SetBackInfo();
			}
		}
		#endregion

		public void SetRect()
		{
			if (this.Width < 5 || this.Height < 5)
			{
				return;
			}
			RectangleGeometry r = new RectangleGeometry();
			Rect rect = new Rect();
			rect.Width = this.Width;
			rect.Height = this.Height;
			r.RadiusX = r.RadiusY = Radius;
			r.Rect = rect;
			this.Clip = r;

			//--_ContentRect
			_ContentRect.RadiusX = _ContentRect.RadiusY = Radius;
			_ContentRect.Stroke = new SolidColorBrush(this.BorderColor);
			_ContentRect.StrokeThickness = BorderSize;
		}

		#region 控件公共属性
		public override event EventHandler Selected;

		public override event EventHandler Unselected;

		protected void OnUnselected(object sender, EventArgs e)
		{
			if (null != Unselected)
			{
				Unselected(this, RoutedEventArgs.Empty);
			}
			HideRect();
		}

		#region 属性设置
		ViewCalloutProperty tpp = new ViewCalloutProperty();
        private void PropertyMenuItem_Click(object sender, RoutedEventArgs e)
        {
            tpp.Closing += new EventHandler<System.ComponentModel.CancelEventArgs>(tpp_Closing);
            tpp.Screen = GetChildScreenID();

			tpp.DeviceID = this.ScreenElement.DeviceID.Value;
			tpp.ChanncelID = this.ScreenElement.ChannelNo.Value;
			tpp.LevelNo = this.ScreenElement.LevelNo.Value;
			tpp.ComputeStr = this.ScreenElement.ComputeStr;

			tpp.Init();
            tpp.Show();
        }

        protected void tpp_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (tpp.IsOK)
            {
				var oldScr = GetChildScreenID();
                this.ScreenElement.ChildScreenID = string.Format("{0}#{1};", tpp.Screen.ScreenName,tpp.Screen.ScreenID);
				this.ScreenElement.DeviceID = tpp.DeviceID;
				this.ScreenElement.ChannelNo = tpp.ChanncelID;
				this.ScreenElement.LevelNo = tpp.LevelNo;
				this.ScreenElement.ComputeStr = tpp.ComputeStr;

				var newScr = GetChildScreenID();
				if (oldScr != null && newScr != null && oldScr.ScreenID == newScr.ScreenID)
					return;
				ScreenInit();
            }
        }
        private t_Screen GetChildScreenID()
        {
            string mScreenID = base.ScreenElement.ChildScreenID;
            if (string.IsNullOrEmpty(mScreenID) || mScreenID == "0")
            {
                return null;
            }
            mScreenID = mScreenID.Replace(";", "");
            string[] attr = mScreenID.Split('#');
            if (attr.Length == 2)
            {
                int Scrennid = Convert.ToInt32(attr[1]);
                t_Screen t = LoadScreen.listScreen.Single(a => a.ScreenID == Scrennid);
                return t;
            }
            return null;
        }
		#endregion

		public override void DesignMode()
		{
			if (!IsDesignMode)
			{
				AdornerLayer = new Adorner(this);
				AdornerLayer.Selected += OnSelected;

				var menu = new ContextMenu();
				var menuItem = new MenuItem() { Header = "属性" };
				menuItem.Click += PropertyMenuItem_Click;
				menu.Items.Add(menuItem);
				AdornerLayer.SetValue(ContextMenuService.ContextMenuProperty, menu);
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

		private void OnSelected(object sender, EventArgs e)
		{
			if (null != Selected)
			{
				Selected(this, RoutedEventArgs.Empty);
			}
            ShowRect();
		} 
		private string[] m_BrowsableProperties = new string[] { "Left", "Top", "Width", "Height", "FontFamily", "FontSize","ForeColor","BackColor", "Translate"
           ,"Radius","BorderColor","BorderSize"};

		public override string[] BrowsableProperties
		{
			get { return m_BrowsableProperties; }
			set { m_BrowsableProperties = value; }
		}

		public override void SetCommonPropertyValue()
		{
			this.SetValue(Canvas.LeftProperty, (double)ScreenElement.ScreenX);
			this.SetValue(Canvas.TopProperty, (double)ScreenElement.ScreenY);

			_Translate = ScreenElement.Transparent.Value;
			_NowColor = _ForeColor = Common.StringToColor(ScreenElement.ForeColor);
			_BackColor = Common.StringToColor(ScreenElement.BackColor);
			
            this.Width = (double)ScreenElement.Width;
            this.Height = (double)ScreenElement.Height;

			SetBackInfo();
		}

        public override void SetPropertyValue()
        {
            foreach (t_ElementProperty pro in ListElementProp)
            {
                string name = pro.PropertyName.ToUpper();
                string value = pro.PropertyValue;
                switch (name.ToLower())
                {
					case "radius":
						Radius = Convert.ToInt32(value);
                        break;
					case "bordercolor":
						BorderColor = Common.StringToColor(value);
						break;
					case "bordersize":
						BorderSize = Convert.ToInt32(value);
						break;
                }
            }
        }

		#region 属性
		private static readonly DependencyProperty BorderColorProperty = DependencyProperty.Register("BorderColor", typeof(Color), typeof(MyLine), new PropertyMetadata(Colors.Blue));
		private Color _BorderColor = Colors.Blue;
		[DefaultValue(""), Description("边框颜色"), Category("边框")]
		public Color BorderColor
		{
			get { return _BorderColor; }
			set
			{
				_BorderColor = value;
				SetAttrByName("BorderColor", value);
				SetRect();
			}
		}

		private static readonly DependencyProperty BorderSizeProperty = DependencyProperty.Register("BorderSize", typeof(int), typeof(MyLine), new PropertyMetadata(0));
		private int _BorderSize = 0;
		[DefaultValue(0), Description("边框圆角半径"), Category("边框")]
		public int BorderSize
		{
			get { return _BorderSize; }
			set
			{
				_BorderSize = value;
				SetAttrByName("BorderSize", value);
				SetRect();
			}
		}

		private static readonly DependencyProperty RadiusProperty = DependencyProperty.Register("Radius", typeof(int), typeof(MyLine), new PropertyMetadata(0));
		private int _Radius = 0;
		[DefaultValue(0), Description("边框圆角半径"), Category("边框")]
		public int Radius
		{
			get { return _Radius; }
			set
			{
				_Radius = value;
				SetAttrByName("Radius", value);
				SetRect();
			}
		}

		private static readonly DependencyProperty TransparentProperty = DependencyProperty.Register("Translate", typeof(int), typeof(MyLine), new PropertyMetadata(0));
		private int _Transparent = 0;
		[DefaultValue(""), Description("透明"), Category("杂项")]
		public int _Translate
		{
			get { return _Transparent; }
			set
			{
				this.Translate = value;
				_Transparent = value;
				if (ScreenElement != null)
					ScreenElement.Transparent = value;
			}
		}

		private static readonly DependencyProperty BackColorProperty = DependencyProperty.Register("BackColor", typeof(int), typeof(MyLine), new PropertyMetadata(0));
		private Color _BackColor = Colors.Black;
		[DefaultValue(""), Description("背景色"), Category("外观")]
		public Color BackColor
		{
			get { return _BackColor; }
			set
			{
				_NowColor = _BackColor = value;
				 
				if (ScreenElement != null)
					ScreenElement.BackColor = value.ToString();
				SetBackInfo();
			}
		}

		private static readonly DependencyProperty ForeColorProperty = DependencyProperty.Register("ForeColor", typeof(int), typeof(MyLine), new PropertyMetadata(0));
		private Color _ForeColor = Colors.Black;
		[DefaultValue(""), Description("背景颜色"), Category("外观")]
		public Color ForeColor
		{
			get { return _ForeColor; }
			set
			{
				_NowColor = _ForeColor = value;
				if (ScreenElement != null)
					ScreenElement.ForeColor = value.ToString();
				SetBackInfo();
			}
		}
		#endregion

		public List<t_ElementProperty> GetProperty()
		{
			return ListElementProp;
		}

		public override FrameworkElement GetRootControl()
		{
			return this;
		}


		#endregion

		#region  加载数据		
		public void ScreenInit()
		{
            csScreen.Children.Clear();
            t_Screen obj = GetChildScreenID();
            if (obj == null)
                return;
			ScreentID = obj.ScreenID;

			if (obj.Width != null && obj.Height != null)
			{
				csScreen.Width =obj.Width.Value;
				csScreen.Height =obj.Height.Value;
			}
			else
			{
				csScreen.Width =    1024;
				csScreen.Height =    768;
			}			
			SetScreenImg(obj.ImageURL);


			double sfPerw = this.Width / csScreen.Width;
			double sfPerh = this.Height / csScreen.Height;
            if (sfPerh < sfPerw)
                sfPerw = sfPerh;
            ScaleTransform mainShowCanvasScaleTrans = new ScaleTransform();
            mainShowCanvasScaleTrans.CenterX = 0.0;
            mainShowCanvasScaleTrans.CenterY = 0.0;
            mainShowCanvasScaleTrans.ScaleX = sfPerw;
            mainShowCanvasScaleTrans.ScaleY = sfPerw;
            csScreen.RenderTransform = mainShowCanvasScaleTrans;

            LoadScreen._DataContext.Load(LoadScreen._DataContext.GetT_ElementsByScreenIDQuery(obj.ScreenID),
               LoadElementCompleted, obj.ScreenID);
		}

        /// <summary>
        /// 加载元素
        /// </summary>
        /// <param name="result"></param>
        private void LoadElementCompleted(LoadOperation<t_Element> result)
        {
            if (result.HasError)
            {
                return;
            }
            LoadScreen._DataContext.Load(LoadScreen._DataContext.GetScreenElementPropertyQuery(Convert.ToInt32(result.UserState)),
                LoadElementPropertiesCompleted, result.UserState);
        }
        private void LoadElementPropertiesCompleted(LoadOperation<t_ElementProperty> result)
        {
            if (result.HasError)
            {
                return;
            }
            List<t_Element> lsitElement = LoadScreen._DataContext.t_Elements.Where(a => a.ScreenID == Convert.ToInt32(result.UserState) && null == a.ParentID).OrderBy(a => a.ElementName).ToList();
            ShowElements(lsitElement, csScreen);
        }

		private void SetScreenImg(string strImg, bool resize = false)
		{
			var gbUrl = string.Format("{0}/Upload/ImageMap/{1}", Common.TopUrl(), strImg);
			var bitmap = new BitmapImage(new Uri(gbUrl, UriKind.Absolute));

			var imgB = new ImageBrush() { Stretch = Stretch.UniformToFill };
			imgB.ImageSource = bitmap;
			csScreen.Background = imgB;
		}

		public MonitorControl ShowElement(t_Element obj, ElementSate eleStae, List<t_ElementProperty> listObj)
		{
			Canvas canvas = csScreen;
			try
			{
				if (obj.ImageURL != null && obj.ImageURL.IndexOf("MonitorSystem") == 0)
				{
					MonitorControl instance = (MonitorControl)Activator.CreateInstance(Type.GetType(obj.ImageURL));
					SetEletemt(canvas, instance, obj, eleStae, listObj);
					return instance;
				}
				else
				{
					switch (obj.ElementName)
					{
						case "NetLine":
							NetLine mNetLine = new NetLine();
							obj.Width = 50;
							obj.Height = 20;
							SetEletemt(canvas, mNetLine, obj, eleStae, listObj);
							return mNetLine;
						case "NetDevice":
							NetDevice mNetDevvice = new NetDevice();
							SetEletemt(canvas, mNetDevvice, obj, eleStae, listObj);
							return mNetDevvice;
						case "MyButton":
							TP_Button mtpButtom = new TP_Button();
							SetEletemt(canvas, mtpButtom, obj, eleStae, listObj);
							return mtpButtom;
						//break;
						case "MonitorLine":
							MonitorLine mPubLine = new MonitorLine();
							SetEletemt(canvas, mPubLine, obj, eleStae, listObj);
							return mPubLine;
						//break;
						case "MonitorText":
							MonitorText mPubText = new MonitorText();
							mPubText.MyText = obj.TxtInfo;
							SetEletemt(canvas, mPubText, obj, eleStae, listObj);
							return mPubText;
						//break;
						case "ColorText":
							ColorText mColorText = new ColorText();
							SetEletemt(canvas, mColorText, obj, eleStae, listObj);
							return mColorText;
						//break;
						case "InputTextBox":
							InputTextBox mInputTextBox = new InputTextBox();
							mInputTextBox.MyText = obj.TxtInfo;
							SetEletemt(canvas, mInputTextBox, obj, eleStae, listObj);
							return mInputTextBox;
						//break;
						case "ButtonCtrl":
							ButtonCtrl mButtonCtrl = new ButtonCtrl();
							mButtonCtrl.MyText = obj.TxtInfo;
							SetEletemt(canvas, mButtonCtrl, obj, eleStae, listObj);
							return mButtonCtrl;
						//break;
						case "MonitorCur":
							MonitorCur mPubCur = new MonitorCur();
							SetEletemt(canvas, mPubCur, obj, eleStae, listObj);
							return mPubCur;
						//break;
						case "MonitorRectangle":
							MonitorRectangle mPubRec = new MonitorRectangle();
							SetEletemt(canvas, mPubRec, obj, eleStae, listObj);
							return mPubRec;
						//break;
						case "MonitorGrid":
							MonitorGrid mPubGrid = new MonitorGrid();
							SetEletemt(canvas, mPubGrid, obj, eleStae, listObj);
							return mPubGrid;
						//break;
						case "FoldLine":
							MonitorFoldLine mPubFoldLine = new MonitorFoldLine();
							SetEletemt(canvas, mPubFoldLine, obj, eleStae, listObj);
							return mPubFoldLine;
						//break;
						case "Temprary":
							Temprary mTemprary = new Temprary();
							SetEletemt(canvas, mTemprary, obj, eleStae, listObj);
							return mTemprary;
						case "DLBiaoPan":
							DLBiaoPan mDLBiaoPan = new DLBiaoPan();
							obj.Width = 2 * obj.Height.Value;
							SetEletemt(canvas, mDLBiaoPan, obj, eleStae, listObj);
							return mDLBiaoPan;
						case "DigitalBiaoPan":
							DigitalBiaoPan mDigitalBiaoPan = new DigitalBiaoPan();
							SetEletemt(canvas, mDigitalBiaoPan, obj, eleStae, listObj);
							return mDigitalBiaoPan;
						case "Switch":
							Switch mSwitch = new Switch();
							SetEletemt(canvas, mSwitch, obj, eleStae, listObj);
							return mSwitch;
						case "SignalSwitch":
							SignalSwitch mSignalSwitch = new SignalSwitch();
							//obj.Width = obj.Height;
							SetEletemt(canvas, mSignalSwitch, obj, eleStae, listObj);
							return mSignalSwitch;
						case "DetailSwitch":
							DetailSwitch mDetailSwitch = new DetailSwitch();
							SetEletemt(canvas, mDetailSwitch, obj, eleStae, listObj);
							return mDetailSwitch;
						case "RealTimeCurve":
							RealTimeCurve mRealTime = new RealTimeCurve();
							SetEletemt(canvas, mRealTime, obj, eleStae, listObj);
							return mRealTime;
						case "TableCtrl":
							TableCtrl mTableCtrl = new TableCtrl();
							SetEletemt(canvas, mTableCtrl, obj, eleStae, listObj);
							return mTableCtrl;
						case "zedGraphCtrl":
							zedGraphCtrl mzedGraphCtrl = new zedGraphCtrl();
							SetEletemt(canvas, mzedGraphCtrl, obj, eleStae, listObj);
							return mzedGraphCtrl;
						case "zedGraphLineCtrl":
							zedGraphLineCtrl mzedGraphLineCtrl = new zedGraphLineCtrl();
							SetEletemt(canvas, mzedGraphLineCtrl, obj, eleStae, listObj);
							return mzedGraphLineCtrl;
						case "zedGraphPieCtrl":
							zedGraphPieCtrl mzedGraphPieCtrl = new zedGraphPieCtrl();
							SetEletemt(canvas, mzedGraphPieCtrl, obj, eleStae, listObj);
							return mzedGraphPieCtrl;
						case "MyLine"://曲线
							MyLine mMyLine = new MyLine();
							SetEletemt(canvas, mMyLine, obj, eleStae, listObj);
							return mMyLine;
						case "BackgroundRect"://背景
							BackgroundRect mBackgroundRect = new BackgroundRect();
							SetEletemt(canvas, mBackgroundRect, obj, eleStae, listObj);
							return mBackgroundRect;
						case "PicBox"://窗口式背景控件
							PicBox mPicBox = new PicBox();
							SetEletemt(canvas, mPicBox, obj, eleStae, listObj);
							return mPicBox;
						case "DrawLine"://窗口式背景控件
							DrawLine mDrawLine = new DrawLine();
							SetEletemt(canvas, mDrawLine, obj, eleStae, listObj);
							return mDrawLine;
						case "ExtProControl"://窗口式背景控件
							ExtProControl mExtProControl = new ExtProControl();
							SetEletemt(canvas, mExtProControl, obj, eleStae, listObj);
							return mExtProControl;
						case "DimorphismGraphCtrl"://窗口式背景控件
							DimorphismGraphCtrl mDimorphismGraphCtrl = new DimorphismGraphCtrl();
							SetEletemt(canvas, mDimorphismGraphCtrl, obj, eleStae, listObj);
							return mDimorphismGraphCtrl;
						case "BackgroundControl":
							BackgroundControl backgroundControl = new BackgroundControl();
							SetEletemt(canvas, backgroundControl, obj, eleStae, listObj);
							var childElements = LoadScreen._DataContext.t_Elements.Where(e => e.ParentID == obj.ElementID && e.ElementType == "Background").ToList();
							ShowElements(childElements, backgroundControl.BackgroundCanvas, backgroundControl);
							return backgroundControl;
						default:
							string url = string.Format("/MonitorSystem;component/Images/ControlsImg/{0}", obj.ImageURL);
							BitmapImage bitmap = new BitmapImage(new Uri(url, UriKind.Relative));
							ImageSource mm = bitmap;
							TP mtp = new TP();
							mtp.Source = mm;
							SetEletemt(canvas, mtp, obj, eleStae, listObj);
							return mtp;
					}
				}
			}
			catch
			{
				return null;
			}
		}
		private void ShowElements(List<t_Element> lsitElement, Canvas canvas, MonitorControl parentContol = null)
		{
			foreach (t_Element el in lsitElement)
			{
				var list = LoadScreen._DataContext.t_ElementProperties.Where(a => a.ElementID == el.ElementID);
				var monitorControl = ShowElement(el, ElementSate.Save, list.ToList());

				if (null != monitorControl && null != parentContol)
				{
					monitorControl.ParentControl = parentContol;
					monitorControl.AllowToolTip = false;
					monitorControl.ClearValue(Canvas.ZIndexProperty);
					if (null != monitorControl.AdornerLayer)
					{
						monitorControl.AdornerLayer.AllToolTip = false;
					}
				}
			}

			LoadChanncelValue();
		}

		private void SetEletemt(Canvas canvas, MonitorControl mControl, t_Element obj, ElementSate eleStae, List<t_ElementProperty> listObj)
		{
			mControl.Selected += (o, e) =>
			{
				PropertyMain.Instance.ControlPropertyGrid.SelectedObject = null;
				PropertyMain.Instance.ControlPropertyGrid.BrowsableProperties = mControl.BrowsableProperties;
				PropertyMain.Instance.ControlPropertyGrid.SelectedObject = mControl;
			};
			if (eleStae == ElementSate.Save)
			{
				mControl.Name = "viewCal" + obj.ElementID.ToString();
			}
			mControl.ScreenElement = obj;
			mControl.ListElementProp = listObj;
			mControl.ElementState = eleStae;

			mControl.SetPropertyValue();
			mControl.SetCommonPropertyValue();
			//添加到场景
			canvas.Children.Add(mControl);
		}
		#endregion

		#region  刷新值
		DispatcherTimer timerRefrshValue = new DispatcherTimer();
		int ScreentID = -1;
		public  MonitorServers _DataContext = new MonitorServers();
		protected void timer_Tick(object sender, EventArgs e)
		{
			LoadChanncelValue();
		}

		private void LoadChanncelValue()
		{
			if (ScreentID == -1 || IsDesignMode)
				return;
			EntityQuery<V_ScreenMonitorValue> v = _DataContext.GetScreenMonitorValueQuery(ScreentID);

			_DataContext.Load(v, ValueLoadComplete, null);
		}

		public void ValueLoadComplete(LoadOperation<V_ScreenMonitorValue> result)
		{
			if (result.HasError)
				return;
			float digitalValue = 0f;
			foreach (V_ScreenMonitorValue obj in result.Entities)
			{
				var vobj = (MonitorControl)this.csScreen.FindName("viewCal" + obj.ElementID.ToString());
				if (null != vobj)
				{
					SetChannelValue(digitalValue, obj, vobj);
					if (vobj.ToolTipControl != null)
					{
						var child = (MonitorControl)vobj.ToolTipControl.ToolTipCanvas.FindName(obj.ElementID.ToString());
						SetChannelValue(digitalValue, obj, child);
					}
					if (vobj is BackgroundControl)
					{
						var backgroundControl = vobj as BackgroundControl;
						var child = (MonitorControl)backgroundControl.BackgroundCanvas.FindName(obj.ElementID.ToString());
						SetChannelValue(digitalValue, obj, child);
					}
				}
			}
			_DataContext.V_ScreenMonitorValues.Clear();
		}

		private void SetChannelValue(float digitalValue, V_ScreenMonitorValue obj, MonitorControl vobj)
		{
			if (vobj == null)
			{
				return;
			}

			if (vobj is RealTimeT)
			{
				(vobj as RealTimeT).SetLineValue(obj);
				return;
			}

			if (vobj.ScreenElement.DeviceID.Value != -1 && vobj.ScreenElement.ChannelNo.Value != -1)
			{
				float fValue;
				if (float.TryParse(obj.MonitorValue.ToString(), out fValue))
				{
					if (vobj.ScreenElement.ElementName == "DigitalBiaoPan")
					{
						digitalValue = fValue;
						vobj.SetChannelValue(fValue);
					}
					else if (vobj.ScreenElement.ElementName == "DrawLine")
					{
						vobj.SetChannelValue(fValue, digitalValue);
					}
					else
					{
						vobj.SetChannelValue(fValue);
					}
				}
				else//非flat值
				{
					vobj.SetStringValue(obj.MonitorValue);
				}
			}
		}
		#endregion
	}
}
