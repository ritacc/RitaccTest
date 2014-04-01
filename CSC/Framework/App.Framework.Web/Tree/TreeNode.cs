//**********************************************************
//Copyright(C)2011 By AIS版权所有。
//
//文件名：TreeNode.cs
//文件功能：
//
//创建标识：鲜红 || 2011-04-08
//
//修改标识：
//修改描述：
//**********************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.Framework.Web.Tree
{
    /// <summary>
    /// 树节点
    /// </summary>
    public class TreeNode
    {
        /// <summary>
        /// 低级编号
        /// </summary>
        public string ParentId { get; set; }

        /// <summary>
        /// 自身编号
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 子节点
        /// </summary>
        public List<TreeNode> ChildList{get;set;}

        /// <summary>
        /// 临时存放值
        /// </summary>
        public string Flag { get; set; }

        /// <summary>
        /// 低级节点
        /// </summary>
        public TreeNode Parent { get; set; }

        /// <summary>
        /// 是否已选中
        /// </summary>
        public bool IsSelected { get; set; }
    }
}
