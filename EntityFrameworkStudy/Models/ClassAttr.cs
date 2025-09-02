using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace EntityFrameworkStudy.Models {
    [Table("class_attr")]
    [PrimaryKey(nameof(ClassCode))]
    public  class ClassAttr {

        [Column("classcode")]
        public string ClassCode { get; set; }

        [Column("tannin")]
        public string Tannin { get; set; }
        public List<Education>? Educations { get; set; }
    }
}
