using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCCamiloMentoria.Models;
using MVCCamiloMentoria.ViewModels;
using System.Threading.Tasks;

namespace MVCCamiloMentoria.Controllers
{
    public class EscolaController : Controller
    {
        private readonly EscolaContext _context;

        public EscolaController(EscolaContext context)
        {
            _context = context;
        }
        // GET: EscolaController
        public async Task<IActionResult> Index()
        {
            var escolas = await _context.Escola
                        .Include(e => e.Endereco)
                        .Include(e => e.Turmas)
                        .Include(e => e.Fornecedores)
                        .Include(e => e.PrestadorServico)
                        .Include(e => e.Telefones)
                        .Include(e => e.Equipamentos)
                        .Select(e => new EscolaViewModel
                        {
                            Nome = e.Nome,
                            Id = e.Id,
                        }).ToListAsync();

            return View(escolas);
        }

        // GET: EscolaController/Details/5
        public async Task<IActionResult> Details(int? id)
        { 
            if (id == null)
            {
                return NotFound();
            }

            var escolas = await _context.Escola
                        .Include(e => e.Endereco)
                        .Include(e => e.Turmas)
                        .Include(e => e.Fornecedores)
                        .Include(e => e.PrestadorServico)
                        .Include(e => e.Telefones)
                        .Include(e => e.Equipamentos)
                        .FirstOrDefaultAsync(e => e.Id == id);

            if(escolas == null)
            {
                return NotFound();
            }

            var escolaViewModel = new EscolaViewModel
            {
                Nome = escolas.Nome,
                Endereco = escolas.Endereco,
                Telefones = escolas.Telefones,
                Turmas = escolas.Turmas,
                Id = escolas.Id,
            };
                return View(escolaViewModel);
        }

        // GET: EscolaController/Create
        public ActionResult Create()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro no método Create: " + ex.Message);
                return View();
            }
        }

        // POST: EscolaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EscolaViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var endereco = new Endereco
                {
                    NomeRua = viewModel.NomeRua,
                    NumeroRua = viewModel.NumeroRua,
                    Complemento = viewModel.Complemento,
                    CEP = viewModel.CEP,
                    EstadoId = 1,
                };

                _context.Endereco.Add(endereco);
                await _context.SaveChangesAsync();

                var escola = new Escola
                {
                    Nome = viewModel.Nome,
                    EstadoId = 1,
                    EnderecoId = endereco.Id
                };

                _context.Escola.Add(escola);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            CarregarViewBags(viewModel);
            return View(viewModel);
        }

        // GET: EscolaController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: EscolaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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

        // GET: EscolaController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: EscolaController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
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
        private void CarregarViewBags(EscolaViewModel viewModel = null)
        {
            var estados = new List<SelectListItem>
    {
        new SelectListItem { Value = "1", Text = "São Paulo (SP)" }
    };
            ViewBag.Estados = estados;
        }



    }
}
