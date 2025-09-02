using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace EntityFrameworkStudy.Models { 

    [Table("education")]
    [PrimaryKey(nameof(ClassCode), nameof(SeitoNo))]
    public class Education {
        [Column("classcode")]
        public string ClassCode { get; set; }

        [Column("seitono")]
        public string SeitoNo { get; set; }

        [Column("kokugo")]
        public uint?  KokugoScore { get; set; }
        [Column("suugaku")]
        public uint? SuugakuScore { get; set; }
        [Column("rika")]
        public uint? RikaScore { get; set; }
      
        public ClassAttr? ClassAttr { get; set; }

    }
}
