using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseAutoMapper.DTOs {
    internal class EducationTotal {
        public string ClassCode { get; set; }
        public string SeitoNo { get; set; }
        public uint? KokugoScore { get; set; }
        public uint? SuugakuScore { get; set; }
        public uint? RikaScore { get; set; }
        public uint? TotalScore { get; set; }

    }
}
