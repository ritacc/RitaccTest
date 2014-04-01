//**********************************************************
//Copyright(C)2011 By AIS版权所有。
//
//文件名：NormalPageProc.cs
//文件功能：普通的分页类
//
//创建标识：鲜红 || 2011-03-24
//
//修改标识：
//修改描述：
//**********************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.Framework.Web.Pager
{
    /// <summary>
    /// 普通的分页类
    /// </summary>
    public class NormalPageProc : IPageAble
    {

        #region fields
        /// <summary>
        /// 
        /// </summary>
        private int _StartPageIndex;

        /// <summary>
        /// 
        /// </summary>
        private int _EndPageIndex;

        /// <summary>
        /// 
        /// </summary>
        public PagerSettings PagerSetting { get; set; }
        #endregion

        #region Private Method

        
        private void Init()
        {
            _StartPageIndex = PagerSetting.CurrentPageIndex - (PagerSetting.NumericPagerCount / 2);
            if (_StartPageIndex + PagerSetting.NumericPagerCount > PagerSetting.PageCount)
                _StartPageIndex = PagerSetting.PageCount + 1 - PagerSetting.NumericPagerCount;
            if (_StartPageIndex < 1)
                _StartPageIndex = 1;

            _EndPageIndex = _StartPageIndex + PagerSetting.NumericPagerCount - 1;
            if (_EndPageIndex > PagerSetting.PageCount)
                _EndPageIndex = PagerSetting.PageCount;
        }

        private void AddFristButton(IList<PageButton> pageBtnList)
        {
            PageButton fristitem = new PageButton(PagerSetting.FirstPageText, 1, PagerSetting.CurrentPageIndex == 1, PageButtonType.FirstPageButton);
            pageBtnList.Add(fristitem);
        }

        private void AddPrevButton(IList<PageButton> pageBtnList)
        {
            var previtem = new PageButton(PagerSetting.PrevPageText, PagerSetting.CurrentPageIndex - 1, PagerSetting.CurrentPageIndex == 1, PageButtonType.PrevPageButton);
            pageBtnList.Add(previtem);
        }

        private void AddMoreButtonBefore(IList<PageButton> pageBtnList)
        {
            if (_StartPageIndex > 1 && PagerSetting.ShowMorePagerItems)
            {
                var index = _StartPageIndex - 1;
                if (index < 1) index = 1;
                PageButton item = new PageButton(PagerSetting.MorePageText, index, false, PageButtonType.MorePageButton);
                pageBtnList.Add(item);
            }
        }

        private void AddNumberButton(IList<PageButton> pageBtnList)
        {
            for (var pageIndex = _StartPageIndex; pageIndex <= _EndPageIndex; pageIndex++)
            {
                var text = pageIndex.ToString();
                if (pageIndex == PagerSetting.CurrentPageIndex && !string.IsNullOrEmpty(PagerSetting.CurrentPageNumberFormatString))
                    text = String.Format(PagerSetting.CurrentPageNumberFormatString, text);
                else if (!string.IsNullOrEmpty(PagerSetting.PageNumberFormatString))
                    text = String.Format(PagerSetting.PageNumberFormatString, text);
                var item = new PageButton(text, pageIndex, false, PageButtonType.NumericPageButton);
                pageBtnList.Add(item);
            }
        }

        private void AddMoreButtonAfter(IList<PageButton> pageBtnList)
        {
            if (_EndPageIndex < PagerSetting.PageCount)
            {
                var index = _StartPageIndex + PagerSetting.NumericPagerCount;
                if (index > PagerSetting.PageCount) index = PagerSetting.PageCount;
                var item = new PageButton(PagerSetting.MorePageText, index, false, PageButtonType.MorePageButton);
                pageBtnList.Add(item);
            }
        }

        private void AddNextButton(IList<PageButton> pageBtnList)
        {
            var nextitem = new PageButton(PagerSetting.NextPageText, PagerSetting.CurrentPageIndex + 1, PagerSetting.CurrentPageIndex >= PagerSetting.PageCount, PageButtonType.NextPageButton);
            pageBtnList.Add(nextitem);
        }

        private void AddLastButton(IList<PageButton> pageBtnList)
        {
            var lastitem = new PageButton(PagerSetting.LastPageText, PagerSetting.PageCount, PagerSetting.CurrentPageIndex >= PagerSetting.PageCount, PageButtonType.LastPageButton);
            pageBtnList.Add(lastitem);
        }


        private IList<PageButton> AddButtons()
        {
            IList<PageButton> pageBtnList = new List<PageButton>();
            AddFristButton(pageBtnList);        //<---添加第一页
            AddPrevButton(pageBtnList);         //<---添加前一页
            AddMoreButtonBefore(pageBtnList);   //<---添加更多按钮（前置）
            AddNumberButton(pageBtnList);       //<---添加数字分页按钮
            AddMoreButtonAfter(pageBtnList);    //<---添加更多按钮（后置）
            AddNextButton(pageBtnList);         //<---添加下一页
            AddLastButton(pageBtnList);         //<---添加最后一页
            IEnumerable<PageButton> currentPages = pageBtnList.Where(p => p.PageIndex == PagerSetting.CurrentPageIndex);
            foreach (PageButton btn in currentPages)
                btn.Disabled = true;
            return pageBtnList;
        }
        #endregion

        /// <summary>
        /// 一个按钮或者少于一个按钮的时候
        /// </summary>
        /// <returns></returns>
        public IList<PageButton> GetBtnLessThanOnePage()
        {
            return new List<PageButton>() { 
                new PageButton(PagerSetting.FirstPageText,1,true,PageButtonType.FirstPageButton)
            };
        }

        /// <summary>
        /// 第一页时
        /// </summary>
        /// <returns></returns>
        public IList<PageButton> GetBtnWhenFrist()
        {
            IList<PageButton> defaultPageButtons = GetBtnWhenDefault();
            defaultPageButtons.SingleOrDefault(m => m.ButtonType == PageButtonType.PrevPageButton).Hide = true;
            defaultPageButtons.SingleOrDefault(m => m.ButtonType == PageButtonType.FirstPageButton).Hide = true;
            return defaultPageButtons;
        }

        /// <summary>
        /// 最后一页时
        /// </summary>
        /// <returns></returns>
        public IList<PageButton> GetBtnWhenLast()
        {
            IList<PageButton> defaultPageButtons = GetBtnWhenDefault();
            defaultPageButtons.SingleOrDefault(m => m.ButtonType == PageButtonType.NextPageButton).Hide = true;
            defaultPageButtons.SingleOrDefault(m => m.ButtonType == PageButtonType.LastPageButton).Hide = true;
            return defaultPageButtons;
        }

        /// <summary>
        /// 默认情况
        /// </summary>
        /// <returns></returns>
        public IList<PageButton> GetBtnWhenDefault()
        {
            Init();
            return AddButtons();
        }

    }
}
