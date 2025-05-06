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
            var estados = await AcessarEstados();

            var professores = await _context.Professor
                .Where(p => !p.Excluido)
                .Include(p => p.Escola)
                .Include(p => p.Endereco)
                .ToListAsync();

            var viewModelList = professores.Select(p => new ProfessorViewModel
            {
                Id = p.Id,
                Nome = p.Nome,
                Matricula = p.Matricula,
                Escola = new EscolaViewModel
                {
                    Nome = p.Escola!.Nome,
                    Id = p.Escola.Id,
                },
                Endereco = new EnderecoViewModel
                {
                    NomeRua = p.Endereco!.NomeRua,
                    NumeroRua = p.Endereco!.NumeroRua,
                    Complemento = p.Endereco.Complemento,
                    EstadoId = p.Endereco.EstadoId,
                    ListaDeEstados = estados

                }

            }).ToList();

            return View(viewModelList);
        }

        // GET: Professor/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var estados = await AcessarEstados();
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
                Foto = professor.Foto,
                Matricula = professor.Matricula,
                Escola = new EscolaViewModel
                {
                    Id = professor.EscolaId,
                    Nome = professor.Escola!.Nome,
                },

                Endereco = new EnderecoViewModel
                {
                    NomeRua = professor.Endereco!.NomeRua,
                    NumeroRua = professor.Endereco.NumeroRua,
                    Complemento = professor.Endereco.Complemento,
                    CEP = professor.Endereco!.CEP,
                    EstadoId = professor.Endereco.EstadoId,
                    ListaDeEstados = estados,
                },

                Aulas = professor.Aulas!
                                 .Select(ap => new AulaViewModel
                                 {
                                     Id = ap.Id,
                                     Nome = ap.Nome,
                                     HorarioFim = ap.HorarioFim,
                                     HorarioInicio = ap.HorarioInicio,
                                 }).ToList(),

                Telefones = professor.Telefones!
                                     .Select(pft => new TelefoneViewModel
                                     {
                                         Numero = pft.Numero,
                                         DDD = pft.DDD,

                                     }).ToList(),

                Turmas = professor.Turmas!
                                  .Select(pt => new ProfessorTurmaViewModel
                                  {
                                      TurmaId = pt.TurmaId,
                                      Turma = new TurmaViewModel
                                      {
                                          NomeTurma = pt.Turma!.NomeTurma,
                                          TurmaId = pt.TurmaId,
                                      }
                                  }).ToList(),

                Disciplinas = professor.Disciplinas!
                                       .Select(pd => new ProfessorDisciplinaViewModel
                                       {
                                           DisciplinaId = pd.DisciplinaId,
                                           ProfessorId = pd.ProfessorId,
                                           Disciplina = new DisciplinaViewModel
                                           {
                                              Id = pd.DisciplinaId,
                                              Nome = pd.Disciplina!.Nome 
                                           }

                                       }).ToList(),
            };

            return View(viewModel);
        }

        // GET: Professor/Create
        public IActionResult Create()
        {
            var viewModel = new ProfessorViewModel
            {
                Telefones = new List<TelefoneViewModel>
        {
            new TelefoneViewModel()
        },
                Endereco = new EnderecoViewModel(),
                TurmaIds = new List<int>(),
                DisciplinaIds = new List<int>()
            };

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
                    var enderecoviewModel = new Endereco
                    {
                        NomeRua = viewModel.Endereco!.NomeRua,
                        NumeroRua = viewModel.Endereco!.NumeroRua,
                        Complemento = viewModel.Endereco!.Complemento,
                        CEP = viewModel.Endereco!.CEP,
                        EstadoId = (int)viewModel.Endereco!.EstadoId!,
                    };

                    _context.Endereco.AddRange(enderecoviewModel);
                    await _context.SaveChangesAsync();

                    var professorViewModel = new Professor
                    {
                        Nome = viewModel.Nome,
                        Matricula = viewModel.Matricula,
                        EscolaId = viewModel.EscolaId,
                        EnderecoId = enderecoviewModel.Id,
                    };

                    _context.Professor.Add(professorViewModel);
                    await _context.SaveChangesAsync();


                    if (viewModel.TurmaIds != null)
                    {
                        foreach (var turmaId in viewModel.TurmaIds)
                        {
                            _context.ProfessorTurma.Add(new ProfessorTurma
                            {
                                ProfessorId = professorViewModel.Id,
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
                                ProfessorId = professorViewModel.Id,
                                DisciplinaId = disciplinaId
                            });
                        }
                    }

                    await _context.SaveChangesAsync();

                    var telefones = viewModel.Telefones!.Select(t => new Telefone
                    {
                        DDD = t.DDD,
                        Numero = t.Numero,
                        EscolaId = viewModel.EscolaId,
                        ProfessorId = professorViewModel.Id
                    }).ToList();

                    _context.Telefone.AddRange(telefones);
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
                .Include(p=> p.Escola)
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
                Foto = professor.Foto,
                Matricula = professor.Matricula,
                EscolaId = professor.Escola!.Id,
                Escola = new EscolaViewModel
                {
                    Nome = professor.Escola.Nome, 
                    Id = professor.Escola.Id,
                },
                Endereco = new EnderecoViewModel
                {
                    NomeRua = professor.Endereco?.NomeRua,
                    NumeroRua = professor.Endereco?.NumeroRua ?? 0,
                    Complemento = professor.Endereco?.Complemento,
                    CEP = professor.Endereco?.CEP,
                    EstadoId = professor.Endereco!.EstadoId
                },
                Telefones = professor.Telefones!
                                     .Select(pft => new TelefoneViewModel
                                     {
                                         DDD = pft?.DDD ?? 0,
                                         Numero = pft?.Numero ?? 0,
                                         Id = pft?.Id ?? 0,
                                     }).ToList(),

                TurmaIds = professor.Turmas?
                                    .Select(t => t.TurmaId).ToList(),
                DisciplinaIds = professor.Disciplinas?
                                          .Select(d => d.DisciplinaId).ToList()
            };

            CarregarDependencias(viewModel);

            return View(viewModel);
        }


        // POST: Professor/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProfessorViewModel viewModel, IFormFile fotoUpload)
        {
            if (id != viewModel.Id)
                return NotFound();

            ModelState.Remove("FotoUpload");

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
                        professor.Endereco.NomeRua = viewModel.Endereco!.NomeRua;
                        professor.Endereco.NumeroRua = viewModel.Endereco!.NumeroRua;
                        professor.Endereco.Complemento = viewModel.Endereco!.Complemento;
                        professor.Endereco.CEP = viewModel.Endereco!.CEP;
                        professor.Endereco.EstadoId = (int)viewModel.Endereco!.EstadoId!;
                    }

                    if (professor.Telefones != null && professor.Telefones.Any())
                    {
                        _context.Telefone.RemoveRange(professor.Telefones);
                    }


                    if (viewModel.Telefones != null && viewModel.Telefones.Any(t => t.Numero > 0))
                    {
                        foreach (var telefoneViewModel in viewModel.Telefones.Where(t => t.Numero > 0))
                        {
                            var novoTel = new Telefone
                            {
                                DDD = telefoneViewModel.DDD,
                                Numero = telefoneViewModel.Numero,
                                EscolaId = viewModel.EscolaId,
                                ProfessorId = professor.Id
                            };

                            _context.Telefone.Add(novoTel);
                        }
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

                    if (fotoUpload != null && fotoUpload.Length > 0)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            await fotoUpload.CopyToAsync(memoryStream);
                            professor.Foto = memoryStream.ToArray();
                        }
                    }
                    else
                    {

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
                Foto = professor.Foto,
                Matricula = professor.Matricula,
                Escola = new EscolaViewModel
                {
                    Nome = professor!.Escola.Nome, 
                    Id = professor.Escola.Id,
                },

                EscolaId = professor.Escola!.Id,
                Endereco = new EnderecoViewModel
                {
                    NomeRua = professor.Endereco?.NomeRua,
                    NumeroRua = professor.Endereco?.NumeroRua ?? 0,
                    Complemento = professor.Endereco?.Complemento,
                    CEP = professor.Endereco?.CEP,
                    EstadoId = professor.Endereco!.EstadoId
                },
                Telefones = professor.Telefones!
                                     .Select(pft => new TelefoneViewModel
                                     {
                                         DDD = pft?.DDD ?? 0,
                                         Numero = pft?.Numero ?? 0,
                                         Id = pft?.Id ?? 0,
                                     }).ToList(),

                Turmas = professor.Turmas!
                                  .Select(pt => new ProfessorTurmaViewModel
                                  {
                                      TurmaId = pt.TurmaId,
                                      Turma = new TurmaViewModel
                                      {
                                          NomeTurma = pt.Turma!.NomeTurma,
                                          TurmaId = pt.TurmaId,
                                      }
                                  }).ToList(),

                Disciplinas = professor.Disciplinas!
                                       .Select(pd => new ProfessorDisciplinaViewModel
                                       {
                                           DisciplinaId = pd.DisciplinaId,
                                           ProfessorId = pd.ProfessorId,
                                           Disciplina = new DisciplinaViewModel
                                           {
                                               Id = pd.DisciplinaId,
                                               Nome = pd.Disciplina!.Nome
                                           }

                                       }).ToList(),

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

        public IActionResult GetFoto(int id)
        {
            var professor = _context.Professor
                                .FirstOrDefault(p => p.Id == id);

            if (professor == null || professor.Foto == null || professor.Foto.Length == 0)
            {
                return NotFound();
            }

            return File(professor.Foto, "image/jpeg");
        }

        private async Task<List<EstadoViewModel>> AcessarEstados()
        {
            var estados = await _context.Estado
             .Select(ac => new EstadoViewModel
             {
                 id = ac.id,
                 Nome = ac.Nome,
                 Sigla = ac.Sigla,
             }).ToListAsync();

            return estados;
        }
    }
}
