using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Modelo.Modelo;
using Modelo.Database;
using ControlVehicular.Models;

namespace ControlVehicular.Controllers
{
    public class UnidadController : Controller
    {

        private ConjuntoUnidad unidades = new ConjuntoUnidad();
        // GET: Unidad
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Agregar(Unidad datos)
        {
            var unidadDB = unidades.Agregar(datos);
            return Json(new { Resultado = true, Unidad = new UnidadModelo(unidadDB) });

        }

        public JsonResult ObtenerTodas()
        {
            var unidadesDB = unidades.ObtenerTodas().Select(em => new UnidadModelo(em)).ToList();
            return Json(new { registro = unidadesDB }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Elemento(int codigo)
        {
            var registro = unidades.Elemento(codigo);
            return Json(new { Resultado = true, Unidad = new UnidadModelo(registro) }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Editar(Unidad uni)
        {
            var cod = unidades.Editar(uni);
            return Json(new { Resultado = true, Unidad = new UnidadModelo(cod) });
        }

        [HttpPost]
        public JsonResult Eliminar(int codigo)
        {
            unidades.Remover(codigo);
            return Json(true);
        }



    }
}