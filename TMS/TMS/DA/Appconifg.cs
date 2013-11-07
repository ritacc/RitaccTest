using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace TM.DA
{
    public  class appconifg
    {
        static readonly object padlock = new object();
        static appconifg instance = null;

        //public appconifg()
        //{
        //    Inite();
        //}

        public static appconifg Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (padlock)
                    {
                        if (instance == null)
                        {
                            instance = new appconifg();
                        }
                        return instance;
                    }
                }
                return instance;
            }
        }

        public static string m_DBConnectionPath;

        public string DBConnectionPath
        {
            get { return m_DBConnectionPath; }
            set { m_DBConnectionPath = value; }
        }
       


        //public void Inite()
        //{
        //    Configuration config = System.Configuration.ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        //    AppSettingsSection app = config.AppSettings;
            
        //    m_DBConnectionPath = app.Settings["DBConnectionPath"].Value;//Êý¾Ý¿âÂ·¾¶           
        //}
    }
}
