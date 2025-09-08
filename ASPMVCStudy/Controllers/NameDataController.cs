using Microsoft.AspNetCore.Mvc;
using ASPMVCStudy.Models;

namespace ASPMVCStudy.Controllers
{
    public class NameDataController : Controller
    {
        //private readonly ASPMVCStudyContext _context;

        public NameDataController()
        {
        }

        // GET: Data
        public async Task<IActionResult> Index(string id)
        {
            NameDataViewModel model = new NameDataViewModel();
            model.SyoriFlg = NameDataViewModel.EnumShoriFlg.Input;
            model.NameDatas = new List<NameData>() { new NameData() { Name = "名前", Address = "福岡" }, new NameData() { Name = "名前2", Address = "福岡2" } };
            return View(model);
        }

        //Post

        [HttpPost]
        public async Task<IActionResult> Index(string id,[Bind("NameDatas")] NameDataViewModel postedNameDataViewModel) {
            ModelState.Clear();

            NameDataViewModel model = new NameDataViewModel();
            model.SyoriFlg = NameDataViewModel.EnumShoriFlg.Label;
            model.NameDatas = postedNameDataViewModel.NameDatas;
            model.Message = "Postで渡されたデータを表示しました";
            return View(model);
        }
    }
}
