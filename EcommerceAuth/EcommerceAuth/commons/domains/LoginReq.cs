using System.ComponentModel.DataAnnotations;

namespace EcommerceAuth.commons.domains
{
    public class LoginReq
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Pwd {get; set;}
    }
}
