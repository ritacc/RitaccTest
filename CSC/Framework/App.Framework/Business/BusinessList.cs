using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.IO;
using System.Text;
using App.Framework.Data;
using App.Framework.Reflection;

namespace App.Framework
{
    public enum ExportTypes { CSV }

    [Serializable]
    public class BusinessList<T> : IList<T>, IDataObject where T : DataEntity
    {
        private PageParams CurrentPageParams { get; set; }


        public static BusinessList<T> Load(INullableDataReader data)
        {
            BusinessList<T> result = new BusinessList<T>();

            DataReaderDelegate builder = DataEntityBuilder.CreateDataReaderBuilder(typeof(T));
            T entity;
            while ((entity = (T)builder(data)) != null)
            {
                //entity = (T)builder(data);
                //if(entity == null) { break; }
                result.Add(entity);
            }
            return result;
        }


        private static int Comparison<C>(Comparison<C> c, C x, C y, SortDirectionEnum sortDirection)
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

        public BusinessList<T> Page(PageParams pageParams, out int recordCount, List<Comparison<T>> comparisonList = null)
        {
            if (comparisonList != null && comparisonList.Count > 0 &&  pageParams.SortField >= 0)
            {
                if (!pageParams.SortEquals(CurrentPageParams))
                {
                    m_InnerList.Sort((x, y) =>
                    {
                        var result = Comparison(comparisonList[pageParams.SortField], x, y, pageParams.sortDirection);
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
                    CurrentPageParams = pageParams;
                }
            }

            var items = m_InnerList.Skip((pageParams.PageIndex - 1) * pageParams.PageSize).Take(pageParams.PageSize);
            var bList = new BusinessList<T>();
            foreach (var item in items)
                bList.Add(item);
            recordCount = m_InnerList.Count;
            return bList;
        }

		/// <summary>
		/// 过滤列表 by lm
		/// </summary>
		/// <param name="func"></param>
		/// <returns></returns>
		public BusinessList<T> Where(Func<T, bool> func)
		{
			BusinessList<T> result = new BusinessList<T>();
			foreach (var item in m_InnerList)
			{
				if (func(item))
					result.Add(item);
			}
			return result;
		}



        /// <summary>
        /// Author : 涂江
        /// Date   : 2013-05-31
        /// Desc   : 仅用于数据列表排序
        /// </summary>
        /// <param name="pageParams"></param>
        /// <param name="comparisonList"></param>
        /// <returns></returns>
        public BusinessList<T> Sort(PageParams pageParams, List<Comparison<T>> comparisonList = null)
        {
            if (comparisonList != null && comparisonList.Count > 0 && pageParams.SortField >= 0)
            {
                if (!pageParams.SortEquals(CurrentPageParams))
                {
                    m_InnerList.Sort((x, y) =>
                    {
                        var result = Comparison(comparisonList[pageParams.SortField], x, y, pageParams.sortDirection);
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
                    CurrentPageParams = pageParams;
                }
            }

            var bList = new BusinessList<T>();
            foreach (var item in m_InnerList)
                bList.Add(item);

            return bList;
        }

        public int ExportToCSV(string fileName, List<string> fields, Type resourceType)
        {
            int result = 0;
            using (StreamWriter writer = new StreamWriter(fileName, false, Encoding.Unicode))
            {
                result = ExportToCSV(writer, fields, resourceType);
                writer.Close();
            }
            return result;
        }

        public int ExportToCSV(StreamWriter writer, List<string> fields, Type resourceType, bool haswrittenColHeader = false)
        {
            if (this.Count <= 0) return -1;
            StringBuilder stringBuilder = new StringBuilder();
            if (!haswrittenColHeader)
            {
                stringBuilder.AppendLine(this[0].TitleToCSV(fields, resourceType));
            }
            foreach (DataEntity entity in this)
            {
                string entityCSV = entity.ToCSV(fields, resourceType);
                if (!string.IsNullOrWhiteSpace(entityCSV))
                {
                    stringBuilder.AppendLine(entityCSV);
                }
            }
            writer.Write(stringBuilder.ToString());
            return 0;
        }

        #region 使用逗号分割（add by xj）
        public int ExportToCSVWithTab(string fileName, List<string> fields, Type resourceType)
        {
            int result = 0;
            using (StreamWriter writer = new StreamWriter(fileName, false, Encoding.Unicode))
            {
                result = ExportToCSVWithTab(writer, fields, resourceType);
                writer.Close();
            }
            return result;
        }

        public int ExportToCSVWithTab(StreamWriter writer, List<string> fields, Type resourceType)
        {
            if (this.Count <= 0) return -1;
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(this[0].TitleToCSVWithTab(fields, resourceType));
            foreach (DataEntity entity in this)
            {
                string entityCSV = entity.ToCSVWithTab(fields);
                if (!string.IsNullOrWhiteSpace(entityCSV))
                {
                    stringBuilder.AppendLine(entityCSV);
                }
            }
            writer.Write(stringBuilder.ToString());
            return 0;
        }
        #endregion


        #region IDataObject 成员

        public BusinessResult Save()
        {
            throw new NotImplementedException();
        }

        public BusinessResult Save(IDbTransaction trans)
        {
            throw new NotImplementedException();
        }

        public BusinessResult Delete()
        {
            throw new NotImplementedException();
        }

        public BusinessResult Delete(IDbTransaction trans)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IList<T> 成员

        private List<T> m_InnerList = new List<T>();

        public List<T> InnerList
        {
            get
            {
                return m_InnerList;
            }
        }

        public int IndexOf(T item)
        {
            return m_InnerList.IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            m_InnerList.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            m_InnerList.RemoveAt(index);
        }

        public T this[int index]
        {
            get { return m_InnerList[index]; }
            set { m_InnerList[index] = value; }
        }

        #endregion

        #region ICollection<T>

        public void Add(T item)
        {
            m_InnerList.Add(item);
        }

        public void Clear()
        {
            m_InnerList.Clear();
        }

        public bool Contains(T item)
        {
            return m_InnerList.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            m_InnerList.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return m_InnerList.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(T item)
        {
            return m_InnerList.Remove(item);
        }

        #endregion

        #region IEnumerable<T>

        public IEnumerator<T> GetEnumerator()
        {
            return m_InnerList.GetEnumerator();
        }

        #endregion

        #region IEnumerable

        IEnumerator IEnumerable.GetEnumerator()
        {
            return m_InnerList.GetEnumerator();
        }

        #endregion
    }

    //[Serializable]
    //public class BusinessList : IDataObject, IList
    //{
    //    public static BusinessList Load(INullableDataReader data)
    //    {
    //        BusinessList result = new BusinessList();

    //        return result;
    //    }

    //    #region IDataObject

    //    public BusinessResult Save()
    //    {
    //        BusinessResult result = new BusinessResult();
    //        using(IDbTransaction trans = DataPortal.BeginTransaction())
    //        {
    //            foreach(BusinessEntity entity in InnerList)
    //            {
    //                result = entity.Save(trans);
    //                if(result.ResultType <= 0) { break; }
    //            }
    //            if(result.ResultType == 0)
    //            {
    //                trans.Commit();
    //            }
    //            else
    //            {
    //                trans.Rollback();
    //            }
    //        }
    //        return result;
    //    }

    //    public BusinessResult Save(IDbTransaction trans)
    //    {
    //        BusinessResult result = new BusinessResult();
    //        foreach(BusinessEntity entity in InnerList)
    //        {
    //            result = entity.Save(trans);
    //            if(result.ResultType <= 0) { break; }
    //        }
    //        return result;
    //    }

    //    public BusinessResult Delete()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public BusinessResult Delete(IDbTransaction trans)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    #endregion

    //    #region IList

    //    private ArrayList _innerList = new ArrayList();

    //    protected ArrayList InnerList
    //    {
    //        get
    //        {
    //            if (_innerList == null)
    //            {
    //                _innerList = new ArrayList();
    //            }
    //            return _innerList;
    //        }
    //    }

    //    public bool IsFixedSize
    //    {
    //        get { return InnerList.IsFixedSize; }
    //    }

    //    public bool IsReadOnly
    //    {
    //        get { return InnerList.IsReadOnly; }
    //    }

    //    public bool Contains(BusinessEntity value)
    //    {
    //        return InnerList.Contains(value);
    //    }

    //    public int IndexOf(BusinessEntity value)
    //    {
    //        return InnerList.IndexOf(value);
    //    }

    //    public int Add(BusinessEntity value)
    //    {
    //        return InnerList.Add(value);
    //    }

    //    public void Insert(int index, BusinessEntity value)
    //    {
    //        InnerList.Insert(index, value);
    //    }

    //    public void Remove(BusinessEntity value)
    //    {
    //        InnerList.Remove(value);
    //    }

    //    public void RemoveAt(int index)
    //    {
    //        InnerList.RemoveAt(index);
    //    }

    //    public void Clear()
    //    {
    //        InnerList.Clear();
    //    }

    //    public BusinessEntity this[int index]
    //    {
    //        get { return (BusinessEntity)InnerList[index]; }
    //        set { InnerList[index] = value; }
    //    }

    //    bool IList.Contains(object value)
    //    {
    //        return InnerList.Contains(value);
    //    }

    //    int IList.IndexOf(object value)
    //    {
    //        return InnerList.IndexOf(value);
    //    }

    //    int IList.Add(object value)
    //    {
    //        return InnerList.Add(value);
    //    }

    //    void IList.Insert(int index, object value)
    //    {
    //        InnerList.Insert(index, value);
    //    }

    //    void IList.Remove(object value)
    //    {
    //        InnerList.Remove(value);
    //    }

    //    object IList.this[int index]
    //    {
    //        get { return InnerList[index]; }
    //        set { InnerList[index] = value; }
    //    }

    //    #endregion

    //    #region ICollection

    //    public int Count
    //    {
    //        get { return InnerList.Count; }
    //    }

    //    public bool IsSynchronized
    //    {
    //        get { return InnerList.IsSynchronized; }
    //    }

    //    public object SyncRoot
    //    {
    //        get { return InnerList.SyncRoot; }
    //    }

    //    public void CopyTo(Array array, int index)
    //    {
    //        InnerList.CopyTo(array, index);
    //    }

    //    #endregion

    //    #region IEnumerable

    //    public IEnumerator GetEnumerator()
    //    {
    //        return InnerList.GetEnumerator();
    //    }

    //    #endregion
    //}
}