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
using MonitorSystem.MonitorSystemGlobal;

namespace MonitorSystem.ItMonitor
{
    public abstract class ItMonitorBase : MonitorControl
    {
        public ItMonitorBase()
        {
            this.MouseEnter += new MouseEventHandler(rectRoot_MouseEnter);
            this.MouseLeave += new MouseEventHandler(rectRoot_MouseLeave);
            this.MouseMove += new MouseEventHandler(rectRoot_MouseMove);
        }
        #region 显示信息
        private void rectRoot_MouseEnter(object sender, MouseEventArgs e)
        {
            Point point1 = e.GetPosition(LoadScreen._instance.csScreen);
            TootHead t = new TootHead();
            t.point1 = point1;
            TootHead.CureentPoint = point1;
            t.PropItems = ScreenElement.ChildScreenID;
            t.DeviceID = ScreenElement.DeviceID.HasValue ? ScreenElement.DeviceID.Value : -1;

            TootHead.IsShow = true;
            Timeout.SetTimeout(t.Show, 1500);
        }


        private void rectRoot_MouseMove(object sender, MouseEventArgs e)
        {
            TootHead.CureentPoint = e.GetPosition(LoadScreen._instance.csScreen);
        }

        private void rectRoot_MouseLeave(object sender, MouseEventArgs e)
        {
            TootHead.HideTool();
            TootHead.IsShow = false;
        }

        #endregion

       

        #region 通道选择
        SetItMonitorProperty tpp = new SetItMonitorProperty();
        protected void PropertyMenuItem_Click(object sender, RoutedEventArgs e)
        {
            tpp = new SetItMonitorProperty();

            tpp.Closing += new EventHandler<System.ComponentModel.CancelEventArgs>(tpp_Closing);
            tpp.DeviceID = this.ScreenElement.DeviceID.Value;
            tpp.ChanncelID = this.ScreenElement.ChannelNo.Value;

            tpp.PropertyItems = this.ScreenElement.ChildScreenID;
            tpp.Init();
            tpp.Show();
        }

        protected void tpp_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (tpp.IsOK)
            {
                this.ScreenElement.DeviceID = tpp.DeviceID;
                this.ScreenElement.ChildScreenID = tpp.PropertyItems;
                this.ScreenElement.ChannelNo = tpp.ChanncelID;
            }
        }
        #endregion

        #region  
        /// <summary>
        /// 表达试与值是否匹配
        /// </summary>
        /// <param name="paser">表达试</param>
        /// <param name="devVal">通道值</param>
        /// <returns></returns>
        public bool IsMatch(string paser, string devVal)
        {
            if (string.IsNullOrEmpty(paser) || string.IsNullOrEmpty(devVal))
                return false;

            if (paser.Length <= 2)
                return false;

            bool isResult = false;

            string[] czf1 = {"=",">","<" };
            string[] czf2 = { ">=", "<=" };
            string fh1 = paser.Substring(0, 1);
            string fh2 = paser.Substring(0, 2);

            bool fhBool1 = false;
            bool fhBool2 = false;

            foreach (string s in czf1)
            {
                if (s == fh1)
                {
                    fhBool1 = true;
                    break;
                }
            }
            foreach (string s in czf2)
            {
                if (s == fh2)
                {
                    fhBool2 = true;
                    break;
                }
            }

            if (!fhBool1 && !fhBool2)
                return false;

            if (fhBool2 )//
            {
                string strVal = paser.Substring(2);
                double Val;
                if (!double.TryParse(strVal, out Val))
                    return false;
                double ChanncelVal;
                if (!double.TryParse(devVal, out ChanncelVal))
                    return false;

                switch (fh2)
                {
                    case ">=":
                       return ChanncelVal>= Val;
                    case "<=":
                       return ChanncelVal <= Val;
                }
            }
            else
            {
                string strVal = paser.Substring(1);
                switch (fh1)
                {
                    case "=":
                        return strVal == devVal;
                    case ">":
                    case "<":
                        double Val;
                        if (!double.TryParse(strVal, out Val))
                            return false;
                        double ChanncelVal;
                        if (!double.TryParse(devVal, out ChanncelVal))
                            return false;
                        if (fh1 == ">")
                        {
                            return ChanncelVal > Val;
                        }
                        else if (fh1 == "<")
                        {
                            return ChanncelVal < Val;
                        }
                        break;
                }
            }
            return false;
        }
        #endregion
    }
}
