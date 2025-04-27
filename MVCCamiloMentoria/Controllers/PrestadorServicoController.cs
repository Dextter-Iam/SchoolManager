using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCCamiloMentoria.Models;
using MVCCamiloMentoria.ViewModels;

namespace MVCCamiloMentoria.Controllers
{
    public class PrestadorServicoController : Controller
    {
        private readonly EscolaContext _context;

        public PrestadorServicoController(EscolaContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var prestadores = await _context.PrestadorServico
                .Include(p => p.Escola)
                .Include(p => p.Telefones)
                .ToListAsync();

            return View(prestadores);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var prestador = await _context.PrestadorServico
                .Include(p => p.Escola)
                .Include(p => p.Telefones)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (prestador == null)
                return NotFound();

            return View(prestador);
        }

        public IActionResult Create()
        {
            ViewBag.EscolaId = new SelectList(_context.Escola, "Id", "Nome");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PrestadorServicoViewModel prestadorViewModel)
        {
            if (ModelState.IsValid)
            {

                var prestador = new PrestadorServico
                {
                    Nome = prestadorViewModel.Nome,
                    CNPJ = prestadorViewModel.CNPJ,
                    CPF = prestadorViewModel.CPF,
                    EmpresaNome = prestadorViewModel.EmpresaNome,
                    EscolaId = prestadorViewModel.EscolaId,
                    ServicoFinalidade = prestadorViewModel.ServicoFinalidade,
                    Escola = prestadorViewModel.Escola,
                };

                _context.Add(prestador);
                await _context.SaveChangesAsync();
                TempData["MensagemSucesso"] = "Prestador cadastrado com sucesso!";
                return RedirectToAction(nameof(Index));
            }
            ViewBag.EscolaId = new SelectList(_context.Escola, "Id", "Nome", prestadorViewModel.EscolaId);
            return View(prestadorViewModel);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var prestador = await _context.PrestadorServico.FindAsync(id);
            if (prestador == null)
                return NotFound();

            ViewBag.EscolaId = new SelectList(_context.Escola, "Id", "Nome", prestador.EscolaId);
            return View(prestador);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PrestadorServicoViewModel prestadorViewModel)
        {
            if (id != prestadorViewModel.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var prestador = new PrestadorServico
                    {   
                        Id = prestadorViewModel.Id,
                        Nome = prestadorViewModel.Nome,
                        CNPJ = prestadorViewModel.CNPJ,
                        CPF = prestadorViewModel.CPF,
                        EmpresaNome = prestadorViewModel.EmpresaNome,
                        EscolaId = prestadorViewModel.EscolaId,
                        ServicoFinalidade = prestadorViewModel.ServicoFinalidade,
                        Escola = prestadorViewModel.Escola,
                    };

                    _context.Update(prestador);
                    await _context.SaveChangesAsync();
                    TempData["MensagemSucesso"] = "Prestador atualizado com sucesso!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PrestadorExists(prestadorViewModel.Id))
                        return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewBag.EscolaId = new SelectList(_context.Escola, "Id", "Nome", prestadorViewModel.EscolaId);
            return View(prestadorViewModel);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var prestador = await _context.PrestadorServico
                .Include(p => p.Escola)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (prestador == null)
                return NotFound();

            return View(prestador);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var prestador = await _context.PrestadorServico.FindAsync(id);
            if (prestador != null)
            {
                _context.PrestadorServico.Remove(prestador);
                await _context.SaveChangesAsync();
                TempData["MensagemSucesso"] = "Prestador excluído com sucesso!";
            }
            return RedirectToAction(nameof(Index));
        }

        private bool PrestadorExists(int id)
        {
            return _context.PrestadorServico.Any(e => e.Id == id);
        }
    }
}