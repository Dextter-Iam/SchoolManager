using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCCamiloMentoria.Models;
using MVCCamiloMentoria.ViewModels;

namespace MVCCamiloMentoria.Controllers
{
    public class FornecedorController : Controller
    {
        private readonly EscolaContext _context;

        public FornecedorController(EscolaContext context)
        {
            _context = context;
        }

        // GET: FornecedorController
        public async Task<IActionResult> Index(FornecedorViewModel filtro = null)
        {
            var query = _context.Fornecedor.AsQueryable();
            query = AplicarFiltros(query, filtro);


            var fornecedores = await query
                                 .Select(f => new FornecedorViewModel
                                 {
                                     Nome = f.Nome,
                                     CNPJ = f.CNPJ,
                                     CPF = f.CPF,
                                     EscolaId = f.EscolaId,
                                     Escola = f.Escola,
                                     FinalidadeFornecedor = f.FinalidadeFornecedor,
                                     Id = f.Id,
                                 }).ToListAsync();

            ViewBag.AplicarFiltros = filtro;
            return View(fornecedores);
        }

        // GET: FornecedorController/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                TempData["MensagemErro"] = "Fornecedor não encontrado.";
                return NotFound();
            }

            var fornecedor = await _context.Fornecedor
                .Include(f => f.Escola)
                .Include(f => f.Telefones)
                .FirstOrDefaultAsync(f => f.Id == id);

            if (fornecedor == null)
            {
                TempData["MensagemErro"] = "Fornecedor não encontrado.";
                return NotFound();
            }

            var fornecedorViewModel = new FornecedorViewModel
            {
                Id = fornecedor.Id,
                Nome = fornecedor.Nome,
                FinalidadeFornecedor = fornecedor.FinalidadeFornecedor,
                CPF = fornecedor.CPF,
                CNPJ = fornecedor.CNPJ,
                EscolaId = fornecedor.EscolaId,
            };

