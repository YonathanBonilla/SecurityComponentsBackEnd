using System.ComponentModel.DataAnnotations;

namespace EcommerceAuth.model.entities
{
    public class Roles
    {
        [Key]
        public int RolId { get; set; }

        public string RolName { get; set; }
        
        public string Codigo { get; set; }

        public bool Estado { get; set; }
    }
}
