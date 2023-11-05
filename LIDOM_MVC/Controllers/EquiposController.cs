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
using Microsoft.AspNetCore.Http.HttpResults;
using System.Security.Cryptography.X509Certificates;
using System.Globalization;
using System.Net;

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


            foreach (Equipo equipo in equipos)
            {
                EquipoViewModel customEquipo = new EquipoViewModel()
                {
                    EqId = equipo.EqId,
                    EqNombre = equipo.EqNombre,
                    EqDescripcion = equipo.EqDescripcion,
                    EqCiudad = equipo.EqCiudad,
                    EqEstadioNombre = estadios.Where(e => equipo.EqEstadio == e.EstId).Select(e => e.EstNombre).FirstOrDefault()!,
                    EqEstatus = equipo.EqEstatus,
                };
                customEquipos.Add(customEquipo);
            };

            return View(customEquipos);
        }

        // GET: EquiposController/Details/5
        [HttpGet]
        public ActionResult Details(int id)
        {
            string baseApiUrl = _configuration.GetSection("LigaDominicanaApi").Value!;

            Equipo equipo = new Equipo();
            Estadio estadio = new Estadio();
            List<Equipo> equipos = new List<Equipo>();
            List<Estadio> estadios = new List<Estadio>();
            EqEstadioNombreViewModel eqEstadioNombreViewModel = new EqEstadioNombreViewModel();

            string jsonEquiposResponse = httpClient.GetStringAsync($"{baseApiUrl}/equipos").Result;
            equipos = JsonConvert.DeserializeObject<List<Equipo>>(jsonEquiposResponse) ?? new List<Equipo>();

            string jsonEstadiosResponse = httpClient.GetStringAsync($"{baseApiUrl}/estadios").Result;
            estadios = JsonConvert.DeserializeObject<List<Estadio>>(jsonEstadiosResponse) ?? new List<Estadio>();

            // Por ejemplo, podrías filtrar equipos y estadios según el ID:
            equipo = equipos.FirstOrDefault(e => e.EqId == id)!;
            estadio = estadios.FirstOrDefault(s => s.EstId == equipo.EqEstadio)!;

            // Luego, asigna los valores al ViewModel como lo hacías antes.
            eqEstadioNombreViewModel.EqId = equipo.EqId;
            eqEstadioNombreViewModel.EqCiudad = equipo.EqCiudad;
            eqEstadioNombreViewModel.EqEstadio = estadio.EstNombre;
            eqEstadioNombreViewModel.EqDescripcion = equipo.EqDescripcion;
            eqEstadioNombreViewModel.EqEstatus = equipo.EqEstatus;
            eqEstadioNombreViewModel.EqNombre = equipo.EqNombre;

            return View(eqEstadioNombreViewModel);
        }

        // GET: EquiposController/Create
        public async Task<ActionResult> Create()
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
            equipo.EqEstatus = "A".ToString();

            try
            {
                var postTask = httpClient.PostAsJsonAsync<Equipo>($"{baseApiUrl}/equipos", equipo);
                postTask.Wait();
                var result = postTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    Console.WriteLine("Response Content: " + result.Content.ReadAsStringAsync().Result);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);

                ModelState.AddModelError(string.Empty, "Ocurrió un error al crear el equipo.");
            }
            return View(equipo);
        }

        // GET: EquiposController/Edit/5
        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            string baseApiUrl = _configuration.GetSection("LigaDominicanaApi").Value!;
            Equipo equipo = new Equipo();
            List<Estadio> estadios = new List<Estadio>();

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage ResEquipo = await client.GetAsync($"{baseApiUrl}/equipos/" + id.ToString());
                HttpResponseMessage ResEstadio = await client.GetAsync($"{baseApiUrl}/estadios/");

                if (ResEquipo.IsSuccessStatusCode && ResEstadio.IsSuccessStatusCode)
                {
                    var EquiResponse = ResEquipo.Content.ReadAsStringAsync().Result;
                    equipo = JsonConvert.DeserializeObject<Equipo>(EquiResponse)!;

                    var EstadioResponse = ResEstadio.Content.ReadAsStringAsync().Result;
                    estadios = JsonConvert.DeserializeObject<List<Estadio>>(EstadioResponse)!;

                    ViewBag.DropDownData = new SelectList(estadios, "EstId", "EstNombre");
                }
                return View(equipo);
            }
        }

        // POST: EquiposController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <ActionResult> Edit(int id, [FromForm] Equipo equipo)
        {
            string baseApiUrl = _configuration.GetSection("LigaDominicanaApi").Value!;

            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    // Serializa el objeto temporada en formato JSON
                    var content = new StringContent(JsonConvert.SerializeObject(equipo), Encoding.UTF8, "application/json");

                    // Envía una solicitud PUT a la API con los datos actualizados
                    HttpResponseMessage response = await client.PutAsync($"{baseApiUrl}/equipos/" + id.ToString(), content);

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
            return View(equipo);
        }

        // GET: EquiposController/Delete/5
        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            string baseApiUrl = _configuration.GetSection("LigaDominicanaApi").Value!;

            Equipo TempInfo = new Equipo();
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync($"{baseApiUrl}/equipos/" + id.ToString());

                if (Res.IsSuccessStatusCode)
                {
                    var EquiResponse = Res.Content.ReadAsStringAsync().Result;
                    TempInfo = JsonConvert.DeserializeObject<Equipo>(EquiResponse)!;
                }
                return View(TempInfo);
            }
        }

        // POST: EquiposController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id)
        {
            string baseApiUrl = _configuration.GetSection("LigaDominicanaApi").Value!;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseApiUrl);
                var deleteTask = client.DeleteAsync($"{baseApiUrl}/equipos/" + id.ToString());
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
