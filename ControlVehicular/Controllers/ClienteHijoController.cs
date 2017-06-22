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
    public class ClienteHijoController : Controller
    {
        // GET: ClienteHijo
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PadreHijo(int id) // de la padre
        {
            int a = id;

            ViewBag.Padre = padres.Obtener(id); //Cabmiar
            return View();
        }
        [HttpGet]
        public JsonResult GetHijosPadre(int id)
        {
            var hijosLista = hijos.Listar(id).ToList();
            var listaAux = hijosLista.Select(x => new ClienteHijoModelo(x));
            return Json(new { Registro = listaAux }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Guardar(ClienteHijo hijo)
        {
            var hijoDB = hijos.Agregar(hijo);
            return Json(new { Resultado = true, Hijo = new ClienteHijoModelo(hijoDB) });
        }

        [HttpPost]
        public JsonResult Remover(int codigo)
        {
            return Json(new { Resultado = hijos.Remover(codigo) });
        }

        [HttpPost]
        public JsonResult Editar(ClienteHijo hijo)
        {

            ClienteHijo hijoDB = hijos.Editar(hijo);

            if (hijoDB != null)
                return Json(new { Resultado = true, Hijo = new ClienteHijoModelo(hijoDB) }, JsonRequestBehavior.AllowGet);
            else
                return Json(new { Resultado = false }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult Obtener(int codigo)
        {
            ClienteHijo registro = hijos.Obtener(codigo);
            if (registro != null)
                return Json(new { Resultado = true, Hijo = new ClienteHijoModelo(registro) }, JsonRequestBehavior.AllowGet);
            else
                return Json(new { Resultado = false }, JsonRequestBehavior.AllowGet);
        }

        private ConjuntoClienteHijo hijos = new ConjuntoClienteHijo();
        private ConjuntoPadresClientes padres = new ConjuntoPadresClientes();
    }
}