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
        public static DateTime baseDate = new DateTime(2019, 5, 15);
        public const int maxImageSize = 400;

        public const string BYNCode = "BYN";
        //url списка валют
        public static string BYN_CURRENCY_LIST_URL = "http://www.nbrb.by/API/ExRates/Currencies";
        //при добавлении в конец внутреннего ИД валюты получим url курса конкретной валюты на сегодня
        public static string BYN_CURRENCY_RATE_URL = "http://www.nbrb.by/API/ExRates/Rates/";
        //атрибут даты курса валюты
        public static string BYN_ATTR_DATE = "?onDate=";
        //имя поля начала действия валюты
        /*public static string BYN_RECORD_CUR_DATE_BEGIN_NAME = "Cur_DateStart";
        //имя поля конца действия валюты
        public static string BYN_RECORD_CUR_DATE_END_NAME = "Cur_DateEnd";
        //имя поля аббривеатуры валюты
        public static string BYN_RECORD_CUR_ABBR_NAME = "Cur_Abbreviation";
        //имя поля ИД валюты
        public static string BYN_RECORD_CUR_ID_NAME = "Cur_ID";
        //имя поля базы курса валюты
        public static string BYN_RECORD_CUR_SCALE_NAME = "Cur_Scale";
        //имя поля курса валюты
        public static string BYN_RECORD_CUR_OFFRATE_NAME = "Cur_OfficialRate";
        */
        //базовая валюта
        //public static string BYN_BASE_CURRENCY = "BYN";





        public static string defaultAdminName = "admin@gmail.com";
        public static string adminFirstPwd = "_Aa321321";
        public static string defaultPwd = "_Aa321321";
        public static int pageSize = 10;
        public static List<string> rolesList = new List<string> { "Common_Admin", "Common_User", "Expences_Admin", "Expences_User" };
    }
}
