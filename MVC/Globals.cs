namespace MVC
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    
    public static class Globals
    {
        static Globals()
        {
            Globals.CurrentQuarter = "Fall";
            Globals.CurrentYear = "2011";
        }

        public static string CurrentYear { get; set; }

        public static string CurrentQuarter { get; set; }
    }
}