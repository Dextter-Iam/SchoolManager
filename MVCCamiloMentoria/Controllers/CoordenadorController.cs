using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCCamiloMentoria.Models;
using MVCCamiloMentoria.ViewModels;

namespace MVCCamiloMentoria.Controllers
{
    public class CoordenadorController : Controller
    {
        private readonly EscolaContext _context;

        public CoordenadorController(EscolaContext context)
        {
            _context = context;
        }

        // GET: Coordenador
        public async Task<IActionResult> Index()
        {
            var coordenadores = await _context.Coordenador
                .Where(c=>!c.Excluido)
                .Include(c => c.Endereco)
                .Include(c => c.Escola)
                .AsNoTracking()
                .ToListAsync();

            var viewModel = new List<CoordenadorViewModel>();

            foreach (var c in coordenadores)
            {
                viewModel.Add(new CoordenadorViewModel
                {
                    Id = c.Id,
                    Nome = c.Nome,
                    Matricula = c.Matricula,
                    Endereco = c.Endereco == null ? null : new EnderecoViewModel
                    {
                        NomeRua = c.Endereco.NomeRua,
                        Complemento = c.Endereco.Complemento,
                        NumeroRua = c.Endereco.NumeroRua,
                        CEP = c.Endereco.CEP,
                        Estado = await AcessarEstados()
                    },
                    Escola = c.Escola == null ? null : new EscolaViewModel
                    {
                        Id = c.EscolaId,
                        Nome = c.Escola.Nome
                    }
                });
            }

            return View(viewModel);
        }

        // GET: Coordenador/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                TempData["MensagemErro"] = "Coordenador não encontrado.";
                return RedirectToAction(nameof(Index));
            }

