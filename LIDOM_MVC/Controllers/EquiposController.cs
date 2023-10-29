using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LIDOM_MVC.Controllers
{
    public class EquiposController : Controller
    {
        // GET: EquiposController
        public ActionResult Index()
        {
            return View();
        }

        // GET: EquiposController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: EquiposController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EquiposController/Create
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

        // GET: EquiposController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: EquiposController/Edit/5
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

        // GET: EquiposController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: EquiposController/Delete/5
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
