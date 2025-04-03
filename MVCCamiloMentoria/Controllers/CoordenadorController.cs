using Microsoft.AspNetCore.Mvc;

namespace MVCCamiloMentoria.Controllers
{
    public class CoordenadorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
