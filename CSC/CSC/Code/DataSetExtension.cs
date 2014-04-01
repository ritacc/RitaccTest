using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using App.Framework;
using System.Reflection;
using System.Data;

namespace CSC
{
    public class DataTableColumns
    {
        public object Column1 { get; set; }
        public object Column2 { get; set; }
        public object Column3 { get; set; }
        public object Column4 { get; set; }
        public object Column5 { get; set; }
        public object Column6 { get; set; }
        public object Column7 { get; set; }
        public object Column8 { get; set; }
        public object Column9 { get; set; }
        public object Column10 { get; set; }
        public object Column11 { get; set; }
        public object Column12 { get; set; }
        public object Column13 { get; set; }
        public object Column14 { get; set; }
        public object Column15 { get; set; }
        public object Column16 { get; set; }
        public object Column17 { get; set; }
        public object Column18 { get; set; }
        public object Column19 { get; set; }
        public object Column20 { get; set; }
        public object Column21 { get; set; }
        public object Column22 { get; set; }
        public object Column23 { get; set; }
        public object Column24 { get; set; }
        public object Column25 { get; set; }
        public object Column26 { get; set; }
        public object Column27 { get; set; }
        public object Column28 { get; set; }
        public object Column29 { get; set; }
        public object Column30 { get; set; }

    }

    public static class MemoryPage
    {


        public static DataTable ToDataTable(this List<DataRow> list)
        {
			if (list == null || list.Count <= 0)
				return null;
            var dt = new DataTable();
            var drone = list[0];

            for (int i = 0; i < drone.Table.Columns.Count; i++)
            {
                var column = new DataColumn(drone.Table.Columns[i].ColumnName, drone.Table.Columns[i].DataType);
                dt.Columns.Add(column);
            }
            list.ForEach(m =>
                             {
                                 var dr = dt.NewRow();
                                 m.BeginEdit();
                                 for (int i = 0; i < drone.Table.Columns.Count; i++)
                                 {
                                     dr[i] = m[i];
                                 }
                                 dt.Rows.Add(dr);
                             });
            return dt;
        }

        public static List<DataRow> ToTableList(this DataTable dt)
        {
            var list = new List<DataRow>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                list.Add(dt.Rows[i]);

            }
            return list;
        }

        public static List<DataTableColumns> ToList(this DataTable dt)
        {
            return dt.ToList<DataTableColumns>();
        }

        /// <summary>
        /// DataTable 转换为List 集合
        /// </summary>
        /// <typeparam name="TResult">类型</typeparam>
        /// <param name="dt">DataTable</param>
        /// <returns></returns>
        public static List<TResult> ToList<TResult>(this DataTable dt) where TResult : class,new()
        {
            //创建一个属性的列表
            List<PropertyInfo> prlist = new List<PropertyInfo>();
            //获取TResult的类型实例  反射的入口
            Type t = typeof(TResult);
            //获得TResult 的所有的Public 属性 并找出TResult属性和DataTable的列名称相同的属性(PropertyInfo) 并加入到属性列表 
            Array.ForEach<PropertyInfo>(t.GetProperties(), prlist.Add);
            //创建返回的集合
            List<TResult> oblist = new List<TResult>();

            foreach (DataRow row in dt.Rows)
            {
                //创建TResult的实例
                TResult ob = new TResult();
                //找到对应的数据  并赋值
                var i = 0;
                prlist.ForEach(p =>
                                   {

                                       if (i < row.Table.Columns.Count && row[i] != DBNull.Value)
                                           p.SetValue(ob, row[i], null);
                                       i++;
                                   });
                //放入到返回的集合中.
                oblist.Add(ob);
            }
            return oblist;
        }

        private static int Comparison<T>(Comparison<T> c, T x, T y, SortDirectionEnum sortDirection)
        {
            if (c == null)
                return 0;
            var xCopy = x;
            var yCopy = y;
            if (sortDirection == SortDirectionEnum.Desc)
            {
                var tmp = xCopy;
                xCopy = yCopy;
                yCopy = tmp;
            }
            return c(xCopy, yCopy);
        }

        public static List<T> Page<T>(this List<T> lst, PageParams pageParams, out int recordCount, List<Comparison<T>> comparisonList = null)
        {
            if (comparisonList != null && comparisonList.Count > 0)
            {
                lst.Sort((x, y) =>
                {
                    var result = 0;
                    if (pageParams.SortField >= 0)
                        result = Comparison(comparisonList[pageParams.SortField], x, y, pageParams.sortDirection);
                    if (pageParams.SecondSortFields != null)
                    {
                        int i = 0;
                        while (result == 0 && i < pageParams.SecondSortFields.Count)
                        {
                            var comp = pageParams.SecondSortFields[i];
                            if (comp.Key != pageParams.SortField)
                            {
                                result = Comparison(comparisonList[comp.Key], x, y, comp.Value);
                            }
                            i++;
                        }
                    }
                    return result;
                });
            }
            var items = lst.Skip((pageParams.PageIndex - 1) * pageParams.PageSize).Take(pageParams.PageSize);
            var bList = items.ToList();
            recordCount = lst.Count;
            return bList;
        }

    }
}