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
                    Escola = a.Escola,
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
                .Include(a => a.AlunosPresentes)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (aula == null)
            {
                return NotFound();
            }

            var viewModel = new AulaViewModel
            {
                Id = aula.Id,
                Nome = aula.Nome,
                HorarioInicio = aula.HorarioInicio,
                HorarioFim = aula.HorarioFim,
                EscolaId = aula.EscolaId,
                Escola = aula.Escola,
                TurmaId = aula.TurmaId,
                Turma = aula.Turma,
                ProfessorId = aula.ProfessorId,
                Professor = aula.Professor,
                DisciplinaId = aula.DisciplinaId,
                Disciplina = aula.Disciplina,
                ConfirmacaoPresenca = aula.ConfirmacaoPresenca,
                AlunosPresentes = aula.AlunosPresentes?.ToList()
            };

            return View(viewModel);
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
                    throw;
                }
            }

            await CarregarViewBagsAsync(aula);
            return View(aula);
        }

        // GET: AulaController/Edit/5
        public async Task<IActionResult> Edit(int id)
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

            var viewModel = new AulaViewModel
            {
                Id = aula.Id,
                Nome = aula.Nome,
                HorarioInicio = aula.HorarioInicio,
                HorarioFim = aula.HorarioFim,
                EscolaId = aula.EscolaId,
                ProfessorId = aula.ProfessorId,
                TurmaId = aula.TurmaId,
                DisciplinaId = aula.DisciplinaId,
                ConfirmacaoPresenca = aula.ConfirmacaoPresenca,
                Escola = aula.Escola,
                Professor = aula.Professor,
                Turma = aula.Turma,
                Disciplina = aula.Disciplina,
                AlunosPresentes = aula.AlunosPresentes?.ToList()
            };

            await CarregarViewBagsAsync(aula); 

            return View(viewModel);
        }

        // POST: AulaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AulaViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var aula = await _context.Aula.FindAsync(id);
                    if (aula == null)
                    {
                        return NotFound();
                    }
                    aula.Nome = viewModel.Nome;
                    aula.HorarioInicio = viewModel.HorarioInicio;
                    aula.HorarioFim = viewModel.HorarioFim;
                    aula.EscolaId = viewModel.EscolaId;
                    aula.ProfessorId = viewModel.ProfessorId;
                    aula.TurmaId = viewModel.TurmaId;
                    aula.DisciplinaId = viewModel.DisciplinaId;
                    aula.ConfirmacaoPresenca = viewModel.ConfirmacaoPresenca;

                    _context.Update(aula);
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AulaExists(viewModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            await CarregarViewBagsAsync();
            return View(viewModel);
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

            ViewBag.Escolas = escolas;
            ViewBag.Professores = professores;
            ViewBag.TurmaId = new SelectList(await _context.Turma.ToListAsync(), "TurmaId", "NomeTurma", aula?.TurmaId);
            ViewBag.Disciplinas = disciplinas;
        }


        private bool AulaExists(int id)
        {
            return _context.Aula.Any(e => e.Id == id);
        }
    }
}
