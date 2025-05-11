using Microsoft.AspNetCore.Mvc;
using MVCCamiloMentoria.ViewModels;
using MVCCamiloMentoria.Data;
using MVCCamiloMentoria.Models;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Threading.Tasks;

namespace MVCCamiloMentoria.Controllers
{
    public class AuthUserController : Controller
    {
        private readonly EscolaContext _context;

        public AuthUserController(EscolaContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                TempData["Error"] = "Usuário ou senha inválidos!";
                return View();
            }

            var user = _context.AuthUser.FirstOrDefault(u => u.UserName == username);

            if (user == null)
            {
                TempData["Error"] = "Usuário não encontrado!";
                return View();
            }

            

            if (PasswordHasher.Verify(password, user.Password))
            {
                HttpContext.Session.SetString("UserName", user.UserName);
                TempData["Success"] = "Login realizado com sucesso!";
                return RedirectToAction("Index", "ActionsPage");
            }
            else
            {
                TempData["Error"] = "Senha incorreta!";
                return View();
            }
        }


        public IActionResult Logout()
        {
            HttpContext.Session.Remove("UserName");
            return RedirectToAction("Login");
        }
    }

}
