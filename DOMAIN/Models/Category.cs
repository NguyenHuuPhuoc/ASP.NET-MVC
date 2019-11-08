using DOMAIN.Abtracts;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DOMAIN.Models
{
    [Table("Categories")]
    public class Category : Auditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(128)]
        [Column(TypeName = "varchar")]
        public string Code { get; set; }

        public int? ParentId { get; set; }

        [MaxLength(256)]
        public string Name { get; set; }

        [MaxLength(256)]
        public string Descreption { get; set; }

        //1 is products, 2 is news
        public int Type { get; set; }

        public virtual IEnumerable<Design> Designs { get; set; }
    }
}