using LIDOM_MVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.Configuration;
using System.Globalization;
using System.Net.Http.Headers;

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
        [Route("api/Details/T")]
        public async Task<ActionResult> Details(int id)
        {

            string baseApiUrl = _configuration.GetSection("LigaDominicanaApi").Value!;


            Temporada TempInfo = new Temporada();
            using (var client = new HttpClient())
            {
   
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync($"{baseApiUrl}/temporadas/" + id.ToString());
                Console.WriteLine(Res.ToString());

                if (Res.IsSuccessStatusCode)
                {
                    var EquiResponse = Res.Content.ReadAsStringAsync().Result;
                    TempInfo = JsonConvert.DeserializeObject<Temporada>(EquiResponse);
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
