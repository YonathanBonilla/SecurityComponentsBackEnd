using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceAuth.model.entities
{
    public class User_Rol
    {
        [Key]
        public int User_RolId { get; set; }

        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual Users Users { get; set; }

        public int RolId { get; set; }
        [ForeignKey("RolId")]
        public virtual Roles Roles { get; set; }

        public int FuncionalityId { get; set; }
        [ForeignKey("FuncionalityId")]
        public virtual Funcionality Funcionality { get; set; }
    }
}
