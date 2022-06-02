using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeroesApi.Models
{
    public class Appsettings
    {
        public class AppSettings
        {
            public string Secret { get; set; }
            public string ElectionsDayManagmentBaseUrl { get; set; }
            public string TokenFilePath { get; set; }
            public Jwt Jwt { get; set; }
        }
        public class Jwt
        {
            public string Key { get; set; }
            public string Issuer { get; set; }
            public string Audience { get; set; }
        }
    }
}
