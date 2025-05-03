using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCCamiloMentoria.Models;
using MVCCamiloMentoria.ViewModels;
using System;

namespace MVCCamiloMentoria.Controllers
{
    public class ResponsavelController : Controller
    {
        private readonly EscolaContext _context;

        public ResponsavelController(EscolaContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var responsaveis = await _context.Responsavel
                .Include(r => r.Endereco)
                .Include(r => r.Telefones)
                .Include(r => r.AlunoResponsavel!)
                    .ThenInclude(ar => ar.Aluno)
                .ToListAsync();
            var responsavelViewModel = responsaveis.Select(r => new ResponsavelViewModel
            {
                Id = r.Id,
                Nome = r.Nome,
                Endereco = new EnderecoViewModel
                {

                },

                Telefones = r.Telefones!
                             .Select(tr => new TelefoneViewModel
                             {
                                 Numero = tr.Numero,
                                 DDD = tr.DDD,
                                 Id = tr.Id,

                             }).ToList(),

                AlunoResponsavel = r.AlunoResponsavel!
                                    .Select(ar => new AlunoResponsavelViewModel
                                    {
                                        AlunoId = ar.AlunoId,
                                        ResponsavelId = ar.ResponsavelId,
                                    }).ToList(),
            }).ToList();

            return View(responsavelViewModel);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var responsavel = await _context.Responsavel
                .Include(r => r.Endereco)
                    .ThenInclude(re => re!.Estado)
                .Include(r => r.Telefones)
                .Include(r => r.AlunoResponsavel!)
                    .ThenInclude(ar => ar.Aluno)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (responsavel == null)
                return NotFound();

            var responsavelViewModel = new ResponsavelViewModel
            {
                Id = responsavel.Id,
                Nome = responsavel.Nome,
                Endereco = new EnderecoViewModel
                {
                    NomeRua = responsavel.Endereco!.NomeRua,
                    NumeroRua = responsavel.Endereco!.NumeroRua,
                    Complemento = responsavel.Endereco.Complemento,
                    CEP = responsavel.Endereco.CEP,
                    EstadoId = responsavel.Endereco.EstadoId,
                },

                Telefones = responsavel.Telefones!
                             .Select(tr => new TelefoneViewModel
                             {
                                 Numero = tr.Numero,
                                 DDD = tr.DDD,
                                 Id = tr.Id,

                             }).ToList(),

                AlunoResponsavel = responsavel.AlunoResponsavel!
                                    .Select(ar => new AlunoResponsavelViewModel
                                    {
                                        AlunoId = ar.AlunoId,
                                        ResponsavelId = ar.ResponsavelId,
                                    }).ToList(),
            };

            ViewBag.AlunoIds = responsavel.AlunoResponsavel?
                                          .Select(ar => ar.AlunoId)
                                          .ToList() ?? new List<int>();
            CarregarDependencias();
            return View(responsavelViewModel);
        }

        public IActionResult Create()
        {
            var viewModel = new ResponsavelViewModel();
            CarregarDependencias();
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ResponsavelViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                if (viewModel.Telefones == null || !viewModel.Telefones.Any(t => t.DDD > 0 && t.Numero > 0))
                {
                    TempData["MensagemErro"] = "DDD e Número do telefone são obrigatórios";
                    CarregarDependencias(viewModel);
                    return View(viewModel);
                }

                var endereco = new Endereco
                {
                    NomeRua = viewModel.Endereco!.NomeRua,
                    NumeroRua = viewModel.Endereco!.NumeroRua,
                    Complemento = viewModel.Endereco!.Complemento,
                    CEP = viewModel.Endereco.CEP,
                    EstadoId = (int)viewModel.Endereco!.EstadoId!
                };

                var responsavel = new Responsavel
                {   
                    Nome = viewModel.Nome,
                    Endereco = endereco
                };

                _context.Responsavel.Add(responsavel);
                await _context.SaveChangesAsync();


                int? alunoIdx = viewModel.AlunoIds?.FirstOrDefault();
                var aluno = alunoIdx.HasValue
                    ? await _context.Aluno.FirstOrDefaultAsync(a => a.Id == alunoIdx.Value)
                    : null;

                var buscarTelefone = viewModel.Telefones?.FirstOrDefault();

                if (buscarTelefone != null)
                {
                    var telefone = new Telefone
                    {
                        DDD = buscarTelefone.DDD,
                        Numero = buscarTelefone.Numero,
                        EscolaId = aluno?.EscolaId ?? 0,
                        ResponsavelId = responsavel.Id
                    };

                    _context.Telefone.Add(telefone);
                }
                await _context.SaveChangesAsync();


                if (viewModel.AlunoIds != null && viewModel.AlunoIds.Any())
                {
                    foreach (var alunoId in viewModel.AlunoIds)
                    {
                        _context.AlunoResponsavel.Add(new AlunoResponsavel
                        {
                            ResponsavelId = responsavel.Id,
                            AlunoId = alunoId
                        });
                    }
                    await _context.SaveChangesAsync();
                }

                TempData["MensagemSucesso"] = "Responsável cadastrado com sucesso!";
                return RedirectToAction(nameof(Index));
            }

