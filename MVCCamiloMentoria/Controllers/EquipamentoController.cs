using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCCamiloMentoria.Models;
using MVCCamiloMentoria.ViewModels;

namespace MVCCamiloMentoria.Controllers
{
    public class EquipamentoController : Controller
    {
        private readonly EscolaContext _context;

        public EquipamentoController(EscolaContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int pagina = 1, EquipamentoViewModel filtro = null)
        {
            int registrosPorPagina = 10;

            var query = _context.Equipamento
                                .Where(e => !e.Excluido)
                                .Include(e => e.Marca)
                                .Include(e => e.Modelo)
                                .Include(e => e.Escola)
                                .AsQueryable();

            query = AplicarFiltros(query, filtro);

            var totalRegistros = await query.CountAsync();
            var totalPaginas = (int)Math.Ceiling(totalRegistros / (double)registrosPorPagina);

            var equipamentos = await query
                                 .Skip((pagina - 1) * registrosPorPagina)
                                 .Take(registrosPorPagina)
                                 .ToListAsync();

            var equipamentosViewModel = equipamentos.Select(e => new EquipamentoViewModel
            {
                Id = e.Id,
                Nome = e.Nome,
                StatusOperacional = e.StatusOperacional,
                MarcaId = e.MarcaId,
                Marca = e.Marca,
                ModeloId = e.ModeloId,
                Modelo = e.Modelo,
                EscolaId = e.EscolaId,
                Escola = e.Escola
            }).ToList();

            ViewBag.AplicarFiltros = filtro;
            ViewBag.PaginaAtual = pagina;
            ViewBag.TotalPaginas = totalPaginas;
            return View(equipamentosViewModel);
        }


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var equipamento = await _context.Equipamento
                .Where(e => !e.Excluido)
                .Include(e => e.Escola)
                .Include(e => e.Marca)
                .Include(e => e.Modelo)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (equipamento == null) return NotFound();

            var viewModel = new EquipamentoViewModel
            {
                Id = equipamento.Id,
                Nome = equipamento.Nome,
                StatusOperacional = equipamento.StatusOperacional,
                MarcaId = equipamento.MarcaId,
                ModeloId = equipamento.ModeloId,
                EscolaId = equipamento.EscolaId,
                Escola = equipamento.Escola,
                Marca = equipamento.Marca,
                Modelo = equipamento.Modelo
            };

            return View(viewModel);
        }

        public async Task<IActionResult> Create()
        {
            await CarregarViewBagsAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EquipamentoViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var equipamento = new Equipamento
                {
                    Nome = viewModel.Nome,
                    StatusOperacional = viewModel.StatusOperacional,
                    MarcaId = viewModel.MarcaId,
                    ModeloId = viewModel.ModeloId,
                    EscolaId = viewModel.EscolaId
                };

                _context.Add(equipamento);
                await _context.SaveChangesAsync();
                TempData["MensagemSucesso"] = "Equipamento criado com sucesso!";
                return RedirectToAction(nameof(Index));
            }

            TempData["MensagemErro"] = "Erro ao criar Equipamento. Verifique os dados informados.";
            await CarregarViewBagsAsync(viewModel);
            return View(viewModel);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var equipamento = await _context.Equipamento
                                            .Where(e => !e.Excluido)
                                            .Include(e => e.Escola)
                                            .Include(e => e.Marca)
                                            .Include(e => e.Modelo)
                                            .FirstOrDefaultAsync(m => m.Id == id);

            if (equipamento == null) return NotFound();

            var viewModel = new EquipamentoViewModel
            {
                Id = equipamento.Id,
                Nome = equipamento.Nome,
                StatusOperacional = equipamento.StatusOperacional,
                MarcaId = equipamento.MarcaId,
                ModeloId = equipamento.ModeloId,
                EscolaId = equipamento.EscolaId
            };

