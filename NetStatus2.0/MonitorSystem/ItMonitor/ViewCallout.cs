﻿using System;
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

namespace MonitorSystem.ItMonitor
{
	public class ViewCallout : MonitorControl
	{
		Canvas csScreen = new Canvas();
		public ViewCallout()
		{
			Content = csScreen;
			this.SizeChanged += new SizeChangedEventHandler(ViewCallout_SizeChanged);
		}

		void ViewCallout_SizeChanged(object sender, SizeChangedEventArgs e)
		{
			csScreen.Width = this.Width = e.NewSize.Width;
			csScreen.Height = this.Height = e.NewSize.Height;
		}

		#region 控件公共属性
		public override event EventHandler Selected;

		public override event EventHandler Unselected;

		private void OnUnselected(object sender, EventArgs e)
		{
			if (null != Unselected)
			{
				Unselected(this, RoutedEventArgs.Empty);
			}
		}

		#region 属性设置
		SetSingleProperty tpp = new SetSingleProperty();
		private void PropertyMenuItem_Click(object sender, RoutedEventArgs e)
		{
			tpp = new SetSingleProperty();
			if (ScreenElement != null)
			{
				tpp.Closing += new EventHandler<System.ComponentModel.CancelEventArgs>(tpp_Closing);
				tpp.DeviceID = this.ScreenElement.DeviceID.Value;
				tpp.ChanncelID = this.ScreenElement.ChannelNo.Value;
				tpp.LevelNo = this.ScreenElement.LevelNo.Value;
				tpp.ComputeStr = this.ScreenElement.ComputeStr;
			}
			tpp.Init();
			tpp.Show();
		}

		protected void tpp_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (tpp.IsOK && ScreenElement != null)
			{
				this.ScreenElement.DeviceID = tpp.DeviceID;
				this.ScreenElement.ChannelNo = tpp.ChanncelID;
				this.ScreenElement.LevelNo = tpp.LevelNo;
				this.ScreenElement.ComputeStr = tpp.ComputeStr;
			}
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
		}

		private string[] m_BrowsableProperties = new string[] { "Left", "Top", "Width", "Height", "FontFamily", "FontSize","ForeColor","BackColor", "Transparent", 
            "MyData","Title","SetXTitle","Range","SetYTitle","SetMinValue","SetMaxValue","DataZone","DottedLine","LineColor"};

		public override string[] BrowsableProperties
		{
			get { return m_BrowsableProperties; }
			set { m_BrowsableProperties = value; }
		}

		public override void SetCommonPropertyValue()
		{
			this.SetValue(Canvas.LeftProperty, (double)ScreenElement.ScreenX);
			this.SetValue(Canvas.TopProperty, (double)ScreenElement.ScreenY);
			Transparent = ScreenElement.Transparent.Value;
			_ForeColor = Common.StringToColor(ScreenElement.ForeColor);
			_BackColor = Common.StringToColor(ScreenElement.BackColor);
		}

		#region 属性
		private static readonly DependencyProperty TransparentProperty = DependencyProperty.Register("Transparent", typeof(int), typeof(MyLine), new PropertyMetadata(0));
		private int _Transparent = 0;
		[DefaultValue(""), Description("透明"), Category("杂项")]
		public int Transparent
		{
			get { return _Transparent; }
			set
			{
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
				_BackColor = value;
				 
				if (ScreenElement != null)
					ScreenElement.BackColor = value.ToString();
			}
		}

		private static readonly DependencyProperty ForeColorProperty = DependencyProperty.Register("ForeColor", typeof(int), typeof(MyLine), new PropertyMetadata(0));
		private Color _ForeColor = Colors.Black;
		[DefaultValue(""), Description("字体颜色"), Category("外观")]
		public Color ForeColor
		{
			get { return _ForeColor; }
			set
			{
				_ForeColor = value;
				if (ScreenElement != null)
					ScreenElement.ForeColor = value.ToString();
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
		public override void SetPropertyValue()
		{
			foreach (t_ElementProperty pro in ListElementProp)
			{
				string name = pro.PropertyName.ToUpper();
				string value = pro.PropertyValue;
			}
		}

		#region  加载数据
		public void ScreenInit(t_Screen obj)
		{
			csScreen.Children.Clear();
			RectangleGeometry r = new RectangleGeometry();
			Rect rect = new Rect();
			if (obj.Width != null && obj.Height != null)
			{
				csScreen.Width = this.Width = rect.Width = obj.Width.Value;
				csScreen.Height = this.Height = rect.Height = obj.Height.Value;
			}
			else
			{
				csScreen.Width = this.Width = rect.Width = 1024;
				csScreen.Height = this.Height = rect.Height = 768;
			}
			r.Rect = rect;
			this.Clip = r;
			SetScreenImg(obj.ImageURL);
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
							mNetDevvice.Name = obj.ElementID.ToString();
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
				mControl.Name = "slt" + obj.ElementID.ToString();
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
	}
}