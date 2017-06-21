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
    public class EmpresaController : Controller
    {

        private ConjuntoEmpresa ConexionEmpresa = new ConjuntoEmpresa();

        // GET: Empresa
        public ActionResult Index()
        {
            if (VariablesGlobales.Codigo == 0) {
                ViewBag.Codigo = 0;
            }
            else
            {
                ViewBag.Codigo = VariablesGlobales.Codigo;
            }
            return View();
        }

        [HttpPost]
        public JsonResult Guardar(Empresa emp)
        {
            
            var cod = ConexionEmpresa.Guardar(emp);

            return Json(new { Resultado = true, Empresa = new EmpresaModelo(cod) } );


        }

        [HttpPost]
        public JsonResult Editar(Empresa emp)
        {
            var cod = ConexionEmpresa.Editar(emp);
            return Json( new { Resultado = true, Empresa = new EmpresaModelo(cod) });
        }

        public JsonResult Elemento(int codigo)
        {

            var registro = ConexionEmpresa.Elemento(codigo);
            return Json(new { Resultado = true, Empresa = new EmpresaModelo(registro) }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Eliminar(int codigo)
        {
            ConexionEmpresa.Remover(codigo);
            return Json(true);
        }


        public JsonResult ObtenerTodas() {
            var empresasDB = ConexionEmpresa.ObtenerTodas().Select(em => new EmpresaModelo(em)).ToList();
            return Json(new { res = true, registro = empresasDB},JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SeleccionarEmpresa(int codigo)
        {
            VariablesGlobales.Codigo = codigo;
            return Json(new { result = true });
        }

    }
}