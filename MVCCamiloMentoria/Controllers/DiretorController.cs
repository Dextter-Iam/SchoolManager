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
                .Include(d=> d.Escola)
                .ToListAsync(); 

            var estados = await AcessarEstados(); 

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
                    Estados = estados,
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
                NomeRua = diretor.Endereco?.NomeRua,
                NumeroRua = diretor.Endereco?.NumeroRua ?? 0,
                Complemento = diretor.Endereco?.Complemento,
                CEP = diretor.Endereco?.CEP?.ToString("00000000"),
                EstadoId = diretor.Endereco?.EstadoId,
                EscolaId = diretor.EscolaId,
                DDD = telefone?.DDD ?? 0,
                Numero = telefone?.Numero ?? 0
            };

            return View(viewModel);
        }


        // GET: Diretor/Create
        public IActionResult Create()
        {
            CarregarDependencias();
            return View(new DiretorViewModel());
        }

        // POST: Diretor/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DiretorViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (viewModel.DDD <= 0 || viewModel.Numero <= 0)
                    {
                        TempData["MensagemErro"] = "DDD e Número do telefone são obrigatórios.";
                        CarregarDependencias(viewModel);
                        return View(viewModel);
                    }

                    var endereco = new Endereco
                    {
                        NomeRua = viewModel.Endereco!.NomeRua,
                        NumeroRua = viewModel.Endereco.NumeroRua,
                        Complemento = viewModel.Endereco.Complemento,
                        CEP = viewModel.Endereco.CEP,
                        EstadoId = (int)viewModel.Endereco.EstadoId!
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

                    var telefone = new Telefone
                    {
                        DDD = viewModel.DDD,
                        Numero = viewModel.Numero,
                        EscolaId = viewModel.EscolaId,
                        DiretorId = diretor.Id
                    };

                    _context.Telefone.Add(telefone);
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
        public async Task<IActionResult> Edit(int? id)
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

            var escolad = new EscolaViewModel
            {
                Id = diretor.EscolaId,
                Nome = diretor.Escola!.Nome,
            };

            var viewModel = new DiretorViewModel
            {
                Id = diretor.Id,
                Nome = diretor.Nome,
                Matricula = diretor.Matricula,
                NomeRua = diretor.Endereco?.NomeRua,
                NumeroRua = diretor.Endereco?.NumeroRua ?? 0,
                Complemento = diretor.Endereco?.Complemento,
                CEP = diretor.Endereco?.CEP?.ToString("00000000"),
                EstadoId = diretor.Endereco?.EstadoId,
                EscolaId = diretor.EscolaId,
                DDD = telefone?.DDD ?? 0,
                Numero = telefone?.Numero ?? 0,
                Escola = escolad,
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
                        diretor.Endereco.NomeRua = viewModel.NomeRua;
                        diretor.Endereco.NumeroRua = viewModel.NumeroRua;
                        diretor.Endereco.Complemento = viewModel.Complemento;
                        diretor.Endereco.CEP = string.IsNullOrWhiteSpace(viewModel.CEP) ? null : int.Parse(viewModel.CEP);
                        diretor.Endereco.EstadoId = (int)viewModel.EstadoId!;
                    }

                    if (diretor.Telefones != null && diretor.Telefones.Any())
                    {
                        foreach (var telefone in diretor.Telefones)
                        {
                            telefone.DDD = viewModel.DDD;
                            telefone.Numero = viewModel.Numero;
                            telefone.EscolaId = viewModel.EscolaId;
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
            var escolad = new EscolaViewModel
            {
                Id = diretor.EscolaId,
                Nome = diretor.Escola!.Nome,
            };
            var viewModel = new DiretorViewModel
            {
                Id = diretor.Id,
                Nome = diretor.Nome,
                Matricula = diretor.Matricula,
                NomeRua = diretor.Endereco!.NomeRua,
                NumeroRua = diretor.Endereco.NumeroRua, 
                Complemento = diretor.Endereco.Complemento,
                EscolaId = diretor.EscolaId,
                Escola = escolad,
                Telefones = diretor.Telefones!
                                   .Select(d => new TelefoneViewModel
                                   { 
                                       Numero = d.Numero,
                                       DDD = d.DDD,

                                   }).ToList(),
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
            ViewBag.EscolaId = new SelectList(_context.Escola, "Id", "Nome");
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
