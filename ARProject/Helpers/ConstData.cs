using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARProject.Helpers
{
    public static class ConstData
    {
        public class Api
        {
            public const string BASEURL = "http://10.0.2.2:5093";
            //http://localhost:5093
            //mongodb://10.0.2.2:5093/
            public const string LOGIN = "/api/v1/auth/login";
            // Nho tao cac string constant api o day nha!!!
        }

    }
}
