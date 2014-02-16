using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Site.Controllers
{
    public class FacebookLists
    {
        public List<FacebookViewModel> OldList { get; set; }

        public List<FacebookViewModel> NewList { get; set; }


        public List<FacebookViewModel> DeletedMe { get; set; }
    }
}
