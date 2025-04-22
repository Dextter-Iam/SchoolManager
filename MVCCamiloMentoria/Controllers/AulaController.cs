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
        public async Task<IActionResult> Create(AulaViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    var aulas = new Aula
                    {
                        Nome = viewModel.Nome,
                        HorarioInicio = viewModel.HorarioInicio,
                        HorarioFim = viewModel.HorarioFim,
                        EscolaId = viewModel.EscolaId,
                        TurmaId = viewModel.TurmaId,
                        ProfessorId = viewModel.ProfessorId,
                        DisciplinaId = viewModel.DisciplinaId,
                        ConfirmacaoPresenca = viewModel.ConfirmacaoPresenca
                    };
                    _context.Aula.Add(aulas);

                    var turmaDisciplinaExistente = await _context.TurmaDisciplina
                        .FirstOrDefaultAsync(td => td.DisciplinaId == viewModel.DisciplinaId && td.TurmaId == viewModel.TurmaId);

                    if (turmaDisciplinaExistente == null)
                    {
                        var turmaDisciplina = new TurmaDisciplina
                        {
                            DisciplinaId = viewModel.DisciplinaId,
                            TurmaId = viewModel.TurmaId
                        };

                        _context.TurmaDisciplina.Add(turmaDisciplina);
                    }

                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception)
                {
                    throw;
                }
            }

            await CarregarViewBagsAsync(viewModel);
            return View(viewModel);
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
                Turma = aula.Turma,
                TurmaId = aula.TurmaId,
                DisciplinaId = aula.DisciplinaId,
            };

            await CarregarViewBagsAsync();

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

            await CarregarViewBagsAsync(viewModel);
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

        private async Task CarregarViewBagsAsync(AulaViewModel viewModel = null)
        {
            var escolas = await _context.Escola.ToListAsync();
            var professores = await _context.Professor.ToListAsync();
            var turmas = await _context.Turma.ToListAsync();
            var disciplinas = await _context.Disciplina.ToListAsync();

            ViewBag.Escolas = new SelectList(
                escolas,
                "Id",
                "Nome",
                viewModel?.EscolaId ?? (escolas.Any() ? escolas.First().Id : (int?)null)
            );

            ViewBag.Professores = new SelectList(
                professores,
                "Id",
                "Nome",
                viewModel?.ProfessorId ?? (professores.Any() ? professores.First().Id : (int?)null)
            );

            ViewBag.Turmas = new SelectList(
                turmas,
                "TurmaId",
                "NomeTurma",
                viewModel?.TurmaId ?? (turmas.Any() ? turmas.First().TurmaId : (int?)null)
            );

            ViewBag.Disciplinas = new SelectList(
                disciplinas,
                "Id",
                "Nome",
                viewModel?.DisciplinaId ?? (disciplinas.Any() ? disciplinas.First().Id : (int?)null)
            );
        }



        private bool AulaExists(int id)
        {
            return _context.Aula.Any(e => e.Id == id);
        }
    }
}
