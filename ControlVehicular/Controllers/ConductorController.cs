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
    public class ConductorController : Controller
    {
        // GET: Pacientes
        public ActionResult Empresa(int id) // de la empresa
        {
            int a = id;
            //empresas.Obtener(codigo);
            //ViewBag.Empresa = new EmpresaModelo();
            return View();
        }
        [HttpGet]
        public JsonResult GetConductoresEmpresa(int empresa)
        {
            var conductoresLista = conductores.Listar();
            var listaAux = new LinkedList<ConductorModelo>();
            conductoresLista.Select(x => listaAux.AddLast(new ConductorModelo(x)));
            return Json(listaAux, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Guardar(Conductor conductor)
        {
            var conductorDB = conductores.Agregar(conductor);
            return Json(new { Resultado = true, Conductor = new ConductorModelo(conductorDB) });
        }

        [HttpPost]
        public JsonResult Remover(int codigo)
        {          
            return Json(conductores.Remover(codigo));
        }

        [HttpPost]
        public JsonResult Editar(Conductor conductor)
        {             
                return Json(conductores.Editar(conductor));       
        }

        [HttpGet]
        public JsonResult Obtener(int codigo)
        {
            Conductor registro = conductores.Obtener(codigo);
            if (registro != null)
                return Json(new { Resultado = true, Conductor = new ConductorModelo(registro) }, JsonRequestBehavior.AllowGet);
            else
                return Json(new { Resultado = false }, JsonRequestBehavior.AllowGet);
        }

        private ConjuntoConductor conductores = new ConjuntoConductor();
    }
}