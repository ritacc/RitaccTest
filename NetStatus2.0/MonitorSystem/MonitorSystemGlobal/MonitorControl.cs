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
using MonitorSystem.Web.Moldes;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ServiceModel.DomainServices.Client;

namespace MonitorSystem.MonitorSystemGlobal
{
    public abstract partial class MonitorControl : UserControl
    {
        /// <summary>
        /// 是否为ToolTip
        /// </summary>
        public bool IsToolTip { get; protected set; }

        public MonitorControl ParentControl { get; set; }

        private bool _allowToolTip = true;
        public bool AllowToolTip
        {
            get { return _allowToolTip; }
            set { _allowToolTip = value; }
        }

        public bool IsDesignMode { get { return null != AdornerLayer; } }
        public Adorner AdornerLayer { get; protected set; }
        public abstract void DesignMode();
        public abstract void UnDesignMode();
        public abstract FrameworkElement GetRootControl();
        public virtual void SetChildScreen(ObservableCollection<ScreenAddShowName> litobj)
        {
            throw new NotImplementedException();
        }

        public virtual ObservableCollection<ScreenAddShowName> GetChildScreenObj()
        {
            throw new NotImplementedException();
        }

        public abstract event EventHandler Selected;

        public abstract event EventHandler Unselected;

        private t_Element _ScreenElement;
        public t_Element ScreenElement
        {
            get
            {

                SetFont();
                return _ScreenElement;
            }
            set
            {
                _ScreenElement = value;
                GetFont(value.Font);
            }
        }
        #region 字体处理
        private void SetFont()
        {
            //[Font: Name=宋体, Size=15.75, Units=3, GdiCharSet=134, GdiVerticalFont=False]
            string s = string.Format("[Font: Name={0}, Size={1}, Units=3, GdiCharSet=134, GdiVerticalFont=False]",
                                   Common.GetFontCN(this.FontFamily.Source),
                                  this.FontSize.ToString());
            if (_ScreenElement != null)
                _ScreenElement.Font = s;
        }

        private void GetFont(string strFont)
        {
            if (strFont == null)
            {
                this.FontSize = 12f;
                //this.FontFamily = new FontFamily("Simsun");
            }
            else
            {
                // 设置字体
                string Name = "STSong";

                double fontSize = 12f;

                int idx = strFont.IndexOf("Font:");
                if (idx != -1)
                {
                    strFont = strFont.Substring(idx + 5);
                    strFont = strFont.Remove(strFont.Length - 1);
                    char[] slip = new char[] { ',' };
                    string[] arrStr = strFont.Split(slip);
                    foreach (string str in arrStr)
                    {
                        char[] slipKey = new char[] { '=' };
                        string[] keyVal = str.Split(slipKey);
                        int LEN = keyVal[0].Length;
                        string tmp = "Name";
                        int LEN2 = tmp.Length;
                        if (keyVal[0].Equals(" Name", StringComparison.OrdinalIgnoreCase))
                            Name = keyVal[1];
                        if (keyVal[0].Equals(" Size", StringComparison.OrdinalIgnoreCase))
                            fontSize = (double)(Convert.ToDouble(keyVal[1]));
                    }
                }
                this.FontSize = fontSize;
                this.FontFamily = new FontFamily(Common.GetFontEn(Name));
            }
        }
        #endregion
        /// <summary>
        /// 控件自定义属性列表,控件值
        /// </summary>
        public List<t_ElementProperty> ListElementProp { get; set; }
        /// <summary>
        /// 设置控件自定义属性值
        /// </summary>
        public abstract void SetPropertyValue();

        public abstract void SetCommonPropertyValue();

        /// <summary>
        /// 控件状态，新添加的，或以保存的
        /// </summary>
        public ElementSate ElementState;

        public void SetAttrByName(string name, object value)
        {
            if (null == ScreenElement
                || !ScreenElement.ControlID.HasValue)
            {
                return;
            }
            if (ListElementProp == null)
            {
                ListElementProp = new List<t_ElementProperty>();
                var elementProperties = LoadScreen._DataContext.t_ControlProperties.Where(t => t.ControlID == ScreenElement.ControlID.Value);
                foreach (t_ControlProperty elementProperty in elementProperties)
                {
                    t_ElementProperty tt = new t_ElementProperty();
                    tt.Caption = elementProperty.Caption;
                    tt.ElementID = ScreenElement.ElementID;
                    tt.PropertyNo = elementProperty.PropertyNo;
                    tt.PropertyValue = elementProperty.DefaultValue;
                    tt.PropertyName = elementProperty.PropertyName;
                    ListElementProp.Add(tt);
                }
            }

            var property = ListElementProp.FirstOrDefault(p => string.Equals(p.PropertyName, name, StringComparison.CurrentCultureIgnoreCase));
            if (null != property)
            {
                property.PropertyValue = null == value ? string.Empty : value.ToString();
            }
         
        }

