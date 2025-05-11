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
        public async Task<IActionResult> Index(int pagina = 1)
        {
            int registrosPorPagina = 10;
            var totalRegistros = await _context.Supervisor.CountAsync();
            var totalPaginas = (int)Math.Ceiling(totalRegistros / (double)registrosPorPagina);

            var supervisores = await _context.Supervisor
                  .Where(s => !s.Excluido)
                .Include(s => s.Endereco)
                .Include(s => s.SupervisorEscolas!)
                    .ThenInclude(se => se.Escola)
                .Include(s => s.Telefones)
                .Skip((pagina - 1) * registrosPorPagina)
                .Take(registrosPorPagina)
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
                        Estado = await AcessarEstados(),
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

            ViewBag.PaginaAtual = pagina;
            ViewBag.TotalPaginas = totalPaginas;

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
                  .Where(s => !s.Excluido)
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
                Foto = supervisor.Foto,
                FotoUrl = supervisor.FotoUrl,

                Endereco = new EnderecoViewModel
                {
                    NomeRua = supervisor.Endereco!.NomeRua,
                    NumeroRua = supervisor.Endereco.NumeroRua,
                    Complemento = supervisor.Endereco.Complemento,
                    CEP = supervisor.Endereco.CEP,
                    EstadoId = supervisor.Endereco.EstadoId,
                },

                Telefones = new List<TelefoneViewModel>
                {   new TelefoneViewModel
                {
                      DDD = supervisor.Telefones!.FirstOrDefault()?.DDD ?? 0,
                      Numero = supervisor.Telefones!.FirstOrDefault()?.Numero ?? 0,
                      Id = supervisor.Telefones!.FirstOrDefault()?.Id ?? 0
                }
                },

                SupervisorEscola = supervisor.SupervisorEscolas!
                             .Select(se => new SupervisorEscolaViewModel
                             {
                                 SupervisorId = supervisor.Id,

                                 Escola = new EscolaViewModel
                                 {
                                     Id = se.EscolaId,
                                     Nome = se.Escola!.Nome,
                                 }
                             }).ToList(),

            };

            return View(viewModel);
        }

        // GET: Supervisor/Create
        public IActionResult Create()
        {
            var estados = _context.Estado.Select(e => new EstadoViewModel
            {

                id = e.id,
                Nome = e.Nome,
                Sigla = e.Sigla

            }).ToList();
            var viewModel = new SupervisorViewModel
            {
                Telefones = new List<TelefoneViewModel>
                 {
                     new TelefoneViewModel()
                },
                Endereco = new EnderecoViewModel(),
                SupervisorEscola = new List<SupervisorEscolaViewModel>()
            };

            ViewData["Estados"] = new SelectList(estados.ToList(), "id", "Nome");
            TempData["MensagemInfo"] = "Preencha o formulário para cadastrar um novo Supervisor.";
            CarregarViewBags(viewModel);
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
                    if (viewModel.Endereco == null)
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


                    var supervisorCreate = new Supervisor
                    {
                        Nome = viewModel.Nome,
                        Matricula = viewModel.Matricula,
                        EnderecoId = endereco.Id
                    };

                    _context.Supervisor.Add(supervisorCreate);
                    await _context.SaveChangesAsync();

                    if (viewModel.EscolaIds != null && viewModel.EscolaIds.Any())
                    {
                        foreach (var escolaId in viewModel.EscolaIds)
                        {
                            _context.SupervisorEscola!.Add(new SupervisorEscola
                            {
                                SupervisorId = supervisorCreate.Id,
                                EscolaId = escolaId
                            });
                        }
                    }
                    await _context.SaveChangesAsync();

                    if (viewModel.Telefones != null && viewModel.Telefones.Any())
                    {
                        var escolaId = viewModel.EscolaIds!.First();

                        var telefones = viewModel.Telefones
                            .Where(t => t.Numero > 0)
                            .Select(t => new Telefone
                            {
                                DDD = t.DDD,
                                Numero = t.Numero,
                                EscolaId = escolaId,
                                SupervisorId = supervisorCreate.Id
                            }).ToList();

                        _context.Telefone.AddRange(telefones);
                        await _context.SaveChangesAsync();
                    }

                    TempData["MensagemSucesso"] = "Supervisor cadastrado com sucesso!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {

                    TempData["MensagemErro"] = $"Erro ao cadastrar supervisor: {ex.Message}";
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
                  .Where(s => !s.Excluido)
                .Include(s => s.Endereco)
                .Include(s => s.SupervisorEscolas!)
                    .ThenInclude(se => se.Escola)
                .Include(s => s.Telefones)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (supervisor == null)
            {
                TempData["MensagemErro"] = "Supervisor não encontrado.";
                return RedirectToAction(nameof(Index));
            }
            var estados = _context.Estado.ToList();
            var viewModel = new SupervisorViewModel
            {
                Id = supervisor.Id,
                Nome = supervisor.Nome,
                Matricula = supervisor.Matricula,
                Foto = supervisor.Foto,
                FotoUrl = supervisor.FotoUrl,
                Endereco = new EnderecoViewModel
                {
                    NomeRua = supervisor.Endereco?.NomeRua,
                    NumeroRua = supervisor.Endereco?.NumeroRua ?? 0,
                    Complemento = supervisor.Endereco?.Complemento,
                    CEP = supervisor.Endereco?.CEP,
                    EstadoId = supervisor.Endereco?.EstadoId ?? 0,
                    Estado = estados.Select(e => new EstadoViewModel
                    {
                        id = e.id,
                        Nome = e.Nome,
                        Sigla = e.Sigla
                    }).ToList(),
                },

                Telefones = new List<TelefoneViewModel>
                {   new TelefoneViewModel
                {
                      DDD = supervisor.Telefones!.FirstOrDefault()?.DDD ?? 0,
                      Numero = supervisor.Telefones!.FirstOrDefault()?.Numero ?? 0,
                      Id = supervisor.Telefones!.FirstOrDefault()?.Id ?? 0
                }
                },


                SupervisorEscola = supervisor.SupervisorEscolas!.Select(spe => new SupervisorEscolaViewModel
                {
                    EscolaId = spe.EscolaId,
                    SupervisorId = supervisor.Id,
                    Escola = new EscolaViewModel
                    {
                        Nome = spe.Escola!.Nome,
                        Id = spe.Escola.Id
                    }
                }).ToList(),

                EscolaIds = supervisor.SupervisorEscolas!.Select(se => se.EscolaId).ToList()
            };

            CarregarDependencias(viewModel);
            CarregarViewBags(viewModel);
            return View(viewModel);
        }



        // POST: Supervisor/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(SupervisorViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var supervisor = await _context.Supervisor
                    .Include(s => s.Endereco)
                    .Include(s => s.Telefones)
                    .Include(s => s.SupervisorEscolas)
                    .FirstOrDefaultAsync(s => s.Id == viewModel.Id);

                if (supervisor == null)
                {
                    TempData["MensagemErro"] = "Supervisor não encontrado.";
                    return RedirectToAction(nameof(Index));
                }

                supervisor.Nome = viewModel.Nome;
                supervisor.Matricula = viewModel.Matricula;

                supervisor.Endereco!.NomeRua = viewModel.Endereco!.NomeRua;
                supervisor.Endereco.NumeroRua = viewModel.Endereco.NumeroRua;
                supervisor.Endereco.Complemento = viewModel.Endereco.Complemento;
                supervisor.Endereco.CEP = viewModel.Endereco.CEP;
                supervisor.Endereco.EstadoId = viewModel.Endereco.EstadoId;

                var escolasAntigas = _context.SupervisorEscola!
                                              .Where(se => se.SupervisorId == supervisor.Id)
                                              .ToList();

                _context.SupervisorEscola!.RemoveRange(escolasAntigas);

                if (viewModel.EscolaIds != null && viewModel.EscolaIds.Any())
                {
                    foreach (var escolaId in viewModel.EscolaIds)
                    {
                        _context.SupervisorEscola.Add(new SupervisorEscola
                        {
                            SupervisorId = supervisor.Id,
                            EscolaId = escolaId
                        });
                    }
                }

                var telefonesExistentes = await _context.Telefone
                    .Where(t => t.SupervisorId == supervisor.Id)
                    .ToListAsync();

                foreach (var telefoneExistente in telefonesExistentes)
                {
                    var correspondente = viewModel.Telefones!.FirstOrDefault(t =>
                        t.Id == telefoneExistente.Id &&
                        t.DDD == telefoneExistente.DDD &&
                        t.Numero == telefoneExistente.Numero);

                    if (correspondente == null)
                    {
                        _context.Telefone.Remove(telefoneExistente);
                    }
                }

                if (viewModel.EscolaIds != null && viewModel.EscolaIds.Any())
                {
                    foreach (var escolaId in viewModel.EscolaIds)
                    {
                        foreach (var telefoneViewModel in viewModel.Telefones!.Where(t => t.Numero > 0))
                        {
                            var novoTel = new Telefone
                            {
                                DDD = telefoneViewModel.DDD,
                                Numero = telefoneViewModel.Numero,
                                EscolaId = escolaId,
                                SupervisorId = supervisor.Id
                            };

                            _context.Telefone.Add(novoTel);
                            await _context.SaveChangesAsync();
                        }
                    }
                }
                else
                {
                    Console.WriteLine("ESCOLA NÃO ENCONTRADA!");

                    throw new InvalidOperationException($"Escola com Id {viewModel.EscolaId} não existe.");
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

                    var caminhoPasta = Path.Combine("wwwroot", "uploads", viewModel.Id.ToString(), "imgperfil");
                    Directory.CreateDirectory(caminhoPasta);

                    var nomeArquivo = $"foto{extensao}";
                    var caminhoCompleto = Path.Combine(caminhoPasta, nomeArquivo);

                    using (var stream = new FileStream(caminhoCompleto, FileMode.Create))
                    {
                        await viewModel.FotoUpload.CopyToAsync(stream);
                    }

                    supervisor.FotoUrl = $"/uploads/{viewModel.Id}/imgperfil/{nomeArquivo}";
                }

                await _context.SaveChangesAsync();

                TempData["MensagemSucesso"] = "Supervisor atualizado com sucesso.";
                return RedirectToAction(nameof(Index));
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
                .Where(s => !s.Excluido)
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
                Foto = supervisor.Foto,
                FotoUrl = supervisor.FotoUrl,

                Endereco = new EnderecoViewModel
                {
                    NomeRua = supervisor.Endereco!.NomeRua,
                    NumeroRua = supervisor.Endereco.NumeroRua,
                    Complemento = supervisor.Endereco.Complemento,
                    CEP = supervisor.Endereco.CEP,
                    EstadoId = supervisor.Endereco.EstadoId,
                    Estado = await AcessarEstados(),

                },

                Telefones = new List<TelefoneViewModel>
                {   new TelefoneViewModel
                {
                      DDD = supervisor.Telefones!.FirstOrDefault()?.DDD ?? 0,
                      Numero = supervisor.Telefones!.FirstOrDefault()?.Numero ?? 0,
                      Id = supervisor.Telefones!.FirstOrDefault()?.Id ?? 0
                }
                },

                SupervisorEscola = supervisor.SupervisorEscolas!
                             .Select(se => new SupervisorEscolaViewModel
                             {
                                 SupervisorId = supervisor.Id,

                                 Escola = new EscolaViewModel
                                 {
                                     Id = se.EscolaId,
                                     Nome = se.Escola!.Nome,
                                 }
                             }).ToList(),

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
                      .Where(s => !s.Excluido)
                    .Include(s => s.Telefones)
                    .Include(s => s.SupervisorEscolas)
                    .FirstOrDefaultAsync(s => s.Id == id);

                //var telefone = _context.Telefone
                //                        .Include(t => t.Excluido);
                if (supervisor == null)
                {
                    TempData["MensagemErro"] = "Supervisor não encontrado.";
                    return RedirectToAction(nameof(Index));
                }


                if (supervisor.Telefones != null && supervisor.Telefones.Any())
                    _context.Telefone.RemoveRange(supervisor.Telefones);


                if (supervisor.SupervisorEscolas != null && supervisor.SupervisorEscolas.Any())
                    
                    _context.SupervisorEscola!.RemoveRange(supervisor.SupervisorEscolas);

                    supervisor.Excluido = true;
                _context.Supervisor.Update(supervisor);
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


        private void CarregarViewBags(SupervisorViewModel viewModel = null)
        {
            ViewBag.Escolas = _context.Escola
                            .OrderBy(e => e.Nome)
                            .Select(e => new SelectListItem
                            {
                                Value = e.Id.ToString(),
                                Text = e.Nome
                            }).ToList();

        }

        private void CarregarDependencias(SupervisorViewModel viewModel = null)
        {



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
