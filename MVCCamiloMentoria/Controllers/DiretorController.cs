using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCCamiloMentoria.Models;
using MVCCamiloMentoria.ViewModels;

namespace MVCCamiloMentoria.Controllers
{
    public class DiretorController : Controller
    {
        private readonly EscolaContext _context;

        public DiretorController(EscolaContext context)
        {
            _context = context;
        }

        // GET: Diretor
        public async Task<IActionResult> Index()
        {
            var diretores = await _context.Diretor
                .Include(d => d.Endereco)
                .Include(d => d.Telefones)
                .Include(d => d.Escola)
                .ToListAsync();

            var viewModelList = diretores.Select(d => new DiretorViewModel
            {
                Id = d.Id,
                Nome = d.Nome,
                Matricula = d.Matricula,
                Endereco = new EnderecoViewModel
                {
                    NomeRua = d.Endereco!.NomeRua,
                    NumeroRua = d.Endereco.NumeroRua,
                    Complemento = d.Endereco.Complemento,
                    CEP = d.Endereco.CEP,
                    EstadoId = d.Endereco.EstadoId,
                },
                Telefones = d.Telefones!
                             .Select(td => new TelefoneViewModel
                             {
                                 Id = td.Id,
                                 Numero = td.Numero,
                                 DDD = td.DDD,
                             }).ToList(),
                Escola = new EscolaViewModel
                {
                    Id = d.EscolaId,
                    Nome = d.Escola!.Nome,
                }
            }).ToList();


            CarregarViewBags();
            return View(viewModelList);
        }

        // GET: Diretor/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                TempData["MensagemErro"] = "Diretor não encontrado.";
                return RedirectToAction(nameof(Index));
            }

            var diretor = await _context.Diretor
                .Include(d => d.Escola)
                .Include(d => d.Endereco)
                .Include(d => d.Telefones)
                .FirstOrDefaultAsync(d => d.Id == id);

            if (diretor == null)
            {
                TempData["MensagemErro"] = "Diretor não encontrado.";
                return RedirectToAction(nameof(Index));
            }

            var telefone = diretor.Telefones?.FirstOrDefault();

            var viewModel = new DiretorViewModel
            {
                Id = diretor.Id,
                Nome = diretor.Nome,
                Matricula = diretor.Matricula,
                EscolaId = diretor.EscolaId,
                Endereco = diretor.Endereco == null ? null : new EnderecoViewModel
                {
                    NomeRua = diretor.Endereco.NomeRua,
                    Complemento = diretor.Endereco.Complemento,
                    NumeroRua = diretor.Endereco.NumeroRua,
                    CEP = diretor.Endereco.CEP,
                    ListaDeEstados = new List<EstadoViewModel>
            {
                new EstadoViewModel
                {
                    id = diretor.Endereco.EstadoId,
                    Nome = diretor.Endereco.Estado?.Nome,
                    Sigla = diretor.Endereco.Estado?.Sigla
                }
            }
                },
                Escola = diretor.Escola == null ? null : new EscolaViewModel
                {
                    Id = diretor.EscolaId,
                    Nome = diretor.Escola.Nome
                },
                Telefones = diretor.Telefones?
                    .Select(t => new TelefoneViewModel
                    {
                        DDD = t.DDD,
                        Numero = t.Numero
                    }).ToList()
            };

