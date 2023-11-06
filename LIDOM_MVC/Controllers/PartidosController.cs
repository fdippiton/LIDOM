using LIDOM_MVC.Models;
using LIDOM_MVC.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Reflection;
using System.Text;


namespace LIDOM_MVC.Controllers
{
    public class PartidosController : Controller
    {
        // GET: PartidosController
        private readonly IConfiguration _configuration;
        private readonly HttpClient httpClient;

        public PartidosController(IConfiguration configuration)
        {
            httpClient = new HttpClient();
            _configuration = configuration;
        }

        // GET: PartidosController
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            string baseApiUrl = _configuration.GetSection("LigaDominicanaApi").Value!;
            List<PartidoView> partidoViewModel = new List<PartidoView>();

            try
            {

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseApiUrl);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = await client.GetAsync($"{baseApiUrl}/partidos");

                    if (response.IsSuccessStatusCode)
                    {
                        var data = await response.Content.ReadAsStringAsync();
                        partidoViewModel = JsonConvert.DeserializeObject<List<PartidoView>>(data) ?? new List<PartidoView>();
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
            return View(partidoViewModel);
        }

        // GET: PartidosController/Details/5
        [HttpGet]
        public async Task<ActionResult> Details(int id)
        {
            string baseApiUrl = _configuration.GetSection("LigaDominicanaApi").Value!;
            List<PartidoView> partidoViewModels = new List<PartidoView>();
            PartidoView partidoViewModel = new PartidoView();

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseApiUrl);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = await client.GetAsync($"{baseApiUrl}/partidos");


                    if (response.IsSuccessStatusCode)
                    {
                        var data = await response.Content.ReadAsStringAsync();
                        partidoViewModels = JsonConvert.DeserializeObject<List<PartidoView>>(data) ?? new List<PartidoView>();
                    }
                    else
                    {
                        Console.WriteLine("Error al llamar a la Web API: " + response.ReasonPhrase);
                        ModelState.AddModelError(String.Empty, "Error al obtener datos. Código de estado: " + response.StatusCode);
                    }
                    partidoViewModel = partidoViewModels.FirstOrDefault(e => e.ParId == id)!;
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
            return View(partidoViewModel);
        }


        // GET: PartidosController/Create
        [HttpGet]
        public async Task <ActionResult> Create()
        {
            string baseApiUrl = _configuration.GetSection("LigaDominicanaApi").Value!;
            List<PartidoView> partidoViewModels = new List<PartidoView>();

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseApiUrl);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = await client.GetAsync($"{baseApiUrl}/partidos");

                    if (response.IsSuccessStatusCode)
                    {
                        var data = await response.Content.ReadAsStringAsync();
                        partidoViewModels = JsonConvert.DeserializeObject<List<PartidoView>>(data) ?? new List<PartidoView>();

                        var fechaPartidoList = partidoViewModels.Select(p => new SelectListItem
                        {
                            Text = p.FecFechaPartido.ToString(), // Texto visible en el DropDownList
                            Value = p.ParJuego.ToString() // Valor seleccionado cuando se elige un elemento del DropDownList
                        }).ToList();

                        ViewBag.FecFechaPartidoList = fechaPartidoList!;

                        var equipoGanadorList = partidoViewModels.Select(p => new SelectListItem
                        {
                            Text = p.ParEquipoGanador.ToString(), // Texto visible en el DropDownList
                            Value = p.ParEquipoGanadorId.ToString() // Valor seleccionado cuando se elige un elemento del DropDownList
                        }).ToList();

                        ViewBag.EquipoGanadorList = equipoGanadorList!;

                        var equipoPerdedorList = partidoViewModels.Select(p => new SelectListItem
                        {
                            Text = p.ParEquipoPerdedor.ToString(), // Texto visible en el DropDownList
                            Value = p.ParEquipoPerdedorId.ToString() // Valor seleccionado cuando se elige un elemento del DropDownList
                        }).ToList();

                        ViewBag.EquipoPerdedorList = equipoPerdedorList!;
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
            return View();
        }

