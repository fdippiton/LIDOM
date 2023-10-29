using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LIDOM_MVC.Controllers
{
    public class FechaPartidosController : Controller
    {
        // GET: FechaPartidosController
        public ActionResult Index()
        {
            return View();
        }

        // GET: FechaPartidosController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: FechaPartidosController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FechaPartidosController/Create
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

        // GET: FechaPartidosController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: FechaPartidosController/Edit/5
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

        // GET: FechaPartidosController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: FechaPartidosController/Delete/5
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
