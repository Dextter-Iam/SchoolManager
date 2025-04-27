using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCCamiloMentoria.Models;
using MVCCamiloMentoria.ViewModels;

namespace MVCCamiloMentoria.Controllers
{
    public class SupervisorController : Controller
    {
        private readonly EscolaContext _context;

        public SupervisorController(EscolaContext context)
        {
            _context = context;
        }

        // GET: Supervisor
        public async Task<IActionResult> Index()
        {
            var supervisores = await _context.Supervisor
                .Include(s => s.Endereco)
                .Include(s => s.Escolas)
                .Select(s => new SupervisorViewModel
                {
                    Id = s.Id,
                    Nome = s.Nome,
                    Matricula = s.Matricula,
                    Endereco = s.Endereco,
                    Escolas = s.Escolas
                }).ToListAsync();

            return View(supervisores);
        }

        // GET: Supervisor/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var supervisor = await _context.Supervisor
                .Include(s => s.Endereco)
                .Include(s => s.Escolas)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (supervisor == null)
                return NotFound();

            var viewModel = new SupervisorViewModel
            {
                Id = supervisor.Id,
                Nome = supervisor.Nome,
                Matricula = supervisor.Matricula,
                Endereco = supervisor.Endereco,
                Escolas = supervisor.Escolas
            };

            return View(viewModel);
        }

        // GET: Supervisor/Create
        public IActionResult Create()
        {
            var viewModel = new SupervisorViewModel();
            CarregarDependencias();
            return View(viewModel);
        }

        // POST: Supervisor/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SupervisorViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var supervisor = new Supervisor
                    {
                        Nome = viewModel.Nome,
                        Matricula = viewModel.Matricula,
                        EnderecoId = viewModel.EnderecoId
                    };

                    _context.Add(supervisor);
                    await _context.SaveChangesAsync();

                    TempData["MensagemSucesso"] = "Supervisor cadastrado com sucesso!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["MensagemErro"] = $"Erro ao cadastrar supervisor: {ex.Message}";
                }
            }

            CarregarDependencias();
            return View(viewModel);
        }

        // GET: Supervisor/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var supervisor = await _context.Supervisor
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (supervisor == null)
                return NotFound();

            var viewModel = new SupervisorViewModel
            {
                Id = supervisor.Id,
                Nome = supervisor.Nome,
                Matricula = supervisor.Matricula,
                EnderecoId = supervisor.EnderecoId
            };

            CarregarDependencias();
            return View(viewModel);
        }

        // POST: Supervisor/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SupervisorViewModel viewModel)
        {
            if (id != viewModel.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var supervisor = await _context.Supervisor.FindAsync(id);
                    if (supervisor == null)
                        return NotFound();

                    supervisor.Nome = viewModel.Nome;
                    supervisor.Matricula = viewModel.Matricula;
                    supervisor.EnderecoId = viewModel.EnderecoId;

                    _context.Update(supervisor);
                    await _context.SaveChangesAsync();

                    TempData["MensagemSucesso"] = "Supervisor atualizado com sucesso!";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SupervisorExists(viewModel.Id))
                        return NotFound();
                    throw;
                }
                catch (Exception ex)
                {
                    TempData["MensagemErro"] = $"Erro ao editar supervisor: {ex.Message}";
                }
            }

            CarregarDependencias();
            return View(viewModel);
        }

        // GET: Supervisor/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var supervisor = await _context.Supervisor
                .Include(s => s.Endereco)
                .Include(s => s.Escolas)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (supervisor == null)
                return NotFound();

            var viewModel = new SupervisorViewModel
            {
                Id = supervisor.Id,
                Nome = supervisor.Nome,
                Matricula = supervisor.Matricula,
                Endereco = supervisor.Endereco,
                Escolas = supervisor.Escolas
            };

            return View(viewModel);
        }

        // POST: Supervisor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var supervisor = await _context.Supervisor.FindAsync(id);
                if (supervisor == null)
                {
                    TempData["MensagemErro"] = "Supervisor não encontrado.";
                    return RedirectToAction(nameof(Index));
                }

                _context.Supervisor.Remove(supervisor);
                await _context.SaveChangesAsync();

                TempData["MensagemSucesso"] = "Supervisor excluído com sucesso!";
            }
            catch (DbUpdateException)
            {
                TempData["MensagemErro"] = "Não foi possível excluir o supervisor. Verifique dependências.";
            }
            catch (Exception ex)
            {
                TempData["MensagemErro"] = $"Erro inesperado: {ex.Message}";
            }

            return RedirectToAction(nameof(Index));
        }

        private bool SupervisorExists(int id)
        {
            return _context.Supervisor.Any(e => e.Id == id);
        }

        private void CarregarDependencias()
        {
            ViewBag.EnderecoId = new SelectList(_context.Endereco, "Id", "NomeRua");
        }
    }
}
