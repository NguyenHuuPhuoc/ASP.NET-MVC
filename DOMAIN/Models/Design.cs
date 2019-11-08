using DOMAIN.Abtracts;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DOMAIN.Models
{
    [Table("Designs")]
    public class Design : Auditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(128)]
        public string Name { get; set; }

        [MaxLength(256)]
        public string Descreption { get; set; }

        public int ParentCategoryId { get; set; }

        [ForeignKey("ParentCategoryId")]
        public virtual Category Category { get; set; }
    }
}