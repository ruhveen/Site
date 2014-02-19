using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Site.Helpers
{
    public class MyTextHelper
    {
        private static MyTextHelper _instance;
        public static MyTextHelper Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new MyTextHelper();
                return _instance;
            }
        }

        public string EmploymentHistory
        {
            get
            {
                return @"This is an example paragraph,
                        Just to give you a tasete of the brand-new 
                        your-friendly-neighborhood-html5 feature 
                        Server side events
                        Very cool
                        Just not supported by IE :(
                        but when one comes to think of it..
                        What is supported by IE? :)
                        ";
            }
        }

    }
}