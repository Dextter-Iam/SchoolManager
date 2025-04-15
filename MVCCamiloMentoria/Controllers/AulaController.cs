using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MVCCamiloMentoria.Controllers
{
    public class AulaController : Controller
    {
        // GET: AulaController
        public ActionResult Index()
        {
            return View();
        }

        // GET: AulaController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AulaController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AulaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: AulaController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AulaController/Edit/5
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

        // GET: AulaController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AulaController/Delete/5
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
    }
}
