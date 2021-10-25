using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Education.Data.Security
{
    public class TokenOptions
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }

        public int AccessTokenExpiration { get; set; }
        public int RefreshTokenExpiration { get; set; }

        public string SecurityKey { get; set; }
        public TokenOptions()
        {
            this.AccessTokenExpiration = 1;
            this.Audience = "";
            this.Issuer = "";
            this.RefreshTokenExpiration = 60;
            this.SecurityKey ="mysecuritykeymysecuritykeymysecuritykeymysecuritykey";
        }
    }
}
