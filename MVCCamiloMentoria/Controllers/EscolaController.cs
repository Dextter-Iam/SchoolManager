using Microsoft.AspNetCore.Mvc;

namespace MVCCamiloMentoria.Controllers
{
    public class EscolaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
