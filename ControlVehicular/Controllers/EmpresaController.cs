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
            ConexionEmpresa.Editar(emp);
            return Json(true);
        }

        public JsonResult Elemento(int codigo)
        {

            var registro = ConexionEmpresa.Elemento(codigo);

            return Json(new { Resultado = true, Empresa = new EmpresaModelo(registro) }
            
               


            , JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Eliminar(int codigo)
        {
            ConexionEmpresa.Remover(codigo);
            return Json(true);
        }


    }
}