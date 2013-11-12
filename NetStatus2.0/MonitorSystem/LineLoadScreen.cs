using MonitorSystem.Controls;
using System;
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
using System.Collections.Generic;
using MonitorSystem.ItMonitor;

namespace MonitorSystem
{
    public partial class LoadScreen
    {
        private void AddLineModel()
        {
            if (AddLineCanvas.Visibility != System.Windows.Visibility.Visible)
            {
                AddLineCanvas.SetValue(CustomCursor.CustomProperty, true);
                AddLineCanvas.Visibility = Visibility.Visible;

                AddLine.Points.Clear();

                GridScreen.MouseLeftButtonDown -= GridScreen_MouseLeftButtonDown;
                GridScreen.MouseLeftButtonDown -= AddLineCanvas_MouseLeftButtonDown;
                GridScreen.MouseLeftButtonDown += AddLineCanvas_MouseLeftButtonDown;
                GridScreen.MouseRightButtonDown -= GridScreen_MouseRightButtonDown;
            }
        }

        private void UnAddLineModel()
        {
            if (AddLineCanvas.Visibility != System.Windows.Visibility.Collapsed)
            {
                AddLineCanvas.SetValue(CustomCursor.CustomProperty, false);
                AddLineCanvas.Visibility = Visibility.Collapsed;
                AddLine.Visibility = Visibility.Collapsed;

                AddLine.Points.Clear();

                GridScreen.MouseLeftButtonDown -= GridScreen_MouseLeftButtonDown;
                GridScreen.MouseLeftButtonDown += GridScreen_MouseLeftButtonDown;
                GridScreen.MouseLeftButtonDown -= AddLineCanvas_MouseLeftButtonDown;
                GridScreen.MouseRightButtonDown -= GridScreen_MouseRightButtonDown;
                GridScreen.MouseRightButtonDown += GridScreen_MouseRightButtonDown;
            }
        }

        private void AddLineCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _originPoint = e.GetPosition(csScreen);
            if (AddLine.Points.Count == 0)
            {
                AddLine.Visibility = Visibility.Visible;
                AddLine.Points.Add(_originPoint);

                GridScreen.MouseMove -= AddLineCanvas_MouseMove;
                GridScreen.MouseMove += AddLineCanvas_MouseMove;
                GridScreen.MouseRightButtonDown -= AddLineCanvas_MouseRightButtonDown;
                GridScreen.MouseRightButtonDown += AddLineCanvas_MouseRightButtonDown;
            }

            AddLine.Points.Add(_originPoint);
        }

        private void AddLineCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            _originPoint = e.GetPosition(csScreen);

