using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using MVCCamiloMentoria.Models;
using MVCCamiloMentoria.ViewModels;

namespace MVCCamiloMentoria.Controllers
{
    public class PrestadorServicoController : Controller
    {
        private readonly EscolaContext _context;

        public PrestadorServicoController(EscolaContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(PrestadorServicoViewModel filtro )
        {
            var query = _context.PrestadorServico
                                .Include(p => p.Escola)
                                .Include(p => p.Telefones)
                                .AsQueryable();
            query = AplicarFiltros(query, filtro);

            var prestadores = await query.ToListAsync();

            var prestadoresViewModel = prestadores.Select(p => new PrestadorServicoViewModel
                {
                    Id = p.Id,
                    Nome = p.Nome,
                    CPF = p.CPF,
                    CNPJ = p.CNPJ,
                    EmpresaNome = p.EmpresaNome,
                    ServicoFinalidade = p.ServicoFinalidade,
                    Telefones = p.Telefones?.Select(t => new TelefoneViewModel
                    {
                        Id = t.Id,
                        Numero = t.Numero,
                    }).ToList(),
                    Escola = p.Escola
                }).ToList();

            ViewBag.AplicarFiltros = filtro;
            return View(prestadoresViewModel);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var prestador = await _context.PrestadorServico
                .Include(p => p.Escola)
                .Include(p => p.Telefones)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (prestador == null)
                return NotFound();

            return View(prestador);
        }

        public ActionResult Create()
        {
            var viewModel = new PrestadorServicoViewModel
            {
                Escola = new Escola()
            };

            CarregarViewBags(viewModel);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PrestadorServicoViewModel prestadorViewModel)
        {
            if (ModelState.IsValid)
            {
                var prestador = new PrestadorServico
                {
                    Nome = prestadorViewModel.Nome,
                    CNPJ = prestadorViewModel.CNPJ,
                    CPF = prestadorViewModel.CPF,
                    EmpresaNome = prestadorViewModel.EmpresaNome,
                    EscolaId = prestadorViewModel.Escola.Id, 
                    ServicoFinalidade = prestadorViewModel.ServicoFinalidade,
                };

                _context.Add(prestador);
                await _context.SaveChangesAsync();
                TempData["MensagemSucesso"] = "Prestador cadastrado com sucesso!";
                return RedirectToAction(nameof(Index));
            }

            ViewBag.EscolaId = new SelectList(_context.Escola, "Id", "Nome", prestadorViewModel.Escola.Id);

            return View(prestadorViewModel);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var prestador = await _context.PrestadorServico.FindAsync(id);
            if (prestador == null)
                return NotFound();

            ViewBag.EscolaId = new SelectList(_context.Escola, "Id", "Nome", prestador.EscolaId);
            return View(prestador);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PrestadorServicoViewModel prestadorViewModel)
        {
            if (id != prestadorViewModel.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var prestador = new PrestadorServico
                    {   
                        Id = prestadorViewModel.Id,
                        Nome = prestadorViewModel.Nome,
                        CNPJ = prestadorViewModel.CNPJ,
                        CPF = prestadorViewModel.CPF,
                        EmpresaNome = prestadorViewModel.EmpresaNome,
                        EscolaId = prestadorViewModel.Escola!.Id,
                        ServicoFinalidade = prestadorViewModel.ServicoFinalidade,
                        Escola = prestadorViewModel.Escola,
                    };

                    _context.Update(prestador);
                    await _context.SaveChangesAsync();
                    TempData["MensagemSucesso"] = "Prestador atualizado com sucesso!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PrestadorExists(prestadorViewModel.Id))
                        return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewBag.EscolaId = new SelectList(_context.Escola, "Id", "Nome", prestadorViewModel.Escola!.Id);
            return View(prestadorViewModel);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var prestador = await _context.PrestadorServico
                .Include(p => p.Escola)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (prestador == null)
                return NotFound();

            return View(prestador);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var prestador = await _context.PrestadorServico.FindAsync(id);
            if (prestador != null)
            {
                _context.PrestadorServico.Remove(prestador);
                await _context.SaveChangesAsync();
                TempData["MensagemSucesso"] = "Prestador excluído com sucesso!";
            }
            return RedirectToAction(nameof(Index));
        }

        private bool PrestadorExists(int id)
        {
            return _context.PrestadorServico.Any(e => e.Id == id);
        }
        private void CarregarViewBags(PrestadorServicoViewModel viewModel)
        {
            ViewBag.EscolaId = new SelectList(_context.Escola, "Id", "Nome", viewModel.Escola?.Id);
        }
        private IQueryable<PrestadorServico> AplicarFiltros(IQueryable<PrestadorServico> query, PrestadorServicoViewModel filtro)
        {
            if (filtro == null)
                return query;

            if (!string.IsNullOrWhiteSpace(filtro.FiltroNome))
            {
                var nomeFiltro = filtro.NomeNormalizado;
                query = query.Where(a => a.Nome != null && a.Nome.ToLower().Contains(nomeFiltro));
            }

            if (!string.IsNullOrWhiteSpace(filtro.FiltroCNPJ))
            {
                var cnpjFiltro = filtro.CNPJNormalizado;
                query = query.Where(a => a.CNPJ != null && a.CNPJ.ToLower().Contains(cnpjFiltro));
            }

            if (!string.IsNullOrWhiteSpace(filtro.FiltroCPF))
            {
                var cpfFiltro = filtro.CPFNormalizado;
                query = query.Where(a => a.CPF != null && a.CPF.ToLower().Contains(cpfFiltro));
            }

            if (!string.IsNullOrWhiteSpace(filtro.FiltroEmpresaNome))
            {
                var empresaFiltro = filtro.EmpresaNomeNormalizado;
                query = query.Where(a => a.EmpresaNome != null && a.EmpresaNome.ToLower().Contains(empresaFiltro));
            }

            if (!string.IsNullOrWhiteSpace(filtro.EscolaNormalizada))
            {
                var escolaFiltro = filtro.EscolaNormalizada;
                query = query.Where(a => a.Escola != null && a.Escola.Nome.ToLower().Contains(escolaFiltro));
            }

            if (!string.IsNullOrEmpty(filtro.FiltroGeralNormalizado) && filtro.FiltroGeralNormalizado.Length >= 3)
            {
                var termo = filtro.FiltroGeralNormalizado;
                query = query.Where(c =>
                    (c.Nome != null && c.Nome.ToLower().Contains(termo)) ||
                    (c.CNPJ != null && c.CNPJ.ToLower().Contains(termo)) ||
                    (c.CPF != null && c.CPF.ToLower().Contains(termo)) ||
                    (c.EmpresaNome != null && c.EmpresaNome.ToLower().Contains(termo)) ||
                    (c.Escola != null && c.Escola.Nome.ToLower().Contains(termo))
                );
            }

            return query;
        }



    }
}