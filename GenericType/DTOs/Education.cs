

using GenericType.Interfaces;

namespace GenericType.DTOs {


    public class Education : ISelect {
        public string ClassCode { get; set; }

        public string SeitoNo { get; set; }

        public uint?  KokugoScore { get; set; }
        public uint? SuugakuScore { get; set; }
        public uint? RikaScore { get; set; }

        public string ListItem => ClassCode + ":" + SeitoNo;

    }
}
