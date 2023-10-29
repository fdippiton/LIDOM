using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LIDOM_MVC.Controllers
{
    public class JugadoresController : Controller
    {
        // GET: JugadoresController
        public ActionResult Index()
        {
            return View();
        }

        // GET: JugadoresController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: JugadoresController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: JugadoresController/Create
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

        // GET: JugadoresController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: JugadoresController/Edit/5
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

        // GET: JugadoresController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: JugadoresController/Delete/5
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
