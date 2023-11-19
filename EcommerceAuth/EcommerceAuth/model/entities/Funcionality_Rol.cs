using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace EcommerceAuth.model.entities
{
    public class Funcionality_Rol
    {
        [Key]
        public string Funcionality_RolId { get; set; }

        public int FuncionalityId { get; set; }
        [ForeignKey("FuncionalityId")]
        public virtual Funcionality Funcionality { get; set; }

        public int RolId { get; set; }
        [ForeignKey("RolId")]
        public virtual Roles Roles { get; set; }

        public bool Estado { get; set; }
    }
}
