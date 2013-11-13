﻿using System;
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
using MonitorSystem.MonitorSystemGlobal;

namespace MonitorSystem.ItMonitor
{
    public class DeviceLineHeadle
    {
        public static MonitorControl GetDeviceByElementID(int ElementID)
        {
            foreach (FrameworkElement element in LoadScreen._instance.csScreen.Children)
            {
                var obj = element as NetDevice;
                if (obj != null)
                {
                    if (obj.ScreenElement.ElementID == ElementID)
                    {
                        return obj;
                    }
                }
                if (element is ViewCallout)
                {
                    if ((element as ViewCallout).ScreenElement.ElementID == ElementID)
                    {
                        return element as ViewCallout;
                    }
                }
            }
            return null;
        }
        /// <summary>
        /// 根据点位置选中设备
        /// </summary>
        /// <param name="_index"></param>
        /// <param name="_line"></param>
        public static MonitorControl PointSelectRectDevices(int _index, Polyline _line)
        {
            if (_index == 0 || _index == _line.Points.Count - 1)
            {
                foreach (FrameworkElement element in LoadScreen._instance.csScreen.Children)
                {
                    var _dev =element as NetDevice;
                    if (_dev != null)
                    {
                        var rect = new Rect(_dev.Left, _dev.Top, _dev.Width, _dev.Height);
                        if (rect.Contains(_line.Points[_index]))
                        {
                            _dev.ShowRect();
                            return _dev;
                        }
                    }

                    var _Net = element as ViewCallout;
                    if (_Net != null)
                    {
                        var rect = new Rect(_Net.Left, _Net.Top, _Net.Width, _Net.Height);
                        if (rect.Contains(_line.Points[_index]))
                        {
                            _Net.ShowRect();
                            return _Net;
                        }
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// 线移动时，选中设备
        /// </summary>
        /// <param name="_Line"></param>
        public static void LineMoveSelectDevices(Polyline _Line, FrameworkElement _Control)
        {
            if (_Line.Points.Count < 2)
                return;
            var obj = _Control as NetLine;
            if (obj != null)
            {
                CanncelDeviceFcous();
                PointSelectRectDevices(0, _Line);
                PointSelectRectDevices(_Line.Points.Count - 1, _Line);
            }
        }

        /// <summary>
        /// 根据网络设备，查找与之相关联的线。
        /// </summary>
        /// <param name="_netDev"></param>
        /// <returns></returns>
        public static List<NetLine> GetDeviceOnLinesByNetDevice(MonitorControl _netDev)
        {
            List<NetLine> ListLines = new List<NetLine>();
            foreach (FrameworkElement element in LoadScreen._instance.csScreen.Children)
            {
                var obj = element as NetLine;
                if (obj != null)
                {
                    if (obj.UpLineDevice == _netDev || obj.DownLineDevice== _netDev)
                    {
                        ListLines.Add(obj);
                    }
                }
            }
            return ListLines;
        }



        /// <summary>
        /// 取消设备焦点
        /// </summary>
        public static void CanncelDeviceFcous()
        {
            foreach (FrameworkElement element in LoadScreen._instance.csScreen.Children)
            {
                if (element is NetDevice)
                {
                    (element as NetDevice).HideRect();
                }
                else if (element is ViewCallout)
                {
                    (element as ViewCallout).HideRect();
                }
            }
        }

        ///// <summary>
        ///// 判断指定的点，是否在设备上。
        ///// </summary>
        ///// <param name="_point"></param>
        ///// <param name="_dev"></param>
        ///// <returns></returns>
        //public static bool IsInDeivceUP(Point _point, NetDevice _dev)
        //{
        //    double _top = _dev.Top;
        //    double _left = _dev.Left;
        //    double _width = _dev.Width;
        //    double _height = _dev.Height;

        //    if ((_top+3 < _point.Y && (_top + _height - 20) > _point.Y) && (_left+5 < _point.X && (_left + _width - 20) > _point.X))
        //        return true;

        //    return false;
        //}

    }
}
