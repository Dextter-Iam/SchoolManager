using System;
using System.Linq;
using System.Threading.Tasks;
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

        // GET: Aluno
        public async Task<IActionResult> Index()
        {
            var alunos = await _context.Aluno
                .Include(a => a.Turma)
                .Include(a => a.Endereco)
                .Include(a => a.Escola)
                .Select(a => new AlunoViewModel
                {
                    Nome = a.Nome,
                    Id = a.Id
                }).ToListAsync();

            return View(alunos);
        }

        // GET: Aluno/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var aluno = await _context.Aluno
                .Include(a => a.Turma)
                .Include(a => a.Endereco)
                .Include(a => a.Escola)
                .Include(a => a.Responsaveis)
                .Include(a => a.Aulas)
                .Include(a => a.AlunoTelefone)
                    .ThenInclude(at => at.Telefone) 
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
                DDD = aluno.AlunoTelefone?.FirstOrDefault()?.Telefone?.DDD ?? 0, 
                Numero = aluno.AlunoTelefone?.FirstOrDefault()?.Telefone?.Numero ?? 0, 
                NomeRua = aluno.Endereco?.NomeRua,
                NumeroRua = aluno.Endereco?.NumeroRua ?? 0,
                Complemento = aluno.Endereco?.Complemento,
                CEP = aluno.Endereco?.CEP ?? 0,
                EstadoId = aluno.EstadoId,
                Estado = aluno.Estado,
                EscolaId = aluno.EscolaId,
                Escola = aluno.Escola,
                Responsaveis = aluno.Responsaveis,
                Aulas = aluno.Aulas
            };

            return View(viewModel);
        }


        // GET: Aluno/Create
        public IActionResult Create()
        {
            CarregarViewBags();
            CarregarViewBagsEstados();
            return View();
        }

        // POST: Aluno/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AlunoViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
              
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
                        EscolaId = viewModel.EscolaId
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
                        EstadoId = (int)viewModel.EstadoId,
                        EscolaId = viewModel.EscolaId,
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

                    // Mensagem de sucesso
                    TempData["MensagemSucesso"] = "Aluno cadastrado com sucesso!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["MensagemErro"] = $"Erro ao cadastrar aluno: {ex.Message}";
                }
            }

            CarregarViewBags(viewModel);
            CarregarViewBagsEstados(viewModel);
            return View(viewModel);
        }

        // GET: Aluno/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var aluno = await _context.Aluno.FindAsync(id);
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
                NomeRua = aluno.Endereco?.NomeRua, 
                NumeroRua = aluno.Endereco?.NumeroRua ?? 0,
                Complemento = aluno.Endereco?.Complemento,
                CEP = aluno.Endereco?.CEP ?? 0,
                EstadoId = aluno.EstadoId,
                Estado = aluno.Estado,  
                EscolaId = aluno.EscolaId,
                Escola = aluno.Escola,  
                Responsaveis = aluno.Responsaveis,  
                Aulas = aluno.Aulas  
            };

            CarregarViewBags(viewModel);
            CarregarViewBagsEstados(viewModel);
            return View(viewModel);
        }

        // POST: Aluno/Edit/5
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
                    var aluno = await _context.Aluno.FindAsync(id);
                    if (aluno == null)
                        return NotFound();

                    aluno.Nome = viewModel.Nome;
                    aluno.DataNascimento = viewModel.DataNascimento;
                    aluno.EmailEscolar = viewModel.EmailEscolar;
                    aluno.AnoInscricao = viewModel.AnoInscricao;
                    aluno.BolsaEscolar = viewModel.BolsaEscolar;
                    aluno.TurmaId = viewModel.TurmaId;
                    aluno.EnderecoId = viewModel.EnderecoId;
                    aluno.EscolaId = viewModel.EscolaId;

                    _context.Update(aluno);
                    await _context.SaveChangesAsync();  


                    var alunoTelefone = _context.AlunoTelefone.FirstOrDefault(at => at.AlunoId == aluno.Id);
                    if (alunoTelefone != null)
                    {

                        _context.AlunoTelefone.Remove(alunoTelefone);
                        await _context.SaveChangesAsync();
                    }

                    var telefone = new Telefone
                    {
                        DDD = viewModel.DDD,
                        Numero = viewModel.Numero
                    };

                    _context.Telefone.Add(telefone);  
                    await _context.SaveChangesAsync();

                    var novoAlunoTelefone = new AlunoTelefone
                    {
                        AlunoId = aluno.Id,
                        TelefoneId = telefone.Id
                    };

                    _context.Add(novoAlunoTelefone);  
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

            CarregarViewBags(viewModel);
            CarregarViewBagsEstados(viewModel);
            return View(viewModel);
        }


        // GET: Aluno/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var aluno = await _context.Aluno
                .Include(a => a.Turma)
                .Include(a => a.Endereco)
                .Include(a => a.Escola)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (aluno == null)
                return NotFound();

            var viewModel = new AlunoViewModel
            {
                Id = aluno.Id,
                Nome = aluno.Nome,
                EscolaId = aluno.EscolaId,
                TurmaId = aluno.TurmaId,
                EnderecoId = aluno.EnderecoId,
            };

            return View(viewModel);
        }

        // POST: Aluno/Delete/5
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
                })
                .ToList();

            ViewBag.Estados = estados;
        }
    }

    }

