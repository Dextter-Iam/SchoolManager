using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCCamiloMentoria.Models;
using MVCCamiloMentoria.ViewModels;

namespace MVCCamiloMentoria.Controllers
{
    public class AlunoController : Controller
    {
        private readonly EscolaContext _context;

        public AlunoController(EscolaContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var alunos = await _context.Aluno
                .Include(a => a.Turma)
                .Include(a => a.Endereco)
                .Include(a => a.Escola)
                .Select(a => new AlunoViewModel
                {
                    Id = a.Id,
                    Nome = a.Nome,
                    EmailEscolar = a.EmailEscolar,
                    Escola = a.Escola,
                    DataNascimento = a.DataNascimento,
                    NomeResponsavel1 = a.NomeResponsavel1,
                    Parentesco1 = a.Parentesco1,
                    NomeResponsavel2 = a.NomeResponsavel2,
                    Parentesco2 = a.Parentesco2
                }).ToListAsync();

            return View(alunos);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var aluno = await _context.Aluno
                .Include(a => a.Turma)
                .Include(a => a.Endereco)
                    .ThenInclude(e => e.Estado)
                .Include(a => a.Escola)
                .Include(a => a.Aulas)
                .Include(a => a.AlunoTelefone!)
                    .ThenInclude(at => at.Telefone)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (aluno == null)
                return NotFound();

            var viewModel = new AlunoViewModel
            {
                Id = aluno.Id,
                Nome = aluno.Nome,
                DataNascimento = aluno.DataNascimento,
                EmailEscolar = aluno.EmailEscolar,
                AnoInscricao = aluno.AnoInscricao,
                BolsaEscolar = aluno.BolsaEscolar,
                TurmaId = aluno.TurmaId,
                Turma = aluno.Turma,
                NomeResponsavel1 = aluno.NomeResponsavel1,
                Parentesco1 = aluno.Parentesco1,
                NomeResponsavel2 = aluno.NomeResponsavel2,
                Parentesco2 = aluno.Parentesco2,
                AlunoTelefone = aluno.AlunoTelefone?
                .Select(at => new AlunoTelefone
                {
                    DDD = at.Telefone?.DDD ?? 0,
                    Numero = at.Telefone?.Numero ?? 0
                }).ToList(),

                Endereco = aluno.Endereco != null ? new Endereco
                {
                    Id = aluno.Endereco.Id,
                    NomeRua = aluno.Endereco.NomeRua,
                    NumeroRua = aluno.Endereco.NumeroRua,
                    Complemento = aluno.Endereco.Complemento,
                    CEP = aluno.Endereco.CEP,
                    EstadoId = aluno.Endereco.EstadoId,
                    Estado = aluno.Endereco.Estado
                } : null,
                EscolaId = aluno.EscolaId,
                Escola = aluno.Escola,
                Aulas = aluno.Aulas?.Select(a => new Aula
                {
                    Id = a.Id,
                    Nome = a.Nome,
                }).ToList()
            };

            return View(viewModel);
        }

