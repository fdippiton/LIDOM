using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using LIDOM_MVC.Models;
using System.Net.Http.Headers;
using System.Text;

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
        [HttpGet]
        public async Task <ActionResult> Edit(int id)
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
                    var EstaResponse = Res.Content.ReadAsStringAsync().Result;
                    EstaInfo = JsonConvert.DeserializeObject<Estadio>(EstaResponse)!;
                }

                return View(EstaInfo);
            }
        }

        // POST: EstadiosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [FromForm] Estadio estadio)
        {
            string baseApiUrl = _configuration.GetSection("LigaDominicanaApi").Value!;

            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    // Serializa el objeto temporada en formato JSON
                    var content = new StringContent(JsonConvert.SerializeObject(estadio), Encoding.UTF8, "application/json");

                    // Envía una solicitud PUT a la API con los datos actualizados
                    HttpResponseMessage response = await client.PutAsync($"{baseApiUrl}/estadios/" + id.ToString(), content);

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError(String.Empty, "Error al actualizar el estadio. Código de estado: " + response.StatusCode);
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                ModelState.AddModelError(String.Empty, "Error de conexión: " + ex.Message);
            }
            catch (JsonException ex)
            {
                ModelState.AddModelError(String.Empty, "Error al deserializar los datos: " + ex.Message);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(String.Empty, "Error: " + ex.Message);
            }

            return View(estadio);
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
