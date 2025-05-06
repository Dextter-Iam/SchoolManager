using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
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

        public async Task<IActionResult> Index(int pagina = 1)
        {
            int registrosPorPagina = 10;
            var totalRegistros = await _context.Turma.CountAsync();
            var totalPaginas = (int)Math.Ceiling(totalRegistros / (double)registrosPorPagina);

            var turmas = await _context.Turma
                .Include(t => t.Escola)
                .Include(t => t.Alunos)
                .Include(t => t.Professores)
                .Include(t => t.TurmaDisciplinas)
                    .ThenInclude(td => td.Disciplina)
                .Skip((pagina - 1) * registrosPorPagina)
                .Take(registrosPorPagina)
                .ToListAsync();

            var turmaViewModels = turmas.Select(t => new TurmaViewModel
            {
                TurmaId = t.TurmaId,
                NomeTurma = t.NomeTurma,
                AnoLetivo = t.AnoLetivo,
                Turno = t.Turno,
                Alunos = t.Alunos!
                          .Select(a => new AlunoViewModel
                          {
                              Nome = a.Nome,
                              Id = a.Id,
                          }).ToList(),

                Escola = new EscolaViewModel
                {
                    Nome = t.Escola!.Nome,
                    Id = t.Escola!.Id,
                }

            }).ToList();

            ViewBag.PaginaAtual = pagina;
            ViewBag.TotalPaginas = totalPaginas;

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
                .Include(t => t.Aulas)
                .Include(t => t.Alunos)
                .Include(t => t.TurmaDisciplinas)
                .ThenInclude(td => td.Disciplina)
                .FirstOrDefaultAsync(m => m.TurmaId == id);

            if (turma == null)
            {
                return NotFound();
            }

            var turmaViewModels = new TurmaViewModel
            {
                TurmaId = turma.TurmaId,
                NomeTurma = turma.NomeTurma,
                AnoLetivo = turma.AnoLetivo,
                Turno = turma.Turno,

                Aulas = turma.Aulas!
                             .Select(al => new AulaViewModel
                             {
                                 Id = al.Id,
                                 Nome = al.Nome,
                             }).ToList(),

                Alunos = turma.Alunos!
                          .Select(a => new AlunoViewModel
                          {
                              Nome = a.Nome,
                              Id = a.Id,
                          }).ToList(),

                Escola = new EscolaViewModel
                {
                    Nome = turma.Escola!.Nome,
                    Id = turma.Escola!.Id,
                }
            };

            return View(turmaViewModels);
        }

        // GET: Turmas/Create
        public IActionResult Create()
        {
            CarregarViewBags();
            return View();
        }


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
                    EscolaId = turma.EscolaId,
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

                TempData["MensagemSucesso"] = "Turma criada com sucesso!";
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

            var turma = await _context.Turma
                                      .Include(t => t.Escola)
                                      .FirstOrDefaultAsync(t => t.TurmaId == id);

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
                Escola = new EscolaViewModel
                {
                    Nome = turma.Escola!.Nome,
                    Id = turma.EscolaId,
                },
            };

            await CarregarViewBagsAsync();
            return View(turmaViewModel);
        }

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

                    TempData["MensagemSucesso"] = "Turma atualizada com sucesso!";
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

            var turmaViewModel = new TurmaViewModel
            {
                TurmaId = turma.TurmaId,
                NomeTurma = turma.NomeTurma,
                AnoLetivo = turma.AnoLetivo,
                Turno = turma.Turno,
                EscolaId = turma.EscolaId,
                Escola = new EscolaViewModel
                {
                    Nome = turma.Escola!.Nome,
                    Id = turma.EscolaId
                },

                Alunos = turma.Alunos!
                          .Select(a => new AlunoViewModel
                          {
                              Nome = a.Nome,
                              Id = a.Id,
                          }).ToList(),
                TurmaDisciplinas = turma.TurmaDisciplinas!
                                        .Select(td => new TurmaDisciplinaViewModel
                                        {
                                            TurmaId = td.TurmaId,
                                            DisciplinaId = td.DisciplinaId,
                                            Disciplina = new DisciplinaViewModel
                                            {
                                                Id = td.DisciplinaId,
                                                Nome = td.Disciplina!.Nome
                                            },
                                        }).ToList(),
            };

            return View(turmaViewModel);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var turma = await _context.Turma
                .Include(t => t.Alunos)
                .Include(t => t.TurmaDisciplinas)
                    .ThenInclude(td => td.Disciplina)
                .FirstOrDefaultAsync(t => t.TurmaId == id);

            if (turma == null)
            {
                return NotFound();
            }

            foreach (var aluno in turma.Alunos!.ToList())
            {
                _context.Aluno.Remove(aluno);
            }

            foreach (var turmaDisciplina in turma.TurmaDisciplinas.ToList())
            {
                _context.TurmaDisciplina.Remove(turmaDisciplina);
            }

            _context.Turma.Remove(turma);
            await _context.SaveChangesAsync();

            TempData["MensagemSucesso"] = "Turma excluída com sucesso!";
            return RedirectToAction(nameof(Index));
        }


        private async Task CarregarViewBagsAsync(TurmaViewModel viewModel = null)
        {
            var escolas = await _context.Escola.ToListAsync();
            var disciplinas = await _context.Disciplina.ToListAsync();

            ViewBag.EscolaId = new SelectList(escolas, "Id", "Nome", viewModel?.Escola!.Id ?? escolas.FirstOrDefault()?.Id);
            ViewBag.Disciplinas = new SelectList(disciplinas, "Id", "Nome");
        }

        private bool TurmaExists(int id)
        {
            return _context.Turma.Any(e => e.TurmaId == id);
        }

        private void CarregarViewBags(TurmaViewModel viewModel = null)
        {

            ViewBag.EscolaId = new SelectList(_context.Escola, "Id", "Nome");
        }
    }
}
