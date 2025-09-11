
using GenericType.Interfaces;

namespace GenericType.DTOs {

    public  class ClassAttr : ISelect {
        public string ClassCode { get; set; }
        public string Tannin { get; set; }
        public string ListItem  => ClassCode + ":" + Tannin;
    }
}