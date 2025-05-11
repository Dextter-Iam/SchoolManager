using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using MVCCamiloMentoria.Models;
using MVCCamiloMentoria.ViewModels;
using System.Collections.Immutable;
using System.Linq.Expressions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MVCCamiloMentoria.Controllers
{
    public class AlunoController : Controller
    {
        private readonly EscolaContext _context;

        public AlunoController(EscolaContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int pagina = 1)
        {
            int registrosPorPagina = 10;
            var totalRegistros = await _context.Aluno.CountAsync();
            var totalPaginas = (int)Math.Ceiling(totalRegistros / (double)registrosPorPagina);

            var alunos = await _context.Aluno
                .Where(a =>!a.Excluido)
                .Include(a => a.Turma)
                .Include(a => a.Endereco)
                .Include(a => a.Escola)
                .Skip((pagina - 1) * registrosPorPagina)
                .Take(registrosPorPagina)
                .Select(a => new AlunoViewModel
                {
                    Id = a.Id,
                    Nome = a.Nome,
                    EmailEscolar = a.EmailEscolar,
                    DataNascimento = a.DataNascimento,
                    NomeResponsavel1 = a.NomeResponsavel1,
                    Parentesco1 = a.Parentesco1,
                    NomeResponsavel2 = a.NomeResponsavel2,
                    Parentesco2 = a.Parentesco2,
                    Turmas = new List<TurmaViewModel>
                {
                    new TurmaViewModel
                    {
                        NomeTurma = a.Turma!.NomeTurma,
                        TurmaId = a.TurmaId,
                    },
                },
                    Escolas = new List<EscolaViewModel>
                {
                    new EscolaViewModel
                    {
                        Nome = a.Escola!.Nome,
                        Id = a.Escola.Id,
                    }

                },
                }).ToListAsync();

            ViewBag.PaginaAtual = pagina;
            ViewBag.TotalPaginas = totalPaginas;
            return View(alunos);
        }


        //HTTP/GET/DETAILS
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var aluno = await _context.Aluno
                .Where(a => !a.Excluido)
                .Include(a => a.Turma)
                .Include(a => a.Endereco)
                    .ThenInclude(e => e!.Estado)
                .Include(e => e.Estado)
                .Include(a => a.Escola)
                .Include(a => a.Aulas)
                .Include(a => a.AlunoTelefone!)
                    .ThenInclude(at => at.Telefone)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (aluno == null)
                return NotFound();

            var alunoTel = aluno.AlunoTelefone?
                          .Where(at => at.Telefone != null)
                          .Select(at => new AlunoTelefoneViewModel
                          {
                              AlunoId = at.AlunoId,
                              TelefoneId = at.TelefoneId,
                              Telefones = new TelefoneViewModel
                              {
                                  Id = at.Telefone!.Id,
                                  DDD = at.Telefone.DDD,
                                  Numero = at.Telefone.Numero,
                              }
                          }).ToList();

            var viewModel = new AlunoViewModel
            {
                Id = aluno.Id,
                Nome = aluno.Nome,
                Foto = aluno.Foto,
                DataNascimento = aluno.DataNascimento,
                EmailEscolar = aluno.EmailEscolar,
                AnoInscricao = aluno.AnoInscricao,
                BolsaEscolar = aluno.BolsaEscolar,
                NomeResponsavel1 = aluno.NomeResponsavel1,
                Parentesco1 = aluno.Parentesco1,
                NomeResponsavel2 = aluno.NomeResponsavel2,
                Parentesco2 = aluno.Parentesco2,
                Endereco = aluno.Endereco != null ? new EnderecoViewModel
                {
                    Id = aluno.Endereco.Id,
                    NomeRua = aluno.Endereco.NomeRua,
                    NumeroRua = aluno.Endereco.NumeroRua,
                    Complemento = aluno.Endereco.Complemento,
                    CEP = aluno.Endereco.CEP,
                    EstadoId = aluno.Endereco.EstadoId,
                    Estado = new List<EstadoViewModel>
                    {
                          new EstadoViewModel
                          {
                            Nome = aluno.Endereco.Estado!.Nome,
                            id = aluno.Endereco.Estado.id,
                            Sigla = aluno.Endereco.Estado.Sigla,
                           }
                    }
                } : null,


                Escolas = new List<EscolaViewModel>
                {   new EscolaViewModel
                 {
                    Nome = aluno.Escola!.Nome,
                    Id = aluno.EscolaId,
                 }
                },
                Turmas = new List<TurmaViewModel>
                {
                    new TurmaViewModel
                    {
                        NomeTurma = aluno.Turma!.NomeTurma,
                        TurmaId = aluno.TurmaId,
                    },
                },
                Telefones = alunoTel,

            };


            return View(viewModel);
        }
        public IActionResult Create()
        {
            try
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

                var viewModel = new AlunoViewModel
                {
                    Telefones = new List<AlunoTelefoneViewModel>
                {
                     new AlunoTelefoneViewModel 
                { Telefones = new TelefoneViewModel() 
                    
                }
                },
                    Endereco = new EnderecoViewModel
                    {

                    }

                };

                ViewData["Estados"] = new SelectList(estados.ToList(), "id", "Nome");

                TempData["MensagemInfo"] = "Preencha o formulário para cadastrar um novo Aluno.";
                CarregarViewBags(viewModel);
                return View(viewModel);
                
            }
            catch (Exception ex)
            {
                TempData["MensagemErro"] = $"Erro ao carregar formulário: {ex.Message}";
                return RedirectToAction("Index");
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AlunoViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var estados = _context.Estado.ToList();
                    var estadoViewModel = estados.FirstOrDefault();
                    var endereco = viewModel.Endereco != null ? new Endereco
                    {
                        NomeRua = viewModel.Endereco.NomeRua,
                        NumeroRua = viewModel.Endereco.NumeroRua,
                        CEP = viewModel.Endereco.CEP,
                        Complemento = viewModel.Endereco.Complemento,
                        EstadoId = viewModel.Endereco.EstadoId,
                    } : null;

                    var aluno = new Aluno
                    {
                        Nome = viewModel.Nome,
                        DataNascimento = viewModel.DataNascimento,
                        EmailEscolar = viewModel.EmailEscolar,
                        AnoInscricao = viewModel.AnoInscricao,
                        BolsaEscolar = viewModel.BolsaEscolar,
                        TurmaId = viewModel.TurmaId,
                        EscolaId = viewModel.EscolaId,
                        NomeResponsavel1 = viewModel.NomeResponsavel1,
                        Parentesco1 = viewModel.Parentesco1,
                        NomeResponsavel2 = viewModel.NomeResponsavel2,
                        Parentesco2 = viewModel.Parentesco2,
                        Endereco = endereco,

                        AlunoTelefone = viewModel.Telefones?.Select(t => new AlunoTelefone
                        {
                            Telefone = new Telefone
                            {
                                DDD = t.Telefones!.DDD,
                                Numero = t.Telefones!.Numero,
                                EscolaId = viewModel.EscolaId,
                            }
                        }).ToList()
                    };

                    _context.Add(aluno);
                    await _context.SaveChangesAsync();

                    TempData["MensagemSucesso"] = "Aluno cadastrado com sucesso!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {

                    TempData["MensagemErro"] = $"Erro ao cadastrar aluno: {ex.Message}";
                }
            }

            CarregarDependencias(viewModel);
            return View(viewModel);
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var aluno = await _context.Aluno
                .Where(a => !a.Excluido)
                .Include(a => a.AlunoTelefone!)
                    .ThenInclude(at => at.Telefone)
                .Include(a => a.Turma)
                .Include(a => a.Escola)
                .Include(a => a.Endereco)
                    .ThenInclude(a => a!.Estado)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (aluno == null)
                return NotFound();

            var alunoTelefones = aluno.AlunoTelefone?
                .Where(at => at.Telefone != null)
                .Select(at => new AlunoTelefoneViewModel
                {
                    AlunoId = at.AlunoId,
                    TelefoneId = at.TelefoneId,
                    Telefones = new TelefoneViewModel
                    {
                        Id = at.Telefone!.Id,
                        DDD = at.Telefone.DDD,
                        Numero = at.Telefone.Numero,
                    }
                })
                .ToList() ?? new List<AlunoTelefoneViewModel>();

            var primeiroTelefone = alunoTelefones.FirstOrDefault()?.Telefones;

            var estado = _context.Estado.ToList();
            var viewModel = new AlunoViewModel
            {
                Id = aluno.Id,
                Nome = aluno.Nome,
                Foto = aluno.Foto,
                DataNascimento = aluno.DataNascimento,
                EmailEscolar = aluno.EmailEscolar,
                AnoInscricao = aluno.AnoInscricao,
                BolsaEscolar = aluno.BolsaEscolar,
                TurmaId = aluno.TurmaId,
                Turma = new TurmaViewModel
                {
                    NomeTurma = aluno.Turma!.NomeTurma,
                    TurmaId = aluno.TurmaId,
                },

                EscolaId = aluno.EscolaId,
                Escolas = new List<EscolaViewModel>
                {
                    new EscolaViewModel
                    {
                        Nome = aluno.Escola!.Nome,
                        Id = aluno.Escola.Id,
                    }

                },

                Telefones = alunoTelefones,
                Endereco = new EnderecoViewModel
                {
                    NomeRua = aluno.Endereco!.NomeRua,
                    NumeroRua = aluno.Endereco!.NumeroRua,
                    Complemento = aluno.Endereco!.Complemento,
                    CEP = aluno.Endereco.CEP,
                    EstadoId = aluno.Endereco.EstadoId,
                    Estado = estado.Select(e => new EstadoViewModel
                    {
                        id = e.id,
                        Nome = e.Nome,
                        Sigla = e.Sigla
                    }).ToList()
                },
                NomeResponsavel1 = aluno.NomeResponsavel1,
                Parentesco1 = aluno.Parentesco1,
                NomeResponsavel2 = aluno.NomeResponsavel2,
                Parentesco2 = aluno.Parentesco2
            };

            CarregarDependencias(viewModel);

            return View(viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AlunoViewModel viewModel, IFormFile fotoUpload)
        {
            if (id != viewModel.Id)
                return NotFound();

            ModelState.Remove("FotoUpload");

            if (ModelState.IsValid)
            {
                try
                {
                    var aluno = await _context.Aluno
                        .Include(a => a.Endereco)
                        .Include(a => a.AlunoTelefone!)
                            .ThenInclude(at => at.Telefone)
                        .FirstOrDefaultAsync(a => a.Id == id);

                    if (aluno == null)
                        return NotFound();

                    aluno.Nome = viewModel.Nome;
                    aluno.DataNascimento = viewModel.DataNascimento;
                    aluno.EmailEscolar = viewModel.EmailEscolar;
                    aluno.AnoInscricao = viewModel.AnoInscricao;
                    aluno.BolsaEscolar = viewModel.BolsaEscolar;
                    aluno.TurmaId = viewModel.TurmaId;
                    aluno.EscolaId = viewModel.EscolaId;
                    aluno.NomeResponsavel1 = viewModel.NomeResponsavel1;
                    aluno.Parentesco1 = viewModel.Parentesco1;
                    aluno.NomeResponsavel2 = viewModel.NomeResponsavel2;
                    aluno.Parentesco2 = viewModel.Parentesco2;
                    aluno.Endereco ??= new Endereco();
                    aluno.Endereco.NomeRua = viewModel.Endereco!.NomeRua;
                    aluno.Endereco.NumeroRua = viewModel.Endereco.NumeroRua;
                    aluno.Endereco.Complemento = viewModel.Endereco.Complemento;
                    aluno.Endereco.CEP = viewModel.Endereco.CEP;
                    aluno.Endereco.EstadoId = viewModel.Endereco.EstadoId;

                    aluno.AlunoTelefone?.Clear();
                    if (viewModel.Telefones != null)
                    {
                        aluno.AlunoTelefone = viewModel.Telefones
                            .Where(t => t.Telefones != null)
                            .Select(t => new AlunoTelefone
                            {
                                Telefone = new Telefone
                                {
                                    DDD = t.Telefones!.DDD,
                                    Numero = t.Telefones.Numero,
                                    EscolaId = viewModel.EscolaId,
                                }
                            }).ToList();
                    }

                    if (fotoUpload != null && fotoUpload.Length > 0)
                    {
                        using var memoryStream = new MemoryStream();
                        await fotoUpload.CopyToAsync(memoryStream);
                        aluno.Foto = memoryStream.ToArray();
                    }
                    else
                    {
                        aluno.Foto = viewModel.Foto;
                    }

                    _context.Update(aluno);
                    await _context.SaveChangesAsync();

                    TempData["MensagemSucesso"] = "Aluno atualizado com sucesso!";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlunoExists(viewModel.Id))
                        return NotFound();
                    throw;
                }
                catch (Exception ex)
                {
                    TempData["MensagemErro"] = $"Erro ao editar aluno: {ex.Message}";
                }
            }


            CarregarDependencias(viewModel);
            return View(viewModel);
        }



        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var aluno = await _context.Aluno
                .Where(a => !a.Excluido)
                .Include(a => a.Turma)
                .Include(a => a.Endereco)
                    .ThenInclude(e => e!.Estado)
                .Include(e => e.Estado)
                .Include(a => a.Escola)
                .Include(a => a.Aulas)
                .Include(a => a.AlunoTelefone!)
                    .ThenInclude(at => at.Telefone)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (aluno == null)
                return NotFound();

            var alunoTel = aluno.AlunoTelefone?
                          .Where(at => at.Telefone != null)
                          .Select(at => new AlunoTelefoneViewModel
                          {
                              AlunoId = at.AlunoId,
                              TelefoneId = at.TelefoneId,
                              Telefones = new TelefoneViewModel
                              {
                                  Id = at.Telefone!.Id,
                                  DDD = at.Telefone.DDD,
                                  Numero = at.Telefone.Numero,
                              }
                          }).ToList();

            var viewModel = new AlunoViewModel
            {
                Id = aluno.Id,
                Nome = aluno.Nome,
                Foto = aluno.Foto,
                DataNascimento = aluno.DataNascimento,
                EmailEscolar = aluno.EmailEscolar,
                AnoInscricao = aluno.AnoInscricao,
                BolsaEscolar = aluno.BolsaEscolar,
                NomeResponsavel1 = aluno.NomeResponsavel1,
                Parentesco1 = aluno.Parentesco1,
                NomeResponsavel2 = aluno.NomeResponsavel2,
                Parentesco2 = aluno.Parentesco2,
                Endereco = aluno.Endereco != null ? new EnderecoViewModel
                {
                    Id = aluno.Endereco.Id,
                    NomeRua = aluno.Endereco.NomeRua,
                    NumeroRua = aluno.Endereco.NumeroRua,
                    Complemento = aluno.Endereco.Complemento,
                    CEP = aluno.Endereco.CEP,
                    EstadoId = aluno.Endereco.EstadoId,
                    Estado = new List<EstadoViewModel>
                    {
                          new EstadoViewModel
                          {
                            Nome = aluno.Endereco.Estado!.Nome,
                            id = aluno.Endereco.Estado.id,
                            Sigla = aluno.Endereco.Estado.Sigla,
                           }
                    }
                } : null,


                Escolas = new List<EscolaViewModel>
                {   new EscolaViewModel
                 {
                    Nome = aluno.Escola!.Nome,
                    Id = aluno.EscolaId,
                 }
                },
                Turmas = new List<TurmaViewModel>
                {
                    new TurmaViewModel
                    {
                        NomeTurma = aluno.Turma!.NomeTurma,
                        TurmaId = aluno.TurmaId,
                    },
                },
                Telefones = alunoTel,

            };

            return View(viewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var aluno = await _context.Aluno.FindAsync(id);
                if (aluno == null)
                {
                    TempData["MensagemErro"] = "Aluno não encontrado.";
                    return RedirectToAction(nameof(Index));
                }

                 aluno.Excluido = true;
                _context.Aluno.Update(aluno);
                await _context.SaveChangesAsync();

                TempData["MensagemSucesso"] = "Aluno excluído com sucesso!";
            }
            catch (DbUpdateException)
            {
                TempData["MensagemErro"] = "Não foi possível excluir o aluno. Verifique dependências.";
            }
            catch (Exception ex)
            {
                TempData["MensagemErro"] = $"Erro inesperado: {ex.Message}";
            }

            return RedirectToAction(nameof(Index));
        }

        private bool AlunoExists(int id)
        {
            return _context.Aluno.Any(e => e.Id == id);
        }

        private void CarregarDependencias(AlunoViewModel viewModel = null)
        {
            CarregarViewBags(viewModel);
            CarregarViewBagsEstados(viewModel);
            CarregarParentescoOptions(viewModel);
        }

        private void CarregarViewBags(AlunoViewModel viewModel = null)
        {
            ViewBag.EscolaId = new SelectList(_context.Escola, "Id", "Nome", viewModel?.EscolaId);
            ViewBag.TurmaId = new SelectList(_context.Turma, "TurmaId", "NomeTurma", viewModel?.TurmaId);
        }

        private void CarregarViewBagsEstados(AlunoViewModel viewModel = null)
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

        private void CarregarParentescoOptions(AlunoViewModel viewModel)
        {
            var parentescos = new List<string> { "PAI", "MÃE", "IRMÃO", "TIO", "AVÔ", "AVÓ", "TIA", "CUIDADOR" };
            viewModel.ParentescoOptions = new SelectList(parentescos);
        }

        public IActionResult GetFoto(int id)
        {
            var aluno = _context.Aluno
                                .FirstOrDefault(a => a.Id == id);

            if (aluno == null || aluno.Foto == null || aluno.Foto.Length == 0)
            {
                return NotFound();
            }

            return File(aluno.Foto, "image/jpeg");
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