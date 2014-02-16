using Site.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Site.Controllers
{
    public class ReadWriteCacheController : ApiController
    {
        [HttpGet]
        public string Start(string numba, string reqNum)
        {
            TimeSpan now = DateTime.Now.TimeOfDay;
            for(int i=0;i<long.Parse(numba);i++)
            {
                MyCache.Instance.Get(i.ToString());
            }
            return now.ToString() + "reqNum: " + reqNum + " Finished Normal!";
        }

        [HttpGet]
        public string Clear()
        {
            
            MyCache.Instance.Clear();
            
            return "Cleared";
        }

        [HttpGet]
        public string FasterStart(string numba)
        {
            for (int i = 0; i < long.Parse(numba); i++)
            {
                MyCache.Instance.FasterGet(i.ToString());
            }
            return "Finished Faster!";
        }

        [HttpGet]
        public string MediumFastGet(string numba)
        {
            for (int i = 0; i < long.Parse(numba); i++)
            {
                MyCache.Instance.MediumFastGet(i.ToString());
            }
            return "Finished Medium!";
        }
    }
}
