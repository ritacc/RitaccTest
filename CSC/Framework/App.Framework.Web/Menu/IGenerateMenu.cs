//**********************************************************
//Copyright(C)2011 By AIS版权所有。
//
//文件名：IGenerateMenu.cs
//文件功能：能够根据分页按钮生成HTML接口
//
//创建标识：鲜红 || 2011-03-31
//
//修改标识：
//修改描述：
//**********************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.Framework.Web.Menu
{
    /// <summary>
    /// 生成菜单接口
    /// </summary>
    public interface IGenerateMenu
    {
        /// <summary>
        /// 生成模块
        /// </summary>
        /// <param name="moudles"></param>
        /// <returns></returns>
        string GenerateMoudle(IEnumerable<Module> moudles);

        /// <summary>
        /// 生成模块下的菜单
        /// </summary>
        /// <param name="moudle"></param>
        /// <returns></returns>
        string GenerateMenu(Module moudle);
    }
}
