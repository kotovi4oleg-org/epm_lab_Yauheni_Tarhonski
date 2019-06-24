using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TinyERP4Fun
{
    public static class Constants
    {
        public const string adminRoleName = "Admin";
        public const string rolesCommon_User = "Common_User, Common_Admin, Admin";
        public const string rolesCommon_Admin = "Common_Admin, Admin";
        public const string rolesExpences_User = "Expences_User, Expences_Admin, Admin";
        public const string rolesExpences_Admin = "Expences_Admin, Admin";
        public static readonly DateTime baseDate = new DateTime(2019, 5, 15);
        public const int maxImageSize = 400;

        public const string BYNCode = "BYN";
        public static readonly string BYN_CURRENCY_LIST_URL = "http://www.nbrb.by/API/ExRates/Currencies";
        public static readonly string BYN_CURRENCY_RATE_URL = "http://www.nbrb.by/API/ExRates/Rates/";
        public static readonly string BYN_ATTR_DATE = "?onDate=";


        public static readonly string defaultAdminName = "admin@gmail.com";
        public static readonly string adminFirstPwd = "_Aa321321";
        public static readonly string defaultPwd = "_Aa321321";
        public static readonly int pageSize = 10;
        public static readonly List<string> rolesList = new List<string> { "Common_Admin", "Common_User", "Expences_Admin", "Expences_User" };
    }
}
