namespace ASPMVCStudy.Models {
    public class NameDataViewModel {

        public enum EnumInputOrDisplayFlag{
            Nothing=0,
            Input=1,
            Display=2,
        }

        public required EnumInputOrDisplayFlag InputOrDisplayFlag { get; set; }
        public required List<NameData> NameDatas { get; set; }
        public string Message { get; set; }
        public NameDataViewModel() {
            NameDatas = new List<NameData>();
            Message = string.Empty;
            InputOrDisplayFlag = EnumInputOrDisplayFlag.Nothing;
        }
    }
}
