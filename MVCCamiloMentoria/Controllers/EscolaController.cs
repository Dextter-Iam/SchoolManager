using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor.Extensions;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCCamiloMentoria.Models;
using MVCCamiloMentoria.ViewModels;
using System.IO;
using System.Runtime.ConstrainedExecution;
using System.Threading.Tasks;

namespace MVCCamiloMentoria.Controllers
{
    public class EscolaController : Controller
    {
        private readonly EscolaContext _context;

        public EscolaController(EscolaContext context)
        {
            _context = context;
        }

        // GET: EscolaController/Index
        public async Task<IActionResult> Index()
        {
            var estados = new List<EstadoViewModel>();
            TempData["MensagemInfo"] = "Lista de escolas carregada com sucesso.";
            var escolas = await _context.Escola
                .Select(e => new EscolaViewModel
                {
                    Id = e.Id,
                    Nome = e.Nome,
                    Endereco = new EnderecoViewModel
                    {
                        NomeRua = e.Endereco!.NomeRua,
                        NumeroRua = e.Endereco!.NumeroRua,
                        CEP = e.Endereco.CEP,
                        Complemento = e.Endereco!.Complemento,
                        EstadoId = e.Endereco.EstadoId,
                        Estado = estados.Select(est => new EstadoViewModel
                        {
                            id = est.id,
                            Nome = est.Nome,
                            Sigla = est.Sigla
                        }).ToList(),
                    },
                })
                .ToListAsync();

            return View(escolas);
        }

        // GET: EscolaController/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                TempData["MensagemErro"] = "ID da escola não fornecido.";
                return NotFound();
            }

            var escolas = await _context.Escola
                .Include(e => e.Endereco)
                         .ThenInclude(e => e.Estado)
                .Include(e => e.Professores)
                .Include(e => e.Coordenadores)
                .Include(e => e.Diretores)
                .Include(e => e.Turmas)
                .Include(e => e.Fornecedores)
                .Include(e => e.PrestadorServico)
                .Include(e => e.Telefones)
                .Include(e => e.Equipamentos)
                .Include(e => e.SupervisorEscolas!)
                    .ThenInclude(se => se.Supervisor)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (escolas == null)
            {
                TempData["MensagemErro"] = "Escola não encontrada.";
                return NotFound();
            }

            TempData["MensagemInfo"] = "Detalhes da escola carregados com sucesso.";

