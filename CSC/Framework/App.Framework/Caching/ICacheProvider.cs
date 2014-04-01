using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.Framework.Caching
{
    public interface ICacheProvider
    {
        void Add(string key, object obj);
        void Remove(string key);
        T GetCache<T>(string key);
    }
}
