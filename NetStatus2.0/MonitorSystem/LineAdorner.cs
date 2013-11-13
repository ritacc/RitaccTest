using MonitorSystem.MonitorSystemGlobal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using MonitorSystem.ItMonitor;

namespace MonitorSystem
{
    [TemplatePart(Name = "Canvas", Type = typeof(Canvas))]
    public class LineAdorner : Adorner
    {
        private Canvas _canvas;
        readonly List<Rectangle> _blocks = new List<Rectangle>();
        Brush _pointBrush = new SolidColorBrush(Colors.Blue);
        const double BLOCK_WIDTH = 10d;// 节点 选中 块的大小

        private static Rectangle _selectRect = null;

        // 选择之后替换原来的线， 主要是换样式
        Polyline _polyline = new Polyline()
        {
            Stroke = new SolidColorBrush(Color.FromArgb(0xFF, 0x56, 0x94, 0xBA)),
            StrokeThickness = 5d
        };

        #region IsSelected Property

        protected override void SetSelect()
        {
            if (!_isSelected)
            {

                if (null != _selectRect)
                {
                    _selectRect.Fill = _selectRect.Stroke;
                    _selectRect = null;
                }

                if (null != _canvas)
                {
                    _polyline.Opacity = 1d;
                    _associatedElement.Visibility = Visibility.Collapsed;
                    foreach (FrameworkElement element in _canvas.Children)
                    {
                        element.Visibility = Visibility.Visible;
                    }
                    if (!_selectedAdorners.Contains(this))
                    {
                        CancelSelected();
                        _selectedAdorners.Add(this);
                    }
                }
            }
            _isSelected = true;
        }

        protected override void SetMutiSelect()
        {
            _isSelected = true;
            IsSelected = true;
            if (null != _canvas)
            {

                if (null != _selectRect)
                {
                    _selectRect.Fill = _selectRect.Stroke;
                    _selectRect = null;
                }

                _polyline.Opacity = 1d;
                _associatedElement.Visibility = Visibility.Collapsed;
                foreach (FrameworkElement element in _canvas.Children)
                {
                    element.Visibility = Visibility.Visible;
                }
            }
        }

        protected override void SetUnselect()
        {
            if (_isSelected)
            {
                if (null != _canvas)
                {
                    if (null != _selectRect)
                    {
                        _selectRect.Fill = _selectRect.Stroke;
                        _selectRect = null;
                    }
                    _polyline.Opacity = 0d;
                    _associatedElement.Visibility = Visibility.Visible;
                    foreach (FrameworkElement element in _canvas.Children)
                    {
                        if (element != _polyline)
                        {
                            element.Visibility = Visibility.Collapsed;
                        }
                    }
                }
            }
            _isSelected = false;
        }

        protected override void SetMutiUnselect()
        {
            _isSelected = false;
            if (null != _canvas)
            {
                _polyline.Opacity = 0d;
                _associatedElement.Visibility = Visibility.Visible;
                foreach (FrameworkElement element in _canvas.Children)
                {
                    if (element != _polyline)
                    {
                        element.Visibility = Visibility.Collapsed;
                    }
                }
            }
            IsSelected = false;
        }
        #endregion

        public LineAdorner(NetLine associatedElement)
        {
            base.DefaultStyleKey = typeof(LineAdorner);
            Visibility = Visibility.Visible;
            _associatedElement = associatedElement;
            _associatedElement.Visibility = Visibility.Collapsed;
            _parent = _associatedElement.Parent as Canvas;
            _parent.Children.Add(this);

            _polyline.MouseLeftButtonDown += _polyline_MouseLeftButtonDown;

            IsSelected = true;
        }

        void _polyline_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _polyline.CaptureMouse();

            _initialPoint = e.GetPosition(_parent);
            _originPoints = _selectedAdorners.Select(a => new Point((double)a.GetValue(Canvas.LeftProperty) + a._offsetLeft, (double)a.GetValue(Canvas.TopProperty) + a._offsetTop));

            _polyline.MouseLeftButtonUp -= _polyline_MouseLeftButtonUp;
            _polyline.MouseLeftButtonUp += _polyline_MouseLeftButtonUp;
            _polyline.MouseMove -= _polyline_MouseMove;
            _polyline.MouseMove += _polyline_MouseMove;

            if (null != _selectRect)
            {
                _selectRect.Fill = _selectRect.Stroke;
                _selectRect = null;
            }

            if (_associatedElement is NetLine)
            {
                DeviceLineHeadle.LineMoveSelectDevices(_polyline, _associatedElement);
            }

            Canvas.SetZIndex(this, 9999);
        }
        

