using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCCamiloMentoria.Models;
using MVCCamiloMentoria.ViewModels;

namespace MVCCamiloMentoria.Controllers
{
    public class DisciplinaController : Controller
    {
        private readonly EscolaContext _context;

        public DisciplinaController(EscolaContext context)
        {
            _context = context;
        }

        // GET: Disciplinas
        public async Task<IActionResult> Index()
        {
            return View(await _context.Disciplina.Select(d => new DisciplinaViewModel { Nome = d.Nome, Id = d.Id }).ToListAsync());
        }

        // GET: Disciplinas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var disciplina = await _context.Disciplina
                .FirstOrDefaultAsync(m => m.Id == id);
            if (disciplina == null)
            {
                return NotFound();
            }

            var viewModel = new DisciplinaViewModel { Id = disciplina.Id, Nome = disciplina.Nome };
            return View(viewModel);
        }

        // GET: Disciplinas/Create
        public IActionResult Create()
        {
            CarregarViewBags();
            return View();
        }

        // POST: Disciplinas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DisciplinaViewModel disciplinaViewModel)
        {
            if (ModelState.IsValid)
            {
                var disciplina = new Disciplina 
                { Nome = disciplinaViewModel.Nome,
                  EscolaId = disciplinaViewModel.EscolaId
                };
                _context.Add(disciplina);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            CarregarViewBags(disciplinaViewModel);
            return View(disciplinaViewModel);
        }

        // GET: Disciplinas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var disciplina = await _context.Disciplina.FindAsync(id);
            if (disciplina == null)
            {
                return NotFound();
            }

            var viewModel = new DisciplinaViewModel { Id = disciplina.Id, Nome = disciplina.Nome };
            return View(viewModel);
        }

        // POST: Disciplinas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DisciplinaViewModel disciplinaViewModel)
        {
            if (id != disciplinaViewModel.Id)
            {
                return NotFound();

            }

            if (ModelState.IsValid)
            {
                try
                {
                    var disciplina = _context.Disciplina.Find(id);
                    if (disciplina != null)
                    {
                        disciplina.Nome = disciplinaViewModel.Nome;
                        _context.Update(disciplina);
                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DisciplinaExists(disciplinaViewModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(disciplinaViewModel);
        }

        // GET: Disciplinas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var disciplina = await _context.Disciplina
                .FirstOrDefaultAsync(m => m.Id == id);
            if (disciplina == null)
            {
                return NotFound();
            }

            var viewModel = new DisciplinaViewModel { Id = disciplina.Id, Nome = disciplina.Nome };
            return View(viewModel);
        }

        // POST: Disciplinas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var disciplina = await _context.Disciplina.FindAsync(id);
            if (disciplina != null)
            {
                _context.Disciplina.Remove(disciplina);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DisciplinaExists(int id)
        {
            return _context.Disciplina.Any(e => e.Id == id);
        }

        private void CarregarViewBags(DisciplinaViewModel viewModel = null)
        {
            ViewBag.EscolaId = new SelectList(_context.Escola, "Id", "Nome", viewModel?.EscolaId);
        }

    }
}