        // POST: PartidosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([FromForm] Partido partido)
        {
            string baseApiUrl = _configuration.GetSection("LigaDominicanaApi").Value!;

            try
            {
                var postTask = httpClient.PostAsJsonAsync<Partido>($"{baseApiUrl}/partidos", partido);
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
            return View(partido);
        }


            //string baseApiUrl = _configuration.GetSection("LigaDominicanaApi").Value!;
            //List<FechaPartido> fechapartidos = new List<FechaPartido>();
            //List<Equipo> equipos = new List<Equipo>();

            //using (var client = new HttpClient())
            //{
            //    client.DefaultRequestHeaders.Clear();
            //    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //    HttpResponseMessage ResFec = await client.GetAsync($"{baseApiUrl}/fechapartidos/");
            //    HttpResponseMessage ResEqu = await client.GetAsync($"{baseApiUrl}/equipos/");

            //    if (ResFec.IsSuccessStatusCode && ResEqu.IsSuccessStatusCode)
            //    {
            //        var PosResponse = ResFec.Content.ReadAsStringAsync().Result;
            //        fechapartidos = JsonConvert.DeserializeObject<List<FechaPartido>>(PosResponse)!;

            //        var EquResponse = ResEqu.Content.ReadAsStringAsync().Result;
            //        equipos = JsonConvert.DeserializeObject<List<Equipo>>(EquResponse)!;

            //        ViewBag.DropDownDataFec = new SelectList(fechapartidos, "FecId", "FecFechaPartido");
            //        ViewBag.DropDownDataEq = new SelectList(equipos, "EqId", "EqNombre");
            //    }
            //    return View();
            //}

            //string baseApiUrl = _configuration.GetSection("LigaDominicanaApi").Value!;
            //using (var client = new HttpClient())
            //{
            //    client.BaseAddress = new Uri(baseApiUrl);
            //    var postTask = client.PostAsJsonAsync<Partido>($"{baseApiUrl}/partidos", partido);
            //    postTask.Wait();
            //    var result = postTask.Result;

            //    if (result.IsSuccessStatusCode)
            //    {
            //        return RedirectToAction("Index");
            //    }
            //}
            //ModelState.AddModelError(String.Empty, "error, esto no sirve ya.");
            //return View(partido);
        

        // GET: PartidosController/Edit/5
        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            string baseApiUrl = _configuration.GetSection("LigaDominicanaApi").Value!;
            List<PartidoView> partidoViewModels = new List<PartidoView>();
            PartidoView partidoViewModel = new PartidoView();
            Partido partido = new Partido();

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseApiUrl);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = await client.GetAsync($"{baseApiUrl}/partidos");

                    if (response.IsSuccessStatusCode)
                    {
                        var data = await response.Content.ReadAsStringAsync();
                        partidoViewModels = JsonConvert.DeserializeObject<List<PartidoView>>(data) ?? new List<PartidoView>();

                        partidoViewModel = partidoViewModels.FirstOrDefault(e => e.ParId == id)!;

                        var fechaPartidoList = partidoViewModels.Select(p => new SelectListItem
                        {
                            Text = p.FecFechaPartido.ToString(), // Texto visible en el DropDownList
                            Value = p.ParJuego.ToString() // Valor seleccionado cuando se elige un elemento del DropDownList
                        }).ToList();

                        ViewBag.FecFechaPartidoList = fechaPartidoList!;

                        var equipoGanadorList = partidoViewModels.Select(p => new SelectListItem
                        {
                            Text = p.ParEquipoGanador.ToString(), // Texto visible en el DropDownList
                            Value = p.ParEquipoGanadorId.ToString() // Valor seleccionado cuando se elige un elemento del DropDownList
                        }).ToList();

                        ViewBag.EquipoGanadorList = equipoGanadorList!;

                        var equipoPerdedorList = partidoViewModels.Select(p => new SelectListItem
                        {
                            Text = p.ParEquipoPerdedor.ToString(), // Texto visible en el DropDownList
                            Value = p.ParEquipoPerdedorId.ToString() // Valor seleccionado cuando se elige un elemento del DropDownList
                        }).ToList();

                        ViewBag.EquipoPerdedorList = equipoPerdedorList!;

                        partido.ParId = partidoViewModel.ParId;
                        partido.ParJuego = partidoViewModel.ParJuego;
                        partido.ParEquipoGanador = partidoViewModel.ParEquipoGanadorId;
                        partido.ParEquipoPerdedor = partidoViewModel.ParEquipoPerdedorId;
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
            return View(partido);
        }

        // POST: PartidosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(string id, [FromForm] Partido partido)
        {
            string baseApiUrl = _configuration.GetSection("LigaDominicanaApi").Value!;

            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var content = new StringContent(JsonConvert.SerializeObject(partido), Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PutAsync($"{baseApiUrl}/partidos/" + id.ToString(), content);

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
            return View(partido);
        }

        // GET: PartidosController/Delete/5
        public async Task <ActionResult> Delete(int id)
        {
            string baseApiUrl = _configuration.GetSection("LigaDominicanaApi").Value!;
            List<PartidoView> partidoViewModels = new List<PartidoView>();
            PartidoView partidoViewModel = new PartidoView();

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseApiUrl);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage response = await client.GetAsync($"{baseApiUrl}/partidos");


                    if (response.IsSuccessStatusCode)
                    {
                        // Procesa la respuesta JSON
                        var data = await response.Content.ReadAsStringAsync();
                        partidoViewModels = JsonConvert.DeserializeObject<List<PartidoView>>(data) ?? new List<PartidoView>();
                    }
                    else
                    {
                        Console.WriteLine("Error al llamar a la Web API: " + response.ReasonPhrase);
                        ModelState.AddModelError(String.Empty, "Error al obtener datos. Código de estado: " + response.StatusCode);
                    }

                    partidoViewModel = partidoViewModels.FirstOrDefault(e => e.ParId == id)!;
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
            return View(partidoViewModel);
        }

        // POST: PartidosController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id)
        {
            string baseApiUrl = _configuration.GetSection("LigaDominicanaApi").Value!;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseApiUrl);
                var deleteTask = client.DeleteAsync($"{baseApiUrl}/partidos/" + id.ToString());
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
