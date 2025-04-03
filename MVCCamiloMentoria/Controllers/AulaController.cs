using Microsoft.AspNetCore.Mvc;

namespace MVCCamiloMentoria.Controllers
{
    public class AulaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
