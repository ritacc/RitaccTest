//**********************************************************
//Copyright(C)2011 By AIS版权所有。
//
//文件名：ServiceInvokeHelper.cs
//文件功能：Wcf服务调用辅助类
//
//创建标识：鲜红 || 2011-04-22
//
//修改标识：
//修改描述：
//**********************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace App.Framework.Web.Helper
{
    /// <summary>
    /// Wcf服务调用辅助类
    /// </summary>
    /// <typeparam name="TChannel"></typeparam>
    public static class ServiceInvokeHelper<TChannel> where TChannel : ICommunicationObject, new()
    {


        #region private fields
        private static Dictionary<string, TChannel> _ChannelDic = new Dictionary<string, TChannel>();
        private static object _Lockhelper = new object();
        #endregion

        #region private Method

        private static void Remove(string key)
        {
            lock (_Lockhelper)
                _ChannelDic.Remove(key);
        }

        private static TResult TryFunc<TResult>(Func<TChannel, TResult> func, TChannel channel)
        {
            string tChannelName = typeof(TChannel).FullName;
            try
            {
                return func(channel);
            }
            catch (CommunicationException)
            {
                channel.Abort();
                Remove(tChannelName);
                throw;
            }
            catch (TimeoutException)
            {
                channel.Abort();
                Remove(tChannelName);
                throw;
            }
            catch (Exception)
            {
                channel.Abort();
                Remove(tChannelName);
                throw;
            }
            finally
            {
                //channel.Close();
            }
        }

        private static TChannel GetChannel()
        {
            TChannel instance;
            string tChannelName = typeof(TChannel).FullName;
            lock (_Lockhelper)
            {
                if (!_ChannelDic.ContainsKey(tChannelName))
                {

                    instance = Activator.CreateInstance<TChannel>();
                    _ChannelDic.Add(tChannelName, instance);
                }

                else
                {
                    instance = _ChannelDic[tChannelName];
                }
            }
            if (instance.State != CommunicationState.Opened && instance.State != CommunicationState.Opening)
                instance.Open();
            return instance;
        }

        #endregion

        #region Helper Method

        /// <summary>
        /// 直接调用，无返回值
        /// </summary>
        /// <param name="action"></param>
        public static void Invoke(Action<TChannel> action)
        {
            TChannel instance = GetChannel();
            TryFunc(
                client =>
                {
                    action(client);
                    return (object)null;
                }
                , instance);

        }


        /// <summary>
        /// 有返回值的调用
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="func"></param>
        /// <returns></returns>
        public static TResult Invoke<TResult>(Func<TChannel, TResult> func)
        {
            TChannel instance = GetChannel();
            ICommunicationObject channel = instance as ICommunicationObject;
            TResult returnValue = default(TResult);
            returnValue = TryFunc(func, instance);
            return returnValue;
        }


        #endregion
    }

}
