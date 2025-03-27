using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCCamiloMentoria.Data;
using MVCCamiloMentoria.Models;

namespace MVCCamiloMentoria.Controllers
{
    public class TurmaController : Controller
    {
        private readonly EscolaContext _context;
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]

        public IActionResult Create(Turma turma)
        {
            _context.turmas.Add(turma);
            int affectedrows = _context.SaveChanges();
            if (affectedrows > 0)
            {
                TempData["SucessMessage"] = "Cadastro de turma realizado com sucesso!";

            }
            return View(turma);
        }
    }
}
