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
            var estados = await AcessarEstados();
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
                        Estados = estados.Select(est => new EstadoViewModel
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

            var estados = AcessarEstados();
            var escolas = await _context.Escola
                .Include(e => e.Endereco)
                .Include(e => e.Estado)
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
                    Estados = await AcessarEstados(),
                },

                Telefones = escolas.Telefones?
                                     .Select(et => new TelefoneViewModel
                                     {
                                         Id = et.Id,
                                         Numero = et.Numero,
                                         DDD = et.DDD,
                                     }).ToList(),

                Turmas = escolas.Turmas!
                                .Select(t => new TurmaViewModel
                                {
                                    TurmaId = t.TurmaId,
                                    NomeTurma = t.NomeTurma,
                                }).ToList(),

                Disciplina = escolas.Disciplina!
                                    .Select(d => new DisciplinaViewModel
                                    {
                                        Id = d.Id,
                                        Nome = d.Nome,
                                    }).ToList(),

                Alunos = escolas.Alunos!
                                .Select(a => new AlunoViewModel
                                {
                                    Id = a.Id,
                                    Nome = a.Nome,
                                }).ToList(),

                Supervisores = escolas.SupervisorEscolas!
                                      .Select(se => new SupervisorEscolaViewModel
                                      {
                                          SupervisorId = se.SupervisorId,
                                          EscolaId = se.EscolaId,
                                      }).ToList(),

                Coordenadores = escolas.Coordenadores!
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

                Diretores = escolas.Diretores!
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

                Professores = escolas.Professores!
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
                CarregarViewBags();
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
                            NumeroRua = viewModel.Endereco!.NumeroRua,
                            Complemento = viewModel.Endereco!.Complemento,
                            EstadoId = viewModel.Endereco!.EstadoId,
                        },

                        Telefones = new List<Telefone>()
                        {   new Telefone
                        {
                            DDD = viewModel.Telefones![0].DDD,
                            Numero = viewModel.Telefones[0].Numero
                        }

                        }
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

            var estado = await AcessarEstados();
            ViewBag.Estados = estado;

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
                .Include(e => e.Estado)
                .Include(e => e.Telefones)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (escola == null)
            {
                TempData["MensagemErro"] = "Escola não encontrada.";
                return NotFound();
            }

            var estados = await AcessarEstados();
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
                },
                Estados = estados,
            };

            TempData["MensagemInfo"] = "Edite os dados da escola e clique em salvar.";
            CarregarViewBags(viewModel);
            return View(viewModel);
        }

        // POST: EscolaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edited(int? id, EscolaViewModel viewModel)
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
                var estados = await AcessarEstados();

                var buscarEstado = estados.FirstOrDefault(e => e.id == viewModel.Endereco!.EstadoId);

                if (buscarEstado == null)
                {
                    TempData["MensagemErro"] = "Estado selecionado inválido.";
                    return RedirectToAction(nameof(Index));
                }

                escola.Nome = viewModel.Nome;
                var selecionarEstado = estados.FirstOrDefault(e => e.id == viewModel.Endereco!.EstadoId);

                if (buscarEstado != null)
                {
                    escola.Estado = new Estado
                    {
                        id = buscarEstado.id,
                        Nome = buscarEstado.Nome,
                        Sigla = buscarEstado.Sigla
                    };
                }

                if (escola.Endereco == null)
                {
                    escola.Endereco = new Endereco();
                }

                escola.Endereco.NomeRua = viewModel.Endereco!.NomeRua;
                escola.Endereco.NumeroRua = viewModel.Endereco.NumeroRua;
                escola.Endereco.Complemento = viewModel.Endereco.Complemento;
                escola.Endereco.CEP = viewModel.Endereco.CEP;
                await _context.SaveChangesAsync();

                escola.Telefones = new List<Telefone>()
                        {   new Telefone
                        {
                            DDD = viewModel.Telefones![0].DDD,
                            Numero = viewModel.Telefones[0].Numero
                        },
                };

                TempData["MensagemSucesso"] = "Escola editada com sucesso!";
                return RedirectToAction(nameof(Index));
            }

            TempData["MensagemErro"] = "Erro ao editar escola. Verifique os dados informados.";
            CarregarViewBags(viewModel);
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
                .Include(e => e.Estado)
                .Include(e => e.Endereco)
                .Include(e => e.Telefones)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (escola == null)
            {
                TempData["MensagemErro"] = "Escola não encontrada.";
                return NotFound();
            }

            var viewModel = new EscolaViewModel
            {
                Id = escola.Id,
                Nome = escola.Nome,
                Telefones = escola.Telefones?.Select(t => new TelefoneViewModel
                {
                    DDD = t.DDD,
                    Numero = t.Numero
                }).ToList() ?? new List<TelefoneViewModel>()
            };

            TempData["MensagemInfo"] = "Confirme a exclusão da escola.";
            return View(viewModel);
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
                    .Include(e => e.Telefones)
                    .FirstOrDefaultAsync(e => e.Id == id);

                if (escola == null)
                {
                    TempData["MensagemErro"] = "Escola não encontrada.";
                    return RedirectToAction(nameof(Index));
                }


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

                TempData["MensagemSucesso"] = "Exclusão realizada com sucesso!";
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException)
            {
                TempData["MensagemErro"] = "Não é possível excluir esta escola. Verifique se há coordenadores, telefones ou outros registros vinculados.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["MensagemErro"] = $"Erro inesperado: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
        }
        private void CarregarViewBags(EscolaViewModel viewModel = null)
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
