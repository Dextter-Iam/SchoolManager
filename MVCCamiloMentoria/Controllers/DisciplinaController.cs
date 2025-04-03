using Microsoft.AspNetCore.Mvc;

namespace MVCCamiloMentoria.Controllers
{
    public class DisciplinaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
