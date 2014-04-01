//**********************************************************
//Copyright(C)2011 By AIS版权所有。
//
//文件名：TreeGenerate.cs
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
using System.Web.Mvc;

namespace App.Framework.Web.Tree
{
    /// <summary>
    /// CheckBox 或者 RadioBox
    /// </summary>
    public enum CheckOrRadio
    {
        /// <summary>
        /// No
        /// </summary>
        No,

        /// <summary>
        /// CheckBox
        /// </summary>
        CheckBox,

        /// <summary>
        /// Radio
        /// </summary>
        Radio
    }

    /// <summary>
    /// 与树相关类
    /// </summary>
    public static class TreeGenerate
    {
        /// <summary>
        /// 根据任一节点查找树的根节点并生成一颗完整树
        /// </summary>
        /// <param name="node"></param>
        /// <param name="nodeList"></param>
        /// <param name="isfindParent"></param>
        /// <param name="rootNode"></param>
        public static void GetTreeNodes(TreeNode node, List<TreeNode> nodeList, bool isfindParent, ref TreeNode rootNode)
        {
            if (node == null) return;
            if ((node.Parent == null && string.IsNullOrEmpty(node.ParentId)) || isfindParent)
            {
                node.ChildList = nodeList.Where(n => (n.ParentId == node.Id && !string.IsNullOrEmpty(n.ParentId))
                                                  || (n.Parent == node && n.Parent != null)).ToList();
                if (!isfindParent)
                    rootNode = node;
                foreach (TreeNode n in node.ChildList)
                {
                    n.Parent = node;
                    GetTreeNodes(n, nodeList, true, ref rootNode);
                }
            }
            else
            {
                if (node.Parent == null && node.ParentId != null)
                    node.Parent = nodeList.SingleOrDefault(n=>n.Id == node.ParentId);
                GetTreeNodes(node.Parent, nodeList, false, ref  rootNode);
            }
        }

        /// <summary>
        /// 递归生成树的HTML代码
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public static string GenerateTree(TreeNode node)
        {
            return GenerateTree(node, CheckOrRadio.CheckBox);
        }
        /// <summary>
        /// 递归生成树的HTML代码
        /// </summary>
        /// <param name="node"></param>
        /// <param name="checkOrRadio"></param>
        /// <returns></returns>
        public static string GenerateTree(TreeNode node, CheckOrRadio checkOrRadio)
        {
            if (node == null) return "无节点";
            TagBuilder liBuilder = new TagBuilder("li");
            TagBuilder spanBuilder = new TagBuilder("span")
            {
                InnerHtml = node.Title,
            };

            //是否有<input>选择框
            if (checkOrRadio != CheckOrRadio.No)
            {
                TagBuilder checkBoxBuilder = new TagBuilder("input");
                checkBoxBuilder.Attributes["type"] = checkOrRadio.ToString();
                checkBoxBuilder.Attributes["name"] = "parentid";
                checkBoxBuilder.Attributes["id"] = node.Id;
                checkBoxBuilder.Attributes["value"] = node.Id;
                checkBoxBuilder.Attributes["flag"] = node.Flag;
                if (node.IsSelected)
                {
                    checkBoxBuilder.Attributes["checked"] = "checked";
                }
                liBuilder.InnerHtml = "\r\n" + checkBoxBuilder.ToString() + "\r\n" + spanBuilder.ToString();
            }
            else
            {
                liBuilder.InnerHtml = "\r\n" + spanBuilder.ToString();
            }

            if (node.ChildList.Count > 0)
            {
                TagBuilder ulBuilder = new TagBuilder("ul");
                ulBuilder.AddCssClass("lastclass");

                foreach (TreeNode n in node.ChildList)
                {
                    ulBuilder.InnerHtml += "\r\n" + GenerateTree(n, checkOrRadio);
                }

                liBuilder.InnerHtml += "\r\n" + ulBuilder.ToString();
            }

            return liBuilder.ToString();
        }
    }
}
