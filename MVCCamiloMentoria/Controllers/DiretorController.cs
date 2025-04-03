using Microsoft.AspNetCore.Mvc;

namespace MVCCamiloMentoria.Controllers
{
    public class DiretorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
