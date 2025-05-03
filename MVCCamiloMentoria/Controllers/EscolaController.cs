using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor.Extensions;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCCamiloMentoria.Models;
using MVCCamiloMentoria.ViewModels;
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
            TempData["MensagemInfo"] = "Lista de escolas carregada com sucesso.";
            var escolas = await _context.Escola
                        .Select(e => new EscolaViewModel
                        {
                            Id = e.Id,
                            Nome = e.Nome,

                        }).ToListAsync();

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


                Telefones = escolas.Telefones!
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
                                    AnoLetivo = t.AnoLetivo,
                                    Turno = t.Turno,
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
                                    EmailEscolar = a.EmailEscolar,
                                    BolsaEscolar = a.BolsaEscolar,
                                    AlunoTelefone = a.AlunoTelefone,
                                    AlunoResponsavel = a.AlunoResponsavel,
                                    AnoInscricao = a.AnoInscricao,
                                    DataNascimento = a.DataNascimento,
                                    Foto = a.Foto,
                                    NomeResponsavel1 = a.NomeResponsavel1,
                                    NomeResponsavel2 = a.NomeResponsavel2,
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

                                       }).ToList(),

                Diretores = escolas.Diretores!
                                    .Select(d => new DiretorViewModel
                                    {
                                        Id = d.Id,
                                        Nome = d.Nome,
                                        Telefones = d.Telefones,
                                        Escola = d.Escola,

                                    }).ToList(),

                PrestadorServico = escolas.PrestadorServico!
                                         .Select(ps => new PrestadorServicoViewModel
                                         {
                                             Id = ps.Id,
                                             Nome = ps.Nome,
                                             EmpresaNome = ps.EmpresaNome,
                                             CNPJ = ps.CNPJ,
                                             CPF = ps.CPF,
                                             ServicoFinalidade = ps.ServicoFinalidade,
                                             Escola = ps.Escola,
                                             Telefones = ps.Telefones,
                                         }).ToList()

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

                var estado = await AcessarEstados();


                var escola = new Escola
                {
                    Id = viewModel.Id,
                    Nome = viewModel.Nome,

                    Endereco = new Endereco
                    {
                        Id = viewModel.Id,
                        NomeRua = viewModel.NomeRua,
                        CEP = viewModel.CEP,
                        Complemento = viewModel.Complemento,
                        NumeroRua = viewModel.NumeroRua,
                    }

                };



                _context.Escola.Add(escola);
                await _context.SaveChangesAsync();

                // Mensagem de sucesso
                TempData["MensagemSucesso"] = "Cadastro realizado com sucesso!";
                return RedirectToAction(nameof(Index));
            }
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
                Estados = estados,
                NomeRua = escola.Endereco?.NomeRua,
                NumeroRua = (int)escola.Endereco?.NumeroRua!,
                Complemento = escola.Endereco?.Complemento,
                CEP = (int)escola.Endereco?.CEP!,
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
                    .FirstOrDefaultAsync(e => e.Id == id);

                if (escola == null)
                {
                    TempData["MensagemErro"] = "Escola não encontrada.";
                    return NotFound();
                }
                var estados = await AcessarEstados();

                var buscarEstado = estados.FirstOrDefault(e => e.id == viewModel.EstadoId);

                if (buscarEstado == null)
                {
                    TempData["MensagemErro"] = "Estado selecionado inválido.";
                    return RedirectToAction(nameof(Index));
                }

                escola.Nome = viewModel.Nome;
                var selecionarEstado = estados.FirstOrDefault(e => e.id == viewModel.EstadoId);

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

                escola.Endereco.NomeRua = viewModel.NomeRua;
                escola.Endereco.NumeroRua = viewModel.NumeroRua;
                escola.Endereco.Complemento = viewModel.Complemento;
                escola.Endereco.CEP = viewModel.CEP;

                await _context.SaveChangesAsync();

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
                    .ThenInclude(end => end.Estado!)
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
                    .FirstOrDefaultAsync(e => e.Id == id);

                if (escola == null)
                {
                    TempData["MensagemErro"] = "Escola não encontrada.";
                    return RedirectToAction(nameof(Index));
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
