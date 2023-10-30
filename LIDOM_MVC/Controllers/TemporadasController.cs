using LIDOM_MVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.Configuration;
using System.Globalization;
using System.Net.Http.Headers;
using System.Text;

namespace LIDOM_MVC.Controllers
{
    public class TemporadasController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient httpClient;

        public TemporadasController(IConfiguration configuration)
        {
            httpClient = new HttpClient();
            _configuration = configuration;
        }

        // GET: TemporadasController
        public ActionResult Index()
        {

            string baseApiUrl = _configuration.GetSection("LigaDominicanaApi").Value!;
            List<Temporada> temporadas = new List<Temporada>();
            string jsonTemporadasResponse = httpClient.GetStringAsync($"{baseApiUrl}/temporadas").Result;
            temporadas = string.IsNullOrEmpty(jsonTemporadasResponse) ? temporadas : JsonConvert.DeserializeObject<List<Temporada>>(jsonTemporadasResponse)!;

            return View(temporadas);
        }

        //// GET: TemporadasController/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        // GET: TemporadasController/Details/5
        [HttpGet]
        [Route("api/details/id")]
        public async Task<ActionResult> Details(int id)
        {

            string baseApiUrl = _configuration.GetSection("LigaDominicanaApi").Value!;


            Temporada TempInfo = new Temporada();
            using (var client = new HttpClient())
            {
   
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync($"{baseApiUrl}/temporadas/" + id.ToString());

                if (Res.IsSuccessStatusCode)
                {
                    var EquiResponse = Res.Content.ReadAsStringAsync().Result;
                    TempInfo = JsonConvert.DeserializeObject<Temporada>(EquiResponse)!;
                }

                return View(TempInfo);
            }
        }


        // GET: TemporadasController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TemporadasController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([FromForm] Temporada temporada)
        {
            string baseApiUrl = _configuration.GetSection("LigaDominicanaApi").Value!;

            try
            {
                var postTask = httpClient.PostAsJsonAsync<Temporada>($"{baseApiUrl}/temporadas", temporada);
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
            return View(temporada);
        }

        // GET: TemporadasController/Edit/5
        [HttpGet]
        public async  Task<ActionResult> Edit(int id)
        {

            string baseApiUrl = _configuration.GetSection("LigaDominicanaApi").Value!;


            Temporada TempInfo = new Temporada();
            using (var client = new HttpClient())
            {

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync($"{baseApiUrl}/temporadas/" + id.ToString());

                if (Res.IsSuccessStatusCode)
                {
                    var EquiResponse = Res.Content.ReadAsStringAsync().Result;
                    TempInfo = JsonConvert.DeserializeObject<Temporada>(EquiResponse)!;
                }

                return View(TempInfo);
            }
        }

        // POST: TemporadasController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [FromForm] Temporada temporada)
        {
            string baseApiUrl = _configuration.GetSection("LigaDominicanaApi").Value!;

            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    // Serializa el objeto temporada en formato JSON
                    var content = new StringContent(JsonConvert.SerializeObject(temporada), Encoding.UTF8, "application/json");

                    // Envía una solicitud PUT a la API con los datos actualizados
                    HttpResponseMessage response = await client.PutAsync($"{baseApiUrl}/temporadas/" + id.ToString(), content);

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError(String.Empty, "Error al actualizar la temporada. Código de estado: " + response.StatusCode);
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

            return View(temporada);
        }

        // GET: TemporadasController/Delete/5
        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            string baseApiUrl = _configuration.GetSection("LigaDominicanaApi").Value!;


            Temporada TempInfo = new Temporada();
            using (var client = new HttpClient())
            {

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync($"{baseApiUrl}/temporadas/" + id.ToString());

                if (Res.IsSuccessStatusCode)
                {
                    var EquiResponse = Res.Content.ReadAsStringAsync().Result;
                    TempInfo = JsonConvert.DeserializeObject<Temporada>(EquiResponse)!;
                }

                return View(TempInfo);
            }
        }

        //POST: TemporadasController/Delete/5
        [HttpPost]
        public  ActionResult Delete(string id)
        {
            string baseApiUrl = _configuration.GetSection("LigaDominicanaApi").Value!;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseApiUrl);
                var deleteTask = client.DeleteAsync($"{baseApiUrl}/temporadas/" + id.ToString());
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
