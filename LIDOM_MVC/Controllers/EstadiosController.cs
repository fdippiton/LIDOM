using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using LIDOM_MVC.Models;

namespace LIDOM_MVC.Controllers
{
    public class EstadiosController : Controller
    {

        private readonly IConfiguration _configuration;
        private readonly HttpClient httpClient;

        public EstadiosController(IConfiguration configuration)
        {
            httpClient = new HttpClient();
            _configuration = configuration;
        }

        // GET: EstadiosController
        public ActionResult Index()
        {
            string baseApiUrl = _configuration.GetSection("LigaDominicanaApi").Value!;
            List<Estadio> estadios = new List<Estadio>(); // []
            string jsonEstadiosResponse = httpClient.GetStringAsync($"{baseApiUrl}/estadios").Result;
            estadios = string.IsNullOrEmpty(jsonEstadiosResponse) ? estadios : JsonConvert.DeserializeObject<List<Estadio>>(jsonEstadiosResponse)!;

            return View(estadios);
        }

        // GET: EstadiosController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: EstadiosController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EstadiosController/Create
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

        // GET: EstadiosController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: EstadiosController/Edit/5
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

        // GET: EstadiosController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: EstadiosController/Delete/5
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