        private string[] m_BrowsableProperties = new string[] { "Left", "Top", "Width", "Height", "FontFamily", "FontSize", "Translate", "Foreground" };

        public abstract string[] BrowsableProperties { set; get; }

        public double Translate
        {
            get { return (double)GetValue(OpacityProperty) * 100d; }
            set { SetValue(OpacityProperty, value / 100d); }
        }


        [DefaultValue(""), Description("距左边位置")]
        public double Left
        {
            get { return (double)GetValue(Canvas.LeftProperty); }
            set { SetValue(Canvas.LeftProperty, value); AdornerLayer.SetValue(Canvas.LeftProperty, value); }
        }
        [DefaultValue(""), Description("距上面位置高度")]
        public double Top
        {
            get { return (double)GetValue(Canvas.TopProperty); }
            set { SetValue(Canvas.TopProperty, value); AdornerLayer.SetValue(Canvas.TopProperty, value); }
        }

        public MonitorControl()
        {
            SetValue(ForegroundProperty, new SolidColorBrush(Colors.Black));
        }

        #region 设置值
        protected float m_fValue;//通道值
        protected float[] m_fValueArray;
        //设置元素的值
        public virtual void SetChannelValue(float fValue)
        {
            m_fValue = fValue;
        }

        //设置元素的值(数组)
        public virtual void SetChannelValue(float[] fValueArray)
        {
            m_fValueArray = fValueArray;
        }

        //设置元素的值(数组)
        public virtual void SetChannelValue(float fPosition, float fValueArray)
        {
            //m_fValueArray = fValueArray;
        }

        public virtual void SetStringValue(string Value)
        {

        }
        #endregion

        public bool IsToolTipLoaded { get; set; }
        public ToolTipControl ToolTipControl { get; set; }
        protected override void OnMouseEnter(MouseEventArgs e)
        {
            if (!IsToolTip && !IsDesignMode)
            {
                if (!IsToolTipLoaded)
                {
                    if (null == ToolTipControl && null != _ScreenElement)
                    {
                        IsToolTipLoaded = true;
                        var parentID = _ScreenElement.ElementID;
                        //LoadScreen._DataContext.Load<t_Element>(LoadScreen._DataContext.GetT_ElementsByScreenIDQuery(screenID), LoadToolTipCallback, null);
                        var toolTipControlElement = LoadScreen._DataContext.t_Elements.FirstOrDefault(t => t.ControlID == -9999 && t.ParentID == parentID && t.ElementType == "ToolTip");
                        if (null != toolTipControlElement
                            && Parent is Canvas)
                        {
                            var parent = Parent as Canvas;
                            ToolTipControl = new ToolTipControl(this);
                            ToolTipControl.Width = toolTipControlElement.Width.HasValue ? toolTipControlElement.Width.Value : 300d;
                            ToolTipControl.Height = toolTipControlElement.Height.HasValue ? toolTipControlElement.Height.Value : 200d;
                            ToolTipControl.Transparent = 100;
                            ToolTipControl.SetValue(Canvas.ZIndexProperty, 10000);
                            ToolTipControl.ScreenElement = toolTipControlElement;
                            ToolTipControl.ListElementProp = LoadScreen._DataContext.t_ElementProperties.Where(p => p.ElementID == toolTipControlElement.ElementID).ToList();
                            ToolTipControl.ElementState = ElementSate.Save;
                            ToolTipControl.SetPropertyValue();
                            ToolTipControl.SetCommonPropertyValue();
                            parent.Children.Add(ToolTipControl);
                            SetToolTipPosition();

                            var childElements = LoadScreen._DataContext.t_Elements.Where(t => t.ParentID == parentID && t.ControlID != -9999 && t.ElementType == "ToolTip");
                            foreach (var childElement in childElements)
                            {
                                var poperties = LoadScreen._DataContext.t_ElementProperties.Where(p => p.ElementID == childElement.ElementID).ToList();
                                LoadScreen._instance.ShowElement(ToolTipControl.ToolTipCanvas, childElement, ElementSate.Save, poperties);
                            }
                        }
                    }
                }
                else
                {
                    SetToolTipPosition();
                }
            }
            base.OnMouseEnter(e);
        }