            var escolaViewModel = new EscolaViewModel
            {
                Id = escolas.Id,
                Nome = escolas.Nome,

                Endereco = new EnderecoViewModel
                {
                    NomeRua = escolas.Endereco!.NomeRua,
                    CEP = escolas.Endereco!.CEP,
                    NumeroRua = escolas.Endereco!.NumeroRua,
                    Complemento = escolas.Endereco!.Complemento,
                    EstadoId = escolas.Endereco.EstadoId,
                    Estado = new List<EstadoViewModel>
                    {
                        new EstadoViewModel
                        {
                            id = escolas.Endereco.Estado!.id,
                            Nome = escolas.Endereco.Estado.Nome,
                            Sigla = escolas.Endereco.Estado.Sigla,
                        }
                    }
                },

                Telefones = escolas.Telefones?
                                     .Select(et => new TelefoneViewModel
                                     {
                                         Id = et.Id,
                                         Numero = et.Numero,
                                         DDD = et.DDD,
                                     }).ToList(),

                Turmas = escolas.Turmas?
                                .Select(t => new TurmaViewModel
                                {
                                    TurmaId = t.TurmaId,
                                    NomeTurma = t.NomeTurma,
                                }).ToList(),

                Disciplina = escolas.Disciplina != null
                                    ? escolas.Disciplina.Select(d => new DisciplinaViewModel
                                    {
                                        Id = d.Id,
                                        Nome = d.Nome,
                                    }).ToList()
                                    : new List<DisciplinaViewModel>(),

                Alunos = escolas.Alunos != null
                                    ? escolas.Alunos.Select(a => new AlunoViewModel
                                    {
                                        Id = a.Id,
                                        Nome = a.Nome,
                                    }).ToList()
                                    : new List<AlunoViewModel>(),

                Supervisores = escolas.SupervisorEscolas!
                                      .Select(se => new SupervisorEscolaViewModel
                                      {
                                          SupervisorId = se.SupervisorId,
                                          EscolaId = se.EscolaId,
                                          Supervisor = se.Supervisor != null
                                                            ? new SupervisorViewModel
                                                            {
                                                                Id = se.Supervisor.Id,
                                                                Nome = se.Supervisor.Nome
                                                            }
                                      : null
                                      }).ToList(),

                Coordenadores = escolas.Coordenadores?
                                       .Select(c => new CoordenadorViewModel
                                       {
                                           Id = c.Id,
                                           Nome = c.Nome,
                                           Escola = new EscolaViewModel
                                           {
                                               Id = c.EscolaId,
                                               Nome = c.Escola!.Nome,
                                           },
                                       }).ToList(),

                Diretores = escolas.Diretores?
                                    .Select(d => new DiretorViewModel
                                    {
                                        Id = d.Id,
                                        Nome = d.Nome,
                                        Escola = new EscolaViewModel
                                        {
                                            Id = d.EscolaId,
                                            Nome = d.Escola!.Nome,
                                        }
                                    }).ToList(),

                Professores = escolas.Professores?
                                     .Select(p => new ProfessorViewModel
                                     {
                                         Id = p.Id,
                                         Nome = p.Nome,
                                         Escola = new EscolaViewModel
                                         {
                                             Id = p.EscolaId,
                                             Nome = p.Escola!.Nome,
                                         },
                                     }).ToList(),

            };
            return View(escolaViewModel);
        }

        // GET: EscolaController/Create
        public ActionResult Create()
        {
            try
            {
                CarregarViewBagsSync();
                TempData["MensagemInfo"] = "Preencha o formulário para cadastrar uma nova escola.";
                return View();
            }
            catch (Exception ex)
            {
                TempData["MensagemErro"] = "Erro ao carregar formulário de criação: " + ex.Message;
                return View();
            }
        }

        // POST: EscolaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EscolaViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    var escola = new Escola
                    {
                        Nome = viewModel.Nome,
                        Endereco = new Endereco
                        {
                            NomeRua = viewModel.Endereco!.NomeRua,
                            CEP = viewModel.Endereco.CEP,
                            NumeroRua = viewModel.Endereco.NumeroRua,
                            Complemento = viewModel.Endereco.Complemento,
                            EstadoId = viewModel.Endereco.EstadoId
                        },
                        Telefones = viewModel.Telefones?.Select(t => new Telefone
                        {
                            DDD = t.DDD,
                            Numero = t.Numero
                        }).ToList()
                    };

                    _context.Escola.Add(escola);
                    await _context.SaveChangesAsync();

                    TempData["MensagemSucesso"] = "Escola criada com sucesso!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["MensagemErro"] = $"Erro ao criar escola: {ex.Message}";
                    return View(viewModel);
                }
            }

            CarregarViewBagsSync();
            return View(viewModel);
        }

        // GET: EscolaController/Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                TempData["MensagemErro"] = "ID da escola não fornecido.";
                return NotFound();
            }

            var escola = await _context.Escola
                .Include(e => e.Endereco)
                    .ThenInclude(e => e.Estado)
                .Include(e => e.Telefones)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (escola == null)
            {
                TempData["MensagemErro"] = "Escola não encontrada.";
                return NotFound();
            }

            var estado = _context.Estado.ToList();

            var viewModel = new EscolaViewModel
            {
                Id = escola.Id,
                Nome = escola.Nome,
                Endereco = new EnderecoViewModel
                {
                    NomeRua = escola.Endereco!.NomeRua,
                    NumeroRua = escola.Endereco!.NumeroRua,
                    Complemento = escola.Endereco!.Complemento,
                    CEP = escola.Endereco.CEP,
                    EstadoId = escola.Endereco.EstadoId,
                    Estado = estado.Select(e => new EstadoViewModel
                    {
                        id = e.id,
                        Nome = e.Nome,
                        Sigla = e.Sigla
                    }).ToList()
                }
            };

            TempData["MensagemInfo"] = "Edite os dados da escola e clique em salvar.";
            CarregarViewBagsSync();
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, EscolaViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                TempData["MensagemErro"] = "ID inválido para edição.";
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var escola = await _context.Escola
                    .Include(e => e.Endereco)
                    .Include(e => e.Estado)
                    .Include(e => e.Telefones)
                    .FirstOrDefaultAsync(e => e.Id == id);

                if (escola == null)
                {
                    TempData["MensagemErro"] = "Escola não encontrada.";
                    return NotFound();
                }

                escola.Nome = viewModel.Nome;

                if (escola.Endereco == null)
                {
                    escola.Endereco = new Endereco();
                }

                escola.Endereco.NomeRua = viewModel.Endereco!.NomeRua;
                escola.Endereco.NumeroRua = viewModel.Endereco.NumeroRua;
                escola.Endereco.Complemento = viewModel.Endereco.Complemento;
                escola.Endereco.CEP = viewModel.Endereco.CEP;
                escola.Endereco.EstadoId = viewModel.Endereco.EstadoId;
                await _context.SaveChangesAsync();

                // if (viewModel.Telefones != null && viewModel.Telefones.Any())
                //{
                //     escola.Telefones = new List<Telefone>
                //     {
                //      new Telefone
                //     {
                //         DDD = viewModel.Telefones[0].DDD,
                //         Numero = viewModel.Telefones[0].Numero
                //      }
                //     };
                // }
                // else
                // {
                //     escola.Telefones = new List<Telefone>();
                // }

                TempData["MensagemSucesso"] = "Escola editada com sucesso!";
                return RedirectToAction(nameof(Index));
            }


            CarregarViewBagsSync();
            TempData["MensagemErro"] = "Erro ao editar escola. Verifique os dados informados.";
            return View(viewModel);
        }

        // GET: EscolaController/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                TempData["MensagemErro"] = "ID da escola não fornecido.";
                return NotFound();
            }

            var escola = await _context.Escola
                .Include(e => e.Endereco)
                        .ThenInclude(e => e.Estado)
                .Include(e => e.Telefones)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (escola == null)
            {
                TempData["MensagemErro"] = "Escola não encontrada.";
                return NotFound();
            }
            var estados = await _context.Estado.ToListAsync();
            var escolaViewModel = new EscolaViewModel
            {
                Id = escola.Id,
                Nome = escola.Nome,
                Endereco = new EnderecoViewModel
                {
                    NomeRua = escola.Endereco!.NomeRua,
                    CEP = escola.Endereco!.CEP,
                    NumeroRua = escola.Endereco!.NumeroRua,
                    Complemento = escola.Endereco!.Complemento,
                    EstadoId = escola.Endereco.EstadoId,
                    Estado = estados.Select(e => new EstadoViewModel
                    {
                        id = e.id,
                        Nome = e.Nome,
                        Sigla = e.Sigla
                    }).ToList()
                },

                Telefones = escola.Telefones?
                                     .Select(et => new TelefoneViewModel
                                     {
                                         Id = et.Id,
                                         Numero = et.Numero,
                                         DDD = et.DDD,
                                     }).ToList(),

                //Turmas = escola.Turmas?
                //                .Select(t => new TurmaViewModel
                //                {
                //                    TurmaId = t.TurmaId,
                //                    NomeTurma = t.NomeTurma,
                //                }).ToList(),

                //Disciplina = escola.Disciplina != null
                //                    ? escola.Disciplina.Select(d => new DisciplinaViewModel
                //                    {
                //                        Id = d.Id,
                //                        Nome = d.Nome,
                //                    }).ToList()
                //                    : new List<DisciplinaViewModel>(),

                //Alunos = escola.Alunos != null
                //                    ? escola.Alunos.Select(a => new AlunoViewModel
                //                    {
                //                        Id = a.Id,
                //                        Nome = a.Nome,
                //                    }).ToList()
                //                    : new List<AlunoViewModel>(),

                //Supervisores = escola.SupervisorEscolas!
                //                      .Select(se => new SupervisorEscolaViewModel
                //                      {
                //                          SupervisorId = se.SupervisorId,
                //                          EscolaId = se.EscolaId,
                //                          Supervisor = se.Supervisor != null
                //                                            ? new SupervisorViewModel
                //                                            {
                //                                                Id = se.Supervisor.Id,
                //                                                Nome = se.Supervisor.Nome
                //                                            }
                //                      : null
                //                      }).ToList(),

                //Coordenadores = escola.Coordenadores?
                //                       .Select(c => new CoordenadorViewModel
                //                       {
                //                           Id = c.Id,
                //                           Nome = c.Nome,
                //                           Escola = new EscolaViewModel
                //                           {
                //                               Id = c.EscolaId,
                //                               Nome = c.Escola!.Nome,
                //                           },
                //                       }).ToList(),

                //Diretores = escola.Diretores?
                //                    .Select(d => new DiretorViewModel
                //                    {
                //                        Id = d.Id,
                //                        Nome = d.Nome,
                //                        Escola = new EscolaViewModel
                //                        {
                //                            Id = d.EscolaId,
                //                            Nome = d.Escola!.Nome,
                //                        }
                //                    }).ToList(),

                //Professores = escola.Professores?
                //                     .Select(p => new ProfessorViewModel
                //                     {
                //                         Id = p.Id,
                //                         Nome = p.Nome,
                //                         Escola = new EscolaViewModel
                //                         {
                //                             Id = p.EscolaId,
                //                             Nome = p.Escola!.Nome,
                //                         },
                //                     }).ToList(),

            };
            return View(escolaViewModel);
        }
        // POST: EscolaController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var escola = await _context.Escola
                    .Include(e => e.Endereco)
                        .ThenInclude(e => e.Estado)
                    .Include(e => e.Telefones)
                    .FirstOrDefaultAsync(e => e.Id == id);

                if (escola == null)
                {
                    TempData["MensagemErro"] = "Escola não encontrada.";
                    return RedirectToAction(nameof(Index));
                }

                using (var transaction = await _context.Database.BeginTransactionAsync())
                {
                    try
                    {
                        if (escola.Telefones != null && escola.Telefones.Any())
                        {
                            _context.Telefone.RemoveRange(escola.Telefones);
                        }

                        if (escola.Endereco != null)
                        {
                            _context.Endereco.Remove(escola.Endereco);
                        }

                        _context.Escola.Remove(escola);

                        await _context.SaveChangesAsync();

                        await transaction.CommitAsync();

                        TempData["MensagemSucesso"] = "Escola excluída com sucesso!";
                        return RedirectToAction(nameof(Index));
                    }
                    catch (DbUpdateException dbEx)
                    {

                        await transaction.RollbackAsync();

                        TempData["MensagemErro"] = $"Erro ao excluir a escola: {dbEx.Message}. Verifique se existem dados vinculados (coordenadores, telefones, etc.).";
                        return RedirectToAction(nameof(Index));
                    }
                    catch (Exception ex)
                    {

                        await transaction.RollbackAsync();

                        TempData["MensagemErro"] = $"Erro inesperado ao excluir a escola: {ex.Message}";
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["MensagemErro"] = $"Erro inesperado: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
        }

        private void CarregarViewBagsSync()
        {
            var estados = _context.Estado
                .OrderBy(e => e.Nome)
                .Select(e => new SelectListItem
                {
                    Value = e.id.ToString(),
                    Text = $"{e.Nome} ({e.Sigla})"
                })
                .ToList();

            ViewBag.Estados = estados;
        }
    }
}
