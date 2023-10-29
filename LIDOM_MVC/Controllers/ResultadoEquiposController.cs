using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LIDOM_MVC.Controllers
{
    public class ResultadoEquiposController : Controller
    {
        // GET: ResultadoEquiposController
        public ActionResult Index()
        {
            return View();
        }

        // GET: ResultadoEquiposController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ResultadoEquiposController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ResultadoEquiposController/Create
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

        // GET: ResultadoEquiposController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ResultadoEquiposController/Edit/5
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

        // GET: ResultadoEquiposController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ResultadoEquiposController/Delete/5
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
