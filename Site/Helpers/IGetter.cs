using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Site.Helpers
{
    interface IGetter
    {
        object Get(string key);
    }

    public class Getter : IGetter
    {

        
        public object Get(string key)
        {
            return Guid.NewGuid();
        }
    }
}
