using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCCamiloMentoria.Models;
using MVCCamiloMentoria.ViewModels;

namespace MVCCamiloMentoria.Controllers
{
    public class ResponsavelController : Controller
    {
        private readonly EscolaContext _context;

        public ResponsavelController(EscolaContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var responsaveis = await _context.Responsavel
                .Include(r => r.Endereco)
                .Include(r => r.Telefones)
                .Include(r => r.AlunoResponsavel)
                .ToListAsync();

            return View(responsaveis);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var responsavel = await _context.Responsavel
                .Include(r => r.Endereco)
                .Include(r => r.Telefones)
                .Include(r => r.AlunoResponsavel!)
                    .ThenInclude(ar => ar.Aluno)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (responsavel == null)
                return NotFound();

            return View(responsavel);
        }

        public IActionResult Create()
        {
            CarregarViewBags();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,EnderecoId")] Responsavel responsavel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(responsavel);
                await _context.SaveChangesAsync();
                TempData["MensagemSucesso"] = "Responsável cadastrado com sucesso!";
                return RedirectToAction(nameof(Index));
            }
            CarregarViewBags();
            return View(responsavel);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var responsavel = await _context.Responsavel.FindAsync(id);
            if (responsavel == null)
                return NotFound();

            CarregarViewBags();
            return View(responsavel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,EnderecoId")] Responsavel responsavel)
        {
            if (id != responsavel.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(responsavel);
                    await _context.SaveChangesAsync();
                    TempData["MensagemSucesso"] = "Responsável atualizado com sucesso!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ResponsavelExists(responsavel.Id))
                        return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            CarregarViewBags();
            return View(responsavel);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var responsavel = await _context.Responsavel
                .Include(r => r.Endereco)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (responsavel == null)
                return NotFound();

            return View(responsavel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var responsavel = await _context.Responsavel.FindAsync(id);
            if (responsavel != null)
            {
                _context.Responsavel.Remove(responsavel);
                await _context.SaveChangesAsync();
                TempData["MensagemSucesso"] = "Responsável excluído com sucesso!";
            }
            return RedirectToAction(nameof(Index));
        }

        private bool ResponsavelExists(int id)
        {
            return _context.Responsavel.Any(e => e.Id == id);
        }

        private void CarregarViewBags()
        {
            ViewBag.EnderecoId = new SelectList(_context.Endereco, "Id", "NomeRua");
        }
    }
}