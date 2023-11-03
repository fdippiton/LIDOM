using LIDOM_MVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace LIDOM_MVC.Controllers
{
    public class ResultadoEquiposController : Controller
    {
        // GET: ResultadoEquiposController
        private readonly IConfiguration _configuration;
        private readonly HttpClient httpClient;

        public ResultadoEquiposController(IConfiguration configuration)
        {
            httpClient = new HttpClient();
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            string baseApiUrl = _configuration.GetSection("LigaDominicanaApi").Value;
            List<ResultadoEquipo> Info = new List<ResultadoEquipo>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseApiUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("api/ResultadoEquipos");
                if (Res.IsSuccessStatusCode)
                {
                    var Response = Res.Content.ReadAsStringAsync().Result;
                    Info = JsonConvert.DeserializeObject<List<ResultadoEquipo>>(Response);
                }

                return View(Info);
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create([FromForm] ResultadoEquipo resultadoEquipo)
        {
            string baseApiUrl = _configuration.GetSection("LigaDominicanaApi").Value;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseApiUrl);
                var postTask = client.PostAsJsonAsync<ResultadoEquipo>("api/ResultadoEquipos", resultadoEquipo);
                postTask.Wait();
                var result = postTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            ModelState.AddModelError(String.Empty, "error, esto no sirve ya.");
            return View(resultadoEquipo);
        }

        [HttpGet]
        public async Task<ActionResult> Details(int id)
        {
            string baseApiUrl = _configuration.GetSection("LigaDominicanaApi").Value;
            ResultadoEquipo Info = new ResultadoEquipo();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseApiUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync($"api/ResultadoEquipos/" + id.ToString());
                if (Res.IsSuccessStatusCode)
                {
                    var Response = Res.Content.ReadAsStringAsync().Result;
                    Info = JsonConvert.DeserializeObject<ResultadoEquipo>(Response);
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
                var deleteTask = client.DeleteAsync($"api/ResultadoEquipos/" + id.ToString());
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
            ResultadoEquipo resultadoEquipo = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseApiUrl);

                var result = await client.GetAsync($"api/ResultadoEquipos" + id.ToString());

                if (result.IsSuccessStatusCode)
                {
                    resultadoEquipo = await result.Content.ReadAsAsync<ResultadoEquipo>();
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "error, esto no sirve ya.");
                }
            }
            return View(resultadoEquipo);
        }

        [HttpPost]
        public async Task<ActionResult> Edit([FromForm] ResultadoEquipo resultadoEquipo)
        {
            string baseApiUrl = _configuration.GetSection("LigaDominicanaApi").Value;
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseApiUrl);
                    var response = await client.PutAsJsonAsync("api/ResultadoEquipos", resultadoEquipo);
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
            return View(resultadoEquipo);
        }
    }
}
