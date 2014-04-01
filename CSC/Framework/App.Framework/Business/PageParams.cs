using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.Framework
{
    public class PageParams
    {
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public int SortField { get; set; }
		public List<KeyValuePair<int, SortDirectionEnum>> SecondSortFields { get; set; }
        public string SortFiledName { get; set; }
        public SortDirectionEnum sortDirection { get; set; }

        public bool SortEquals(PageParams p)
        {
            return p!=null && this.SortField == p.SortField && this.SortFiledName == p.SortFiledName &&
                   this.sortDirection == p.sortDirection;
        }
    }
}
