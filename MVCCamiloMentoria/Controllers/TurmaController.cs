using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCCamiloMentoria.Models;
using MVCCamiloMentoria.ViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace MVCCamiloMentoria.Controllers
{
    public class TurmaController : Controller
    {
        private readonly EscolaContext _context;

        public TurmaController(EscolaContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var turmas = await _context.Turma
                .Include(t => t.Escola)                 
                .Include(t => t.Alunos)                 
                .Include(t => t.Professores)            
                .Include(t => t.TurmaDisciplinas)        
                    .ThenInclude(td => td.Disciplina)     
                .ToListAsync();

            var turmaViewModels = turmas.Select(t => new TurmaViewModel
            {
                TurmaId = t.TurmaId,
                NomeTurma = t.NomeTurma,
                AnoLetivo = t.AnoLetivo,
                Turno = t.Turno,
                Alunos = t.Alunos,
                EscolaId = t.EscolaId,
                Escola = t.Escola,
            }).ToList();

            return View(turmaViewModels);
        }

        // GET: Turmas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var turma = await _context.Turma
                .Include(t => t.Escola)
                .Include(t => t.Alunos)
                .Include(t => t.TurmaDisciplinas)
                .ThenInclude(td => td.Disciplina)
                .FirstOrDefaultAsync(m => m.TurmaId == id);

            if (turma == null)
            {
                return NotFound();
            }

            var turmaViewModel = new TurmaViewModel
            {
                TurmaId = turma.TurmaId,
                NomeTurma = turma.NomeTurma,
                AnoLetivo = turma.AnoLetivo,
                Turno = turma.Turno,
                EscolaId = turma.EscolaId,
                Escola = turma.Escola,
                Aulas = turma.Aulas,
                Alunos = turma.Alunos,
                Professores = turma.Professores,
                TurmaDisciplinas = turma.TurmaDisciplinas
            };

            return View(turmaViewModel);
        }

        // GET: Turmas/Create
        public async Task<IActionResult> Create()
        {
            await CarregarViewBagsAsync();
            return View();
        }

        // POST: Turmas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TurmaViewModel turma)
        {
            if (ModelState.IsValid)
            {
                var novaTurma = new Turma
                {
                    NomeTurma = turma.NomeTurma,
                    AnoLetivo = turma.AnoLetivo,
                    Turno = turma.Turno,
                    EscolaId = turma.EscolaId
                };

                _context.Add(novaTurma);
                await _context.SaveChangesAsync();


                foreach (var disciplina in turma.TurmaDisciplinas)
                {
                    _context.Add(new TurmaDisciplina
                    {
                        TurmaId = novaTurma.TurmaId,
                        DisciplinaId = disciplina.DisciplinaId
                    });
                }

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            await CarregarViewBagsAsync(turma);
            return View(turma);
        }

        // GET: Turmas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var turma = await _context.Turma.FindAsync(id);
            if (turma == null)
            {
                return NotFound();
            }

            var turmaViewModel = new TurmaViewModel
            {
                TurmaId = turma.TurmaId,
                NomeTurma = turma.NomeTurma,
                AnoLetivo = turma.AnoLetivo,
                Turno = turma.Turno,
                EscolaId = turma.EscolaId
            };

            await CarregarViewBagsAsync(turmaViewModel);
            return View(turmaViewModel);
        }

        // POST: Turmas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TurmaViewModel turma)
        {
            if (id != turma.TurmaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var turmaExistente = await _context.Turma.FindAsync(id);
                    if (turmaExistente == null)
                    {
                        return NotFound();
                    }

                    turmaExistente.NomeTurma = turma.NomeTurma;
                    turmaExistente.AnoLetivo = turma.AnoLetivo;
                    turmaExistente.Turno = turma.Turno;
                    turmaExistente.EscolaId = turma.EscolaId;

                    _context.Update(turmaExistente);
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TurmaExists(turma.TurmaId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return View(turma);
        }

        // GET: Turma/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var turma = await _context.Turma
                .Include(t => t.Escola)
                .Include(t => t.Alunos)
                .Include(t => t.TurmaDisciplinas)
                    .ThenInclude(td => td.Disciplina)
                .FirstOrDefaultAsync(t => t.TurmaId == id);

            if (turma == null)
            {
                return NotFound();
            }

            // Mapeamento manual
            var turmaViewModel = new TurmaViewModel
            {
                TurmaId = turma.TurmaId,
                NomeTurma = turma.NomeTurma,
                AnoLetivo = turma.AnoLetivo,
                Turno = turma.Turno,
                Escola = turma.Escola,
                Alunos = turma.Alunos!.ToList(),
                TurmaDisciplinas = turma.TurmaDisciplinas.ToList()
            };

            return View(turmaViewModel);
        }


        // POST: Turma/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var turma = await _context.Turma
                .Include(t => t.Escola)
                .Include(t => t.Alunos)
                .Include(t => t.TurmaDisciplinas)
                    .ThenInclude(td => td.Disciplina)
                .FirstOrDefaultAsync(t => t.TurmaId == id);

            if (turma == null)
            {
                return NotFound();
            }

            _context.Turma.Remove(turma);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private async Task CarregarViewBagsAsync(TurmaViewModel viewModel = null)
        {
            var escolas = await _context.Escola.ToListAsync();
            var disciplinas = await _context.Disciplina.ToListAsync();

            ViewBag.Escolas = new SelectList(escolas, "Id", "Nome", viewModel?.EscolaId ?? escolas.FirstOrDefault()?.Id);
            ViewBag.Disciplinas = new SelectList(disciplinas, "Id", "Nome");
        }

        private bool TurmaExists(int id)
        {
            return _context.Turma.Any(e => e.TurmaId == id);
        }
    }
}
