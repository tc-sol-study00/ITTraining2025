using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASPEnshu.Models.DTOs {

    [Table("employee")]
    public class Employee {
        [Column("employee_no")]
        [DisplayName("従業員番号")]
        [MaxLength(5)]
        [Required]
        [Key]
        public string EmployeeNo { get; set; }

        [Column("employee_name")]
        [DisplayName("氏名")]
        [MaxLength(20)]
        [Required]
        public string EmployeeName { get; set; }

        [Column("current_address")]
        [DisplayName("住所")]
        [MaxLength(50)]
        [Required]
        public string CurrentAddress { get; set; }

        [Column("birthday")]
        [DisplayName("誕生日")]
        [Required]
        [DataType(DataType.Date)]
        public DateOnly BirthDay { get; set; }

        [Column("age")]
        [DisplayName("年齢")]
        [Range(0, 80)]
        [Required]
        public int Age { get; set; }

        [Column("department")]
        [DisplayName("所属")]
        [MaxLength(20)]
        [Required]
        public string Department { get; set; }
    }
}
