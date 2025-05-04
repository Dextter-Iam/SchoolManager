using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCCamiloMentoria.Models;
using MVCCamiloMentoria.ViewModels;

namespace MVCCamiloMentoria.Controllers
{
    public class SupervisorController : Controller
    {
        private readonly EscolaContext _context;

        public SupervisorController(EscolaContext context)
        {
            _context = context;
        }

        // GET: Supervisor
        public async Task<IActionResult> Index()
        {
            var supervisores = await _context.Supervisor
                .Include(s => s.Endereco)
                .Include(s => s.SupervisorEscolas!)
                    .ThenInclude(se => se.Escola)
                .Include(s => s.Telefones)
                .ToListAsync();


            var viewModel = new List<SupervisorViewModel>();

            foreach (var s in supervisores)
            {
                var supervisorViewModel = new SupervisorViewModel
                {
                    Id = s.Id,
                    Nome = s.Nome,
                    Matricula = s.Matricula,
                    Endereco = new EnderecoViewModel
                    {
                        NomeRua = s.Endereco!.NomeRua,
                        NumeroRua = s.Endereco.NumeroRua,
                        Complemento = s.Endereco.Complemento,
                        CEP = s.Endereco.CEP,
                        ListaDeEstados = await AcessarEstados(),
                    },
                    Telefones = s.Telefones!
                                 .Select(st => new TelefoneViewModel
                                 {
                                     DDD = st.DDD,
                                     Numero = st.Numero,
                                     Id = st.Id,
                                 }).ToList(),
                    SupervisorEscola = s.SupervisorEscolas!
                                 .Select(se => new SupervisorEscolaViewModel
                                 {
                                     SupervisorId = s.Id,
                                     Escola = new EscolaViewModel
                                     {
                                         Id = se.Escola!.Id,
                                         Nome = se.Escola.Nome,
                                     }
                                 }).ToList(),
                };

                viewModel.Add(supervisorViewModel);
            }

            return View(viewModel);
        }


        // GET: Supervisor/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                TempData["MensagemErro"] = "Supervisor não encontrado.";
                return RedirectToAction(nameof(Index));
            }

