using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LIDOM_MVC.Controllers
{
    public class CalendarioJuegosController : Controller
    {
        // GET: CalendarioJuegosController
        public ActionResult Index()
        {
            return View();
        }

        // GET: CalendarioJuegosController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CalendarioJuegosController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CalendarioJuegosController/Create
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

        // GET: CalendarioJuegosController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CalendarioJuegosController/Edit/5
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

        // GET: CalendarioJuegosController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CalendarioJuegosController/Delete/5
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