            CarregarDependencias();
            return View(viewModel);
        }


        public async Task<IActionResult> Edit(int? id)
        {
            var estados = AcessarEstados();

            if (id == null)
                return NotFound();

            var responsavel = await _context.Responsavel
                .Include(r => r.Endereco)
                .Include(r => r.AlunoResponsavel)
                .Include(r => r.Telefones)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (responsavel == null)
                return NotFound();

            var telefone = responsavel.Telefones?.FirstOrDefault();

            var viewModel = new ResponsavelViewModel
            {
                Id = responsavel.Id,
                Nome = responsavel.Nome,
                Endereco = new EnderecoViewModel
                {
                    NomeRua = responsavel.Endereco?.NomeRua,
                    NumeroRua = responsavel.Endereco?.NumeroRua ?? 0,
                    Complemento = responsavel.Endereco?.Complemento,
                    CEP = responsavel.Endereco!.CEP,
                    EstadoId = responsavel.Endereco!.EstadoId,
                    ListaDeEstados = await AcessarEstados(),
                },

                Telefones = responsavel.Telefones!
                                       .Select(tr => new TelefoneViewModel
                                       {
                                           DDD = telefone?.DDD ?? 0,
                                           Numero = telefone?.Numero ?? 0,
                                           Id = tr.Id,
                                       }).ToList(),

                AlunoResponsavel = responsavel.AlunoResponsavel!
                                              .Select(ar => new AlunoResponsavelViewModel
                                              { AlunoId = ar.AlunoId,
                                                ResponsavelId = ar.ResponsavelId,
                                              }).ToList(),

                AlunoIds = responsavel.AlunoResponsavel?
                                      .Select(a => a.AlunoId)
                                      .ToList()
            };

            CarregarDependencias();
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ResponsavelViewModel viewModel)
        {
            if (id != viewModel.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var responsavel = await _context.Responsavel
                        .Include(r => r.AlunoResponsavel)
                        .Include(r => r.Telefones)
                        .FirstOrDefaultAsync(r => r.Id == id);

                    if (responsavel == null)
                        return NotFound();

                    responsavel.Nome = viewModel.Nome;
                    responsavel.EnderecoId = viewModel.Endereco!.Id;

                    int? alunoId = viewModel.AlunoIds?.FirstOrDefault();
                    var aluno = alunoId.HasValue
                        ? await _context.Aluno.AsNoTracking().FirstOrDefaultAsync(a => a.Id == alunoId.Value)
                        : null;

                    if (viewModel.Telefones != null && viewModel.Telefones.Any())
                    {
                        foreach (var telefoneViewModel in viewModel.Telefones)
                        {
                            if (telefoneViewModel.DDD > 0 && telefoneViewModel.Numero > 0)
                            {
                                if (responsavel.Telefones != null && responsavel.Telefones.Any())
                                {
                                    foreach (var telefone in responsavel.Telefones)
                                    {
                                        telefone.DDD = telefoneViewModel.DDD;
                                        telefone.Numero = telefoneViewModel.Numero;
                                        telefone.EscolaId = aluno?.EscolaId ?? telefone.EscolaId;
                                    }
                                }
                                else
                                {
                                    var novoTel = new Telefone
                                    {
                                        DDD = telefoneViewModel.DDD,
                                        Numero = telefoneViewModel.Numero,
                                        EscolaId = aluno?.EscolaId ?? 0,
                                        ResponsavelId = responsavel.Id
                                    };
                                    _context.Telefone.Add(novoTel);
                                }
                            }
                        }
                    }

                    var alunosAtuais = responsavel.AlunoResponsavel?    
                                                  .ToList() ?? new List<AlunoResponsavel>();

                    var alunosSelecionados = viewModel.AlunoIds ?? new List<int>();

                    foreach (var alunoRel in alunosAtuais)
                    {
                        if (!alunosSelecionados.Contains(alunoRel.AlunoId))
                        {
                            _context.AlunoResponsavel.Remove(alunoRel);
                        }
                    }

                    foreach (var alunoIdSelecionado in alunosSelecionados)
                    {
                        if (!alunosAtuais.Any(ar => ar.AlunoId == alunoIdSelecionado))
                        {
                            _context.AlunoResponsavel.Add(new AlunoResponsavel
                            {
                                ResponsavelId = responsavel.Id,
                                AlunoId = alunoIdSelecionado
                            });
                        }
                    }

                    _context.Update(responsavel);
                    await _context.SaveChangesAsync();

                    TempData["MensagemSucesso"] = "Responsável atualizado com sucesso!";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ResponsavelExists(viewModel.Id))
                        return NotFound();
                    throw;
                }
            }

            CarregarDependencias();
            return View(viewModel);
        }


        // GET: Responsavel/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                TempData["MensagemErro"] = "Responsável não encontrado.";
                return RedirectToAction(nameof(Index));
            }

            var responsavel = await _context.Responsavel
                .Include(r => r.Endereco)
                .Include(r => r.AlunoResponsavel!)
                    .ThenInclude(ar => ar.Aluno)
                .Include(r => r.Telefones)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (responsavel == null)
            {
                TempData["MensagemErro"] = "Responsável não encontrado.";
                return RedirectToAction(nameof(Index));
            }

            var viewModel = new ResponsavelViewModel
            {
                Id = responsavel.Id,
                Nome = responsavel.Nome,
                Endereco = new EnderecoViewModel
                {
                    NomeRua = responsavel.Endereco?.NomeRua,
                    NumeroRua = responsavel.Endereco?.NumeroRua ?? 0,
                    Complemento = responsavel.Endereco?.Complemento,
                    CEP = responsavel.Endereco!.CEP,
                    EstadoId = responsavel.Endereco!.EstadoId,
                    ListaDeEstados = await AcessarEstados(),
                },

                Telefones = responsavel.Telefones!
                                       .Select(tr => new TelefoneViewModel
                                       {
                                           DDD = tr?.DDD ?? 0,
                                           Numero = tr?.Numero ?? 0,
                                           Id = tr!.Id,

                                       }).ToList(),

                AlunoResponsavel = responsavel.AlunoResponsavel!
                                              .Select(ar => new AlunoResponsavelViewModel
                                              {
                                                  AlunoId = ar.AlunoId,
                                                  ResponsavelId = ar.ResponsavelId,
                                              }).ToList(),

                AlunoIds = responsavel.AlunoResponsavel?
                                      .Select(a => a.AlunoId)
                                      .ToList()
            };
            return View(viewModel);
        }

        // POST: Responsavel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var responsavel = await _context.Responsavel
                    .Include(r => r.AlunoResponsavel)
                    .Include(r => r.Telefones)
                    .FirstOrDefaultAsync(r => r.Id == id);

                if (responsavel == null)
                {
                    TempData["MensagemErro"] = "Responsável não encontrado para exclusão.";
                    return RedirectToAction(nameof(Index));
                }


                if (responsavel.AlunoResponsavel != null && responsavel.AlunoResponsavel.Any())
                    _context.AlunoResponsavel.RemoveRange(responsavel.AlunoResponsavel);


                if (responsavel.Telefones != null && responsavel.Telefones.Any())
                    _context.Telefone.RemoveRange(responsavel.Telefones);


                _context.Responsavel.Remove(responsavel);

                await _context.SaveChangesAsync();

                TempData["MensagemSucesso"] = "Responsável excluído com sucesso!";
            }
            catch (Exception)
            {
                TempData["MensagemErro"] = "Erro ao tentar excluir o responsável.";
            }

            return RedirectToAction(nameof(Index));
        }


        private bool ResponsavelExists(int id)
        {
            return _context.Responsavel.Any(e => e.Id == id);
        }


        private void CarregarDependencias(ResponsavelViewModel viewModel = null)
        {
            CarregarViewBags(viewModel);
            CarregarViewBagsEstados(viewModel);
        }
        private void CarregarViewBags(ResponsavelViewModel viewModel = null)
        {
            ViewBag.EnderecoId = new SelectList(_context.Endereco, "Id", "NomeRua");
            ViewBag.Alunos = new MultiSelectList(_context.Aluno, "Id", "Nome");
        }

        private void CarregarViewBagsEstados(ResponsavelViewModel viewModel = null)
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