            AddLine.Points.RemoveAt(AddLine.Points.Count - 1);
            AddLine.Points.Add(_originPoint);
        }

        // 折线绘制完毕  删除 最后一个点
        private void AddLineCanvas_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            GridScreen.MouseMove -= AddLineCanvas_MouseMove;
            GridScreen.MouseRightButtonDown -= AddLineCanvas_MouseRightButtonDown;
            
            AddLine.Points.RemoveAt(AddLine.Points.Count - 1);
			 
            if (AddLine.Points.Count > 1)
            {
                double maxX = AddLine.Points.Max(p => p.X);
                double maxY = AddLine.Points.Max(p => p.Y);
                double minX = AddLine.Points.Min(p => p.X);
                double minY = AddLine.Points.Min(p => p.Y);
                double width = maxX - minX;
                double height = maxY - minY;
                double left = minX;
                double top = minY;
                AddSelectControlElement(this.csScreen, width, height, left, top);
            }
            UnAddLineModel();
            PropertyMain.Instance.ResetSelected();
            GalleryControl.Instance.ResetSelected();
			 

            e.Handled = true;
        }

        #region 计算框选是否命中

        /// <summary>
        /// 计算框选是否命中
        /// </summary>
        /// <param name="rect">框选的范围</param>
        /// <param name="points">线段的区间</param>
        private bool RelationRectAndLine(Rect rect, PointCollection pc)
        {
            if (pc == null)
                return false;
            
            if (pc.Count == 0)
                return false;

            if (rect.Contains(pc[0]))
            {
                return true;
            }
            // 组成2组线段
            List<Segment> segments = new List<Segment>();
            segments.Add(new Segment(rect.Left, rect.Top, rect.Left, rect.Bottom));
            segments.Add(new Segment(rect.Left, rect.Bottom, rect.Right, rect.Bottom));
            segments.Add(new Segment(rect.Right, rect.Bottom, rect.Right, rect.Top));
            segments.Add(new Segment(rect.Right, rect.Top, rect.Left, rect.Top));

            for (int i = 1; i < pc.Count; i++)
            {
                Segment segment = new Segment(pc[i - 1].X, pc[i - 1].Y, pc[i].X, pc[i].Y);
                if (rect.Contains(pc[i]) ||
                    segments.Any(s => RelationSegmentAndSegment(segment, s)))
                {
                    return true;
                }
            }

            return false;
        }

        private bool RelationSegmentAndSegment(Segment segment1, Segment segment2)
        {
            Point vector1 = GetVector(segment1.Point1, segment2.Point1);
            Point vector2 = GetVector(segment1.Point2, segment2.Point1);
            Point vector3 = GetVector(segment2.Point2, segment2.Point1);
            Point vector4 = GetVector(segment2.Point1, segment1.Point1);
            Point vector5 = GetVector(segment2.Point2, segment1.Point1);
            Point vector6 = GetVector(segment1.Point2, segment1.Point1);

            return CrossMul(vector1, vector3) * CrossMul(vector2, vector3) <= 0
                && CrossMul(vector4, vector6) * CrossMul(vector5, vector6) <= 0;
        }

        private Point GetVector(Point point1, Point point2)
        {
            return new Point(point1.X - point2.X, point1.Y - point2.Y);
        }

        private double CrossMul(Point point1, Point point2)
        {
            return point1.X * point2.Y - point1.Y * point2.X;
        }

        // 空间索引线段
        /// <summary>
        /// 空间索引线段
        /// </summary>
        public struct Segment
        {
            private Point _point1; // 保存线段端点1
            private Point _point2; // 保存线段端点2

            // 获取或设置线段端点1
            /// <summary>
            /// 获取或设置线段端点1
            /// </summary>
            public Point Point1 { get { return _point1; } set { _point1 = value; } }

            // 获取或设置线段端点2
            /// <summary>
            /// 获取或设置线段端点2
            /// </summary>
            public Point Point2 { get { return _point2; } set { _point2 = value; } }

            // 获取或设置线段端点1的 X 轴坐标
            /// <summary>
            /// 获取或设置线段端点1的 X 轴坐标
            /// </summary>
            public double X1 { get { return _point1.X; } set { _point1.X = value; } }

            // 获取或设置线段端点1的 Y 轴坐标
            /// <summary>
            /// 获取或设置线段端点1的 Y 轴坐标
            /// </summary>
            public double X2 { get { return _point2.X; } set { _point2.X = value; } }

            // 获取或设置线段端点2的 X 轴坐标
            /// <summary>
            /// 获取或设置线段端点2的 X 轴坐标
            /// </summary>
            public double Y1 { get { return _point1.Y; } set { _point1.Y = value; } }

            // 获取或设置线段端点2的 Y 轴坐标
            /// <summary>
            /// 获取或设置线段端点2的 Y 轴坐标
            /// </summary>
            public double Y2 { get { return _point2.Y; } set { _point2.Y = value; } }

            // 空间索引线段构造函数
            /// <summary>
            /// 空间索引线段构造函数
            /// </summary>
            /// <param name="Point1">端点1</param>
            /// <param name="Point2">端点2</param>
            public Segment(Point Point1, Point Point2)
            {
                _point1 = Point1;
                _point2 = Point2;
            }

            // 空间索引线段构造函数
            /// <summary>
            /// 空间索引线段构造函数
            /// </summary>
            /// <param name="x1">线段端点1的 X 轴坐标</param>
            /// <param name="y1">线段端点1的 Y 轴坐标</param>
            /// <param name="x2">线段端点2的 X 轴坐标</param>
            /// <param name="y2">线段端点2的 Y 轴坐标</param>
            public Segment(double x1, double y1, double x2, double y2)
                : this()
            {
                _point1.X = x1;
                _point1.Y = y1;
                _point2.X = x2;
                _point2.Y = y2;
            }
        }

        #endregion
    }
}
