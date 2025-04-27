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
                .AsNoTracking()
                .ToListAsync();

            var viewModel = supervisores.Select(s => new SupervisorViewModel
            {
                Id = s.Id,
                Nome = s.Nome,
                Matricula = s.Matricula,
                Endereco = s.Endereco,
                Telefones = s.Telefones,
                Escolas = s.SupervisorEscolas?.Select(se => se.Escola!).ToList()
            }).ToList();

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
                Endereco = supervisor.Endereco,
                Telefones = supervisor.Telefones,
                Escolas = supervisor.SupervisorEscolas?.Select(se => se.Escola!).ToList()
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
                    int? cep = null;
                    if (!string.IsNullOrWhiteSpace(viewModel.CEP))
                    {
                        if (int.TryParse(viewModel.CEP, out int parsedCep))
                            cep = parsedCep;
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

                    var supervisor = new Supervisor
                    {
                        Nome = viewModel.Nome,
                        Matricula = viewModel.Matricula,
                        EnderecoId = endereco.Id
                    };

                    _context.Supervisor.Add(supervisor);
                    await _context.SaveChangesAsync();


                    if (viewModel.Telefones != null && viewModel.Telefones.Any())
                    {
                        foreach (var tel in viewModel.Telefones)
                        {
                            _context.Telefone.Add(new Telefone
                            {
                                DDD = tel.DDD,
                                Numero = tel.Numero,
                                SupervisorId = supervisor.Id,
                                EscolaId = tel.EscolaId
                            });
                        }
                    }
                    else
                    {
                        _context.Telefone.Add(new Telefone
                        {
                            DDD = viewModel.DDD,
                            Numero = viewModel.Numero,
                            SupervisorId = supervisor.Id,
                            EscolaId = (int)viewModel.EscolaIds!.FirstOrDefault()
                        });
                    }


                    if (viewModel.EscolaIds != null && viewModel.EscolaIds.Any())
                    {
                        foreach (var escolaId in viewModel.EscolaIds)
                        {
                            _context.SupervisorEscola!.Add(new SupervisorEscola
                            {
                                SupervisorId = supervisor.Id,
                                EscolaId = escolaId
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
                EnderecoId = supervisor.EnderecoId,
                NomeRua = supervisor.Endereco?.NomeRua,
                NumeroRua = supervisor.Endereco?.NumeroRua ?? 0,
                Complemento = supervisor.Endereco?.Complemento,
                CEP = supervisor.Endereco?.CEP?.ToString("00000000"),
                EstadoId = supervisor.Endereco?.EstadoId,
                EscolaIds = supervisor.SupervisorEscolas?.Select(se => se.EscolaId).ToList(),
                DDD = telefone?.DDD ?? 0,
                Numero = telefone?.Numero ?? 0,
            };

            CarregarDependencias(viewModel);
            return View(viewModel);
        }

        // POST: Supervisor/Edit/5
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
                        .Include(s => s.SupervisorEscolas)
                        .Include(s => s.Telefones)
                        .FirstOrDefaultAsync(s => s.Id == id);

                    if (supervisor == null)
                    {
                        TempData["MensagemErro"] = "Erro ao atualizar: Supervisor não encontrado.";
                        return RedirectToAction(nameof(Index));
                    }

                    supervisor.Nome = viewModel.Nome;
                    supervisor.Matricula = viewModel.Matricula;

              
                    if (supervisor.Endereco != null)
                    {
                        supervisor.Endereco.NomeRua = viewModel.NomeRua;
                        supervisor.Endereco.NumeroRua = viewModel.NumeroRua;
                        supervisor.Endereco.Complemento = viewModel.Complemento;
                        supervisor.Endereco.CEP = string.IsNullOrWhiteSpace(viewModel.CEP) ? null : int.Parse(viewModel.CEP);
                        supervisor.Endereco.EstadoId = (int)viewModel.EstadoId!;
                    }

                    _context.Update(supervisor);
                    await _context.SaveChangesAsync();

               
                    var escolasAtuais = supervisor.SupervisorEscolas?.Select(se => se.EscolaId).ToList() ?? new List<int>();
                    var novasEscolas = viewModel.EscolaIds ?? new List<int>();

                    if (escolasAtuais.Any())
                    {
                        var escolasParaRemover = supervisor.SupervisorEscolas!.Where(se => !novasEscolas.Contains(se.EscolaId)).ToList();
                        _context.SupervisorEscola!.RemoveRange(escolasParaRemover);
                    }

                    var escolasParaAdicionar = novasEscolas.Except(escolasAtuais).ToList();
                    foreach (var escolaId in escolasParaAdicionar)
                    {
                        _context.SupervisorEscola!.Add(new SupervisorEscola
                        {
                            SupervisorId = supervisor.Id,
                            EscolaId = escolaId
                        });
                    }

                    await _context.SaveChangesAsync();

              
                    if (supervisor.Telefones != null && supervisor.Telefones.Any())
                    {
                        _context.Telefone.RemoveRange(supervisor.Telefones);
                    }

                    if (viewModel.Telefones != null)
                    {
                        foreach (var tel in viewModel.Telefones)
                        {
                            _context.Telefone.Add(new Telefone
                            {
                                DDD = tel.DDD,
                                Numero = tel.Numero,
                                SupervisorId = supervisor.Id,
                                EscolaId = tel.EscolaId
                            });
                        }
                    }
                    else
                    {

                        _context.Telefone.Add(new Telefone
                        {
                            DDD = viewModel.DDD,
                            Numero = viewModel.Numero,
                            SupervisorId = supervisor.Id,
                            EscolaId = (int)viewModel.EscolaIds!.FirstOrDefault()
                        });
                    }

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
                Endereco = supervisor.Endereco,
                Telefones = supervisor.Telefones,
                Escolas = supervisor.SupervisorEscolas?.Select(se => se.Escola!).ToList()
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

        private bool SupervisorExists(int id)
        {
            return _context.Supervisor.Any(e => e.Id == id);
        }
    }
}
