using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Constants
{
    public class Common
    {
        public static class OrderProcess
        {
            public static String Success { get { return "Ordered Meals Successfully"; } }
            public static String Fail { get { return "Ordered Meals Unsuccessfully"; } }
            public static String Processing { get { return "Processing"; } }
        }

        public static class Roles
        { 
            public static String Admin { get { return "Admin"; } }
            public static String Member { get { return "Member"; } }

        }
    }
}
