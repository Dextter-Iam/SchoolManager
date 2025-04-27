using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCCamiloMentoria.Models;
using MVCCamiloMentoria.ViewModels;

namespace MVCCamiloMentoria.Controllers
{
    public class ProfessorController : Controller
    {
        private readonly EscolaContext _context;

        public ProfessorController(EscolaContext context)
        {
            _context = context;
        }

        // GET: Professor
        public async Task<IActionResult> Index()
        {
            var professores = await _context.Professor
                .Include(p => p.Endereco)
                .Include(p => p.Escola)
                .Select(p => new ProfessorViewModel
                {
                    Id = p.Id,
                    Nome = p.Nome,
                    Matricula = p.Matricula,
                    Escola = p.Escola,
                    Endereco = p.Endereco
                }).ToListAsync();

            return View(professores);
        }

        // GET: Professor/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var professor = await _context.Professor
                .Include(p => p.Endereco)
                .Include(p => p.Escola)
                .Include(p => p.Aulas)
                .Include(p => p.Telefones)
                .Include(p => p.Turmas)
                .Include(p => p.Disciplinas)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (professor == null)
                return NotFound();

            var viewModel = new ProfessorViewModel
            {
                Id = professor.Id,
                Nome = professor.Nome,
                Matricula = professor.Matricula,
                Escola = professor.Escola,
                Endereco = professor.Endereco,
                Aulas = professor.Aulas,
                Telefones = professor.Telefones,
                Turmas = professor.Turmas,
                Disciplinas = professor.Disciplinas
            };

            return View(viewModel);
        }

        // GET: Professor/Create
        public IActionResult Create()
        {
            var viewModel = new ProfessorViewModel();
            CarregarDependencias(viewModel);
            return View(viewModel);
        }

        // POST: Professor/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProfessorViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var professor = new Professor
                    {
                        Nome = viewModel.Nome,
                        Matricula = viewModel.Matricula,
                        EscolaId = viewModel.EscolaId,
                        EnderecoId = viewModel.EnderecoId
                    };

                    _context.Add(professor);
                    await _context.SaveChangesAsync();

                    TempData["MensagemSucesso"] = "Professor cadastrado com sucesso!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["MensagemErro"] = $"Erro ao cadastrar professor: {ex.Message}";
                }
            }

            CarregarDependencias(viewModel);
            return View(viewModel);
        }

        // GET: Professor/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var professor = await _context.Professor
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (professor == null)
                return NotFound();

            var viewModel = new ProfessorViewModel
            {
                Id = professor.Id,
                Nome = professor.Nome,
                Matricula = professor.Matricula,
                EscolaId = professor.EscolaId,
                EnderecoId = professor.EnderecoId
            };

            CarregarDependencias(viewModel);
            return View(viewModel);
        }

        // POST: Professor/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProfessorViewModel viewModel)
        {
            if (id != viewModel.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var professor = await _context.Professor.FindAsync(id);
                    if (professor == null)
                        return NotFound();

                    professor.Nome = viewModel.Nome;
                    professor.Matricula = viewModel.Matricula;
                    professor.EscolaId = viewModel.EscolaId;
                    professor.EnderecoId = viewModel.EnderecoId;

                    _context.Update(professor);
                    await _context.SaveChangesAsync();

                    TempData["MensagemSucesso"] = "Professor atualizado com sucesso!";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProfessorExists(viewModel.Id))
                        return NotFound();
                    throw;
                }
                catch (Exception ex)
                {
                    TempData["MensagemErro"] = $"Erro ao editar professor: {ex.Message}";
                }
            }

            CarregarDependencias(viewModel);
            return View(viewModel);
        }

        // GET: Professor/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var professor = await _context.Professor
                .Include(p => p.Endereco)
                .Include(p => p.Escola)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (professor == null)
                return NotFound();

            var viewModel = new ProfessorViewModel
            {
                Id = professor.Id,
                Nome = professor.Nome,
                Matricula = professor.Matricula,
                Escola = professor.Escola,
                Endereco = professor.Endereco
            };

            return View(viewModel);
        }

        // POST: Professor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var professor = await _context.Professor.FindAsync(id);
                if (professor == null)
                {
                    TempData["MensagemErro"] = "Professor não encontrado.";
                    return RedirectToAction(nameof(Index));
                }

                _context.Professor.Remove(professor);
                await _context.SaveChangesAsync();

                TempData["MensagemSucesso"] = "Professor excluído com sucesso!";
            }
            catch (DbUpdateException)
            {
                TempData["MensagemErro"] = "Não foi possível excluir o professor. Verifique dependências.";
            }
            catch (Exception ex)
            {
                TempData["MensagemErro"] = $"Erro inesperado: {ex.Message}";
            }

            return RedirectToAction(nameof(Index));
        }

        private bool ProfessorExists(int id)
        {
            return _context.Professor.Any(e => e.Id == id);
        }

        private void CarregarDependencias(ProfessorViewModel viewModel = null)
        {
            ViewBag.EscolaId = new SelectList(_context.Escola, "Id", "Nome", viewModel?.EscolaId);
            ViewBag.EnderecoId = new SelectList(_context.Endereco, "Id", "NomeRua", viewModel?.EnderecoId);
        }
    }
}
