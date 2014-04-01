using System;
using System.Collections.Generic;
using System.Data;
using App.Framework.Data;
using App.Framework.Reflection;
using App.Framework.Caching;

namespace App.Framework
{
    public static class BusinessPortal
    {
        private static ICacheProvider _cacheProvider;
        public static ICacheProvider CacheProvider { set { _cacheProvider = value; } }

        public static T Load<T>(DataCriteria criteria) where T : DataEntity
        {
            return Load(typeof(T), criteria) as T;
        }

        public static T Load<T>(DataCriteria criteria, IDbTransaction trans) where T : DataEntity
        {
            return Load(typeof(T), criteria,trans) as T;
        }

        public static DataEntity Load(Type entityType, DataCriteria criteria)
        {
            DataReaderDelegate builder = DataEntityBuilder.CreateDataReaderBuilder(entityType);
            return (DataEntity)DataPortal.ExecuteReader(criteria, builder);
        }

        public static DataEntity Load(Type entityType, DataCriteria criteria, IDbTransaction trans)
        {
            DataReaderDelegate builder = DataEntityBuilder.CreateDataReaderBuilder(entityType);
            return (DataEntity)DataPortal.ExecuteReader(criteria, trans, builder);
        }

        public static BusinessList<T> Search<T>(DataCriteria criteria) where T : DataEntity
        {
            DataReaderDelegate builder = BusinessList<T>.Load;
            var result = (BusinessList<T>)DataPortal.ExecuteReader(criteria, builder);
           
            return result;
        }

        public static BusinessList<T> Search<T>(DataCriteria criteria, IDbTransaction trans) where T : DataEntity
        {
            DataReaderDelegate builder = BusinessList<T>.Load;
            var result = (BusinessList<T>)DataPortal.ExecuteReader(criteria,trans, builder);

            return result;
        }

        /// <summary>
        /// 分页方法（使用内存分页）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="criteria"></param>
        /// <param name="pageParams"></param>
        /// <param name="recordCount"></param>
        /// <param name="comparisonList"></param>
        /// <returns></returns>
        public static BusinessList<T> Page<T>(DataCriteria criteria,PageParams pageParams,out int recordCount,List<Comparison<T>> comparisonList = null) where T : DataEntity
        {
            var list = Search<T>(criteria);
            return list.Page(pageParams, out recordCount, comparisonList);
        }

        public static BusinessList<T> Search<T>(Type entityType, DataCriteria criteria, IDbTransaction trans) where T : DataEntity
        {
            var builder = new DataReaderDelegate(BusinessList<T>.Load);
            var result = (BusinessList<T>)DataPortal.ExecuteReader(criteria, trans, builder);
           
            return result;
        }

        public static BusinessResult Save(IDataObject obj)
        {
            return obj.Save();
        }

        public static BusinessResult Save(IDataObject obj, IDbTransaction trans)
        {
            return obj.Save(trans);
        }

        public static BusinessResult Delete(IDataObject obj)
        {
            return obj.Delete();
        }

        public static BusinessResult Delete(IDataObject obj, IDbTransaction trans)
        {
            return obj.Delete(trans);
        }

        public static IDbTransaction BeginTransaction()
        {
            return DataPortal.BeginTransaction();
        }

        public static BusinessResult Execute(DataCriteria criteria)
        {
            return DataPortal.ExecuteNonQuery(criteria);
        }

        public static BusinessResult Execute(DataCriteria criteria, IDbTransaction trans)
        {
            return DataPortal.ExecuteNonQuery(criteria, trans);
        }
    }
}