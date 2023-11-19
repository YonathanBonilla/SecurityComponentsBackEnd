using System;

namespace EcommerceAuth.commons.domains
{
    public class TokenInfo
    {
        //public string Sub { get; set; }
        public string TokenType { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public long Exp { get; set; }
        public long Nbf { get; set; }
        public long ExpRt { get; set; }
        public int  AccessTokenExpiresIn { get; set; }
        public int RefreshTokenExpiresIn { get; set; }
        public string UserId { get; set; }
    }
}
