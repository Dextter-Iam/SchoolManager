using Microsoft.AspNetCore.Mvc;

namespace MVCCamiloMentoria.Controllers
{
    public class AlunoController : Controller
    {
        public IActionResult Create()
        {
            return View();
        }
    }
}
