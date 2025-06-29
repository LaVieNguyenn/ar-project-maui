namespace ARProject.Helpers
{
    public static class ConstData
    {
        public class Api
        {
            public const string BASEURL = "http://10.0.2.2:5093";
            //http://localhost:5093
            //mongodb://10.0.2.2:5093/

            /// <summary>
            /// Login API URL
            /// </summary>
            public const string LOGIN = "/api/v1/auth/login";

            /// <summary>
            /// Register API URL
            /// </summary>
            public const string REGISTER = "/api/v1/auth/register";

            /// <summary>
            /// Base API Product URL
            /// </summary>
            public const string BASEPRODUCT = "/api/v1/product";
        }

    }
}
