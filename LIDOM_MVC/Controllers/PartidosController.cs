using LIDOM_MVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace LIDOM_MVC.Controllers
{
    public class PartidosController : Controller
    {
        // GET: PartidosController
        private readonly IConfiguration _configuration;
        private readonly HttpClient httpClient;

        public PartidosController(IConfiguration configuration)
        {
            httpClient = new HttpClient();
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            string baseApiUrl = _configuration.GetSection("LigaDominicanaApi").Value;
            List<Partido> Info = new List<Partido>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseApiUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("api/Partidos");
                if (Res.IsSuccessStatusCode)
                {
                    var Response = Res.Content.ReadAsStringAsync().Result;
                    Info = JsonConvert.DeserializeObject<List<Partido>>(Response);
                }

                return View(Info);
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create([FromForm] Partido partido)
        {
            string baseApiUrl = _configuration.GetSection("LigaDominicanaApi").Value;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseApiUrl);
                var postTask = client.PostAsJsonAsync<Partido>("api/Partidos", partido);
                postTask.Wait();
                var result = postTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            ModelState.AddModelError(String.Empty, "error, esto no sirve ya.");
            return View(partido);
        }

        [HttpGet]
        public async Task<ActionResult> Details(int id)
        {
            string baseApiUrl = _configuration.GetSection("LigaDominicanaApi").Value;
            Partido Info = new Partido();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseApiUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync($"api/Partidos/" + id.ToString());
                if (Res.IsSuccessStatusCode)
                {
                    var Response = Res.Content.ReadAsStringAsync().Result;
                    Info = JsonConvert.DeserializeObject<Partido>(Response);
                }
                return View(Info);
            }
        }

        public ActionResult Delete(int id)
        {
            string baseApiUrl = _configuration.GetSection("LigaDominicanaApi").Value;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseApiUrl);
                var deleteTask = client.DeleteAsync($"api/Partidos/" + id.ToString());
                deleteTask.Wait();
                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> Edit(string id)
        {
            string baseApiUrl = _configuration.GetSection("LigaDominicanaApi").Value;
            Partido partido = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseApiUrl);

                var result = await client.GetAsync($"api/Partidos" + id.ToString());

                if (result.IsSuccessStatusCode)
                {
                    partido = await result.Content.ReadAsAsync<Partido>();
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "error, esto no sirve ya.");
                }
            }
            return View(partido);
        }

        [HttpPost]
        public async Task<ActionResult> Edit([FromForm] Partido partido)
        {
            string baseApiUrl = _configuration.GetSection("LigaDominicanaApi").Value;
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseApiUrl);
                    var response = await client.PutAsJsonAsync("api/Partidos", partido);
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "error, esto no sirve ya.");
                    }
                }
                return RedirectToAction("Index");
            }
            return View(partido);
        }
    }
}
