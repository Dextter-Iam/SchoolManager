using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCCamiloMentoria.Models;
using MVCCamiloMentoria.ViewModels;
using System.Threading.Tasks;

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
                                     CPF = f.CNPJ,
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
                return NotFound();
            }

            var fornecedor = await _context.Fornecedor
                .Include(f => f.Escola)
                .Include(f => f.Telefones)
                .FirstOrDefaultAsync(f => f.Id == id);

            if (fornecedor == null)
            {
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
                return RedirectToAction(nameof(Index));
            }
            {
                CarregarViewBags(viewModel);
                return View(viewModel);
            }
        }

        // GET: FornecedorController/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {   
            if(id == null)
            {
              return  NotFound();
            }

            var fornecedor = await _context.Fornecedor.FindAsync(id);
            if (fornecedor == null)
            {
                return NotFound();
            }

            var viewModel = new FornecedorViewModel
            {
                Id = fornecedor.Id,
                Nome = fornecedor.Nome,
                FinalidadeFornecedor = fornecedor.FinalidadeFornecedor,
                CPF = fornecedor.CPF,
                CNPJ = fornecedor.CNPJ,
                EscolaId = fornecedor.EscolaId,
            };

            CarregarViewBags(viewModel);
            return View(viewModel);
        }

        // POST: FornecedorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Fornecedor/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fornecedordeletar = await _context.Fornecedor
                .Include(f => f.Escola)
                .Include(f => f.Telefones)
                .FirstOrDefaultAsync(f => f.Id == id);

            if (fornecedordeletar == null)
            {
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
            }

            return RedirectToAction(nameof(Index));
        }

        private void CarregarViewBags(FornecedorViewModel viewModel = null)
        {
            ViewBag.EscolaId = new SelectList(_context.Escola, "Id", "Nome", viewModel?.EscolaId);
        }

    }


}
