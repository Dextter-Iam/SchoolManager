using Microsoft.AspNetCore.Mvc;

namespace MVCCamiloMentoria.Controllers
{
    public class AuthUserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
