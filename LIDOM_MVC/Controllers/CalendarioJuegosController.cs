using LIDOM_MVC.Models;
using LIDOM_MVC.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Reflection;
using System.Text;

namespace LIDOM_MVC.Controllers
{
    public class CalendarioJuegosController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient httpClient;


        public CalendarioJuegosController(IConfiguration configuration)
        {
            _configuration = configuration;
            httpClient = new HttpClient();
        }

        // GET: CalendarioJuegosController
        public async Task <ActionResult> Index()
        {
            string baseApiUrl = _configuration.GetSection("LigaDominicanaApi").Value!;
            List<CalendarioViewModel> calendarioViewModel = new List<CalendarioViewModel>();

            try
            {

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseApiUrl);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = await client.GetAsync($"{baseApiUrl}/calendariojuegos");

                    if (response.IsSuccessStatusCode)
                    {
                        var data = await response.Content.ReadAsStringAsync();
                        calendarioViewModel = JsonConvert.DeserializeObject<List<CalendarioViewModel>>(data) ?? new List<CalendarioViewModel>();
                    }
                    else
                    {
                        Console.WriteLine("Error al llamar a la Web API: " + response.ReasonPhrase);
                        ModelState.AddModelError(String.Empty, "Error al obtener datos. Código de estado: " + response.StatusCode);
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
            return View(calendarioViewModel);

            //string baseApiUrl = _configuration.GetSection("LigaDominicanaApi").Value!;
            //List<CalendarioJuego> calendarioJuegos = new List<CalendarioJuego>();
            //List<FechaPartido> fechapartidos = new List<FechaPartido>();
            //List<Equipo> equipos = new List<Equipo>();

            //List<CalendarioJuegoViewModel> calendarioJuegoViewModel = new List<CalendarioJuegoViewModel>();


            //string jsonCalendarioJuegosResponse = httpClient.GetStringAsync($"{baseApiUrl}/calendariojuegos").Result;
            //calendarioJuegos = string.IsNullOrEmpty(jsonCalendarioJuegosResponse) ? calendarioJuegos : JsonConvert.DeserializeObject<List<CalendarioJuego>>(jsonCalendarioJuegosResponse)!;


            //string jsonfechapartidosResponse = httpClient.GetStringAsync($"{baseApiUrl}/fechapartidos").Result;
            //fechapartidos = string.IsNullOrEmpty(jsonfechapartidosResponse) ? fechapartidos : JsonConvert.DeserializeObject<List<FechaPartido>>(jsonfechapartidosResponse)!;

            //string jsonEquiposResponse = httpClient.GetStringAsync($"{baseApiUrl}/equipos").Result;
            //equipos = string.IsNullOrEmpty(jsonEquiposResponse) ? equipos : JsonConvert.DeserializeObject<List<Equipo>>(jsonEquiposResponse)!;


            //foreach (CalendarioJuego juego in calendarioJuegos)
            //{
            //    CalendarioJuegoViewModel customCalendarioJuego = new CalendarioJuegoViewModel()
            //    {

            //        CalJuegoId = juego.CalJuegoId,
            //        CalEquipoLocal = equipos.Where(e => juego.CalEquipoLocal == e.EqId).Select(e => e.EqNombre).FirstOrDefault()!,
            //        CalEquipoVisitante = equipos.Where(e => juego.CalEquipoVisitante == e.EqId).Select(e => e.EqNombre).FirstOrDefault()!,
            //        CalFechaPartido = fechapartidos.Where(e => juego.CalFechaPartido == e.FecId).Select(e => e.FecFechaPartido).FirstOrDefault()!,
            //        FecHora = fechapartidos.Where(e => juego.CalFechaPartido == e.FecId).Select(e => e.FecHora).FirstOrDefault()!,

            //    };
            //    calendarioJuegoViewModel.Add(customCalendarioJuego);
            //};


            //return View(calendarioJuegoViewModel);
        }

        // GET: CalendarioJuegosController/Details/5
        public async  Task<ActionResult> Details(int id)
        {
            string baseApiUrl = _configuration.GetSection("LigaDominicanaApi").Value!;
            List<CalendarioViewModel> calendarioViewModels = new List<CalendarioViewModel>();
            CalendarioViewModel calendarioViewModel = new CalendarioViewModel();

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseApiUrl);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = await client.GetAsync($"{baseApiUrl}/calendariojuegos/");

                    if (response.IsSuccessStatusCode)
                    {
                        var data = await response.Content.ReadAsStringAsync();
                        calendarioViewModels = JsonConvert.DeserializeObject<List<CalendarioViewModel>>(data) ?? new List<CalendarioViewModel>();
                    }
                    else
                    {
                        Console.WriteLine("Error al llamar a la Web API: " + response.ReasonPhrase);
                        ModelState.AddModelError(String.Empty, "Error al obtener datos. Código de estado: " + response.StatusCode);
                    }

                    calendarioViewModel = calendarioViewModels.FirstOrDefault(e => e.CalJuegoId == id)!;

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
            return View(calendarioViewModel);

            //string baseApiUrl = _configuration.GetSection("LigaDominicanaApi").Value!;

            //List<FechaPartido> fechaspartidos = new List<FechaPartido>();
            //FechaPartido fechaspartido = new FechaPartido();

            //List<Equipo> equipos = new List<Equipo>();
            //Equipo equipoLocal = new Equipo();
            //Equipo equipoVisitante = new Equipo();

            //List<CalendarioJuego> calendariojuegos = new List<CalendarioJuego>();
            //CalendarioJuego calendariojuego = new CalendarioJuego();

            //CalendarioJuegoViewModel calendariojuegoViewModel = new CalendarioJuegoViewModel();

            //string jsonFechasPartidosResponse = httpClient.GetStringAsync($"{baseApiUrl}/fechapartidos").Result;
            //fechaspartidos = JsonConvert.DeserializeObject<List<FechaPartido>>(jsonFechasPartidosResponse) ?? new List<FechaPartido>();

            //string jsonEquiposResponse = httpClient.GetStringAsync($"{baseApiUrl}/equipos").Result;
            //equipos = JsonConvert.DeserializeObject<List<Equipo>>(jsonEquiposResponse) ?? new List<Equipo>();

            //string jsonCalendarioJuegoResponse = httpClient.GetStringAsync($"{baseApiUrl}/calendariojuegos").Result;
            //calendariojuegos = JsonConvert.DeserializeObject<List<CalendarioJuego>>(jsonCalendarioJuegoResponse) ?? new List<CalendarioJuego>();

            //// Por ejemplo, podrías filtrar equipos y estadios según el ID:
            //calendariojuego = calendariojuegos.FirstOrDefault(e => e.CalJuegoId == id)!;
            //equipoLocal = equipos.FirstOrDefault(s => s.EqId == calendariojuego.CalEquipoLocal)!;
            //equipoVisitante = equipos.FirstOrDefault(s => s.EqId == calendariojuego.CalEquipoVisitante)!;
            //fechaspartido = fechaspartidos.FirstOrDefault(s => s.FecId == calendariojuego.CalFechaPartido)!;

            //// Luego, asigna los valores al ViewModel como lo hacías antes.
            //calendariojuegoViewModel.CalJuegoId = calendariojuego.CalJuegoId;
            //calendariojuegoViewModel.CalFechaPartido = fechaspartido.FecFechaPartido;
            //calendariojuegoViewModel.CalEquipoLocal = equipoLocal.EqNombre;
            //calendariojuegoViewModel.CalEquipoVisitante = equipoVisitante.EqNombre;

            //return View(calendariojuegoViewModel);
        }