            var coordenador = await _context.Coordenador
                .Where(c => !c.Excluido)
                .Include(c => c.Endereco)
                .Include(c => c.Escola)
                .Include(c => c.Telefones)
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);

            if (coordenador == null)
            {
                TempData["MensagemErro"] = "Coordenador não encontrado.";
                return RedirectToAction(nameof(Index));
            }

            var viewModel = new CoordenadorViewModel
            {
                Id = coordenador.Id,
                Nome = coordenador.Nome,
                Matricula = coordenador.Matricula,
                Endereco = new EnderecoViewModel
                {
                    NomeRua = coordenador.Endereco!.NomeRua,
                    Complemento = coordenador.Endereco.Complemento,
                    NumeroRua = coordenador.Endereco.NumeroRua,
                    CEP = coordenador.Endereco.CEP,
                    Estado = new List<EstadoViewModel>
                    {
                        new EstadoViewModel
                        {
                            id = coordenador.Endereco.EstadoId,
                            Nome = coordenador.Endereco.Estado?.Nome,
                            Sigla = coordenador.Endereco.Estado?.Sigla,

                        },
                    }
                },
                Escola = new EscolaViewModel
                {
                    Id = coordenador.EscolaId,
                    Nome = coordenador.Escola!.Nome,
                },
                Telefones = coordenador.Telefones?
                                       .Select(t => new TelefoneViewModel
                                       {
                                           DDD = t.DDD,
                                           Numero = t.Numero

                                       }).ToList(),
            };

            return View(viewModel);
        }

        // GET: Coordenador/Create
        public IActionResult Create()
        {
            var estados = _context.Estado
                                    .Select(e => new EstadoViewModel
                                    {
                                        id = e.id,
                                        Nome = e.Nome,
                                        Sigla = e.Sigla
                                    })
                                    .OrderBy(e => e.Nome)
                                    .ToList();
            var viewModel = new CoordenadorViewModel
            {
                Endereco = new EnderecoViewModel
                {

                }
            };

            ViewData["Estados"] = new SelectList(_context.Estado.ToList(), "id", "Nome");

            TempData["MensagemInfo"] = "Preencha o formulário para cadastrar um novo Aluno.";
            CarregarViewBags(viewModel);
            return View(viewModel);
        }

        // POST: Coordenador/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CoordenadorViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    var endereco = new Endereco
                    {
                        NomeRua = viewModel.Endereco!.NomeRua,
                        NumeroRua = viewModel.Endereco.NumeroRua,
                        Complemento = viewModel.Endereco.Complemento,
                        CEP = viewModel.Endereco!.CEP,
                        EstadoId = viewModel.Endereco.EstadoId,
                    };

                    _context.Endereco.Add(endereco);
                    await _context.SaveChangesAsync();

                    var coordenador = new Coordenador
                    {
                        Nome = viewModel.Nome,
                        Matricula = viewModel.Matricula,
                        EnderecoId = endereco.Id,
                        EscolaId = (int)viewModel.EscolaId!,
                    };

                    _context.Coordenador.Add(coordenador);
                    await _context.SaveChangesAsync();

                    var telefone = viewModel.Telefones!.Select(t => new Telefone
                    {
                        DDD = t.DDD,
                        Numero = t.Numero,
                        CoordenadorId = coordenador.Id,
                        EscolaId = (int)viewModel.EscolaId!
                    }).ToList();

                    _context.Telefone.AddRange(telefone);

                    await _context.SaveChangesAsync();

                    TempData["MensagemSucesso"] = "Coordenador cadastrado com sucesso!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception)
                {
                    TempData["MensagemErro"] = "Erro ao cadastrar coordenador.";
                }
            }

            CarregarDependencias();
            return View(viewModel);
        }

        // GET: Coordenador/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                TempData["MensagemErro"] = "Coordenador não encontrado.";
                return RedirectToAction(nameof(Index));
            }

            var coordenador = await _context.Coordenador
                .Where(c => !c.Excluido)
                .Include(c => c.Endereco)
                .Include(c => c.Escola)
                .Include(c => c.Telefones)
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);

            if (coordenador == null)
            {
                TempData["MensagemErro"] = "Coordenador não encontrado.";
                return RedirectToAction(nameof(Index));
            }

            var estados = _context.Estado.ToList(); 
            var telefone = coordenador.Telefones?.FirstOrDefault();
            var viewModel = new CoordenadorViewModel
            {
                Id = coordenador.Id,
                Nome = coordenador.Nome,
                Matricula = coordenador.Matricula,
                EscolaId = coordenador.EscolaId,

                Endereco = new EnderecoViewModel
                {
                    NomeRua = coordenador.Endereco!.NomeRua,
                    Complemento = coordenador.Endereco.Complemento,
                    NumeroRua = coordenador.Endereco.NumeroRua,
                    CEP = coordenador.Endereco.CEP,
                    EstadoId = coordenador.Endereco.EstadoId,
                    Estado = estados.Select(e => new EstadoViewModel
                    {
                        id = e.id,
                        Nome = e.Nome,
                        Sigla = e.Sigla
                    }).ToList()

                },
                Escola = coordenador.Escola == null ? null : new EscolaViewModel
                {
                    Id = coordenador.EscolaId,
                    Nome = coordenador.Escola.Nome
                },
                Telefones = coordenador.Telefones?
                                       .Select(c => new TelefoneViewModel
                                       {
                                           DDD = c.DDD,
                                           Numero = c.Numero,

                                       }).ToList(),
            };

            CarregarDependencias();
            return View(viewModel);
        }

        // POST: Coordenador/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CoordenadorViewModel viewModel)
        {
            if (id != viewModel.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var coordenador = await _context.Coordenador
                        .Include(c => c.Endereco)
                        .Include(c => c.Telefones)
                        .FirstOrDefaultAsync(c => c.Id == id);

                    if (coordenador == null)
                    {
                        TempData["MensagemErro"] = "Erro ao atualizar: Coordenador não encontrado.";
                        return RedirectToAction(nameof(Index));
                    }

                    coordenador.Nome = viewModel.Nome;
                    coordenador.Matricula = viewModel.Matricula;
                    coordenador.EscolaId = (int)viewModel.EscolaId!;

                    if (coordenador.Endereco != null)
                    {
                        coordenador.Endereco.NomeRua = viewModel.Endereco!.NomeRua;
                        coordenador.Endereco.NumeroRua = viewModel.Endereco!.NumeroRua;
                        coordenador.Endereco.Complemento = viewModel.Endereco!.Complemento;
                        coordenador.Endereco.CEP = viewModel.Endereco!.CEP;
                        coordenador.Endereco.EstadoId = (int)viewModel.Endereco!.EstadoId;
                    }
                    await _context.SaveChangesAsync();

                    if (coordenador.Telefones != null && coordenador.Telefones.Any())
                        _context.Telefone.RemoveRange(coordenador.Telefones);

                    var telefones = coordenador.Telefones!
                        .Select(c => new Telefone
                        {
                            Id = c.Id,
                            DDD = c.DDD,
                            Numero = c.Numero,
                            CoordenadorId = coordenador.Id,
                            EscolaId = coordenador.EscolaId,
                        }).ToList();

                    _context.Telefone.AddRange(telefones);

                    _context.Update(coordenador);
                    await _context.SaveChangesAsync();

                    TempData["MensagemSucesso"] = "Coordenador atualizado com sucesso!";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CoordenadorExists(viewModel.Id))
                    {
                        TempData["MensagemErro"] = "Erro de concorrência ao atualizar o coordenador.";
                        return RedirectToAction(nameof(Index));
                    }
                    throw;
                }
                catch (Exception)
                {
                    TempData["MensagemErro"] = "Erro inesperado ao atualizar o coordenador.";
                }
            }

            CarregarDependencias();
            return View(viewModel);
        }

        // GET: Coordenador/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                TempData["MensagemErro"] = "Coordenador não encontrado.";
                return RedirectToAction(nameof(Index));
            }

            var coordenador = await _context.Coordenador
                .Where(c => !c.Excluido)
                .Include(c => c.Endereco)
                .Include(c => c.Escola)
                .Include(c => c.Telefones)
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);

            if (coordenador == null)
            {
                TempData["MensagemErro"] = "Coordenador não encontrado.";
                return RedirectToAction(nameof(Index));
            }

            var viewModel = new CoordenadorViewModel
            {
                Id = coordenador.Id,
                Nome = coordenador.Nome,
                Matricula = coordenador.Matricula,
                Endereco = new EnderecoViewModel
                {
                    NomeRua = coordenador.Endereco!.NomeRua,
                    Complemento = coordenador.Endereco.Complemento,
                    NumeroRua = coordenador.Endereco.NumeroRua,
                    CEP = coordenador.Endereco.CEP,
                    Estado = new List<EstadoViewModel>
                    {
                        new EstadoViewModel
                        {
                            id = coordenador.Endereco.EstadoId,
                            Nome = coordenador.Endereco.Estado?.Nome,
                            Sigla = coordenador.Endereco.Estado?.Sigla,

                        },
                    }
                },
                Escola = new EscolaViewModel
                {
                    Id = coordenador.EscolaId,
                    Nome = coordenador.Escola!.Nome,
                },
                Telefones = coordenador.Telefones?
                                       .Select(t => new TelefoneViewModel
                                       {
                                           DDD = t.DDD,
                                           Numero = t.Numero

                                       }).ToList(),
            };

            return View(viewModel);
        }

        // POST: Coordenador/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var coordenador = await _context.Coordenador
                    .Where(c => !c.Excluido)
                    .Include(c => c.Endereco)
                    .Include(c => c.Telefones)
                    .FirstOrDefaultAsync(c => c.Id == id);

                if (coordenador == null)
                {
                    TempData["MensagemErro"] = "Coordenador não encontrado.";
                    return RedirectToAction(nameof(Index));
                }

                //if (coordenador.Telefones != null && coordenador.Telefones.Any())
                //    _context.Telefone.RemoveRange(coordenador.Telefones);

                //if (coordenador.Endereco != null)
                //    _context.Endereco.Remove(coordenador.Endereco);

                 coordenador.Excluido = false;
                _context.Coordenador.Update(coordenador);
                await _context.SaveChangesAsync();

                TempData["MensagemSucesso"] = "Coordenador excluído com sucesso!";
            }
            catch (DbUpdateException)
            {
                TempData["MensagemErro"] = "Não foi possível excluir o coordenador. Verifique dependências.";
            }
            catch (Exception)
            {
                TempData["MensagemErro"] = "Erro inesperado ao excluir o coordenador.";
            }

            return RedirectToAction(nameof(Index));
        }

        private void CarregarViewBags(CoordenadorViewModel viewModel = null)
        {
            ViewBag.Escolas = _context.Escola
                .OrderBy(e => e.Nome)
                .Select(e => new SelectListItem
                {
                    Value = e.Id.ToString(),
                    Text = e.Nome
                }).ToList();
        }

        private void CarregarDependencias(CoordenadorViewModel viewModel = null)
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
        private bool CoordenadorExists(int id)
        {
            return _context.Coordenador.Any(e => e.Id == id);
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