            var supervisor = await _context.Supervisor
                .Include(s => s.Endereco)
                .Include(s => s.SupervisorEscolas!)
                    .ThenInclude(se => se.Escola)
                .Include(s => s.Telefones)
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == id);

            if (supervisor == null)
            {
                TempData["MensagemErro"] = "Supervisor não encontrado.";
                return RedirectToAction(nameof(Index));
            }

            var viewModel = new SupervisorViewModel
            {
                Id = supervisor.Id,
                Nome = supervisor.Nome,
                Matricula = supervisor.Matricula,

                Endereco = supervisor.Endereco != null ? new EnderecoViewModel
                {
                    NomeRua = supervisor.Endereco.NomeRua,
                    NumeroRua = supervisor.Endereco.NumeroRua,
                    Complemento = supervisor.Endereco.Complemento,
                    CEP = supervisor.Endereco.CEP,
                    ListaDeEstados = await AcessarEstados(),
                } : null,


                Telefones = supervisor.Telefones != null
                    ? supervisor.Telefones.Select(st => new TelefoneViewModel
                    {
                        DDD = st.DDD,
                        Numero = st.Numero,
                        Id = st.Id,
                    }).ToList()
                    : new List<TelefoneViewModel>(),


                SupervisorEscola = supervisor.SupervisorEscolas != null
                    ? supervisor.SupervisorEscolas.Select(se => new SupervisorEscolaViewModel
                    {
                        SupervisorId = supervisor.Id,
                        Escola = se.Escola != null ? new EscolaViewModel
                        {
                            Id = se.Escola.Id,
                            Nome = se.Escola.Nome,
                        } : null,
                    }).ToList()
                    : new List<SupervisorEscolaViewModel>()
            };


            return View(viewModel);
        }



        // GET: Supervisor/Create
        public IActionResult Create()
        {
            var viewModel = new SupervisorViewModel();
            CarregarDependencias(viewModel);
            return View(viewModel);
        }

        // POST: Supervisor/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SupervisorViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    if (viewModel.Endereco!.CEP == null)
                    {
                        Console.WriteLine("Erro, por favor Preencha o CEP");
                    }
                    else
                    {
                        ModelState.AddModelError("Endereco.CEP", "CEP inválido. Informe apenas números.");
                        CarregarDependencias(viewModel);
                        return View(viewModel);
                    }

                    var endereco = new Endereco
                    {
                        NomeRua = viewModel.Endereco.NomeRua,
                        NumeroRua = viewModel.Endereco.NumeroRua,
                        Complemento = viewModel.Endereco.Complemento,
                        CEP = viewModel.Endereco.CEP,
                        EstadoId = viewModel.Endereco.EstadoId,
                    };
                    _context.Endereco.Add(endereco);
                    await _context.SaveChangesAsync();

                    var supervisorViewModel = new Supervisor
                    {
                        Nome = viewModel.Nome,
                        Matricula = viewModel.Matricula,
                        EnderecoId = endereco.Id
                    };

                    _context.Supervisor.Add(supervisorViewModel);
                    await _context.SaveChangesAsync();

                    if (viewModel.Telefones != null && viewModel.Telefones.Any())
                    {
                        foreach (var tel in viewModel.Telefones)
                        {
                            _context.Telefone.Add(new Telefone
                            {
                                DDD = tel.DDD,
                                Numero = tel.Numero,
                                SupervisorId = supervisorViewModel.Id,
                                EscolaId = tel.Escola!.Id,
                            });
                        }
                    }

                    if (viewModel.SupervisorEscola != null && viewModel.SupervisorEscola.Any())
                    {
                        foreach (var escola in viewModel.SupervisorEscola)
                        {
                            _context.SupervisorEscola!.Add(new SupervisorEscola
                            {
                                SupervisorId = supervisorViewModel.Id,
                                EscolaId = escola.EscolaId
                            });
                        }
                    }

                    await _context.SaveChangesAsync();
                    TempData["MensagemSucesso"] = "Supervisor cadastrado com sucesso!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception)
                {
                    TempData["MensagemErro"] = "Erro ao cadastrar supervisor.";
                }
            }

            CarregarDependencias(viewModel);
            return View(viewModel);
        }

        // GET: Supervisor/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                TempData["MensagemErro"] = "Supervisor não encontrado.";
                return RedirectToAction(nameof(Index));
            }

            var supervisor = await _context.Supervisor
                .Include(s => s.Endereco)
                .Include(s => s.SupervisorEscolas!)
                    .ThenInclude(se => se.Escola)
                .Include(s => s.Telefones)
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == id);

            if (supervisor == null)
            {
                TempData["MensagemErro"] = "Supervisor não encontrado.";
                return RedirectToAction(nameof(Index));
            }

            var telefone = supervisor.Telefones?.FirstOrDefault();

            var viewModel = new SupervisorViewModel
            {
                Id = supervisor.Id,
                Nome = supervisor.Nome,
                Matricula = supervisor.Matricula,

                Endereco = new EnderecoViewModel
                {
                    NomeRua = supervisor.Endereco!.NomeRua,
                    NumeroRua = supervisor.Endereco.NumeroRua,
                    Complemento = supervisor.Endereco.Complemento,
                    CEP = supervisor.Endereco.CEP,
                    EstadoId = supervisor.Endereco.EstadoId,
                },

                Telefones = supervisor.Telefones!
                                      .Select(st => new TelefoneViewModel
                                      {
                                          Id = st.Id,
                                          Numero = st.Numero,
                                          DDD = st.DDD,
                                      }).ToList(),

                SupervisorEscola = supervisor.SupervisorEscolas!
                                             .Select(spe => new SupervisorEscolaViewModel
                                             {
                                                 EscolaId = spe.EscolaId,
                                                 SupervisorId = supervisor.Id,
                                                 Escola = new EscolaViewModel
                                                 {
                                                     Nome = spe.Escola!.Nome,
                                                     Id = spe.Escola.Id
                                                 }

                                             }).ToList(),
            };


            CarregarDependencias(viewModel);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SupervisorViewModel viewModel)
        {
            if (id != viewModel.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var supervisor = await _context.Supervisor
                        .Include(s => s.Endereco)
                        .Include(s => s.SupervisorEscolas!)
                                       .ThenInclude(se => se.Escola)
                        .Include(s => s.Telefones)
                        .FirstOrDefaultAsync(s => s.Id == id);

                    if (supervisor == null)
                    {
                        TempData["MensagemErro"] = "Erro ao atualizar: Supervisor não encontrado.";
                        return RedirectToAction(nameof(Index));
                    }

                    supervisor.Nome = viewModel.Nome;
                    supervisor.Matricula = viewModel.Matricula;
                    supervisor.SupervisorEscolas = viewModel.SupervisorEscola!
                                                             .Select(spe => new SupervisorEscola
                                                             {
                                                                 EscolaId = spe.EscolaId,
                                                                 SupervisorId = supervisor.Id,
                                                                 Escola = new Escola
                                                                 {
                                                                     Nome = spe.Escola!.Nome,
                                                                     Id = spe.Escola.Id
                                                                 }

                                                             }).ToList();

                    if (supervisor.Endereco != null && viewModel.Endereco != null)
                    {
                        supervisor.Endereco.NomeRua = viewModel.Endereco.NomeRua;
                        supervisor.Endereco.NumeroRua = viewModel.Endereco.NumeroRua;
                        supervisor.Endereco.Complemento = viewModel.Endereco.Complemento;
                        supervisor.Endereco.CEP = viewModel.Endereco.CEP;
                        supervisor.Endereco.EstadoId = (int)viewModel.Endereco.EstadoId!;
                    }

                    if (viewModel.FotoUpload != null && viewModel.FotoUpload.Length > 0)
                    {
                        var extensao = Path.GetExtension(viewModel.FotoUpload.FileName).ToLowerInvariant();
                        var permitidas = new[] { ".jpg", ".jpeg", ".png" };

                        if (!permitidas.Contains(extensao) || viewModel.FotoUpload.Length > 2 * 1024 * 1024)
                        {
                            ModelState.AddModelError("FotoUpload", "Apenas imagens JPG ou PNG com no máximo 2MB são permitidas.");
                            CarregarDependencias(viewModel);
                            return View(viewModel);
                        }

                        var pastaDestino = Path.Combine("wwwroot", "uploads", supervisor.Id.ToString(), "imgperfil");
                        Directory.CreateDirectory(pastaDestino);

                        var nomeArquivo = $"foto{extensao}";
                        var caminhoCompleto = Path.Combine(pastaDestino, nomeArquivo);

                        using (var stream = new FileStream(caminhoCompleto, FileMode.Create))
                        {
                            await viewModel.FotoUpload.CopyToAsync(stream);
                        }

                        supervisor.FotoUrl = $"/uploads/{supervisor.Id}/imgperfil/{nomeArquivo}";
                    }

                    _context.Update(supervisor);
                    await _context.SaveChangesAsync();

                    if (supervisor.Telefones != null && supervisor.Telefones.Any())
                        _context.Telefone.RemoveRange(supervisor.Telefones);

                    await _context.SaveChangesAsync();

                    TempData["MensagemSucesso"] = "Supervisor atualizado com sucesso!";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SupervisorExists(viewModel.Id))
                    {
                        TempData["MensagemErro"] = "Erro de concorrência ao atualizar.";
                        return RedirectToAction(nameof(Index));
                    }
                    throw;
                }
                catch (Exception)
                {
                    TempData["MensagemErro"] = "Erro inesperado ao atualizar.";
                }
            }

            CarregarDependencias(viewModel);
            return View(viewModel);
        }


        // GET: Supervisor/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                TempData["MensagemErro"] = "Supervisor não encontrado.";
                return RedirectToAction(nameof(Index));
            }

            var supervisor = await _context.Supervisor
                .Include(s => s.Endereco)
                .Include(s => s.SupervisorEscolas!)
                    .ThenInclude(se => se.Escola)
                .Include(s => s.Telefones)
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == id);

            if (supervisor == null)
            {
                TempData["MensagemErro"] = "Supervisor não encontrado.";
                return RedirectToAction(nameof(Index));
            }

            var viewModel = new SupervisorViewModel
            {
                Id = supervisor.Id,
                Nome = supervisor.Nome,
                Matricula = supervisor.Matricula,
                Endereco = new EnderecoViewModel
                {
                    NumeroRua = supervisor.Endereco!.NumeroRua,
                    NomeRua = supervisor.Endereco.NomeRua,
                    Complemento = supervisor.Endereco.Complemento,
                    CEP = supervisor.Endereco.CEP,
                },
                Telefones = new List<TelefoneViewModel>
                {
                     new TelefoneViewModel
                      {
                            DDD = 0,
                           Numero = 0
                      }
                },
            };

            return View(viewModel);
        }

        // POST: Supervisor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var supervisor = await _context.Supervisor
                    .Include(s => s.Telefones)
                    .Include(s => s.SupervisorEscolas)
                    .FirstOrDefaultAsync(s => s.Id == id);

                if (supervisor == null)
                {
                    TempData["MensagemErro"] = "Supervisor não encontrado.";
                    return RedirectToAction(nameof(Index));
                }


                if (supervisor.Telefones != null && supervisor.Telefones.Any())
                    _context.Telefone.RemoveRange(supervisor.Telefones);


                if (supervisor.SupervisorEscolas != null && supervisor.SupervisorEscolas.Any())
                    _context.SupervisorEscola!.RemoveRange(supervisor.SupervisorEscolas);

                _context.Supervisor.Remove(supervisor);
                await _context.SaveChangesAsync();

                TempData["MensagemSucesso"] = "Supervisor excluído com sucesso!";
            }
            catch (DbUpdateException)
            {
                TempData["MensagemErro"] = "Não foi possível excluir o supervisor. Verifique dependências.";
            }
            catch (Exception)
            {
                TempData["MensagemErro"] = "Erro inesperado ao excluir o supervisor.";
            }

            return RedirectToAction(nameof(Index));
        }


        private void CarregarDependencias(SupervisorViewModel viewModel = null)
        {
            ViewBag.Escolas = _context.Escola
                    .OrderBy(e => e.Nome)
                    .Select(e => new SelectListItem
                    {
                        Value = e.Id.ToString(),
                        Text = e.Nome
                    }).ToList();


            ViewBag.Estados = _context.Estado
                .OrderBy(e => e.Nome)
                .Select(e => new SelectListItem
                {
                    Value = e.id.ToString(),
                    Text = $"{e.Nome} ({e.Sigla})"
                }).ToList();
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
        private bool SupervisorExists(int id)
        {
            return _context.Supervisor.Any(e => e.Id == id);
        }
    }
}
