using System.ComponentModel.DataAnnotations;

namespace EcommerceAuth.model.entities
{
    public class ADirectory
    {
        [Key]
        public int ADUserId { get; set; }

        public string Email { get; set; }

        public string Pwd { get; set; }

        public string ADId { get; set; }

        public string FirtsName { get; set; }

        public string LastName { get; set; }

        public string UserName { get; set; }

        public string Position { get; set; }

        public string Area { get; set; }
    }
}
