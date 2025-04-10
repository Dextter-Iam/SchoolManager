using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCCamiloMentoria.Models;
using MVCCamiloMentoria.ViewModels;

namespace MVCCamiloMentoria.Controllers
{
    public class AlunoController : Controller
    {
        private readonly EscolaContext _context;

        public AlunoController(EscolaContext context)
        {
            _context = context;
        }

        // GET: Aluno
        public async Task<IActionResult> Index()
        {
            var alunos = await _context.Aluno
                .Include(a => a.Turma)
                .Include(a => a.Endereco)
                .Include(a => a.Escola)
                .Select(a => new AlunoViewModel
                {
                    NomeAluno = a.NomeAluno,
                    Id = a.Id
                }).ToListAsync();

            return View(alunos);
        }

        // GET: Aluno/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aluno = await _context.Aluno
                .Include(a => a.Turma)
                .Include(a => a.Endereco)
                .Include(a => a.Escola)
                .Include(a => a.Responsaveis)
                .Include(a => a.Aulas)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (aluno == null)
            {
                return NotFound();
            }

            var viewModel = new AlunoViewModel
            {
                Id = aluno.Id,
                NomeAluno = aluno.NomeAluno,
                Telefone = aluno.Telefone,
                DataNascimento = aluno.DataNascimento,
                EmailEscolar = aluno.EmailEscolar,
                AnoInscricao = aluno.AnoInscricao,
                BolsaEscolar = aluno.BolsaEscolar,
                Turma = aluno.Turma,
                Endereco = aluno.Endereco,
                Escola = aluno.Escola,
                Responsaveis = aluno.Responsaveis,
                Aulas = aluno.Aulas
            };

            return View(viewModel);
        }

        // GET: Aluno/Create
        public IActionResult Create()
        {
            CarregarViewBags();
            return View();
        }

        // POST: Aluno/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AlunoViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var aluno = new Aluno
                {
                    NomeAluno = viewModel.NomeAluno,
                    Telefone = viewModel.Telefone,
                    DataNascimento = viewModel.DataNascimento,
                    EmailEscolar = viewModel.EmailEscolar,
                    AnoInscricao = viewModel.AnoInscricao,
                    BolsaEscolar = viewModel.BolsaEscolar,
                    TurmaId = viewModel.TurmaId,
                    EnderecoId = viewModel.EnderecoId,
                    EscolaId = viewModel.EscolaId
                };

                _context.Add(aluno);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            CarregarViewBags(viewModel);
            return View(viewModel);
        }

        // GET: Aluno/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aluno = await _context.Aluno.FindAsync(id);
            if (aluno == null)
            {
                return NotFound();
            }

            var viewModel = new AlunoViewModel
            {
                Id = aluno.Id,
                NomeAluno = aluno.NomeAluno,
                Telefone = aluno.Telefone,
                DataNascimento = aluno.DataNascimento,
                EmailEscolar = aluno.EmailEscolar,
                AnoInscricao = aluno.AnoInscricao,
                BolsaEscolar = aluno.BolsaEscolar,
                TurmaId = aluno.TurmaId,
                EnderecoId = aluno.EnderecoId,
                EscolaId = aluno.EscolaId
            };

            CarregarViewBags(viewModel);
            return View(viewModel);
        }

        // POST: Aluno/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AlunoViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var aluno = new Aluno
                    {
                        Id = viewModel.Id,
                        NomeAluno = viewModel.NomeAluno,
                        Telefone = viewModel.Telefone,
                        DataNascimento = viewModel.DataNascimento,
                        EmailEscolar = viewModel.EmailEscolar,
                        AnoInscricao = viewModel.AnoInscricao,
                        BolsaEscolar = viewModel.BolsaEscolar,
                        TurmaId = viewModel.TurmaId,
                        EnderecoId = viewModel.EnderecoId,
                        EscolaId = viewModel.EscolaId
                    };

                    _context.Update(aluno);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlunoExists(viewModel.Id))
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

            CarregarViewBags(viewModel);
            return View(viewModel);
        }

        // GET: Aluno/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aluno = await _context.Aluno
                .Include(a => a.Turma)
                .Include(a => a.Endereco)
                .Include(a => a.Escola)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (aluno == null)
            {
                return NotFound();
            }

            return View(aluno);
        }

        // POST: Aluno/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var aluno = await _context.Aluno.FindAsync(id);
            if (aluno != null)
            {
                _context.Aluno.Remove(aluno);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AlunoExists(int id)
        {
            return _context.Aluno.Any(e => e.Id == id);
        }

        private void CarregarViewBags(AlunoViewModel viewModel = null)
        {
            ViewBag.EnderecoId = new SelectList(_context.Endereco, "Id", "Estado", viewModel?.EnderecoId);
            ViewBag.EscolaId = new SelectList(_context.Escola, "Id", "Nome", viewModel?.EscolaId);
            ViewBag.TurmaId = new SelectList(_context.Turma, "TurmaId", "NomeTurma", viewModel?.TurmaId); // Correção aqui
        }
    }
}
