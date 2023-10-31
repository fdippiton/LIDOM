using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using LIDOM_MVC.Models;
using LIDOM_MVC.ViewModels;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using NuGet.Protocol;
using System.Text;

namespace LIDOM_MVC.Controllers
{
    public class EquiposController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient httpClient;


        public EquiposController(IConfiguration configuration)
        {
            _configuration = configuration;
            httpClient = new HttpClient();
        }

        //GET: EquiposController
        //public async Task<ActionResult> Index()
        //{
        //    string baseApiUrl = _configuration.GetSection("LigaDominicanaApi").Value!;
        //    List<Equipo> equipo = new List<Equipo>();
        //    using (var client = new HttpClient())
        //    {
        //        client.BaseAddress = new Uri(baseApiUrl);
        //        client.DefaultRequestHeaders.Clear();
        //        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //        HttpResponseMessage Res = await client.GetAsync($"{baseApiUrl}/equipos");
        //        if (Res.IsSuccessStatusCode)
        //        {
        //            var EquiResponse = Res.Content.ReadAsStringAsync().Result;
        //            equipo = JsonConvert.DeserializeObject<List<Equipo>>(EquiResponse)!;
        //        }

        //        return View(equipo);
        //    }
        //}

        public ActionResult Index()
        {
            string baseApiUrl = _configuration.GetSection("LigaDominicanaApi").Value!;
            List<Equipo> equipos = new List<Equipo>();
            List<Estadio> estadios = new List<Estadio>();
            List<EquipoViewModel> customEquipos = new List<EquipoViewModel>();

            string jsonEquiposResponse = httpClient.GetStringAsync($"{baseApiUrl}/equipos").Result;
            equipos = string.IsNullOrEmpty(jsonEquiposResponse) ? equipos : JsonConvert.DeserializeObject<List<Equipo>>(jsonEquiposResponse)!;


            string jsonEstadiosResponse = httpClient.GetStringAsync($"{baseApiUrl}/estadios").Result;
            estadios = string.IsNullOrEmpty(jsonEstadiosResponse) ? estadios : JsonConvert.DeserializeObject<List<Estadio>>(jsonEstadiosResponse)!;
            Console.WriteLine(equipos.ToJson());

            foreach (Equipo equipo in equipos)
            {
                EquipoViewModel customEquipo = new EquipoViewModel()
                {
                   EqId = equipo.EqId,
                   EqNombre = equipo.EqNombre,
                   EqDescripcion = equipo.EqDescripcion,
                   EqCiudad = equipo.EqCiudad,
                   EqEstadioNombre = estadios.Where(e => equipo.EqEstadio == e.EstId ).Select(e => e.EstNombre).FirstOrDefault()!,
                   EqEstatus = equipo.EqEstatus,
                };
                customEquipos.Add(customEquipo);
            };

            return View(customEquipos);
        }

        // GET: EquiposController/Details/5
        [HttpGet]
        [Route("api/details/id")]
        public async Task<ActionResult> Details(int id)
        {

            string baseApiUrl = _configuration.GetSection("LigaDominicanaApi").Value!;


            Equipo equipo = new Equipo();
            using (var client = new HttpClient())
            {

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync($"{baseApiUrl}/equipos/" + id.ToString());

                if (Res.IsSuccessStatusCode)
                {
                    var EquiResponse = Res.Content.ReadAsStringAsync().Result;
                    equipo = JsonConvert.DeserializeObject<Equipo>(EquiResponse)!;
                }

                return View(equipo);
            }
        }

        // GET: EquiposController/Create
        public async Task <ActionResult> Create()
        {
            string baseApiUrl = _configuration.GetSection("LigaDominicanaApi").Value!;
            List<Estadio> estadios = new List<Estadio>();

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage ResEstadio = await client.GetAsync($"{baseApiUrl}/estadios/");

                if (ResEstadio.IsSuccessStatusCode)
                {
                    var EstadioResponse = ResEstadio.Content.ReadAsStringAsync().Result;
                    estadios = JsonConvert.DeserializeObject<List<Estadio>>(EstadioResponse)!;

                    ViewBag.DropDownData = new SelectList(estadios, "EstId", "EstNombre");
                }

                return View();
            }
        }

        // POST: EquiposController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([FromForm] Equipo equipo)
        {
            string baseApiUrl = _configuration.GetSection("LigaDominicanaApi").Value!;

            try
            {
                equipo.EqEstatus = "A".ToString(); 
                Console.WriteLine(equipo.ToJson());

                var postTask = httpClient.PostAsJsonAsync<Equipo>($"{baseApiUrl}/equipos", equipo);
                postTask.Wait();
                var result = postTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    Console.WriteLine($"Request failed with status code {result.StatusCode}");
                    var responseContent = result.Content.ReadAsStringAsync().Result;
                    Console.WriteLine(responseContent);
                    ModelState.AddModelError(string.Empty, "Error: " + result.ReasonPhrase);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Error");
                Console.WriteLine("Error: " + ex.Message);
            }
            return View(equipo);
        }

        // GET: EquiposController/Edit/5
        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            string baseApiUrl = _configuration.GetSection("LigaDominicanaApi").Value!;
            Equipo equipo = new Equipo();

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage ResEquipo = await client.GetAsync($"{baseApiUrl}/equipos/" + id.ToString());

                if (ResEquipo.IsSuccessStatusCode)
                {
                    var EquiResponse = ResEquipo.Content.ReadAsStringAsync().Result;
                    equipo = JsonConvert.DeserializeObject<Equipo>(EquiResponse)!;
                }
               
                return View(equipo);
            }
        }

        // POST: EquiposController/Edit/5
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

        // GET: EquiposController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: EquiposController/Delete/5
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
