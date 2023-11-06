using LIDOM_MVC.Models;
using LIDOM_MVC.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;

namespace LIDOM_MVC.Controllers
{
    public class JugadoresController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient httpClient;


        public JugadoresController(IConfiguration configuration)
        {
            _configuration = configuration;
            httpClient = new HttpClient();
        }

        // GET: JugadoresController
        public ActionResult Index()
        {
            string baseApiUrl = _configuration.GetSection("LigaDominicanaApi").Value!;
            List<Jugadore> jugadores = new List<Jugadore>();
            List<Equipo> equipos = new List<Equipo>();
            List<Posicione> posiciones = new List<Posicione>();
            List<JugadoresViewModel> jugadoresViewModel = new List<JugadoresViewModel>();

            string jsonEquiposResponse = httpClient.GetStringAsync($"{baseApiUrl}/equipos").Result;
            equipos = string.IsNullOrEmpty(jsonEquiposResponse) ? equipos : JsonConvert.DeserializeObject<List<Equipo>>(jsonEquiposResponse)!;


            string jsonPosicionesResponse = httpClient.GetStringAsync($"{baseApiUrl}/posiciones").Result;
            posiciones = string.IsNullOrEmpty(jsonPosicionesResponse) ? posiciones : JsonConvert.DeserializeObject<List<Posicione>>(jsonPosicionesResponse)!;

            string jsonJugadoresResponse = httpClient.GetStringAsync($"{baseApiUrl}/jugadores").Result;
            jugadores = string.IsNullOrEmpty(jsonJugadoresResponse) ? jugadores : JsonConvert.DeserializeObject<List<Jugadore>>(jsonJugadoresResponse)!;


            foreach (Jugadore jugador in jugadores)
            {
                JugadoresViewModel customJugador = new JugadoresViewModel()
                {
                    JugId = jugador.JugId,
                    JugNombre = jugador.JugNombre,
                    JugApellidos = jugador.JugApellidos,
                    JugPosicion = posiciones.Where(e => jugador.JugPosicion == e.PosId).Select(e => e.PosNombre).FirstOrDefault()!,
                    JugEquipo = equipos.Where(e => jugador.JugEquipo == e.EqId).Select(e => e.EqNombre).FirstOrDefault()!,
                    JugEdad = jugador.JugEdad,
                    JugNumCamiseta = jugador.JugNumCamiseta,
                };
                jugadoresViewModel.Add(customJugador);
            };
            return View(jugadoresViewModel);
        }


        // GET: JugadoresController/Details/5
        public ActionResult Details(int id)
        {
            string baseApiUrl = _configuration.GetSection("LigaDominicanaApi").Value!;
            List<Posicione> posiciones = new List<Posicione>();
            Posicione posicion = new Posicione();
            List<Equipo> equipos = new List<Equipo>();
            Equipo equipo = new Equipo();
            List<Jugadore> jugadores = new List<Jugadore>();
            Jugadore jugador = new Jugadore();
            JugadoresViewModel jugadoresViewModel = new JugadoresViewModel();

            string jsonPosicionesResponse = httpClient.GetStringAsync($"{baseApiUrl}/posiciones").Result;
            posiciones = JsonConvert.DeserializeObject<List<Posicione>>(jsonPosicionesResponse) ?? new List<Posicione>();

            string jsonEquiposResponse = httpClient.GetStringAsync($"{baseApiUrl}/equipos").Result;
            equipos = JsonConvert.DeserializeObject<List<Equipo>>(jsonEquiposResponse) ?? new List<Equipo>();

            string jsonJugadoresResponse = httpClient.GetStringAsync($"{baseApiUrl}/jugadores").Result;
            jugadores = JsonConvert.DeserializeObject<List<Jugadore>>(jsonJugadoresResponse) ?? new List<Jugadore>();

            jugador = jugadores.FirstOrDefault(e => e.JugId == id)!;
            equipo = equipos.FirstOrDefault(s => s.EqId == jugador.JugEquipo)!;
            posicion = posiciones.FirstOrDefault(s => s.PosId == jugador.JugPosicion)!;

            jugadoresViewModel.JugId = jugador.JugId;
            jugadoresViewModel.JugPosicion = posicion.PosNombre;
            jugadoresViewModel.JugEquipo = equipo.EqNombre;
            jugadoresViewModel.JugEdad = jugador.JugEdad;
            jugadoresViewModel.JugNumCamiseta = jugador.JugNumCamiseta;
            jugadoresViewModel.JugNombre = jugador.JugNombre;
            jugadoresViewModel.JugApellidos = jugador.JugApellidos;

            return View(jugadoresViewModel);
        }


