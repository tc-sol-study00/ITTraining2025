using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASPMVCStudy.Models {
    [Table("data")]
    public class NameData {

        [DisplayName("氏名")]
        [Column("name")]
        [MaxLength(20)]
        [Required]
        [Key]
        public string Name { get; set; }

        [DisplayName("住所")]
        [Column("address")]
        [MaxLength(50)]
        [Required]
        public string Address { get; set; }
    }
}
