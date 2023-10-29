using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LIDOM_MVC.Controllers
{
    public class PosicionesController : Controller
    {
        // GET: PosicionesController
        public ActionResult Index()
        {
            return View();
        }

        // GET: PosicionesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PosicionesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PosicionesController/Create
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

        // GET: PosicionesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PosicionesController/Edit/5
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

        // GET: PosicionesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PosicionesController/Delete/5
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
