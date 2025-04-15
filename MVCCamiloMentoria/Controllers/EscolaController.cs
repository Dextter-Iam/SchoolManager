using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCCamiloMentoria.Models;
using MVCCamiloMentoria.ViewModels;
using System.Threading.Tasks;

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
            var escolas = await _context.Escola
                        .Include(e => e.Endereco)
                        .Include(e => e.Turmas)
                        .Include(e => e.Fornecedores)
                        .Include(e => e.PrestadorServico)
                        .Include(e => e.Telefones)
                        .Include(e => e.Equipamentos)
                        .Select(e => new EscolaViewModel
                        {
                            Nome = e.Nome,
                            Id = e.Id,
                        }).ToListAsync();

            return View(escolas);
        }

        // GET: EscolaController/Details/5
        public async Task<IActionResult> Details(int? id)
        { 
            if (id == null)
            {
                return NotFound();
            }

             var escolas = await _context.Escola
                        .Include(e => e.Endereco)
                        .Include(e => e.Turmas)
                        .Include(e => e.Fornecedores)
                        .Include(e => e.PrestadorServico)
                        .Include(e => e.Telefones)
                        .Include(e => e.Equipamentos)
                        .FirstOrDefaultAsync(e => e.Id == id);

            if(escolas == null)
            {
                return NotFound();
            }

            var escolaViewModel = new EscolaViewModel
            {
                Nome = escolas.Nome,
                Id = escolas.Id,
            };
                return View(escolaViewModel);
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