            await CarregarViewBagsAsync(viewModel);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EquipamentoViewModel viewModel)
        {
            if (id != viewModel.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var equipamento = await _context.Equipamento.FindAsync(id);
                    if (equipamento != null)
                    {
                        equipamento.Nome = viewModel.Nome;
                        equipamento.StatusOperacional = viewModel.StatusOperacional;
                        equipamento.MarcaId = viewModel.MarcaId;
                        equipamento.ModeloId = viewModel.ModeloId;
                        equipamento.EscolaId = viewModel.EscolaId;

                        _context.Update(equipamento);
                        await _context.SaveChangesAsync();

                        TempData["MensagemSucesso"] = "Equipamento editado com sucesso!";
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EquipamentoExists(viewModel.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }

            TempData["MensagemErro"] = "Erro ao editar Equipamento. Verifique os dados informados.";
            await CarregarViewBagsAsync(viewModel);
            return View(viewModel);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var equipamento = await _context.Equipamento
                .Where(e => !e.Excluido)
                .Include(e => e.Escola)
                .Include(e => e.Marca)
                .Include(e => e.Modelo)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (equipamento == null) return NotFound();

            var viewModel = new EquipamentoViewModel
            {
                Id = equipamento.Id,
                Nome = equipamento.Nome,
                Escola = equipamento.Escola,
                Marca = equipamento.Marca,
                Modelo = equipamento.Modelo,
                StatusOperacional = equipamento.StatusOperacional
            };

            return View(viewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var equipamento = await _context.Equipamento.FindAsync(id);
            if (equipamento != null)
            {
                equipamento.Excluido = true;
                _context.Equipamento.Update(equipamento);
                await _context.SaveChangesAsync();

                TempData["MensagemSucesso"] = "Equipamento excluído com sucesso!";
            }
            else
            {
                TempData["MensagemErro"] = "Erro ao excluir Equipamento. Registro não encontrado.";
            }

            return RedirectToAction(nameof(Index));
        }

        private bool EquipamentoExists(int id)
        {
            return _context.Equipamento.Any(e => e.Id == id);
        }

        private async Task CarregarViewBagsAsync(EquipamentoViewModel viewModel = null)
        {
            var escolas = await _context.Escola.ToListAsync();
            var marcas = await _context.Marca.ToListAsync();
            var modelos = await _context.Modelo.ToListAsync();

            ViewBag.EscolaId = new SelectList(escolas, "Id", "Nome", viewModel?.EscolaId);
            ViewBag.MarcaId = new SelectList(marcas, "Id", "Nome", viewModel?.MarcaId);
            ViewBag.ModeloId = new SelectList(modelos, "Id", "Nome", viewModel?.ModeloId);
        }
        private IQueryable<Equipamento> AplicarFiltros(IQueryable<Equipamento> query, EquipamentoViewModel filtro)
        {
            if (filtro == null)
                return query;

            if (!string.IsNullOrEmpty(filtro.NomeNormalizado))
            {
                var nome = filtro.NomeNormalizado;
                query = query.Where(a => a.Nome != null && a.Nome.ToLower().Contains(nome));
            }

            if (!string.IsNullOrEmpty(filtro.MarcaNormalizada))
            {
                var marca = filtro.MarcaNormalizada;
                query = query.Where(a => a.Marca != null && a.Marca.Nome.ToLower().Contains(marca));
            }

            if (!string.IsNullOrEmpty(filtro.ModeloNormalizado))
            {
                var modelo = filtro.ModeloNormalizado;
                query = query.Where(a => a.Modelo != null && a.Modelo.Nome.ToLower().Contains(modelo));
            }

            if (!string.IsNullOrWhiteSpace(filtro.EscolaNormalizada))
            {
                var escola = filtro.EscolaNormalizada;
                query = query.Where(a => a.Escola != null && a.Escola.Nome.ToLower().Contains(escola));
            }

            if (!string.IsNullOrWhiteSpace(filtro.FiltroGeralNormalizado) && filtro.FiltroGeralNormalizado.Length >= 3)
            {
                var termo = filtro.FiltroGeralNormalizado;
                query = query.Where(a =>
                    (a.Nome != null && a.Nome.ToLower().Contains(termo)) ||
                    (a.Escola != null && a.Escola.Nome.ToLower().Contains(termo)) ||
                    (a.Marca != null && a.Marca.Nome.ToLower().Contains(termo)) ||
                    (a.Modelo != null && a.Modelo.Nome.ToLower().Contains(termo))
                );
            }

            return query;
        }


    }
}
