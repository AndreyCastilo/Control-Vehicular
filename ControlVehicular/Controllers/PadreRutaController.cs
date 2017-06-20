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
    public class PadreRutaController : Controller
    {
        // GET: PadreRuta
        public ActionResult RutaHijos(int id)
        {
            ViewBag.Padre = padres.Obtener(id);
            return View();
        }

        [HttpGet]
        public ActionResult GetClienteRuta(int id) {
            ClienteRuta clienteRuta = clientesRutas.ObtenerRutaCliente(id); //Pedir agui la ruta con el codigo del clienteHijo
            if (clienteRuta != null)
            {
                Ruta ruta = clienteRuta.Ruta1;
                Empresa empresa = ruta.Empresa1;
                Conductor conductor = ruta.Conductor1;
                Unidad unidad = ruta.Unidad;
                return Json(new { Resultado = true,
                                  Empresa = new EmpresaModelo(empresa),
                                  Conductor =new ConductorModelo(conductor),
                                  Unidad = new UnidadModelo(unidad),
                                  Latitud = unidad.Latitud,
                                  Longitud = unidad.Longitud
                },JsonRequestBehavior.AllowGet);
            }
            else {
                return Json(false);
            }
       
        }


        private ConjuntoClienteRuta clientesRutas = new ConjuntoClienteRuta();
        private ConjuntoPadresClientes padres = new ConjuntoPadresClientes();
    }
}