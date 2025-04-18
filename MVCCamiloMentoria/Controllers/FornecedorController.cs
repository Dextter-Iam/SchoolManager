// ... using directives mantidos

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
        public async Task<IActionResult> Index()
        {
            var fornecedores = await _context.Fornecedor
                                 .Select(f => new FornecedorViewModel
                                 {
                                     Nome = f.Nome,
                                     CNPJ = f.CNPJ,
                                     CPF = f.CPF,
                                     EscolaId = f.EscolaId,
                                     FinalidadeFornecedor = f.FinalidadeFornecedor,
                                     Id = f.Id,
                                 }).ToListAsync();
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
    }
}
