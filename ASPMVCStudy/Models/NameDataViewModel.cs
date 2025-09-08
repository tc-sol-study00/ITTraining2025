namespace ASPMVCStudy.Models {
    public class NameDataViewModel {

        public enum EnumInputOrDisplayFlag{
            Nothing=0,
            Input=1,
            Display=2,
        }

        //initは初期化記述子しばり
        //requiredは、初期化が必ず必要で、方法としてはコンストラクタか、初期化記述し
        public required EnumInputOrDisplayFlag InputOrDisplayFlag { get; init; }
        public required List<NameData> NameDatas { get; init; }
        public string Message { get; set; }
        public NameDataViewModel() {
            NameDatas = new List<NameData>();
            Message = string.Empty;
            InputOrDisplayFlag = EnumInputOrDisplayFlag.Nothing;
        }
    }
}
