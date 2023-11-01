using LIDOM_MVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace LIDOM_MVC.Controllers
{
    public class FechaPartidosController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient httpClient;

        public FechaPartidosController(IConfiguration configuration)
        {
            httpClient = new HttpClient();
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            string baseApiUrl = _configuration.GetSection("LigaDominicanaApi").Value;
            List<FechaPartido> fecInfo = new List<FechaPartido>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseApiUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("api/FechaPartidos");
                if (Res.IsSuccessStatusCode)
                {
                    var fecResponse = Res.Content.ReadAsStringAsync().Result;
                    fecInfo = JsonConvert.DeserializeObject<List<FechaPartido>>(fecResponse);
                }

                return View(fecInfo);
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create([FromForm] FechaPartido fechaPartido)
        {
            string baseApiUrl = _configuration.GetSection("LigaDominicanaApi").Value;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseApiUrl);
                var postTask = client.PostAsJsonAsync<FechaPartido>("api/FechaPartidos", fechaPartido);
                postTask.Wait();
                var result = postTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            ModelState.AddModelError(String.Empty, "error, esto no sirve ya.");
            return View(fechaPartido);
        }

        [HttpGet]
        public async Task<ActionResult> Details(int id)
        {
            string baseApiUrl = _configuration.GetSection("LigaDominicanaApi").Value;
            FechaPartido fecInfo = new FechaPartido();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseApiUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync($"api/FechaPartidos/" + id.ToString());
                if (Res.IsSuccessStatusCode)
                {
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    fecInfo = JsonConvert.DeserializeObject<FechaPartido>(EmpResponse);
                }

                return View(fecInfo);
            }
        }

        public ActionResult Delete(int id)
        {
            string baseApiUrl = _configuration.GetSection("LigaDominicanaApi").Value;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseApiUrl);
                var deleteTask = client.DeleteAsync($"api/FechaPartidos/" + id.ToString());
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
            FechaPartido fec = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseApiUrl);

                var result = await client.GetAsync($"api/FechaPartidos/" + id.ToString());

                if (result.IsSuccessStatusCode)
                {
                    fec = await result.Content.ReadAsAsync<FechaPartido>();
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "error, esto no sirve ya.");
                }
            }
            return View(fec);
        }

        [HttpPost]
        public async Task<ActionResult> Edit([FromForm] FechaPartido fechaPartido)
        {
            string baseApiUrl = _configuration.GetSection("LigaDominicanaApi").Value;
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseApiUrl);
                    var response = await client.PutAsJsonAsync("api/FechaPartidos", fechaPartido);
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
            return View(fechaPartido);
        }
    }
}
