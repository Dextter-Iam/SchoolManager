using Microsoft.AspNetCore.Mvc;

namespace MVCCamiloMentoria.Controllers
{
    public class TurmaController : Controller
    {
        public IActionResult Create()
        {
            return View();
        }
    }
}
