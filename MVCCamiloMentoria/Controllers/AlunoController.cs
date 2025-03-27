using Microsoft.AspNetCore.Mvc;
using MVCCamiloMentoria.Data;
using MVCCamiloMentoria.Models;

namespace MVCCamiloMentoria.Controllers
{
    public class AlunoController : Controller
    {
        private readonly EscolaContext _context;

        public AlunoController(EscolaContext context)
        {
            _context = context;
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        
        public IActionResult Create(Aluno aluno)
        {
            _context.alunos.Add(aluno);
            int affectedrows = _context.SaveChanges();
            if(affectedrows > 0)
            {
                TempData["SucessMessage"] = "Cadastro de aluno realizado com sucesso!";

            }
            return View(aluno);
        }


    }




}
