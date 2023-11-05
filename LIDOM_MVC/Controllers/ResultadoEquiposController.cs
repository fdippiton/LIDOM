using LIDOM_MVC.Models;
using LIDOM_MVC.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

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

        // GET: ResultadoEquiposController
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            string baseApiUrl = _configuration.GetSection("LigaDominicanaApi").Value!;
            List<ResultadoEquipoViewModel> resultadoEquipoViewModel = new List<ResultadoEquipoViewModel>();

            try
            {

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseApiUrl);
                    // Establece la URL de tu Web API


                    // Establece el tipo de contenido que esperas en la respuesta
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    // Realiza la solicitud HTTP para llamar al endpoint de la Web API
                    HttpResponseMessage response = await client.GetAsync($"{baseApiUrl}/resultadoEquipos");


                    if (response.IsSuccessStatusCode)
                    {
                        // Procesa la respuesta JSON
                        var data = await response.Content.ReadAsStringAsync();

                        // Deserializa los datos JSON a objetos C# utilizando un marco como Newtonsoft.Json
                        resultadoEquipoViewModel = JsonConvert.DeserializeObject<List<ResultadoEquipoViewModel>>(data) ?? new List<ResultadoEquipoViewModel>();
                        //calendarioViewModel = JsonConvert.DeserializeObject<CalendarioViewModel[]>(data);


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
            return View(resultadoEquipoViewModel);
        }

        // GET: ResultadoEquiposController/Details/5
        [HttpGet]
        public async Task<ActionResult> Details(int id)
        {
            string baseApiUrl = _configuration.GetSection("LigaDominicanaApi").Value!;
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
                    Info = JsonConvert.DeserializeObject<ResultadoEquipo>(Response)!;
                }
                return View(Info);
            }
        }

        // GET: ResultadoEquiposController/Create
        public async Task <ActionResult> Create()
        {
            string baseApiUrl = _configuration.GetSection("LigaDominicanaApi").Value!;
            List<ResultadoEquipoViewModel> resultadoEquipoViewModels = new List<ResultadoEquipoViewModel>();

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseApiUrl);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = await client.GetAsync($"{baseApiUrl}/resultadoEquipos");

                    if (response.IsSuccessStatusCode)
                    {
                        var data = await response.Content.ReadAsStringAsync();
                        resultadoEquipoViewModels = JsonConvert.DeserializeObject<List<ResultadoEquipoViewModel>>(data) ?? new List<ResultadoEquipoViewModel>();

                        //var fechasPartido = partidoViewModel.Select(p => p.FecFechaPartido).ToList();
                        var numPartidoList = resultadoEquipoViewModels.Select(p => new SelectListItem
                        {
                            Text = p.ResPartido.ToString(), // Texto visible en el DropDownList
                            Value = p.ResPartido.ToString() // Valor seleccionado cuando se elige un elemento del DropDownList
                        }).ToList();

                        ViewBag.PartidoList = numPartidoList!;

                        var equipoList = resultadoEquipoViewModels.Select(p => new SelectListItem
                        {
                            Text = p.ResEquipoNombre.ToString(), // Texto visible en el DropDownList
                            Value = p.ResEquipo.ToString() // Valor seleccionado cuando se elige un elemento del DropDownList
                        }).ToList();

                        ViewBag.EquipoList = equipoList!;
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

        // POST: ResultadoEquiposController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([FromForm] ResultadoEquipo resultadoEquipo)
        {
            string baseApiUrl = _configuration.GetSection("LigaDominicanaApi").Value!;

            try
            {
                var postTask = httpClient.PostAsJsonAsync<ResultadoEquipo>($"{baseApiUrl}/resultadoEquipos", resultadoEquipo);
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
            return View(resultadoEquipo);
        }

        // GET: ResultadoEquiposController/Edit/5
        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            string baseApiUrl = _configuration.GetSection("LigaDominicanaApi").Value!;
            List<ResultadoEquipoViewModel> resultadoEquipoViewModels = new List<ResultadoEquipoViewModel>();
            ResultadoEquipoViewModel resultadoEquipoViewModel = new ResultadoEquipoViewModel();
            ResultadoEquipo resultadoEquipo = new ResultadoEquipo();

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseApiUrl);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = await client.GetAsync($"{baseApiUrl}/resultadoEquipos");

                    if (response.IsSuccessStatusCode)
                    {
                        var data = await response.Content.ReadAsStringAsync();
                        resultadoEquipoViewModels = JsonConvert.DeserializeObject<List<ResultadoEquipoViewModel>>(data) ?? new List<ResultadoEquipoViewModel>();

                        resultadoEquipoViewModel = resultadoEquipoViewModels.FirstOrDefault(e => e.ResId == id)!;

                        var numPartidoList = resultadoEquipoViewModels.Select(p => new SelectListItem
                        {
                            Text = p.ResPartido.ToString(), // Texto visible en el DropDownList
                            Value = p.ResPartido.ToString() // Valor seleccionado cuando se elige un elemento del DropDownList
                        }).ToList();

                        ViewBag.PartidoList = numPartidoList!;

                        var equipoList = resultadoEquipoViewModels.Select(p => new SelectListItem
                        {
                            Text = p.ResEquipoNombre.ToString(), // Texto visible en el DropDownList
                            Value = p.ResEquipo.ToString() // Valor seleccionado cuando se elige un elemento del DropDownList
                        }).ToList();

                        ViewBag.EquipoList = equipoList!;


                        resultadoEquipo.ResId = resultadoEquipoViewModel.ResId;
                        resultadoEquipo.ResPartido = resultadoEquipoViewModel.ResPartido;
                        resultadoEquipo.ResEquipo = resultadoEquipoViewModel.ResEquipo;
                        resultadoEquipo.ResCarreras = resultadoEquipoViewModel.ResCarreras;
                        resultadoEquipo.ResHits = resultadoEquipoViewModel.ResHits;
                        resultadoEquipo.ResErrores = resultadoEquipoViewModel.ResErrores;
                        resultadoEquipo.ResJuegoGanado = resultadoEquipoViewModel.ResJuegoGanado;
                        resultadoEquipo.ResJuegoPerdido = resultadoEquipoViewModel.ResJuegoPerdido;
                        resultadoEquipo.ResJuegoEmpate = resultadoEquipoViewModel.ResJuegoEmpate;

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
            return View(resultadoEquipo);
        }

        // POST: ResultadoEquiposController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, [FromForm] ResultadoEquipo resultadoEquipo)
        {
            string baseApiUrl = _configuration.GetSection("LigaDominicanaApi").Value!;

            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    // Serializa el objeto temporada en formato JSON
                    var content = new StringContent(JsonConvert.SerializeObject(resultadoEquipo), Encoding.UTF8, "application/json");

                    // Envía una solicitud PUT a la API con los datos actualizados
                    HttpResponseMessage response = await client.PutAsync($"{baseApiUrl}/resultadoEquipos/" + id.ToString(), content);

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
            return View(resultadoEquipo);
        }

        // GET: ResultadoEquiposController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        // POST: ResultadoEquiposController/Delete/5
        [HttpGet]
        //[ValidateAntiForgeryToken]
        public ActionResult Delete(string id)
        {
            string baseApiUrl = _configuration.GetSection("LigaDominicanaApi").Value!;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseApiUrl);
                var deleteTask = client.DeleteAsync($"{baseApiUrl}/resultadoEquipos/" + id.ToString());
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
