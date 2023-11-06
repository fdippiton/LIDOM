using LIDOM_MVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace LIDOM_MVC.Controllers
{
    public class PosicionesController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient httpClient;

        public PosicionesController(IConfiguration configuration)
        {
            httpClient = new HttpClient();
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            string baseApiUrl = _configuration.GetSection("LigaDominicanaApi").Value!;
            List<Posicione> Info = new List<Posicione>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseApiUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("api/Posiciones");
                if (Res.IsSuccessStatusCode)
                {
                    var Response = Res.Content.ReadAsStringAsync().Result;
                    Info = JsonConvert.DeserializeObject<List<Posicione>>(Response)!;
                }

                return View(Info);
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create([FromForm] Posicione posicione)
        {
            string baseApiUrl = _configuration.GetSection("LigaDominicanaApi").Value!;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseApiUrl);
                var postTask = client.PostAsJsonAsync<Posicione>("api/Posiciones", posicione);
                postTask.Wait();
                var result = postTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            ModelState.AddModelError(String.Empty, "Error al crear posicion.");
            return View(posicione);
        }

        [HttpGet]
        public async Task<ActionResult> Details(int id)
        {
            string baseApiUrl = _configuration.GetSection("LigaDominicanaApi").Value!;
            Posicione Info = new Posicione();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseApiUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync($"api/Posiciones/" + id.ToString());
                if (Res.IsSuccessStatusCode)
                {
                    var Response = Res.Content.ReadAsStringAsync().Result;
                    Info = JsonConvert.DeserializeObject<Posicione>(Response)!;
                }

                return View(Info);
            }
        }

        [HttpGet]
        public async Task <ActionResult> Delete(int id)
        {
            string baseApiUrl = _configuration.GetSection("LigaDominicanaApi").Value!;
            Posicione Info = new Posicione();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseApiUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync($"{baseApiUrl}/posiciones/" + id.ToString());
                if (Res.IsSuccessStatusCode)
                {
                    var Response = Res.Content.ReadAsStringAsync().Result;
                    Info = JsonConvert.DeserializeObject<Posicione>(Response)!;
                }

                return View(Info);
            }
        }

        [HttpPost]
        public ActionResult Delete(string id)
        {
            string baseApiUrl = _configuration.GetSection("LigaDominicanaApi").Value!;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseApiUrl);
                var deleteTask = client.DeleteAsync($"{baseApiUrl}/posiciones/" + id.ToString());
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
            string baseApiUrl = _configuration.GetSection("LigaDominicanaApi").Value!;
            Posicione posicione = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseApiUrl);

                var result = await client.GetAsync($"api/Posiciones" + id.ToString());

                if (result.IsSuccessStatusCode)
                {
                    posicione = await result.Content.ReadAsAsync<Posicione>();
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "error, esto no sirve ya.");
                }
            }
            return View(posicione);
        }

        [HttpPost]
        public async Task<ActionResult> Edit([FromForm] Posicione posicione)
        {
            string baseApiUrl = _configuration.GetSection("LigaDominicanaApi").Value!;
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseApiUrl);
                    var response = await client.PutAsJsonAsync("api/Posiciones", posicione);
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
            return View(posicione);
        }
    }
}
