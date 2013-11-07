//using System;
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
using System.Windows.Browser;
using MonitorSystem.Other;

namespace MonitorSystem
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
            string strWhere = string.Empty;
            if (HtmlPage.Document.QueryString.Count > 0)
            {
                foreach (KeyValuePair<string, string> kv in HtmlPage.Document.QueryString)
                {
                    switch (kv.Key.ToLower())
                    {
                        case "towhere":
                            strWhere = kv.Value;
                            break;
                    }
                }
            }
            
            if (strWhere == "RealtimeCurve")
            {
                this.Content = new MainRealtimeCurve();
            }
            else
            {
                this.Content = new LoadScreen();
            }
        }
    }
}
