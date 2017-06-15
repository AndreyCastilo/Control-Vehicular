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
        public ActionResult EmpresaConductor(int id) // de la empresa
        {
            int a = id;

            ViewBag.Empresa = empresas.Elemento(1); //Cabmiar
            return View();
        }
        [HttpGet]
        public JsonResult GetConductoresEmpresa(int id)
        {
            var conductoresLista = conductores.Listar(id).ToList();
            var listaAux = conductoresLista.Select(x => new ConductorModelo(x));
            return Json(new { Registro = listaAux }, JsonRequestBehavior.AllowGet);
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
            return Json(new { Resultado = conductores.Remover(codigo) });
        }

        [HttpPost]
        public JsonResult Editar(Conductor conductor)
        {

            Conductor conductorDB = conductores.Editar(conductor);

            if (conductorDB != null)
                return Json(new { Resultado = true, Conductor = new ConductorModelo(conductorDB) }, JsonRequestBehavior.AllowGet);
            else
                return Json(new { Resultado = false }, JsonRequestBehavior.AllowGet);
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
        private ConjuntoEmpresa empresas = new ConjuntoEmpresa();
    }
}