using DOMAIN.Abtracts;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DOMAIN.Models
{
    [Table("Sizes")]
    public class Size : Auditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public double Width { get; set; }

        public double Height { get; set; }

        public string Unit { get; set; } //cm, m, km.... :)
    }
}