        // GET: CalendarioJuegosController/Create
        public async Task <ActionResult> Create()
        {
            string baseApiUrl = _configuration.GetSection("LigaDominicanaApi").Value!;
            List<FechaPartido> fechapartidos = new List<FechaPartido>();
            List<Equipo> equipos = new List<Equipo>();

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage ResFec = await client.GetAsync($"{baseApiUrl}/fechapartidos/");
                HttpResponseMessage ResEqu = await client.GetAsync($"{baseApiUrl}/equipos/");

                if (ResFec.IsSuccessStatusCode && ResEqu.IsSuccessStatusCode)
                {
                    var PosResponse = ResFec.Content.ReadAsStringAsync().Result;
                    fechapartidos = JsonConvert.DeserializeObject<List<FechaPartido>>(PosResponse)!;

                    var EquResponse = ResEqu.Content.ReadAsStringAsync().Result;
                    equipos = JsonConvert.DeserializeObject<List<Equipo>>(EquResponse)!;

                    ViewBag.DropDownDataFec = new SelectList(fechapartidos, "FecId", "FecFechaPartido");
                    ViewBag.DropDownDataEq = new SelectList(equipos, "EqId", "EqNombre");
                }
                return View();
            }
        }

        // POST: CalendarioJuegosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([FromForm] CalendarioJuego calendariojuego)
        {
            string baseApiUrl = _configuration.GetSection("LigaDominicanaApi").Value!;

            try
            {
                var postTask = httpClient.PostAsJsonAsync<CalendarioJuego>($"{baseApiUrl}/calendariojuegos", calendariojuego);
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
            return View(calendariojuego);
        }


        // GET: CalendarioJuegosController/Edit/5
        public async Task <ActionResult> Edit(int id)
        {
            string baseApiUrl = _configuration.GetSection("LigaDominicanaApi").Value!;
            CalendarioJuego calendariojuego = new CalendarioJuego();
            List<FechaPartido> fechapartidos = new List<FechaPartido>();
            List<Equipo> equipos = new List<Equipo>();

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage ResCal = await client.GetAsync($"{baseApiUrl}/calendariojuegos/" + id.ToString());
                HttpResponseMessage ResFec = await client.GetAsync($"{baseApiUrl}/fechapartidos/");
                HttpResponseMessage ResEqu = await client.GetAsync($"{baseApiUrl}/equipos/");

                if (ResCal.IsSuccessStatusCode && ResFec.IsSuccessStatusCode && ResEqu.IsSuccessStatusCode)
                {
                    var CalResponse = ResCal.Content.ReadAsStringAsync().Result;
                    calendariojuego = JsonConvert.DeserializeObject<CalendarioJuego>(CalResponse)!;

                    var FecResponse = ResFec.Content.ReadAsStringAsync().Result;
                    fechapartidos = JsonConvert.DeserializeObject<List<FechaPartido>>(FecResponse)!;

                    var EquResponse = ResEqu.Content.ReadAsStringAsync().Result;
                    equipos = JsonConvert.DeserializeObject<List<Equipo>>(EquResponse)!;

                    ViewBag.DropDownDataFec = new SelectList(fechapartidos, "FecId", "FecFechaPartido");
                    ViewBag.DropDownDataEq = new SelectList(equipos, "EqId", "EqNombre");
                }
                return View(calendariojuego);
            }
        }

        // POST: CalendarioJuegosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <ActionResult> Edit(int id, [FromForm] CalendarioJuego calendariojuego)
        {
            string baseApiUrl = _configuration.GetSection("LigaDominicanaApi").Value!;

            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var content = new StringContent(JsonConvert.SerializeObject(calendariojuego), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PutAsync($"{baseApiUrl}/calendariojuegos/" + id.ToString(), content);

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError(String.Empty, "Error al actualizar el juego. Código de estado: " + response.StatusCode);
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
            return View(calendariojuego);
        }

        // GET: CalendarioJuegosController/Delete/5
        public async Task <ActionResult> Delete(int id)
        {
            string baseApiUrl = _configuration.GetSection("LigaDominicanaApi").Value!;
            List<CalendarioViewModel> calendarioViewModels = new List<CalendarioViewModel>();
            CalendarioViewModel calendarioViewModel = new CalendarioViewModel();

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseApiUrl);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = await client.GetAsync($"{baseApiUrl}/calendariojuegos/");

                    if (response.IsSuccessStatusCode)
                    {
                        var data = await response.Content.ReadAsStringAsync();
                        calendarioViewModels = JsonConvert.DeserializeObject<List<CalendarioViewModel>>(data) ?? new List<CalendarioViewModel>();
                    }
                    else
                    {
                        Console.WriteLine("Error al llamar a la Web API: " + response.ReasonPhrase);
                        ModelState.AddModelError(String.Empty, "Error al obtener datos. Código de estado: " + response.StatusCode);
                    }

                    calendarioViewModel = calendarioViewModels.FirstOrDefault(e => e.CalJuegoId == id)!;

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
            return View(calendarioViewModel);

            //string baseApiUrl = _configuration.GetSection("LigaDominicanaApi").Value!;

            //List<FechaPartido> fechapartidos = new List<FechaPartido>();
            //List<Equipo> equipos = new List<Equipo>();
            //List<CalendarioJuego> calendariojuegos = new List<CalendarioJuego>();

            //FechaPartido fechapartido = new FechaPartido();
            //CalendarioJuego calendariojuego = new CalendarioJuego();
            //Equipo equipoLocal = new Equipo();
            //Equipo equipoVis = new Equipo();
            //CalendarioJuegoViewModel calendariojuegoViewModel = new CalendarioJuegoViewModel();

            //string jsonCalendarioJuegosResponse = httpClient.GetStringAsync($"{baseApiUrl}/calendariojuegos").Result;
            //calendariojuegos = JsonConvert.DeserializeObject<List<CalendarioJuego>>(jsonCalendarioJuegosResponse) ?? new List<CalendarioJuego>();

            //string jsonFechaPartidoResponse = httpClient.GetStringAsync($"{baseApiUrl}/fechapartidos").Result;
            //fechapartidos = JsonConvert.DeserializeObject<List<FechaPartido>>(jsonFechaPartidoResponse) ?? new List<FechaPartido>();

            //string jsonEquiposResponse = httpClient.GetStringAsync($"{baseApiUrl}/equipos").Result;
            //equipos = JsonConvert.DeserializeObject<List<Equipo>>(jsonEquiposResponse) ?? new List<Equipo>();

            //// Por ejemplo, podrías filtrar equipos y estadios según el ID:
            //calendariojuego = calendariojuegos.FirstOrDefault(e => e.CalJuegoId == id)!;
            //equipoLocal = equipos.FirstOrDefault(s => s.EqId == calendariojuego.CalEquipoLocal)!;
            //equipoVis = equipos.FirstOrDefault(s => s.EqId == calendariojuego.CalEquipoVisitante)!;
            //fechapartido = fechapartidos.FirstOrDefault(s => s.FecId == calendariojuego.CalFechaPartido)!;

            //// Luego, asigna los valores al ViewModel como lo hacías antes.
            //calendariojuegoViewModel.CalJuegoId = calendariojuego.CalJuegoId;
            //calendariojuegoViewModel.CalFechaPartido = fechapartido.FecFechaPartido;
            //calendariojuegoViewModel.CalEquipoLocal = equipoLocal.EqNombre;
            //calendariojuegoViewModel.CalEquipoVisitante = equipoVis.EqNombre;

            //return View(calendariojuegoViewModel);
        }

        // POST: CalendarioJuegosController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id)
        {
            string baseApiUrl = _configuration.GetSection("LigaDominicanaApi").Value!;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseApiUrl);
                var deleteTask = client.DeleteAsync($"api/calendariojuegos/" + id.ToString());
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