        public IActionResult Create()
        {
            var viewModel = new AlunoViewModel();
            CarregarDependencias(viewModel);
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AlunoViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (viewModel.DDD <= 0 || viewModel.Numero <= 0)
                    {
                        TempData["MensagemErro"] = "DDD e Número do telefone são obrigatórios";
                        CarregarDependencias(viewModel);
                        return View(viewModel);
                    }

                    var endereco = new Endereco
                    {
                        NomeRua = viewModel.NomeRua,
                        NumeroRua = viewModel.NumeroRua,
                        Complemento = viewModel.Complemento,
                        CEP = viewModel.CEP,
                        EstadoId = (int)viewModel.EstadoId
                    };

                    var telefone = new Telefone
                    {
                        DDD = viewModel.DDD,
                        Numero = viewModel.Numero,
                        EscolaId = viewModel.EscolaId,
                    };

                    _context.Telefone.Add(telefone);
                    await _context.SaveChangesAsync();

                    var aluno = new Aluno
                    {
                        Nome = viewModel.Nome,
                        DataNascimento = viewModel.DataNascimento,
                        EmailEscolar = viewModel.EmailEscolar,
                        AnoInscricao = viewModel.AnoInscricao,
                        BolsaEscolar = viewModel.BolsaEscolar,
                        TurmaId = viewModel.TurmaId,
                        Endereco = endereco,
                        EscolaId = viewModel.EscolaId,
                        NomeResponsavel1 = viewModel.NomeResponsavel1,
                        Parentesco1 = viewModel.Parentesco1,
                        NomeResponsavel2 = viewModel.NomeResponsavel2,
                        Parentesco2 = viewModel.Parentesco2,
                        EstadoId = (int)viewModel.EstadoId,
                    };

                    _context.Add(aluno);
                    await _context.SaveChangesAsync();

                    var alunoTelefone = new AlunoTelefone
                    {
                        AlunoId = aluno.Id,
                        TelefoneId = telefone.Id
                    };
                    _context.Add(alunoTelefone);
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
                .Include(a => a.AlunoTelefone!)
                    .ThenInclude(at => at.Telefone)
                .Include(a => a.Turma)
                .Include(a => a.Escola)
                .Include(a => a.Endereco)
                    .ThenInclude(e => e.Estado)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (aluno == null)
                return NotFound();

            var alunoTelefones = aluno.AlunoTelefone?
                .Where(at => at.Telefone != null)
                .Select(at => new AlunoTelefone
                {
              
                    AlunoId = at.AlunoId,
                    TelefoneId = at.TelefoneId,
                    Telefone = new Telefone
                    {
                        Id = at.Telefone.Id,
                        DDD = at.Telefone.DDD,
                        Numero = at.Telefone.Numero,
                       
                    }
                }).ToList();

            var primeiroTelefone = alunoTelefones?.FirstOrDefault()?.Telefone;

            var viewModel = new AlunoViewModel
            {
                Id = aluno.Id,
                Nome = aluno.Nome,
                DataNascimento = aluno.DataNascimento,
                EmailEscolar = aluno.EmailEscolar,
                AnoInscricao = aluno.AnoInscricao,
                BolsaEscolar = aluno.BolsaEscolar,
                TurmaId = aluno.TurmaId,
                EscolaId = aluno.EscolaId,
                AlunoTelefone = alunoTelefones,
                // Mantendo propriedades individuais para compatibilidade
                DDD = primeiroTelefone?.DDD ?? 0,
                Numero = primeiroTelefone?.Numero ?? 0,
                NomeRua = aluno.Endereco?.NomeRua,
                NumeroRua = aluno.Endereco?.NumeroRua ?? 0,
                Complemento = aluno.Endereco?.Complemento,
                CEP = aluno.Endereco?.CEP ?? 0,
                EstadoId = aluno.Endereco?.EstadoId ?? 0,
                Estado = aluno.Endereco?.Estado,
                Turma = aluno.Turma,
                Escola = aluno.Escola,
                NomeResponsavel1 = aluno.NomeResponsavel1,
                Parentesco1 = aluno.Parentesco1,
                NomeResponsavel2 = aluno.NomeResponsavel2,
                Parentesco2 = aluno.Parentesco2,
                Aulas = aluno.Aulas?.Select(a => new Aula
                {
                    Id = a.Id,
                    Nome = a.Nome,
                    
                }).ToList()
            };

            CarregarDependencias(viewModel);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AlunoViewModel viewModel)
        {
            if (id != viewModel.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    // Verificar se os valores do telefone são válidos antes de salvar
                    if (viewModel.DDD <= 0 || viewModel.Numero <= 0)
                    {
                        TempData["MensagemErro"] = "DDD e Número do telefone são obrigatórios";
                        CarregarDependencias(viewModel);
                        return View(viewModel);
                    }

                    var aluno = await _context.Aluno.FindAsync(id);
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

                    if (aluno.Endereco == null)
                    {
                        aluno.Endereco = new Endereco();
                    }
                    aluno.Endereco.NomeRua = viewModel.NomeRua;
                    aluno.Endereco.NumeroRua = viewModel.NumeroRua;
                    aluno.Endereco.Complemento = viewModel.Complemento;
                    aluno.Endereco.CEP = viewModel.CEP;
                    aluno.Endereco.EstadoId = viewModel.EstadoId ?? 0;

               
                    var alunoTelefone = await _context.AlunoTelefone
                        .Include(at => at.Telefone)
                        .FirstOrDefaultAsync(at => at.AlunoId == aluno.Id);

                    if (alunoTelefone != null && alunoTelefone.Telefone != null)
                    {
                        alunoTelefone.Telefone.DDD = viewModel.DDD;
                        alunoTelefone.Telefone.Numero = viewModel.Numero;
                        _context.Update(alunoTelefone.Telefone);
                    }
                    else
                    {
                        var telefone = new Telefone
                        {
                            DDD = viewModel.DDD,
                            Numero = viewModel.Numero,
                            EscolaId = viewModel.EscolaId
                        };
                        _context.Telefone.Add(telefone);
                        await _context.SaveChangesAsync();

                        var novoAlunoTelefone = new AlunoTelefone
                        {
                            AlunoId = aluno.Id,
                            TelefoneId = telefone.Id
                        };
                        _context.Add(novoAlunoTelefone);
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
                .Include(a => a.Turma)
                .Include(a => a.Endereco)
                .Include(a => a.Escola)
                .Include(a => a.AlunoTelefone!)
                    .ThenInclude(at => at.Telefone)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (aluno == null)
                return NotFound();

            var viewModel = new AlunoViewModel
            {
                Id = aluno.Id,
                Nome = aluno.Nome,
                DataNascimento = aluno.DataNascimento,
                EmailEscolar = aluno.EmailEscolar,
                AnoInscricao = aluno.AnoInscricao,
                BolsaEscolar = aluno.BolsaEscolar,
                TurmaId = aluno.TurmaId,
                Turma = aluno.Turma,
                NomeResponsavel1 = aluno.NomeResponsavel1,
                Parentesco1 = aluno.Parentesco1,
                NomeResponsavel2 = aluno.NomeResponsavel2,
                Parentesco2 = aluno.Parentesco2,
                AlunoTelefone = aluno.AlunoTelefone?
                    .Select(at => new AlunoTelefone
                    {
                        DDD = at.Telefone?.DDD ?? 0,
                        Numero = at.Telefone?.Numero ?? 0
                    }).ToList(),
                Endereco = aluno.Endereco != null ? new Endereco
                {
                    NomeRua = aluno.Endereco.NomeRua,
                    NumeroRua = aluno.Endereco.NumeroRua,
                    Complemento = aluno.Endereco.Complemento,
                    CEP = aluno.Endereco.CEP,
                    EstadoId = aluno.Endereco.EstadoId,
                    Estado = aluno.Endereco.Estado
                } : null,
                EscolaId = aluno.EscolaId,
                Escola = aluno.Escola
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

                _context.Aluno.Remove(aluno);
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
    }
}