using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCCamiloMentoria.Models;
using MVCCamiloMentoria.ViewModels;

namespace MVCCamiloMentoria.Controllers
{
    public class CoordenadorController : Controller
    {
        private readonly EscolaContext _context;

        public CoordenadorController(EscolaContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var coordenadores = await _context.Coordenador
                .Include(c => c.Escola)
                .Include(c => c.Endereco)
                .Include(c => c.Telefones)
                .ToListAsync();

            return View(coordenadores);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var coordenador = await _context.Coordenador
                .Include(c => c.Escola)
                .Include(c => c.Endereco)
                .Include(c => c.Telefones)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (coordenador == null)
                return NotFound();

            return View(coordenador);
        }

        public IActionResult Create()
        {
            CarregarViewBags();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Matricula,EnderecoId,EscolaId")] Coordenador coordenador)
        {
            if (ModelState.IsValid)
            {
                _context.Add(coordenador);
                await _context.SaveChangesAsync();
                TempData["MensagemSucesso"] = "Coordenador cadastrado com sucesso!";
                return RedirectToAction(nameof(Index));
            }
            CarregarViewBags();
            return View(coordenador);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var coordenador = await _context.Coordenador.FindAsync(id);
            if (coordenador == null)
                return NotFound();

            CarregarViewBags();
            return View(coordenador);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Matricula,EnderecoId,EscolaId")] Coordenador coordenador)
        {
            if (id != coordenador.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(coordenador);
                    await _context.SaveChangesAsync();
                    TempData["MensagemSucesso"] = "Coordenador atualizado com sucesso!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CoordenadorExists(coordenador.Id))
                        return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            CarregarViewBags();
            return View(coordenador);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var coordenador = await _context.Coordenador
                .Include(c => c.Escola)
                .Include(c => c.Endereco)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (coordenador == null)
                return NotFound();

            return View(coordenador);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var coordenador = await _context.Coordenador.FindAsync(id);
            if (coordenador != null)
            {
                _context.Coordenador.Remove(coordenador);
                await _context.SaveChangesAsync();
                TempData["MensagemSucesso"] = "Coordenador excluído com sucesso!";
            }
            return RedirectToAction(nameof(Index));
        }

        private bool CoordenadorExists(int id)
        {
            return _context.Coordenador.Any(e => e.Id == id);
        }

        private void CarregarViewBags()
        {
            ViewBag.EscolaId = new SelectList(_context.Escola, "Id", "Nome");
            ViewBag.EnderecoId = new SelectList(_context.Endereco, "Id", "NomeRua");
        }
    }
}