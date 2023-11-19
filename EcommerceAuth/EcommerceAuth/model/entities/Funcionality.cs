using System.ComponentModel.DataAnnotations;

namespace EcommerceAuth.model.entities
{
    public class Funcionality
    {
        [Key]
        public int FuncionalityId { get; set; }

        public string CodTramite { get; set; }

        public string NameFunc { get; set; }

        public string Codigo { get; set; }

        public bool Estado { get; set; }
    }
}
