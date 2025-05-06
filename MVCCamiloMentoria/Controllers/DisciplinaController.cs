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
        public async Task<IActionResult> Index(int pagina = 1)
        {
            int registrosPorPagina = 10;
            var totalRegistros = await _context.Disciplina.CountAsync();
            var totalPaginas = (int)Math.Ceiling(totalRegistros / (double)registrosPorPagina);
            ViewBag.PaginaAtual = pagina;
            ViewBag.TotalPaginas = totalPaginas;

            return View(await _context.Disciplina
                                .Skip((pagina - 1) * registrosPorPagina)
                                .Take(registrosPorPagina)
                                .Select(d => new DisciplinaViewModel
                                {
                                    Nome = d.Nome,
                                    Id = d.Id

                                }).ToListAsync());


        }

        // GET: Disciplinas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var disciplina = await _context.Disciplina.FirstOrDefaultAsync(m => m.Id == id);
            if (disciplina == null)
            {
                return NotFound();
            }

            var viewModel = new DisciplinaViewModel
            {
                Id = disciplina.Id,
                Nome = disciplina.Nome,
                EscolaId = disciplina.EscolaId,
            };

            return View(viewModel);
        }

        // GET: Disciplinas/Create
        public IActionResult Create()
        {
            CarregarViewBags();
            return View();
        }

        // POST: Disciplinas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DisciplinaViewModel disciplinaViewModel)
        {
            if (ModelState.IsValid)
            {
                var disciplina = new Disciplina
                {
                    Nome = disciplinaViewModel.Nome,
                    EscolaId = disciplinaViewModel.EscolaId,
                };
                _context.Add(disciplina);
                await _context.SaveChangesAsync();

                TempData["MensagemSucesso"] = "Disciplina criada com sucesso!";
                return RedirectToAction(nameof(Index));
            }

            CarregarViewBags(disciplinaViewModel);
            TempData["MensagemErro"] = "Erro ao criar disciplina. Verifique os dados informados.";
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

            var viewModel = new DisciplinaViewModel
            {
                Id = disciplina.Id,
                Nome = disciplina.Nome,
            };

            CarregarViewBags(viewModel);
            return View(viewModel);
        }

        // POST: Disciplinas/Edit/5
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
                    var disciplina = await _context.Disciplina.FindAsync(id);
                    if (disciplina != null)
                    {
                        disciplina.Nome = disciplinaViewModel.Nome;
                        _context.Update(disciplina);
                        await _context.SaveChangesAsync();

                        TempData["MensagemSucesso"] = "Disciplina editada com sucesso!";
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

            CarregarViewBags(disciplinaViewModel);
            TempData["MensagemErro"] = "Erro ao editar disciplina. Verifique os dados informados.";
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

            var viewModel = new DisciplinaViewModel
            {
                Id = disciplina.Id,
                Nome = disciplina.Nome,
            };
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
                await _context.SaveChangesAsync();

                TempData["MensagemSucesso"] = "Disciplina excluída com sucesso!";
            }
            else
            {
                TempData["MensagemErro"] = "Erro ao excluir disciplina. Registro não encontrado.";
            }

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
