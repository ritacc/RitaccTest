using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App.Framework.Web
{
    public enum EnumLanguage
    {
       
        /// <summary>
        /// 简体中文
        /// </summary>
        [Enum("zh-cn")]
        Zh_CN,
        /// <summary>
        /// 英语（美国）
        /// </summary>
        [Enum("en-us")]
        En_US
    }
}