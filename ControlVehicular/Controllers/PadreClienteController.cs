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
    public class PadreClienteController : Controller
    {
        // GET: PadreCliente
        public ActionResult Cliente()
        {
            if (VariablesGlobales.Codigo != 0)
            {
                IEnumerable<Empresa> empresasA = empresas.ObtenerTodas();

                var vm = new PadreClienteModelo(empresasA);
                return View(vm);
            }
            return RedirectToAction("Index", "Empresa");
        }

        [HttpPost]
        public JsonResult Guardar(PadreCliente clienteNuevo)
        {
            clienteNuevo.Empresa = VariablesGlobales.Codigo;
            var clienteBase = clientes.Agregar(clienteNuevo);
            return Json(new { Resultado = true, PadreCliente = new PadreClienteModelo(clienteBase) });
        }

        [HttpGet]
        public JsonResult Obtener(int codigo)
        {
            PadreCliente registro = clientes.Obtener(codigo);
            if (registro != null)
                return Json(new { Resultado = true, Cliente = new PadreClienteModelo(registro) }, JsonRequestBehavior.AllowGet);
            else
                return Json(new { Resultado = false }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Editar(PadreCliente cliente)
        {
            var resultado = clientes.Editar(cliente);
            resultado.Empresa = VariablesGlobales.Codigo;

            if(resultado!= null)
            {
                return Json(new { Resultado = true, PadreCliente = new PadreClienteModelo(resultado) },JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { Resultado = false }, JsonRequestBehavior.AllowGet);
            }

            
        }

        public JsonResult GetClientes()
        {
                var listaClientesBD = clientes.GetClientes(VariablesGlobales.Codigo);
                var listaClientesModelo = listaClientesBD.Select(x => new PadreClienteModelo(x));
                return Json( new { Registro = listaClientesModelo }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Remover(int codigo)
        {
            return Json(new { Resultado = clientes.Remover(codigo) });
        }

        private ConjuntoPadresClientes clientes = new ConjuntoPadresClientes();
        private ConjuntoEmpresa empresas = new ConjuntoEmpresa();
    }
}