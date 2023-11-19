using System;
using System.ComponentModel.DataAnnotations;

namespace EcommerceAuth.model.entities
{
    public class RefreshTokens
    {
        [Key]
        public int RefreshTokenId { get; set; }

        public string UserID { get; set; }

        public DateTime DateIssued { get; set; }

        public DateTime DateExpired { get; set; }

        public bool IsUsed { get; set; }

        public bool IsActive { get; set; }

        public string TokenValue { get; set; }
    }
}
