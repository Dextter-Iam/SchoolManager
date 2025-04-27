using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCCamiloMentoria.Models;
using MVCCamiloMentoria.ViewModels;

namespace MVCCamiloMentoria.Controllers
{
    public class DiretorController : Controller
    {
        private readonly EscolaContext _context;

        public DiretorController(EscolaContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var diretores = await _context.Diretor
                .Include(d => d.Escola)
                .Include(d => d.Endereco)
                .Include(d => d.Telefones)
                .ToListAsync();

            return View(diretores);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var diretor = await _context.Diretor
                .Include(d => d.Escola)
                .Include(d => d.Endereco)
                .Include(d => d.Telefones)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (diretor == null)
                return NotFound();

            return View(diretor);
        }

        public IActionResult Create()
        {
            CarregarViewBags();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Matricula,EnderecoId,EscolaId")] Diretor diretor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(diretor);
                await _context.SaveChangesAsync();
                TempData["MensagemSucesso"] = "Diretor cadastrado com sucesso!";
                return RedirectToAction(nameof(Index));
            }
            CarregarViewBags();
            return View(diretor);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var diretor = await _context.Diretor.FindAsync(id);
            if (diretor == null)
                return NotFound();

            CarregarViewBags();
            return View(diretor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Matricula,EnderecoId,EscolaId")] Diretor diretor)
        {
            if (id != diretor.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(diretor);
                    await _context.SaveChangesAsync();
                    TempData["MensagemSucesso"] = "Diretor atualizado com sucesso!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DiretorExists(diretor.Id))
                        return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            CarregarViewBags();
            return View(diretor);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var diretor = await _context.Diretor
                .Include(d => d.Escola)
                .Include(d => d.Endereco)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (diretor == null)
                return NotFound();

            return View(diretor);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var diretor = await _context.Diretor.FindAsync(id);
            if (diretor != null)
            {
                _context.Diretor.Remove(diretor);
                await _context.SaveChangesAsync();
                TempData["MensagemSucesso"] = "Diretor excluído com sucesso!";
            }
            return RedirectToAction(nameof(Index));
        }

        private bool DiretorExists(int id)
        {
            return _context.Diretor.Any(e => e.Id == id);
        }

        private void CarregarViewBags()
        {
            ViewBag.EscolaId = new SelectList(_context.Escola, "Id", "Nome");
            ViewBag.EnderecoId = new SelectList(_context.Endereco, "Id", "NomeRua");
        }
    }
}