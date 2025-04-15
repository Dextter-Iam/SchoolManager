using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCCamiloMentoria.Models;
using MVCCamiloMentoria.ViewModels;

namespace MVCCamiloMentoria.Controllers
{
    public class EscolaController : Controller
    {
        private readonly EscolaContext _context;

        public EscolaController(EscolaContext context)
        {
            _context = context;
        }
        // GET: EscolaController
        public async Task<IActionResult> Index()
        {
            var escola = await _context.Escola
                        .Select(e=> new EscolaViewModel
                        {
                            Nome = e.Nome,
                            Endereco = e.Endereco,
                            Professores = e.Professores,
                            Turmas = e.Turmas,
                            Fornecedores = e.Fornecedores,
                            PrestadorServico = e.PrestadorServico,
                            Telefones = e.Telefones,
                            Equipamentos = e.Equipamentos,
                            Id = e.Id,
                        }).ToListAsync();

            return View(escola);
        }

        // GET: EscolaController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: EscolaController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EscolaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EscolaController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: EscolaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EscolaController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: EscolaController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
