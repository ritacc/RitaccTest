

//**********************************************************
//Copyright(C)2011 By AIS版权所有。
//
//文件名：GuidExtension.cs
//文件功能：Guid类型相关扩展操作
// 备   注：
//
//创建标识：
//
//修改标识：
//修改描述：
//**********************************************************

using System;

namespace App.Framework.Web
{
    /// <summary>
    /// GUID扩展操作类
    /// </summary>
    public static class GuidExtension
    {
        /// <summary>
        /// 判断可空GUID数据是否为null或empty
        /// </summary>
        /// <param name="nullAbleGuidData">可空Guid数据</param>
        /// <returns>Boolean</returns>
        public static Boolean IsNullOrEmpty(this Guid? nullAbleGuidData)
        {
            if (!nullAbleGuidData.HasValue) return true;

            return nullAbleGuidData.Value.Equals(Guid.Empty) ? true : false;
        }
    }
}