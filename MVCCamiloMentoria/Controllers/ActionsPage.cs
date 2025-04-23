using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MVCCamiloMentoria.Controllers
{
    public class ActionsPage : Controller
    {
        // GET: ActionsPage
        public ActionResult Index()
        {
            return View();
        }

    }
}
