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
    public class AlunoController : Controller
    {
        private readonly EscolaContext _context;

        public AlunoController(EscolaContext context)
        {
            _context = context;
        }

        // GET: Alunos
        public async Task<IActionResult> Index()
        {
            return View(await _context.Alunos.Select(a => 
                                       new AlunoViewModel { Id = a.Id, NomeAluno = a.NomeAluno, Turma = a.Turma, Endereco = a.Endereco }).ToListAsync());
        }

        // GET: Alunos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aluno = await _context.Alunos
                .Include(a => a.Endereco)
                .Include(a => a.Escola)
                .Include(a => a.Turma)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aluno == null)
            {
                return NotFound();
            }

            var viewModel = new AlunoViewModel 
            { Id = aluno.Id, NomeAluno = aluno.NomeAluno, Responsaveis = aluno.Responsaveis, DataNascimento = aluno.DataNascimento, BolsaEscolar = aluno.BolsaEscolar};
            return View(viewModel);
        }

        // GET: Alunos/Create
        public IActionResult Create()
        {
            ViewData["EnderecoId"] = new SelectList(_context.Enderecos, "EnderecoId", "Cidade");
            ViewData["EscolaId"] = new SelectList(_context.Escolas, "Id", "Nome");
            ViewData["TurmaId"] = new SelectList(_context.Turmas, "TurmaId", "NomeTurma");

            return View();
        }

        // POST: Alunos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AlunoViewModel alunoViewModel)
        {
            if (ModelState.IsValid)
            {
                var aluno = new AlunoViewModel
                { NomeAluno = alunoViewModel.NomeAluno, Endereco = alunoViewModel.Endereco, Turma = alunoViewModel.Turma};
                _context.Add(aluno);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["EnderecoId"] = new SelectList(_context.Enderecos, "EnderecoId", "Cidade", alunoViewModel.EnderecoId);
            ViewData["EscolaId"] = new SelectList(_context.Escolas, "Id", "Nome", alunoViewModel.EscolaId);
            ViewData["TurmaId"] = new SelectList(_context.Turmas, "TurmaId", "NomeTurma", alunoViewModel.TurmaId);
            
            return View(alunoViewModel);
        }

        // GET: Alunos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aluno = await _context.Alunos.FindAsync(id);
            if (aluno == null)
            {
                return NotFound();
            }
            ViewData["EnderecoId"] = new SelectList(_context.Enderecos, "EnderecoId", "Cidade", aluno.EnderecoId);
            ViewData["EscolaId"] = new SelectList(_context.Escolas, "Id", "Nome", aluno.EscolaId);
            ViewData["TurmaId"] = new SelectList(_context.Turmas, "TurmaId", "NomeTurma", aluno.TurmaId);
            return View(aluno);
        }

        // POST: Alunos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NomeAluno,Telefone,DataNascimento,EmailEscolar,AnoInscricao,BolsaEscolar,TurmaId,EnderecoId,EscolaId")] Aluno aluno)
        {
            if (id != aluno.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(aluno);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlunoExists(aluno.Id))
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
            ViewData["EnderecoId"] = new SelectList(_context.Enderecos, "EnderecoId", "Cidade", aluno.EnderecoId);
            ViewData["EscolaId"] = new SelectList(_context.Escolas, "Id", "Nome", aluno.EscolaId);
            ViewData["TurmaId"] = new SelectList(_context.Turmas, "TurmaId", "NomeTurma", aluno.TurmaId);
            return View(aluno);
        }

        // GET: Alunos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aluno = await _context.Alunos
                .Include(a => a.Endereco)
                .Include(a => a.Escola)
                .Include(a => a.Turma)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aluno == null)
            {
                return NotFound();
            }

            return View(aluno);
        }

        // POST: Alunos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var aluno = await _context.Alunos.FindAsync(id);
            if (aluno != null)
            {
                _context.Alunos.Remove(aluno);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AlunoExists(int id)
        {
            return _context.Alunos.Any(e => e.Id == id);
        }
    }
}
