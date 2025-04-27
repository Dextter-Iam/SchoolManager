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
                .Where(p => !p.Excluido)
                .Include(p => p.Escola)
                .Include(p => p.Endereco)
                .AsNoTracking()
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
            {
                TempData["MensagemErro"] = "Professor não encontrado.";
                return RedirectToAction(nameof(Index));
            }

            var professor = await _context.Professor
                .Where(p => !p.Excluido)
                .Include(p => p.Endereco)
                .Include(p => p.Escola)
                .Include(p => p.Aulas)
                .Include(p => p.Telefones)
                .Include(p => p.Turmas!)
                    .ThenInclude(pt => pt.Turma)
                .Include(p => p.Disciplinas!)
                    .ThenInclude(pd => pd.Disciplina)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);

            if (professor == null)
            {
                TempData["MensagemErro"] = "Professor não encontrado.";
                return RedirectToAction(nameof(Index));
            }

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
                    int? cep = null;
                    if (!string.IsNullOrWhiteSpace(viewModel.CEP))
                    {
                        if (int.TryParse(viewModel.CEP, out int parsedCep))
                        {
                            cep = parsedCep;
                        }
                        else
                        {
                            ModelState.AddModelError("CEP", "CEP inválido. Informe apenas números.");
                            CarregarDependencias(viewModel);
                            return View(viewModel);
                        }
                    }

                    var endereco = new Endereco
                    {
                        NomeRua = viewModel.NomeRua,
                        NumeroRua = viewModel.NumeroRua,
                        Complemento = viewModel.Complemento,
                        CEP = cep,
                        EstadoId = (int)viewModel.EstadoId!
                    };

                    _context.Endereco.Add(endereco);
                    await _context.SaveChangesAsync();

                    var professor = new Professor
                    {
                        Nome = viewModel.Nome,
                        Matricula = viewModel.Matricula,
                        EscolaId = viewModel.EscolaId,
                        EnderecoId = endereco.Id
                    };

                    _context.Professor.Add(professor);
                    await _context.SaveChangesAsync();


                    if (viewModel.TurmaIds != null)
                    {
                        foreach (var turmaId in viewModel.TurmaIds)
                        {
                            _context.ProfessorTurma.Add(new ProfessorTurma
                            {
                                ProfessorId = professor.Id,
                                TurmaId = turmaId
                            });
                        }
                    }

                    if (viewModel.DisciplinaIds != null)
                    {
                        foreach (var disciplinaId in viewModel.DisciplinaIds)
                        {
                            _context.ProfessorDisciplina!.Add(new ProfessorDisciplina
                            {
                                ProfessorId = professor.Id,
                                DisciplinaId = disciplinaId
                            });
                        }
                    }

                    await _context.SaveChangesAsync();

                    var telefone = new Telefone
                    {
                        DDD = viewModel.DDD,
                        Numero = viewModel.Numero,
                        EscolaId = viewModel.EscolaId,
                        ProfessorId = professor.Id
                    };

                    _context.Telefone.Add(telefone);
                    await _context.SaveChangesAsync();

                    TempData["MensagemSucesso"] = "Professor cadastrado com sucesso!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception)
                {
                    TempData["MensagemErro"] = "Erro ao cadastrar professor.";
                }
            }

            CarregarDependencias(viewModel);
            return View(viewModel);
        }

        // GET: Professor/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                TempData["MensagemErro"] = "Professor não encontrado.";
                return RedirectToAction(nameof(Index));
            }

            var professor = await _context.Professor
                .Include(p => p.Endereco)
                .Include(p => p.Telefones)
                .Include(p => p.Turmas!)
                .ThenInclude(pt => pt.Turma)
                .Include(p => p.Disciplinas!)
                .ThenInclude(pd => pd.Disciplina)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);

            if (professor == null)
            {
                TempData["MensagemErro"] = "Professor não encontrado.";
                return RedirectToAction(nameof(Index));
            }

            var telefone = professor.Telefones?.FirstOrDefault();

            var viewModel = new ProfessorViewModel
            {
                Id = professor.Id,
                Nome = professor.Nome,
                Matricula = professor.Matricula,
                EscolaId = professor.EscolaId,
                EnderecoId = professor.EnderecoId,
                Endereco = professor.Endereco,
                NomeRua = professor.Endereco?.NomeRua,
                NumeroRua = professor.Endereco?.NumeroRua ?? 0,
                Complemento = professor.Endereco?.Complemento,
                CEP = professor.Endereco?.CEP?.ToString("00000000"),
                EstadoId = professor.Endereco?.EstadoId,
                DDD = telefone?.DDD ?? 0,
                Numero = telefone?.Numero ?? 0,
                TurmaIds = professor.Turmas?.Select(t => t.TurmaId).ToList(),
                DisciplinaIds = professor.Disciplinas?.Select(d => d.DisciplinaId).ToList()
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
                    var professor = await _context.Professor
                        .Include(p => p.Endereco)
                        .Include(p => p.Telefones)
                        .Include(p => p.Turmas!)
                        .Include(p => p.Disciplinas!)
                        .FirstOrDefaultAsync(p => p.Id == id);

                    if (professor == null)
                    {
                        TempData["MensagemErro"] = "Erro ao atualizar: Professor não encontrado.";
                        return RedirectToAction(nameof(Index));
                    }


                    professor.Nome = viewModel.Nome;
                    professor.Matricula = viewModel.Matricula;
                    professor.EscolaId = viewModel.EscolaId;


                    if (professor.Endereco != null)
                    {
                        professor.Endereco.NomeRua = viewModel.NomeRua;
                        professor.Endereco.NumeroRua = viewModel.NumeroRua;
                        professor.Endereco.Complemento = viewModel.Complemento;
                        professor.Endereco.CEP = string.IsNullOrWhiteSpace(viewModel.CEP) ? null : int.Parse(viewModel.CEP);
                        professor.Endereco.EstadoId = (int)viewModel.EstadoId!;
                    }


                    if (professor.Telefones != null && professor.Telefones.Any())
                    {
                        foreach (var telefone in professor.Telefones)
                        {
                            telefone.DDD = viewModel.DDD;
                            telefone.Numero = viewModel.Numero;
                            telefone.EscolaId = viewModel.EscolaId;
                        }
                    }

                    if (professor.Telefones != null && professor.Telefones.Any())
                    {
                        _context.Telefone.RemoveRange(professor.Telefones);
                    }


                    if (viewModel.DDD > 0 && viewModel.Numero > 0)
                    {
                        var novoTelefone = new Telefone
                        {
                            DDD = viewModel.DDD,
                            Numero = viewModel.Numero,
                            EscolaId = viewModel.EscolaId,
                            ProfessorId = professor.Id
                        };

                        _context.Telefone.Add(novoTelefone);
                    }


                    var turmasAtuais = professor.Turmas?.ToList() ?? new List<ProfessorTurma>();
                    var turmasSelecionadas = viewModel.TurmaIds ?? new List<int>();

                    foreach (var turmaRel in turmasAtuais)
                    {
                        if (!turmasSelecionadas.Contains(turmaRel.TurmaId))
                        {
                            _context.ProfessorTurma.Remove(turmaRel);
                        }
                    }


                    foreach (var turmaId in turmasSelecionadas)
                    {
                        if (!turmasAtuais.Any(pt => pt.TurmaId == turmaId))
                        {
                            _context.ProfessorTurma.Add(new ProfessorTurma
                            {
                                ProfessorId = professor.Id,
                                TurmaId = turmaId
                            });
                        }
                    }


                    var disciplinasAtuais = professor.Disciplinas?.ToList() ?? new List<ProfessorDisciplina>();
                    var disciplinasSelecionadas = viewModel.DisciplinaIds ?? new List<int>();


                    foreach (var discRel in disciplinasAtuais)
                    {
                        if (!disciplinasSelecionadas.Contains(discRel.DisciplinaId))
                        {
                            _context.ProfessorDisciplina!.Remove(discRel);
                        }
                    }

                    foreach (var disciplinaId in disciplinasSelecionadas)
                    {
                        if (!disciplinasAtuais.Any(pd => pd.DisciplinaId == disciplinaId))
                        {
                            _context.ProfessorDisciplina!.Add(new ProfessorDisciplina
                            {
                                ProfessorId = professor.Id,
                                DisciplinaId = disciplinaId
                            });
                        }
                    }

                    _context.Update(professor);
                    await _context.SaveChangesAsync();

                    TempData["MensagemSucesso"] = "Professor atualizado com sucesso!";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProfessorExists(viewModel.Id))
                    {
                        TempData["MensagemErro"] = "Erro de concorrência ao atualizar o professor.";
                        return RedirectToAction(nameof(Index));
                    }
                    throw;
                }
                catch (Exception)
                {
                    TempData["MensagemErro"] = "Erro inesperado ao atualizar o professor.";
                }
            }

            CarregarDependencias(viewModel);
            return View(viewModel);
        }


        // GET: Professor/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                TempData["MensagemErro"] = "Professor não encontrado.";
                return RedirectToAction(nameof(Index));
            }

            var professor = await _context.Professor
                .Where(p => !p.Excluido)
                .Include(p => p.Endereco)
                .Include(p => p.Escola)
                .Include(p => p.Aulas)
                .Include(p => p.Telefones)
                .Include(p => p.Turmas!)
                    .ThenInclude(pt => pt.Turma)
                .Include(p => p.Disciplinas!)
                    .ThenInclude(pd => pd.Disciplina)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);

            if (professor == null)
            {
                TempData["MensagemErro"] = "Professor não encontrado.";
                return RedirectToAction(nameof(Index));
            }

            var viewModel = new ProfessorViewModel
            {
                Id = professor.Id,
                Nome = professor.Nome,
                Matricula = professor.Matricula,
                Escola = professor.Escola,
                Endereco = professor.Endereco,
                Telefones = professor.Telefones,
                Turmas = professor.Turmas,
                Disciplinas = professor.Disciplinas
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
                var professor = await _context.Professor
                    .FirstOrDefaultAsync(p => p.Id == id);

                if (professor == null)
                {
                    TempData["MensagemErro"] = "Professor não encontrado.";
                    return RedirectToAction(nameof(Index));
                }

                professor.Excluido = true;

                _context.Update(professor);
                await _context.SaveChangesAsync();

                TempData["MensagemSucesso"] = "Professor excluído com sucesso!";
            }
            catch (Exception)
            {
                TempData["MensagemErro"] = "Erro inesperado ao excluir o professor.";
            }

            return RedirectToAction(nameof(Index));
        }

        private bool ProfessorExists(int id)
        {
            return _context.Professor.Any(p => p.Id == id && !p.Excluido);
        }
    





        [HttpGet]
        public async Task<IActionResult> ObterTurmasPorEscola(int escolaId)
        {
            var turmas = await _context.Turma
                .Where(t => t.EscolaId == escolaId)
                .Select(t => new { t.TurmaId, t.NomeTurma })
                .ToListAsync();

            return Json(turmas);
        }

        private void CarregarDependencias(ProfessorViewModel viewModel = null)
        {
            ViewBag.EscolaId = new SelectList(_context.Escola.OrderBy(e => e.Nome), "Id", "Nome", viewModel?.EscolaId);

            if (viewModel?.EscolaId != null)
            {
                ViewBag.TurmaIds = new MultiSelectList(
                    _context.Turma.Where(t => t.EscolaId == viewModel.EscolaId).OrderBy(t => t.NomeTurma),
                    "TurmaId", "NomeTurma", viewModel?.TurmaIds
                );
            }
            else
            {
                ViewBag.TurmaIds = new MultiSelectList(Enumerable.Empty<SelectListItem>(), "Value", "Text");
            }

            ViewBag.DisciplinaIds = new MultiSelectList(
                _context.Disciplina.OrderBy(d => d.Nome),
                "Id", "Nome", viewModel?.DisciplinaIds
            );

            ViewBag.Estados = _context.Estado
                .OrderBy(e => e.Nome)
                .Select(e => new SelectListItem
                {
                    Value = e.id.ToString(),
                    Text = $"{e.Nome} ({e.Sigla})"
                }).ToList();
        }

    }
}
