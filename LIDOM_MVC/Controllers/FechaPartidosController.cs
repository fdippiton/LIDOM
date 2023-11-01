using LIDOM_MVC.Models;
using LIDOM_MVC.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using NuGet.Protocol;
using System.Net.Http.Headers;
using System.Text;

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
            string baseApiUrl = _configuration.GetSection("LigaDominicanaApi").Value!;
            List<FechaPartido> fechaPartidos = new List<FechaPartido>();
            List<Temporada> temporadas = new List<Temporada>();
            List<FechaPartidoViewModel> fecViewModel = new List<FechaPartidoViewModel>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseApiUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage ResFecha = await client.GetAsync($"{baseApiUrl}/fechapartidos");

                HttpResponseMessage ResTemp = await client.GetAsync($"{baseApiUrl}/temporadas");


                if (ResFecha.IsSuccessStatusCode && ResFecha.IsSuccessStatusCode)
                {
                    var fecResponse = ResFecha.Content.ReadAsStringAsync().Result;
                    fechaPartidos = JsonConvert.DeserializeObject<List<FechaPartido>>(fecResponse)!;

                    var tempResponse = ResTemp.Content.ReadAsStringAsync().Result;
                    temporadas = JsonConvert.DeserializeObject<List<Temporada>>(tempResponse)!;

                }

                foreach (FechaPartido fecPar in fechaPartidos)
                {
                    FechaPartidoViewModel customFechaPartido = new FechaPartidoViewModel()
                    {
                        FecId = fecPar.FecId,
                        FecFechaPartido = fecPar.FecFechaPartido,
                        FecHora = fecPar.FecHora,
                        FecTemporada = temporadas.Where(e => fecPar.FecTemporada == e.TemId).Select(e => e.TemNombre).FirstOrDefault()!,
                    };
                    fecViewModel.Add(customFechaPartido);
                };

                return View(fecViewModel);
            }
        }

         [HttpGet]
        public async Task<ActionResult> Create()
        {
            string baseApiUrl = _configuration.GetSection("LigaDominicanaApi").Value!;
            List<Temporada> temporadas = new List<Temporada>();

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage ResTemp = await client.GetAsync($"{baseApiUrl}/temporadas/");

                if (ResTemp.IsSuccessStatusCode)
                {
                    var TempResponse = ResTemp.Content.ReadAsStringAsync().Result;
                    temporadas = JsonConvert.DeserializeObject<List<Temporada>>(TempResponse)!;

                    ViewBag.DropDownData = new SelectList(temporadas, "TemId", "TemNombre");
                }
                return View();
            }
        }

        [HttpPost]
        public ActionResult Create([FromForm] FechaPartido fechaPartido)
        {

            string baseApiUrl = _configuration.GetSection("LigaDominicanaApi").Value!;
            Console.WriteLine(fechaPartido.ToJson());

            try
            {
                var postTask = httpClient.PostAsJsonAsync<FechaPartido>($"{baseApiUrl}/fechapartidos", fechaPartido);
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
            return View(fechaPartido);
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            string baseApiUrl = _configuration.GetSection("LigaDominicanaApi").Value!;
            List<FechaPartido> fechapartidos = new List<FechaPartido>();
            List<Temporada> temporadas = new List<Temporada>();
            FechaPartidoViewModel fechapartidosViewModel = new FechaPartidoViewModel();
            FechaPartido fechapartido = new FechaPartido();
            Temporada temporada = new Temporada();

            string jsonfechapartidosResponse = httpClient.GetStringAsync($"{baseApiUrl}/fechapartidos").Result;
            fechapartidos = JsonConvert.DeserializeObject<List<FechaPartido>>(jsonfechapartidosResponse) ?? new List<FechaPartido>();

            string jsonTemporadasResponse = httpClient.GetStringAsync($"{baseApiUrl}/temporadas").Result;
            temporadas = JsonConvert.DeserializeObject<List<Temporada>>(jsonTemporadasResponse) ?? new List<Temporada>();

            // Por ejemplo, podrías filtrar equipos y estadios según el ID:
            fechapartido = fechapartidos.FirstOrDefault(e => e.FecId == id)!;
            temporada = temporadas.FirstOrDefault(s => s.TemId == fechapartido.FecTemporada)!;

            // Luego, asigna los valores al ViewModel como lo hacías antes.
            fechapartidosViewModel.FecId = fechapartido.FecId;
            fechapartidosViewModel.FecFechaPartido = fechapartido.FecFechaPartido;
            fechapartidosViewModel.FecHora = fechapartido.FecHora;
            fechapartidosViewModel.FecTemporada = temporada.TemNombre;

            return View(fechapartidosViewModel);
        }


        // GET: FechaPartidosController/Delete/5
        [HttpGet]
        public  ActionResult Delete(int id)
        {
            string baseApiUrl = _configuration.GetSection("LigaDominicanaApi").Value!;
            List<FechaPartido> fechapartidos = new List<FechaPartido>();
            List<Temporada> temporadas = new List<Temporada>();
            FechaPartidoViewModel fechapartidosViewModel = new FechaPartidoViewModel();
            FechaPartido fechapartido = new FechaPartido();
            Temporada temporada = new Temporada();

            string jsonfechapartidosResponse = httpClient.GetStringAsync($"{baseApiUrl}/fechapartidos").Result;
            fechapartidos = JsonConvert.DeserializeObject<List<FechaPartido>>(jsonfechapartidosResponse) ?? new List<FechaPartido>();

            string jsonTemporadasResponse = httpClient.GetStringAsync($"{baseApiUrl}/temporadas").Result;
            temporadas = JsonConvert.DeserializeObject<List<Temporada>>(jsonTemporadasResponse) ?? new List<Temporada>();

            // Por ejemplo, podrías filtrar equipos y estadios según el ID:
            fechapartido = fechapartidos.FirstOrDefault(e => e.FecId == id)!;
            temporada = temporadas.FirstOrDefault(s => s.TemId == fechapartido.FecTemporada)!;

            // Luego, asigna los valores al ViewModel como lo hacías antes.
            fechapartidosViewModel.FecId = fechapartido.FecId;
            fechapartidosViewModel.FecFechaPartido = fechapartido.FecFechaPartido;
            fechapartidosViewModel.FecHora = fechapartido.FecHora;
            fechapartidosViewModel.FecTemporada = temporada.TemNombre;

            return View(fechapartidosViewModel);
        }

        [HttpPost]
        public ActionResult Delete(string id)
        {
            string baseApiUrl = _configuration.GetSection("LigaDominicanaApi").Value!;
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
        public async Task<ActionResult> Edit(int id)
        {
            string baseApiUrl = _configuration.GetSection("LigaDominicanaApi").Value!;
            FechaPartido fecpartido = new FechaPartido();
            List<Temporada> temporadas = new List<Temporada>();

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage ResFecPar = await client.GetAsync($"{baseApiUrl}/fechapartidos/" + id.ToString());
                HttpResponseMessage ResTemp = await client.GetAsync($"{baseApiUrl}/temporadas/");

                if (ResFecPar.IsSuccessStatusCode && ResTemp.IsSuccessStatusCode)
                {
                    var EquiResponse = ResFecPar.Content.ReadAsStringAsync().Result;
                    fecpartido = JsonConvert.DeserializeObject<FechaPartido>(EquiResponse)!;

                    var TempResponse = ResTemp.Content.ReadAsStringAsync().Result;
                    temporadas = JsonConvert.DeserializeObject<List<Temporada>>(TempResponse)!;

                    ViewBag.DropDownData = new SelectList(temporadas, "TemId", "TemNombre");
                }
                return View(fecpartido);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <ActionResult> Edit(int id, [FromForm] FechaPartido fechaPartido)
        {
            string baseApiUrl = _configuration.GetSection("LigaDominicanaApi").Value!;
            Console.WriteLine(fechaPartido.ToJson());

            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    // Serializa el objeto temporada en formato JSON
                    var content = new StringContent(JsonConvert.SerializeObject(fechaPartido), Encoding.UTF8, "application/json");

                    // Envía una solicitud PUT a la API con los datos actualizados
                    HttpResponseMessage response = await client.PutAsync($"{baseApiUrl}/fechapartidos/" + id.ToString(), content);

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError(String.Empty, "Error al actualizar la fecha del partido. Código de estado: " + response.StatusCode);
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
            return View(fechaPartido);
        }
    }
}
