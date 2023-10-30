﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using LIDOM_MVC.Models;
using LIDOM_MVC.ViewModels;
using System.Net.Http;

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
        public ActionResult Create()
        {
            return View();
        }

        // POST: EquiposController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: EquiposController/Edit/5
        [HttpGet]
        public ActionResult Edit(int id)
        {

            string baseApiUrl = _configuration.GetSection("LigaDominicanaApi").Value!;
            Equipo equipo = new Equipo();
            Estadio estadio = new Estadio();
            EquipoViewModel customEquipo = new EquipoViewModel();


            string jsonEquipoResponse = httpClient.GetStringAsync($"{baseApiUrl}/equipos/" + id.ToString()).Result;
            equipo = string.IsNullOrEmpty(jsonEquipoResponse) ? equipo : JsonConvert.DeserializeObject<Equipo>(jsonEquipoResponse)!;


            string jsonEstadiosResponse = httpClient.GetStringAsync($"{baseApiUrl}/estadios/" + id.ToString()).Result;
            estadio = string.IsNullOrEmpty(jsonEstadiosResponse) ? estadio : JsonConvert.DeserializeObject<Estadio>(jsonEstadiosResponse)!;

            customEquipo.EqId = equipo.EqId;
            customEquipo.EqNombre = equipo.EqNombre;
            customEquipo.EqDescripcion = equipo.EqDescripcion;
            customEquipo.EqCiudad = equipo.EqCiudad;
            customEquipo.EqEstadioNombre = estadio.EstNombre;
            customEquipo.EqEstatus = equipo.EqEstatus;
       
            return View(customEquipo);

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