            return View(viewModel);
        }

        // GET: Diretor/Create
        public IActionResult Create()
        {
            var viewModel = new DiretorViewModel
            {
                Telefones = new List<TelefoneViewModel>
                { 
                    new TelefoneViewModel()
                
                }
            };
            CarregarDependencias();
            return View(viewModel);
        }


        // POST: Diretor/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>    Create(DiretorViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (viewModel.Endereco == null)
                    {
                        ModelState.AddModelError("", "Endereço é obrigatório.");
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

                    var diretor = new Diretor
                    {
                        Nome = viewModel.Nome,
                        Matricula = viewModel.Matricula,
                        EnderecoId = endereco.Id,
                        EscolaId = viewModel.EscolaId
                    };

                    _context.Diretor.Add(diretor);
                    await _context.SaveChangesAsync();

                    if (viewModel.Telefones == null || !viewModel.Telefones.Any())
                    {
                        TempData["MensagemErro"] = "Telefone é obrigatório.";
                        CarregarDependencias(viewModel);
                        return View(viewModel);
                    }

                    var telefones = viewModel.Telefones.Select(t => new Telefone
                    {
                        DDD = t.DDD,
                        Numero = t.Numero,
                        DiretorId = diretor.Id,
                        EscolaId = viewModel.EscolaId
                    }).ToList();

                    _context.Telefone.AddRange(telefones);
                    await _context.SaveChangesAsync();

                    TempData["MensagemSucesso"] = "Diretor cadastrado com sucesso!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception)
                {
                    TempData["MensagemErro"] = "Erro ao cadastrar o diretor.";
                }
            }

            CarregarDependencias(viewModel);
            return View(viewModel);

        }

        // GET: Diretor/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var diretor = await _context.Diretor
                .Include(d => d.Escola)
                .Include(d => d.Endereco)
                .Include(d => d.Telefones)
                .FirstOrDefaultAsync(d => d.Id == id);

            if (diretor == null)
            {
                TempData["MensagemErro"] = "Diretor não encontrado.";
                return RedirectToAction(nameof(Index));
            }

            var viewModel = new DiretorViewModel
            {
                Id = diretor.Id,
                Nome = diretor.Nome,
                Matricula = diretor.Matricula,
                EscolaId = diretor.EscolaId,
                Endereco = diretor.Endereco == null ? null : new EnderecoViewModel
                {
                    NomeRua = diretor.Endereco.NomeRua,
                    Complemento = diretor.Endereco.Complemento,
                    NumeroRua = diretor.Endereco.NumeroRua,
                    CEP = diretor.Endereco.CEP,
                    ListaDeEstados = new List<EstadoViewModel>
            {
                new EstadoViewModel
                {
                    id = diretor.Endereco.EstadoId,
                    Nome = diretor.Endereco.Estado?.Nome,
                    Sigla = diretor.Endereco.Estado?.Sigla
                }
            }
                },
                Escola = diretor.Escola == null ? null : new EscolaViewModel
                {
                    Id = diretor.EscolaId,
                    Nome = diretor.Escola.Nome
                },
                Telefones = diretor.Telefones?
                    .Select(t => new TelefoneViewModel
                    {
                        DDD = t.DDD,
                        Numero = t.Numero
                    }).ToList()
            };

            CarregarDependencias(viewModel);

            return View(viewModel);
        }

        // POST: Diretor/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DiretorViewModel viewModel)
        {
            if (id != viewModel.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var diretor = await _context.Diretor
                        .Include(d => d.Escola)
                        .Include(d => d.Endereco)
                        .Include(d => d.Telefones)
                        .FirstOrDefaultAsync(d => d.Id == id);

                    if (diretor == null)
                    {
                        TempData["MensagemErro"] = "Erro ao atualizar: Diretor não encontrado.";
                        return RedirectToAction(nameof(Index));
                    }

                    diretor.Nome = viewModel.Nome;
                    diretor.Matricula = viewModel.Matricula;
                    diretor.EscolaId = viewModel.EscolaId;

                    if (diretor.Endereco != null)
                    {
                        diretor.Endereco.NomeRua = viewModel.Endereco!.NomeRua;
                        diretor.Endereco.NumeroRua = viewModel.Endereco.NumeroRua;
                        diretor.Endereco.Complemento = viewModel.Endereco.Complemento;
                        diretor.Endereco.CEP = viewModel.Endereco.CEP;
                        diretor.Endereco.EstadoId = (int)viewModel.Endereco.EstadoId!;
                    }

                    if (viewModel.Telefones != null && viewModel.Telefones.Any())
                    {

                        var telefonesParaRemover = diretor.Telefones!
                            .Where(t => !viewModel.Telefones.Any(v => v.DDD == t.DDD && v.Numero == t.Numero))
                            .ToList();

                        foreach (var telefoneRemover in telefonesParaRemover)
                        {
                            _context.Telefone.Remove(telefoneRemover);
                        }

                        // Adiciona novos telefones
                        foreach (var telefoneViewModel in viewModel.Telefones)
                        {
                            var telefoneExistente = diretor.Telefones!
                                .FirstOrDefault(t => t.DDD == telefoneViewModel.DDD && t.Numero == telefoneViewModel.Numero);

                            if (telefoneExistente == null)
                            {
                                var telefone = new Telefone
                                {
                                    DDD = telefoneViewModel.DDD,
                                    Numero = telefoneViewModel.Numero,
                                    EscolaId = viewModel.EscolaId,
                                    DiretorId = diretor.Id
                                };

                                _context.Telefone.Add(telefone);
                            }
                        }
                    }

                    _context.Update(diretor);
                    await _context.SaveChangesAsync();

                    TempData["MensagemSucesso"] = "Diretor atualizado com sucesso!";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DiretorExists(viewModel.Id))
                    {
                        TempData["MensagemErro"] = "Erro de concorrência ao atualizar o diretor.";
                        return RedirectToAction(nameof(Index));
                    }
                    throw;
                }
                catch (Exception)
                {
                    TempData["MensagemErro"] = "Erro inesperado ao atualizar o diretor.";
                }
            }

            CarregarDependencias(viewModel);
            return View(viewModel);
        }

        // GET: Diretor/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                TempData["MensagemErro"] = "Diretor não encontrado.";
                return RedirectToAction(nameof(Index));
            }

            var diretor = await _context.Diretor
                .Include(d => d.Escola)
                .Include(d => d.Telefones)
                .Include(d => d.Endereco)
                .AsNoTracking()
                .FirstOrDefaultAsync(d => d.Id == id);

            if (diretor == null)
            {
                TempData["MensagemErro"] = "Diretor não encontrado.";
                return RedirectToAction(nameof(Index));
            }
            var telefone = diretor.Telefones?.FirstOrDefault();

            var viewModel = new DiretorViewModel
            {
                Id = diretor.Id,
                Nome = diretor.Nome,
                Matricula = diretor.Matricula,
                Endereco = diretor.Endereco == null ? null : new EnderecoViewModel
                {
                    NomeRua = diretor.Endereco.NomeRua,
                    Complemento = diretor.Endereco.Complemento,
                    NumeroRua = diretor.Endereco.NumeroRua,
                    CEP = diretor.Endereco.CEP,
                    ListaDeEstados = new List<EstadoViewModel>
                     {
                        new EstadoViewModel
                        {
                         id = diretor.Endereco.EstadoId,
                         Nome = diretor.Endereco.Estado?.Nome,
                         Sigla = diretor.Endereco.Estado?.Sigla,
                         }
                    }
                },
                Escola = new EscolaViewModel
                {
                    Id = diretor.EscolaId,
                    Nome = diretor.Escola!.Nome,
                },
                Telefones = diretor.Telefones?
                                    .Select(t => new TelefoneViewModel
                                    {
                                        DDD = t.DDD,
                                        Numero = t.Numero
                                    }).ToList()
            };

            return View(viewModel);
        }

        // POST: Diretor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var diretor = await _context.Diretor
                    .Include(d => d.Endereco)
                    .Include(d => d.Telefones)
                    .FirstOrDefaultAsync(d => d.Id == id);

                if (diretor == null)
                {
                    TempData["MensagemErro"] = "Diretor não encontrado.";
                    return RedirectToAction(nameof(Index));
                }

                if (diretor.Telefones != null && diretor.Telefones.Any())
                {
                    _context.Telefone.RemoveRange(diretor.Telefones);
                }

                _context.Diretor.Remove(diretor);
                await _context.SaveChangesAsync();
                TempData["MensagemSucesso"] = "Diretor excluído com sucesso!";
            }
            catch (Exception)
            {
                TempData["MensagemErro"] = "Erro ao excluir o diretor.";
            }

            return RedirectToAction(nameof(Index));
        }

        private bool DiretorExists(int id)
        {
            return _context.Diretor.Any(e => e.Id == id);
        }

        private void CarregarDependencias(DiretorViewModel viewModel = null)
        {
            CarregarViewBags(viewModel);
            CarregarViewBagsEstados(viewModel);
        }
        private void CarregarViewBags(DiretorViewModel viewModel = null)
        {
            ViewBag.Escolas = new SelectList(_context.Escola, "Id", "Nome");
            ViewBag.EnderecoId = new SelectList(_context.Endereco, "Id", "NomeRua");
        }
        private void CarregarViewBagsEstados(DiretorViewModel viewModel = null)
        {
            var estados = _context.Estado
                .OrderBy(e => e.Nome)
                .Select(e => new SelectListItem
                {
                    Value = e.id.ToString(),
                    Text = $"{e.Nome} ({e.Sigla})"
                }).ToList();

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
