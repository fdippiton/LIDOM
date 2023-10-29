using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LIDOM_MVC.Controllers
{
    public class PartidosController : Controller
    {
        // GET: PartidosController
        public ActionResult Index()
        {
            return View();
        }

        // GET: PartidosController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PartidosController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PartidosController/Create
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

        // GET: PartidosController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PartidosController/Edit/5
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

        // GET: PartidosController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PartidosController/Delete/5
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
