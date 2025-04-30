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
                .Include(c => c.Endereco)
                .Include(c => c.Escola)
                .AsNoTracking()
                .ToListAsync();

            var viewModel = coordenadores.Select(c => new CoordenadorViewModel
            {
                Id = c.Id,
                Nome = c.Nome,
                Matricula = c.Matricula,
                Endereco = c.Endereco,
                Escola = c.Escola
            }).ToList();

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
                Endereco = coordenador.Endereco,
                Escola = coordenador.Escola,
                Telefones = coordenador.Telefones?.ToList()
            };

            return View(viewModel);
        }

        // GET: Coordenador/Create
        public IActionResult Create()
        {
            CarregarDependencias();
            return View(new CoordenadorViewModel());
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
                    int? cep = null;
                    if (!string.IsNullOrWhiteSpace(viewModel.CEP))
                    {
                        if (!int.TryParse(viewModel.CEP, out int parsedCep))
                        {
                            ModelState.AddModelError("CEP", "CEP inválido. Informe apenas números.");
                            CarregarDependencias();
                            return View(viewModel);
                        }
                        cep = parsedCep;
                    }

                    var endereco = new EnderecoViewModel
                    {
                        NomeRua = viewModel.NomeRua,
                        NumeroRua = viewModel.NumeroRua,
                        Complemento = viewModel.Complemento,
                        CEP = cep,
                        EstadoId = (int)viewModel.EstadoId!
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

                    var telefone = new Telefone
                    {
                        DDD = viewModel.DDD,
                        Numero = viewModel.Numero,
                        CoordenadorId = coordenador.Id,
                        EscolaId = (int)viewModel.EscolaId!,
                    };
                    _context.Telefone.Add(telefone);

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

            var telefone = coordenador.Telefones?.FirstOrDefault();
            var viewModel = new CoordenadorViewModel
            {
                Id = coordenador.Id,
                Nome = coordenador.Nome,
                Matricula = coordenador.Matricula,
                EnderecoId = coordenador.EnderecoId,
                NomeRua = coordenador.Endereco?.NomeRua,
                NumeroRua = coordenador.Endereco?.NumeroRua ?? 0,
                Complemento = coordenador.Endereco?.Complemento,
                CEP = coordenador.Endereco?.CEP?.ToString("00000000"),
                EstadoId = coordenador.Endereco?.EstadoId,
                EscolaId = coordenador.EscolaId,
                Telefones = coordenador.Telefones?.ToList(),
                DDD = telefone?.DDD ?? 0,
                Numero = telefone?.Numero ?? 0,
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
                        coordenador.Endereco.NomeRua = viewModel.NomeRua;
                        coordenador.Endereco.NumeroRua = viewModel.NumeroRua;
                        coordenador.Endereco.Complemento = viewModel.Complemento;
                        coordenador.Endereco.CEP = string.IsNullOrWhiteSpace(viewModel.CEP) ? null : int.Parse(viewModel.CEP);
                        coordenador.Endereco.EstadoId = (int)viewModel.EstadoId!;
                    }

                    // Atualizar Telefones
                    if (coordenador.Telefones != null && coordenador.Telefones.Any())
                        _context.Telefone.RemoveRange(coordenador.Telefones);

                    var telefone = new Telefone
                    {
                        DDD = viewModel.DDD,
                        Numero = viewModel.Numero,
                        CoordenadorId = coordenador.Id,
                        EscolaId = (int)viewModel.EscolaId!,
                    };
                    _context.Telefone.Add(telefone);

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
                Endereco = coordenador.Endereco,
                Escola = coordenador.Escola,
                Telefones = coordenador.Telefones?.ToList()
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
                    .Include(c => c.Endereco)
                    .Include(c => c.Telefones)
                    .FirstOrDefaultAsync(c => c.Id == id);

                if (coordenador == null)
                {
                    TempData["MensagemErro"] = "Coordenador não encontrado.";
                    return RedirectToAction(nameof(Index));
                }

                if (coordenador.Telefones != null && coordenador.Telefones.Any())
                    _context.Telefone.RemoveRange(coordenador.Telefones);

                if (coordenador.Endereco != null)
                    _context.Endereco.Remove(coordenador.Endereco);

                _context.Coordenador.Remove(coordenador);
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


    }
}