        private void SetToolTipPosition()
        {
            if (null != ToolTipControl)
            {
                ToolTipControl.Visibility = Visibility.Visible;
                ToolTipControl.SetPosition();
            }
        }

        protected override void OnMouseLeave(MouseEventArgs e)
        {
            if (!IsToolTip && !IsDesignMode
                && null != ToolTipControl)
            {
                ToolTipControl.Visibility = Visibility.Collapsed;
            }
            base.OnMouseLeave(e);
        }

        public bool Contains(Rect rect)
        {
            var margin = (Thickness)GetValue(MarginProperty);
            var x = Canvas.GetLeft(this) + margin.Left;
            var y = Canvas.GetTop(this) + margin.Top;
            var w = this.Width - margin.Left - margin.Right;
            var h = this.Height - margin.Top - margin.Bottom;
            return rect.Contains(new Point(x, y)) ||
                rect.Contains(new Point(x + w, y)) ||
                rect.Contains(new Point(x + w, y + h)) ||
                rect.Contains(new Point(x, y + h));
        }
    }

    public enum ElementSate
    {
        New, Save
    }

    public static class StringExtent
    {
        public static string Clone(this string src)
        {
            return string.Copy(src);
        }
    }

    /// <summary>
    /// 屏幕元素、包括Element对象和ListProperty
    /// </summary>
    public class ScreenElementObj
    {
        t_Element m_Element;
        /// <summary>
        /// 元素信息
        /// </summary>
        public t_Element Element
        {
            get { return m_Element; }
            set { m_Element = value; }
        }

        public MonitorControl ParentControl { get; set; }

        List<t_ElementProperty> m_ListElementProperty;
        /// <summary>
        /// 元素属性列表
        /// </summary>
        public List<t_ElementProperty> ListElementProperty
        {
            get { return m_ListElementProperty; }
            set { m_ListElementProperty = value; }
        }


        /// <summary>
        /// 复制对象
        /// </summary>
        /// <param name="obj"></param>
        public void ElementClone(MonitorControl obj, int mWidth, int mHeight)
        {
            this.ParentControl = obj.ParentControl;
             m_Element = new t_Element();
           t_Element m_Older= obj.ScreenElement;
            //ElementID
           m_Element.ElementName = m_Older.ElementName;
           m_Element.ControlID = m_Older.ControlID;
           //m_Element.ScreenX = m_Older.ScreenX;
           m_Element.ScreenX = Convert.ToInt32(obj.GetValue(Canvas.LeftProperty));
           m_Element.ScreenY = Convert.ToInt32(obj.GetValue(Canvas.TopProperty));
          // m_Element.ScreenY = m_Older.ScreenY;
           m_Element.ScreenY += Convert.ToInt16(obj.Height);
           m_Element.TxtInfo = m_Older.TxtInfo;
           m_Element.Width = mWidth;
           m_Element.Height = mHeight;
           m_Element.ImageURL = m_Older.ImageURL;
           m_Element.ForeColor = m_Older.ForeColor;
           m_Element.Font = m_Older.Font;
           m_Element.ChildScreenID = m_Older.ChildScreenID;
           m_Element.DeviceID = m_Older.DeviceID;
           m_Element.ChannelNo = m_Older.ChannelNo;
           m_Element.ScreenID = m_Older.ScreenID;
           m_Element.BackColor = m_Older.BackColor;
           m_Element.Transparent = m_Older.Transparent;
           m_Element.oldX = m_Older.oldX;
           m_Element.oldY = m_Older.oldY;
           m_Element.Method = m_Older.Method;
           m_Element.MinFloat = m_Older.MinFloat;
           m_Element.MaxFloat = m_Older.MaxFloat;
           m_Element.SerialNum = m_Older.SerialNum;
           m_Element.TotalLength = m_Older.TotalLength;
           m_Element.LevelNo = m_Older.LevelNo;
           m_Element.ComputeStr = m_Older.ComputeStr;
           m_ListElementProperty = new List<t_ElementProperty>();
            foreach(t_ElementProperty elePro in obj.ListElementProp)
            {
                t_ElementProperty m_elePro = new t_ElementProperty();
                m_elePro.PropertyNo = elePro.PropertyNo;
                m_elePro.PropertyValue = elePro.PropertyValue;
                m_elePro.Caption = elePro.Caption;
                m_elePro.PropertyName = elePro.PropertyName;
                m_ListElementProperty.Add(m_elePro);
            }
        }
    }

}
