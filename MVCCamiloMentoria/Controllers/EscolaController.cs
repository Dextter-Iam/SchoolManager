using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
                            Nome = e.Nome,
                            Id = e.Id,
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
                Nome = escolas.Nome,
                Endereco = escolas.Endereco,
                Telefones = escolas.Telefones,
                Turmas = escolas.Turmas,
                Estado = escolas.Estado,
                Id = escolas.Id,
                Professores = escolas.Professores,
                Supervisores = escolas.SupervisorEscolas?.Select(se => se.Supervisor!).ToList(),
                Diretores = escolas.Diretores,
            };
            escolaViewModel.Coordenadores = escolas.Coordenadores.Select(c => new CoordenadorViewModel { Id = c.Id, Nome=c.Nome }).ToList();

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
                var endereco = new Endereco
                {
                    NomeRua = viewModel.NomeRua,
                    NumeroRua = viewModel.NumeroRua,
                    Complemento = viewModel.Complemento,
                    CEP = viewModel.CEP,
                    EstadoId = (int)viewModel.EstadoId!,
                };

                var escola = new Escola
                {
                    Nome = viewModel.Nome,
                    EstadoId = (int)viewModel.EstadoId,
                    Endereco = endereco
                };

                _context.Escola.Add(escola);
                await _context.SaveChangesAsync();

                TempData["MensagemSucesso"] = "Cadastro realizado com sucesso!";
                return RedirectToAction(nameof(Index));
            }

            TempData["MensagemErro"] = "Erro ao cadastrar. Verifique os campos obrigatórios.";
            CarregarViewBags(viewModel);
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

            var viewModel = new EscolaViewModel
            {
                Id = escola.Id,
                Nome = escola.Nome,
                EstadoId = escola.EstadoId,
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
                    .FirstOrDefaultAsync(e => e.Id == id);

                if (escola == null)
                {
                    TempData["MensagemErro"] = "Escola não encontrada.";
                    return NotFound();
                }

                escola.Nome = viewModel.Nome;
                escola.EstadoId = (int)viewModel.EstadoId!;

                if (escola.Endereco == null)
                {
                    escola.Endereco = new Endereco();
                }

                escola.Endereco.NomeRua = viewModel.NomeRua;
                escola.Endereco.NumeroRua = viewModel.NumeroRua;
                escola.Endereco.Complemento = viewModel.Complemento;
                escola.Endereco.CEP = viewModel.CEP;
                escola.Endereco.EstadoId = (int)viewModel.EstadoId;

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
                EstadoId = escola.EstadoId,
                Estado = escola.Estado,
                Endereco = escola.Endereco 
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
    }
}