        void _polyline_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _polyline.ReleaseMouseCapture();
            _polyline.MouseLeftButtonUp -= _polyline_MouseLeftButtonUp;
            _polyline.MouseMove -= _polyline_MouseMove;
			if (_associatedElement is NetLine)
			{
				AutoConnectAll();
			}
            base.OnSelected();
            Canvas.SetZIndex(this, 999);
        }

		/// <summary>
		/// 移动两个点到关联位
		/// </summary>
		public void AutoConnectAll()
		{
			AutoConnect(0);
			AutoConnect(_blocks.Count - 1);
			UpdateAssmentElement();
		}

        private void AutoConnect(int index)
        {
            if (index == 0 || index == _blocks.Count - 1)
            {
                MonitorControl obj = DeviceLineHeadle.PointSelectRectDevices(index, _polyline);
                if (index == 0)
                {
                    (_associatedElement as NetLine).UpLineDevice = obj;
                }
                else
                {
                    (_associatedElement as NetLine).DownLineDevice = obj;
                }

                if (null != obj)
                {
                    if (obj is NetDevice)
                    {
                        (obj as NetDevice).DeviceOnLine = DeviceLineHeadle.GetDeviceOnLinesByNetDevice((_associatedElement as NetDevice));

                        AutoConnect(_blocks[index], obj);
                    }
                    else if (obj is ViewCallout)
                    {
                        (obj as ViewCallout).DeviceOnLine = DeviceLineHeadle.GetDeviceOnLinesByNetDevice((_associatedElement as ViewCallout));

                        AutoConnect(_blocks[index], obj);
                    }
                }
            }
        }

        void _polyline_MouseMove(object sender, MouseEventArgs e)
        {
            var mousePoint = e.GetPosition(_parent);

            var offsetX = mousePoint.X - _initialPoint.X;
            var offsetY = mousePoint.Y - _initialPoint.Y;
            MouseMuti(offsetX, offsetY);
            _initialPoint = mousePoint;

            DeviceLineHeadle.LineMoveSelectDevices(_polyline, _associatedElement);
        }

        protected override void MoveTo(Point point, double offsetX, double offsetY)
        {
            int i = 0;
            PointCollection pc = new PointCollection();
            foreach (Point p in _polyline.Points)
            {
                pc.Add(new Point(p.X + offsetX, p.Y + offsetY));
                _blocks[i].SetValue(Canvas.LeftProperty, p.X + offsetX - BLOCK_WIDTH * .5d);
                _blocks[i].SetValue(Canvas.TopProperty, p.Y + offsetY - BLOCK_WIDTH * .5d);
                i++;
            }
            _polyline.Points = pc;

            UpdateAssmentElement();
        }

        protected override void Delete()
        {
            _parent.Children.Remove(_associatedElement);
            _parent.Children.Remove(this);
            _canvas.Children.Clear();
            Dispose();
        }

        public override void Dispose()
        {
            //_associatedElement.SizeChanged -= _associatedElement_SizeChanged;
            if (null != _parent)
            {
                _parent.Children.Remove(this);
            }
            GC.SuppressFinalize(this);
        }

        public override void OnApplyTemplate()
        {
            _canvas = GetTemplateChild("Canvas") as Canvas;
            _canvas.Children.Add(_polyline);
            NetLine netLine = _associatedElement as NetLine;
            for (int i = 0; i < netLine.PointColl.Count; i++)
            {
                Rectangle rect = new Rectangle();
                rect.Height = BLOCK_WIDTH;
                rect.Width = BLOCK_WIDTH;
                rect.Stroke = _pointBrush;
                rect.Fill = _pointBrush;
                rect.StrokeThickness = 1d;
                rect.Tag = i;
                rect.MouseLeftButtonDown += rect_MouseLeftButtonDown;
                _polyline.Points.Add(new Point(netLine.PointColl[i].X + netLine.Left, netLine.PointColl[i].Y + netLine.Top));
                Canvas.SetLeft(rect, netLine.PointColl[i].X + netLine.Left - BLOCK_WIDTH * .5d);
                Canvas.SetTop(rect, netLine.PointColl[i].Y + netLine.Top - BLOCK_WIDTH * .5d);
                _canvas.Children.Add(rect);

                _blocks.Add(rect);
            }

            if (IsSelected)
            {
                SetUnselect();
            }
			if (_associatedElement is NetLine)
			{
				if ((_associatedElement as NetLine).ElementState == ElementSate.New)
				{
					AutoConnectAll();
				}
			}
            base.OnApplyTemplate();
        }

        public void UpdatePoints(int index ,double offsetX, double offsetY)
        {
            NetLine netLine = _associatedElement as NetLine;
            var rect = _blocks[index];
            Canvas.SetLeft(rect, netLine.PointColl[index].X + netLine.Left - BLOCK_WIDTH * .5d);
            Canvas.SetTop(rect, netLine.PointColl[index].Y + netLine.Top - BLOCK_WIDTH * .5d);
            _polyline.Points.Insert(index + 1, new Point(netLine.PointColl[index].X + netLine.Left, netLine.PointColl[index].Y + netLine.Top));
            _polyline.Points.RemoveAt(index);
        }

        void rect_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            FrameworkElement element = sender as FrameworkElement;
            element.CaptureMouse();

            _initialPoint = e.GetPosition(_parent);
            _originPoints = _selectedAdorners.Select(a => new Point((double)a.GetValue(Canvas.LeftProperty) + a._offsetLeft, (double)a.GetValue(Canvas.TopProperty) + a._offsetTop));

            element.MouseMove -= element_MouseMove;
            element.MouseMove += element_MouseMove;
            element.MouseLeftButtonUp -= element_MouseLeftButtonUp;
            element.MouseLeftButtonUp += element_MouseLeftButtonUp;

            if (null != _selectRect)
            {
                _selectRect.Fill = _selectRect.Stroke;
            }
            _selectRect = element as Rectangle;
            _selectRect.Fill = new SolidColorBrush(Colors.Green);


            Canvas.SetZIndex(this, 9999);
        }

        

        void element_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            FrameworkElement element = sender as FrameworkElement;
            element.ReleaseMouseCapture();
            element.MouseMove -= element_MouseMove;
            element.MouseLeftButtonUp -= element_MouseLeftButtonUp;

            int index = Convert.ToInt32(element.Tag);
            AutoConnect(index);
            UpdateAssmentElement();
            base.OnSelected();

            Canvas.SetZIndex(this, 999);
            //DeviceLineHeadle.CanncelDeviceFcous();
        }

        private void AutoConnect(FrameworkElement element, MonitorControl obj)
        {
            // 吸附在设备的中心点上
            if (_associatedElement is NetLine)
            {
                var netLine = _associatedElement as NetLine;
                int index = Convert.ToInt32(element.Tag);
                if (null != obj && ((netLine.UpLineDevice == obj  && index == 0)
                    || (netLine.DownLineDevice == obj && index == _blocks.Count - 1)))
                {
                    var x = obj.Left + obj.Width / 2d;
                    var y = obj.Top + obj.Height / 2d;
                    _polyline.Points.RemoveAt(index);
                    _polyline.Points.Insert(index,new Point(x, y));
                    Canvas.SetLeft(element, x - BLOCK_WIDTH * .5d);
                    Canvas.SetTop(element, y - BLOCK_WIDTH * .5d);
                }
                if (null != netLine.UpLineDevice)
                {
                    if (netLine.UpLineDevice is NetDevice)
                        (netLine.UpLineDevice as NetDevice).HideRect();
                    else if (netLine.UpLineDevice is ViewCallout)
                        (netLine.UpLineDevice as ViewCallout).HideRect();
                }

                if (null != netLine.DownLineDevice)
                {
                    if (netLine.DownLineDevice is NetDevice)
                        (netLine.DownLineDevice as NetDevice).HideRect();
                    else if (netLine.DownLineDevice is ViewCallout)
                        (netLine.DownLineDevice as ViewCallout).HideRect();
                }
            }
        }


        void element_MouseMove(object sender, MouseEventArgs e)
        {
            FrameworkElement element = sender as FrameworkElement;

            var mousePoint = e.GetPosition(_parent);
            var i = 0;
            var offsetX = mousePoint.X - _initialPoint.X;
            var offsetY = mousePoint.Y - _initialPoint.Y;
            int index = Convert.ToInt32(element.Tag);
            Point newPoint = new Point(_polyline.Points[index].X + offsetX, _polyline.Points[index].Y + offsetY);
            _polyline.Points.RemoveAt(index);
            _polyline.Points.Insert(index, newPoint);
            
            element.SetValue(Canvas.LeftProperty, Canvas.GetLeft(element) + offsetX);
            element.SetValue(Canvas.TopProperty, Canvas.GetTop(element) + offsetY);
            _initialPoint = mousePoint;
            if (index == 0 || index == _blocks.Count - 1)
            {
                DeviceLineHeadle.CanncelDeviceFcous();
                DeviceLineHeadle.PointSelectRectDevices(index, _polyline);
            }
        }

        private void UpdateAssmentElement()
        {
            double maxX = _polyline.Points.Max(p => p.X);
            double maxY = _polyline.Points.Max(p => p.Y);
            double minX = _polyline.Points.Min(p => p.X);
            double minY = _polyline.Points.Min(p => p.Y);
            double width = maxX - minX;
            double height = maxY - minY;
            double left = minX;
            double top = minY;
            _associatedElement.Width = width;
            _associatedElement.Height = height;
            Canvas.SetLeft(_associatedElement, left);
            Canvas.SetTop(_associatedElement, top);

            (_associatedElement as NetLine).ConvertPoints(_polyline.Points);
        }

        public static bool RemoveBlock()
        {
            if (_selectedAdorners.Count == 1 && _selectedAdorners[0] is LineAdorner)
            {
                if (null != _selectRect)
                {
                    LineAdorner adorner = _selectedAdorners[0] as LineAdorner;
                    int index = Convert.ToInt32(_selectRect.Tag);
                    if (adorner._polyline.Points.Count > 2)
                    {
                        adorner._polyline.Points.RemoveAt(index);
                        adorner._canvas.Children.Remove(adorner._blocks[index]);
                        adorner._blocks.RemoveAt(index);
                        adorner.UpdateAssmentElement();
                    }
                    else
                    {
                        adorner.Delete();
                    }
                    return true;
                }
            }
            return false;
        }
    }
}
