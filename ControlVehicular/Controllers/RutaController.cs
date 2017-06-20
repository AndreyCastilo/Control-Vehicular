using Modelo.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Modelo.Modelo;
using ControlVehicular.Models;

namespace ControlVehicular.Controllers
{
    public class RutaController : Controller

    {

        private ConjuntoRuta rutas = new ConjuntoRuta();
        private ConjuntoConductor conductores = new ConjuntoConductor();
        private ConjuntoUnidad unidades = new ConjuntoUnidad();
        // GET: Ruta
        public ActionResult Index()
        {
            var rutaModelo = new RutaModelo
            {
                ListaConductores = conductores.Listar(1).ToList(),
                ListaUnidades = unidades.Listar(1).ToList()
                
            };
            return View(rutaModelo);
        }

        [HttpPost]
        public ActionResult Agregar(Ruta datos)
        {
            var rutaDB = rutas.Agregar(datos);
            return Json(new { Resultado = true, Ruta = new RutaModelo(rutaDB) });

        }


        [HttpGet]
        public JsonResult ObtenerTodas()
        {
            var rutasDB = rutas.ObtenerTodas().Select(em => new RutaModelo(em)).ToList();
            return Json(new { registro = rutasDB }, JsonRequestBehavior.AllowGet);
        }
        
        [HttpGet]
        public JsonResult Elemento(int codigo)
        {
            var registro = rutas.Elemento(codigo);
            return Json(new
            {
                Resultado = true,
                Ruta = new RutaModelo(registro),
                
            },JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Editar(Ruta ruta)
        {

            var cod = rutas.Editar(ruta);
            return Json(new { Resultado = true, Ruta = new RutaModelo(cod) });
        }

    }
}