using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCCamiloMentoria.Models;
using MVCCamiloMentoria.ViewModels;

namespace MVCCamiloMentoria.Controllers
{
    public class AulaController : Controller
    {
        private readonly EscolaContext _context;

        // Construtor
        public AulaController(EscolaContext context)
        {
            _context = context;
        }

        // GET: AulaController
        public async Task<IActionResult> Index()
        {
            var aulas = await _context.Aula
                .Include(a => a.Escola)
                .Include(a => a.Turma)
                .Include(a => a.Professor)
                .Include(a => a.Disciplina)
                .Select(a => new AulaViewModel
                {
                    Nome = a.Nome,
                    Id = a.Id,
                    EscolaId = a.EscolaId,
                    HorarioFim = a.HorarioFim,
                    HorarioInicio = a.HorarioInicio,
                    TurmaId = a.TurmaId,
                    ProfessorId = a.ProfessorId,
                    DisciplinaId = a.DisciplinaId
                }).ToListAsync();

            return View(aulas);
        }

        // GET: AulaController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var aula = await _context.Aula
                .Include(a => a.Escola)
                .Include(a => a.Turma)
                .Include(a => a.Professor)
                .Include(a => a.Disciplina)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (aula == null)
            {
                return NotFound();
            }

            return View(aula);
        }

        // GET: AulaController/Create
        public async Task<IActionResult> Create()
        {
            await CarregarViewBagsAsync();
            return View();
        }

        // POST: AulaController/Create
        [HttpPost]
        public async Task<IActionResult> Create(Aula aula)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    _context.Add(aula);


                    var turmaDisciplinaExistente = await _context.TurmaDisciplina
                        .FirstOrDefaultAsync(td => td.DisciplinaId == aula.DisciplinaId && td.TurmaId == aula.TurmaId);

                    if (turmaDisciplinaExistente == null)
                    {
                        var turmaDisciplina = new TurmaDisciplina
                        {
                            DisciplinaId = aula.DisciplinaId,
                            TurmaId = aula.TurmaId
                        };

                        _context.TurmaDisciplina.Add(turmaDisciplina);
                    }

                    // Salvando as alterações no banco de dados
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception)
                {
                    // Se houver erro, pode adicionar um log ou mensagem
                    throw;
                }
            }

            await CarregarViewBagsAsync(aula);
            return View(aula);
        }

        // GET: AulaController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var aula = await _context.Aula.FindAsync(id);
            if (aula == null)
            {
                return NotFound();
            }

            await CarregarViewBagsAsync(aula);
            return View(aula);
        }

        // POST: AulaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Aula aula)
        {
            if (id != aula.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(aula);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AulaExists(aula.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            await CarregarViewBagsAsync(aula);
            return View(aula);
        }

        // GET: AulaController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var aula = await _context.Aula
                .Include(a => a.Escola)
                .Include(a => a.Turma)
                .Include(a => a.Professor)
                .Include(a => a.Disciplina)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (aula == null)
            {
                return NotFound();
            }

            return View(aula);
        }

        // POST: AulaController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var aula = await _context.Aula.FindAsync(id);
            if (aula == null)
            {
                return NotFound();
            }

            _context.Aula.Remove(aula);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task CarregarViewBagsAsync(Aula aula = null)
        {
            var escolas = await _context.Escola.ToListAsync();
            var professores = await _context.Professor.ToListAsync();
            var turmas = await _context.Turma.ToListAsync();
            var disciplinas = await _context.Disciplina.ToListAsync();

            if (escolas == null || professores == null || turmas == null || disciplinas == null)
            {
                throw new Exception("Alguns dados necessários não foram carregados corretamente.");
            }

            ViewBag.EscolaId = new SelectList(_context.Escola, "Id", "Nome", aula?.EscolaId);
            ViewBag.TurmaId = new SelectList(_context.Turma, "TurmaId", "NomeTurma", aula?.TurmaId);
            ViewBag.Professores = new SelectList(professores, "Id", "Nome", aula?.ProfessorId);
            ViewBag.Disciplinas = new SelectList(disciplinas, "Id", "Nome", aula?.DisciplinaId);
        }

        private bool AulaExists(int id)
        {
            return _context.Aula.Any(e => e.Id == id);
        }
    }
}
