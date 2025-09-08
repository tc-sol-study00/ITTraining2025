using Microsoft.AspNetCore.Mvc;
using ASPMVCStudy.Models;

namespace ASPMVCStudy.Controllers {
    public class NameDataController : Controller {
        public NameDataController() {
        }
        // GET: Data
        public async Task<IActionResult> Index(string id) {

            NameDataViewModel nameDataViewModel = new NameDataViewModel() {
                InputOrDisplayFlag = NameDataViewModel.EnumInputOrDisplayFlag.Input,
                NameDatas = new List<NameData>() { new NameData() { Name = "名前", Address = "福岡" }, new NameData() { Name = "名前2", Address = "福岡2" } },
            };
            return View(nameDataViewModel);
        }

        //Post
        [HttpPost]
        public async Task<IActionResult> Index(string id, [Bind("NameDatas")] NameDataViewModel postedNameDataViewModel) {
            ModelState.Clear();

            NameDataViewModel nameDataViewModel = new NameDataViewModel() {
                InputOrDisplayFlag = NameDataViewModel.EnumInputOrDisplayFlag.Display,
                NameDatas = postedNameDataViewModel.NameDatas,
                Message = "Postで渡されたデータを表示しました",
            };
            return View(nameDataViewModel);
        }
    }
}
