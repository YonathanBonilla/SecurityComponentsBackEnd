using System.ComponentModel.DataAnnotations;

namespace EcommerceAuth.commons.domains
{
    public class ClientIdReq
    {
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "ClientId")]
        public string ClientId { get; set; }   
    }
}
