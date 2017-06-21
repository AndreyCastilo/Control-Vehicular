using ControlVehicular.Models;
using Modelo.Database;
using Modelo.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ControlVehicular.Controllers
{
    public class SegurosController : Controller
    {
        private ConjuntoSeguro ConexionSeguro = new ConjuntoSeguro();
        private ConjuntoEmpresa ConexionEmpresa = new ConjuntoEmpresa();

        // GET: Seguro
        public ActionResult Index()
        {
            if (VariablesGlobales.Codigo != 0)
            {
                IEnumerable<Empresa> empresas = ConexionEmpresa.ObtenerTodas();

                var vm = new SeguroModelo(empresas);

                return View(vm);
            }
            return RedirectToAction("Index", "Empresa");
        }

        [HttpPost]
        public JsonResult Guardar(Seguro seg)
        {
            seg.Empresa = VariablesGlobales.Codigo;
            var cod = ConexionSeguro.Guardar(seg);

            return Json(new { Resultado = true, Seguro = new SeguroModelo(cod) });


        }

        [HttpPost]
        public JsonResult Editar(Seguro seg)
        {
            var cod = ConexionSeguro.Editar(seg);
            return Json(new { Resultado = true, Empresa = new SeguroModelo(cod) });
        }

        public JsonResult Elemento(int codigo)
        {

            var registro = ConexionSeguro.Elemento(codigo);

            return Json(new { Resultado = true, Seguro = new SeguroModelo(registro) }
            , JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Eliminar(int codigo)
        {
            ConexionSeguro.Remover(codigo);
            return Json(true);
        }


        public JsonResult ObtenerTodas(){
            IEnumerable<Seguro> segurosDB = ConexionSeguro.ObtenerTodas(VariablesGlobales.Codigo);
            return Json(new { registro = segurosDB.Select(em => new SeguroModelo(em)) }, JsonRequestBehavior.AllowGet);
        }
    }
}