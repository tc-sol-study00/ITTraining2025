namespace ASPMVCStudy.Models {
    public class NameDataViewModel {

        public enum EnumInputOrDisplayFlag{
            Input,
            Display,
        }

        public EnumInputOrDisplayFlag InputOrDisplayFlag { get; set; }
        public List<NameData> NameDatas { get; set; }
        public string Message { get; set; }
        public NameDataViewModel() {
            NameDatas = new List<NameData>();
            Message = string.Empty;
        }
    }
}
