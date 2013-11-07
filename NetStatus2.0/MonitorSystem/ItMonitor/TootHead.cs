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
using System.Threading;
using System.Windows.Threading;

namespace MonitorSystem.ItMonitor
{
    public class TootHead
    {
        public static Point CureentPoint;
        //public static Point ShowPoint;
        
        public Point point1;
        //public double mTankWidth;
        //public double mTankHeight;

        public int DeviceID { get; set; }
        public string PropItems { get; set; }

        public static string Info = "";

        public static bool IsShow {get;set;}
        public void Show()
        {
            FrameworkElement element = null;
            element = LoadScreen._instance.PropView;
            element.Dispatcher.BeginInvoke(new Action(() =>
            {
                element.DataContext = null;
                if (!TootHead.IsShow)
                {
                    return;
                }
                if (DeviceID == -1)
                    return;

                (element as PropertyView).View(PropItems, DeviceID);
                
                element.Visibility = Visibility.Visible;

                element.SetValue(Canvas.LeftProperty, CureentPoint.X);// + SViewer.HorizontalOffset);
                element.SetValue(Canvas.TopProperty, CureentPoint.Y);// + SViewer.VerticalOffset );
                element.SetValue(Canvas.ZIndexProperty, 999);
            }));
        }

        public static void HideTool()
        {
            TootHead.IsShow = false;
            if (null == LoadScreen._instance.PropView)
                return;
            LoadScreen._instance.PropView.Visibility = Visibility.Collapsed;
        }
    }

    public class Timeout
    {
        #region SetTimeout/ClearTimeout Simulation
       public static Thread CourentTh;

        public static void SetTimeout(Action cb, int delay)
        {
            ClearTimeout();
            SetTimeout(cb, delay, null);
        }

        public static void SetTimeout(Action cb, int delay, Dispatcher dispatcher)
        {           
            CourentTh = new Thread(() =>
            {
                Thread.Sleep(delay);
                cb();
            });
            CourentTh.Start();
        }

        public static void ClearTimeout()
        {
            if (CourentTh != null)
            {
                try
                {
                    CourentTh.Abort();
                    CourentTh = null;
                }
                catch
                {
                }
            }
        }

        #endregion
    }
}
