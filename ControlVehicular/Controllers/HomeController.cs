﻿using ControlVehicular.Models;
using Modelo.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ControlVehicular.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public JsonResult ActualizaCoordenadas(int codigoUnidad, double lat, double lon) {
            unidades.ActualizaCoordenadas(codigoUnidad, lat, lon);
            return Json(new { ok = true });
        }
        [HttpGet]
        public JsonResult ClientesEnRuta(int codigoUnidad) {
           var clientes = unidades.ClientesEnRuta(codigoUnidad).Select(cl => new ClienteHijoModelo(cl)).ToList();
            return Json(new { Clientes = clientes }, JsonRequestBehavior.AllowGet);
        }
    

        private ConjuntoUnidad unidades = new ConjuntoUnidad();
    }
}