            return View(fornecedorViewModel);
        }

        // GET: FornecedorController/Create
        public ActionResult Create()
        {
            CarregarViewBags();
            return View();
        }

        // POST: FornecedorController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FornecedorViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var fornecedor = new Fornecedor
                {
                    Id = viewModel.Id,
                    Nome = viewModel.Nome,
                    FinalidadeFornecedor = viewModel.FinalidadeFornecedor,
                    CPF = viewModel.CPF,
                    CNPJ = viewModel.CNPJ,
                    EscolaId = viewModel.EscolaId,
                };

                _context.Add(fornecedor);
                await _context.SaveChangesAsync();
                TempData["MensagemSucesso"] = "Fornecedor criado com sucesso!";
                return RedirectToAction(nameof(Index));
            }

            TempData["MensagemErro"] = "Erro ao criar o fornecedor. Verifique os dados e tente novamente.";
            CarregarViewBags(viewModel);
            return View(viewModel);
        }

        // GET: FornecedorController/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                TempData["MensagemErro"] = "Fornecedor não encontrado.";
                return NotFound();
            }

            var fornecedor = await _context.Fornecedor.FindAsync(id);
            if (fornecedor == null)
            {
                TempData["MensagemErro"] = "Fornecedor não encontrado.";
                return NotFound();
            }

            var viewModel = new FornecedorViewModel
            {
                Id = fornecedor.Id,
                Nome = fornecedor.Nome,
                FinalidadeFornecedor = fornecedor.FinalidadeFornecedor,
                CPF = fornecedor.CPF,
                CNPJ = fornecedor.CNPJ,
                EscolaId = fornecedor.EscolaId
            };

            CarregarViewBags(viewModel);
            return View(viewModel);
        }

        // POST: FornecedorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, FornecedorViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                TempData["MensagemErro"] = "Fornecedor inválido.";
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                TempData["MensagemErro"] = "Erro de validação. Verifique os dados informados.";
                CarregarViewBags(viewModel);
                return View(viewModel);
            }

            try
            {
                var fornecedor = await _context.Fornecedor.FindAsync(id);
                if (fornecedor == null)
                {
                    TempData["MensagemErro"] = "Fornecedor não encontrado.";
                    return NotFound();
                }

                fornecedor.Nome = viewModel.Nome;
                fornecedor.FinalidadeFornecedor = viewModel.FinalidadeFornecedor;
                fornecedor.CPF = viewModel.CPF;
                fornecedor.CNPJ = viewModel.CNPJ;
                fornecedor.EscolaId = viewModel.EscolaId;

                _context.Update(fornecedor);
                await _context.SaveChangesAsync();

                TempData["MensagemSucesso"] = "Fornecedor atualizado com sucesso!";
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException)
            {
                TempData["MensagemErro"] = "Erro ao atualizar o fornecedor. Tente novamente.";
                CarregarViewBags(viewModel);
                return View(viewModel);
            }
        }

        // GET: Fornecedor/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                TempData["MensagemErro"] = "Fornecedor não encontrado.";
                return NotFound();
            }

            var fornecedordeletar = await _context.Fornecedor
                .Include(f => f.Escola)
                .Include(f => f.Telefones)
                .FirstOrDefaultAsync(f => f.Id == id);

            if (fornecedordeletar == null)
            {
                TempData["MensagemErro"] = "Fornecedor não encontrado.";
                return NotFound();
            }

            var viewModel = new FornecedorViewModel
            {
                Id = fornecedordeletar.Id,
                Nome = fornecedordeletar.Nome,
                CNPJ = fornecedordeletar.CNPJ,
                CPF = fornecedordeletar.CPF,
                EscolaId = fornecedordeletar.EscolaId,
                FinalidadeFornecedor = fornecedordeletar.FinalidadeFornecedor,
            };

            return View(viewModel);
        }

        // POST: Fornecedor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fornecedor = await _context.Fornecedor.FindAsync(id);
            if (fornecedor != null)
            {
                _context.Fornecedor.Remove(fornecedor);
                await _context.SaveChangesAsync();
                TempData["MensagemSucesso"] = "Fornecedor excluído com sucesso!";
            }
            else
            {
                TempData["MensagemErro"] = "Erro ao excluir o fornecedor. Fornecedor não encontrado.";
            }

            return RedirectToAction(nameof(Index));
        }

        private void CarregarViewBags(FornecedorViewModel viewModel = null)
        {
            ViewBag.EscolaId = new SelectList(_context.Escola, "Id", "Nome", viewModel?.EscolaId);
        }
        private IQueryable<Fornecedor> AplicarFiltros(IQueryable<Fornecedor> query, FornecedorViewModel filtro)
        {
            if (filtro == null)
                return query;

            if (!string.IsNullOrWhiteSpace(filtro.Nome))
            {
                var nomeFiltro = filtro.Nome.Trim().ToLower();
                query = query.Where(a => a.Nome != null && a.Nome.ToLower().Contains(nomeFiltro));
            }

            if (!string.IsNullOrWhiteSpace(filtro.CNPJ))
            {
                var cnpjFiltro = filtro.CNPJ.Trim().ToLower();
                query = query.Where(a => a.CNPJ != null && a.CNPJ.ToLower().Contains(cnpjFiltro));
            }

            if (!string.IsNullOrWhiteSpace(filtro.CPF))
            {
                var cpfFiltro = filtro.CPF.Trim().ToLower();
                query = query.Where(a => a.CPF != null && a.CPF.ToLower().Contains(cpfFiltro));
            }

            if (!string.IsNullOrWhiteSpace(filtro.FinalidadeFornecedor))
            {
                var finalidadeFiltro = filtro.FinalidadeFornecedor.Trim().ToLower();
                query = query.Where(a => a.FinalidadeFornecedor != null && a.FinalidadeFornecedor.ToLower().Contains(finalidadeFiltro));
            }

            if (filtro.EscolaId > 0)
            {
                query = query.Where(a => a.EscolaId == filtro.EscolaId);
            }
            else if (filtro.Escola != null && !string.IsNullOrWhiteSpace(filtro.Escola.Nome))
            {
                var escolaFiltro = filtro.Escola.Nome.Trim().ToLower();
                query = query.Where(a => a.Escola != null && a.Escola.Nome.ToLower().Contains(escolaFiltro));
            }

            if (!string.IsNullOrEmpty(filtro.FiltroGeralNormalizado) && filtro.FiltroGeralNormalizado.Length >= 3)
            {
                var termo = filtro.FiltroGeralNormalizado;
                bool termoEhNumero = long.TryParse(termo, out long termoNumero);

                query = query.Where(c =>
                    (c.Nome != null && c.Nome.ToLower().Contains(termo)) ||
                    (c.CNPJ != null && c.CNPJ.ToLower().Contains(termo)) ||
                    (c.CPF != null && c.CPF.ToLower().Contains(termo)) ||
                    (c.FinalidadeFornecedor != null && c.FinalidadeFornecedor.ToLower().Contains(termo)) ||
                    (c.Escola != null && c.Escola.Nome.ToLower().Contains(termo))
                );
            }

            return query;
        }

    }
}
