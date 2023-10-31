using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using LIDOM_MVC.Models;
using System.Net.Http.Headers;

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
        [HttpGet]
        public async Task <ActionResult> Details(int id)
        {
            string baseApiUrl = _configuration.GetSection("LigaDominicanaApi").Value!;

            Estadio EstaInfo = new Estadio();
            using (var client = new HttpClient())
            {

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync($"{baseApiUrl}/estadios/" + id.ToString());

                if (Res.IsSuccessStatusCode)
                {
                    var EstResponse = Res.Content.ReadAsStringAsync().Result;
                    EstaInfo = JsonConvert.DeserializeObject<Estadio>(EstResponse)!;
                }

                return View(EstaInfo);
            }
        }

        // GET: EstadiosController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EstadiosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([FromForm] Estadio estadio)
        {
            string baseApiUrl = _configuration.GetSection("LigaDominicanaApi").Value!;

            try
            {
                var postTask = httpClient.PostAsJsonAsync<Estadio>($"{baseApiUrl}/estadios", estadio);
                postTask.Wait();
                var result = postTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError(String.Empty, "error");
            }
            return View(estadio);
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
        [HttpGet]
        public async Task <ActionResult> Delete(int id)
        {
            string baseApiUrl = _configuration.GetSection("LigaDominicanaApi").Value!;


            Estadio EstInfo = new Estadio();
            using (var client = new HttpClient())
            {

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync($"{baseApiUrl}/estadios/" + id.ToString());

                if (Res.IsSuccessStatusCode)
                {
                    var EstaResponse = Res.Content.ReadAsStringAsync().Result;
                    EstInfo = JsonConvert.DeserializeObject<Estadio>(EstaResponse)!;
                }

                return View(EstInfo);
            }
        }

        // POST: EstadiosController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id)
        {
            string baseApiUrl = _configuration.GetSection("LigaDominicanaApi").Value!;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseApiUrl);
                var deleteTask = client.DeleteAsync($"{baseApiUrl}/estadios/" + id.ToString());
                deleteTask.Wait();
                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }
    }
}
