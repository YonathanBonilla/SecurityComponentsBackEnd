using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceAuth.commons.domains
{
    public class RefreshTokenDto
    {
        public string TokenValue { get; set; }

        public string UserID { get; set; }

        public DateTime DateIssued { get; set; }

        public DateTime DateExpired { get; set; }
    }
}
