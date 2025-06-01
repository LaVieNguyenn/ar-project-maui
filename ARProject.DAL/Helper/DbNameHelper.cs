namespace ARProject.DAL.Helper
{
    public static class DbNameHelper
    {
        public class DbName
        {
            public const string AUTH = "ar_clothing_authDB";
            public const string STORAGE = "ar_clothing";
        }

        public class DbCollectionName
        {
            public const string USER = "users";
        }

        public class DbConnectionString
        {
            public const string AUTH = "mongodb://10.0.2.2:27017/";
            public const string STORAGE = "";
            //mongodb://localhost:27017/
            //mongodb://10.0.2.2:27017/
        }
    }
}
