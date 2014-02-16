using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace Site.Helpers
{
    public class MyCache
    {
        private static object _obj = new object();
        private ReaderWriterLockSlim cacheLock = new ReaderWriterLockSlim();
        private static MyCache _instance;
        public static MyCache Instance
        {
            get
            {
                if(_instance == null)
                    _instance = new MyCache();
                return _instance;
            }
        
        }
        private MyCache()
        {
            _dict = new Dictionary<string, object>();
            _getter = new Getter();
        }

        private Dictionary<string, object> _dict;
        private IGetter _getter;

        public object Get(string key)
        {
            lock (_obj)
            {
                if (!_dict.ContainsKey(key))
                    _dict.Add(key, _getter.Get(key));
                return _dict[key];
            }
            
        }

        internal void Clear()
        {
            _dict.Clear();
        }

        public object FasterGet(string key)
        {
            cacheLock.EnterUpgradeableReadLock();

            if (!_dict.ContainsKey(key))
            {
                cacheLock.EnterWriteLock();
                _dict.Add(key, _getter.Get(key));
                cacheLock.ExitWriteLock();

            }

            var valToRet = _dict[key];
            cacheLock.ExitUpgradeableReadLock();

            return valToRet;
        }

        public object MediumFastGet(string key)
        {
            cacheLock.EnterReadLock();

            if (!_dict.ContainsKey(key))
            {
                //cacheLock.EnterWriteLock();
                _dict.Add(key, _getter.Get(key));
                //cacheLock.ExitWriteLock();

            }

            var valToRet = _dict[key];
            cacheLock.ExitReadLock();

            return valToRet;
        }
    }
}