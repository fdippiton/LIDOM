using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LIDOM_MVC.Controllers
{
    public class TemporadasController : Controller
    {
        // GET: TemporadasController
        public ActionResult Index()
        {
            return View();
        }

        // GET: TemporadasController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TemporadasController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TemporadasController/Create
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

        // GET: TemporadasController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TemporadasController/Edit/5
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

        // GET: TemporadasController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TemporadasController/Delete/5
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
