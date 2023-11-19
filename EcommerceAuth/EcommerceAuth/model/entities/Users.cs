using System.ComponentModel.DataAnnotations;

namespace EcommerceAuth.model.entities
{
    public class Users
    {
        [Key]
        public int UserId { get; set; }

        public string UserADId { get; set; }
    }
}