        // GET: JugadoresController/Create
        public async Task <ActionResult> Create()
        {
            string baseApiUrl = _configuration.GetSection("LigaDominicanaApi").Value!;
            List<Posicione> posiciones = new List<Posicione>();
            List<Equipo> equipos = new List<Equipo>();

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage ResPos = await client.GetAsync($"{baseApiUrl}/posiciones/");
                HttpResponseMessage ResEqu = await client.GetAsync($"{baseApiUrl}/equipos/");

                if (ResPos.IsSuccessStatusCode && ResEqu.IsSuccessStatusCode)
                {
                    var PosResponse = ResPos.Content.ReadAsStringAsync().Result;
                    posiciones = JsonConvert.DeserializeObject<List<Posicione>>(PosResponse)!;

                    var EquResponse = ResEqu.Content.ReadAsStringAsync().Result;
                    equipos = JsonConvert.DeserializeObject<List<Equipo>>(EquResponse)!;

                    ViewBag.DropDownDataPos = new SelectList(posiciones, "PosId", "PosNombre");
                    ViewBag.DropDownDataEq = new SelectList(equipos, "EqId", "EqNombre");
                }
                return View();
            }
        }


        // POST: JugadoresController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([FromForm] Jugadore jugadore)
        {
            string baseApiUrl = _configuration.GetSection("LigaDominicanaApi").Value!;

            try
            {
                var postTask = httpClient.PostAsJsonAsync<Jugadore>($"{baseApiUrl}/jugadores", jugadore);
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
            return View(jugadore);
        }


        // GET: JugadoresController/Edit/5
        public async Task <ActionResult> Edit(int id)
        {
            string baseApiUrl = _configuration.GetSection("LigaDominicanaApi").Value!;
            Jugadore jugador = new Jugadore();
            List<Posicione> posiciones = new List<Posicione>();
            List<Equipo> equipos = new List<Equipo>();

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage ResJug = await client.GetAsync($"{baseApiUrl}/jugadores/" + id.ToString());
                HttpResponseMessage ResPos = await client.GetAsync($"{baseApiUrl}/posiciones/");
                HttpResponseMessage ResEqu = await client.GetAsync($"{baseApiUrl}/equipos/");

                if (ResJug.IsSuccessStatusCode && ResPos.IsSuccessStatusCode && ResEqu.IsSuccessStatusCode)
                {
                    var JugResponse = ResJug.Content.ReadAsStringAsync().Result;
                    jugador = JsonConvert.DeserializeObject<Jugadore>(JugResponse)!;

                    var PosResponse = ResPos.Content.ReadAsStringAsync().Result;
                    posiciones = JsonConvert.DeserializeObject<List<Posicione>>(PosResponse)!;

                    var EquResponse = ResEqu.Content.ReadAsStringAsync().Result;
                    equipos = JsonConvert.DeserializeObject<List<Equipo>>(EquResponse)!;

                    ViewBag.DropDownDataPos = new SelectList(posiciones, "PosId", "PosNombre");
                    ViewBag.DropDownDataEq = new SelectList(equipos, "EqId", "EqNombre");
                }
                return View(jugador);
            }

        }


        // POST: JugadoresController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <ActionResult> Edit(int id, [FromForm] Jugadore jugador)
        {
            string baseApiUrl = _configuration.GetSection("LigaDominicanaApi").Value!;

            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var content = new StringContent(JsonConvert.SerializeObject(jugador), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PutAsync($"{baseApiUrl}/jugadores/" + id.ToString(), content);

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
            return View(jugador);
        }


        // GET: JugadoresController/Delete/5
        public ActionResult Delete(int id)
        {
            string baseApiUrl = _configuration.GetSection("LigaDominicanaApi").Value!;

            List<Posicione> posiciones = new List<Posicione>();
            List<Equipo> equipos = new List<Equipo>();
            List<Jugadore> jugadores = new List<Jugadore>();
            JugadoresViewModel jugadoresViewModel = new JugadoresViewModel();
            Posicione posicion = new Posicione();
            Jugadore jugador = new Jugadore();
            Equipo equipo = new Equipo();

            string jsonJugadoresResponse = httpClient.GetStringAsync($"{baseApiUrl}/jugadores").Result;
            jugadores = JsonConvert.DeserializeObject<List<Jugadore>>(jsonJugadoresResponse) ?? new List<Jugadore>();

            string jsonPosicionesResponse = httpClient.GetStringAsync($"{baseApiUrl}/posiciones").Result;
            posiciones = JsonConvert.DeserializeObject<List<Posicione>>(jsonPosicionesResponse) ?? new List<Posicione>();

            string jsonEquiposResponse = httpClient.GetStringAsync($"{baseApiUrl}/equipos").Result;
            equipos = JsonConvert.DeserializeObject<List<Equipo>>(jsonEquiposResponse) ?? new List<Equipo>();

            jugador = jugadores.FirstOrDefault(e => e.JugId == id)!;
            equipo = equipos.FirstOrDefault(s => s.EqId == jugador.JugEquipo)!;
            posicion = posiciones.FirstOrDefault(s => s.PosId == jugador.JugPosicion)!;

            jugadoresViewModel.JugId = jugador.JugId;
            jugadoresViewModel.JugNumCamiseta = jugador.JugNumCamiseta;
            jugadoresViewModel.JugEquipo = equipo.EqNombre;
            jugadoresViewModel.JugPosicion = posicion.PosNombre;
            jugadoresViewModel.JugApellidos = jugador.JugApellidos;
            jugadoresViewModel.JugNombre = jugador.JugNombre;
            jugadoresViewModel.JugEdad = jugador.JugEdad;

            return View(jugadoresViewModel);
        }


        // POST: JugadoresController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id)
        {
            string baseApiUrl = _configuration.GetSection("LigaDominicanaApi").Value!;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseApiUrl);
                var deleteTask = client.DeleteAsync($"api/jugadores/" + id.ToString());
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
