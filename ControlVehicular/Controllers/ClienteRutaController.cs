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
    public class ClienteRutaController : Controller
    {
        // GET: ClienteRuta
        public ActionResult RutaClientesEmpresa(int id)
        {
            ViewBag.Empresa = empresas.Elemento(id);
            IEnumerable<Ruta> rutas = Conexion.Open.Ruta.Where(x=> x.Empresa == id);
            IEnumerable<PadreCliente> padres = Conexion.Open.PadreCliente.Where(x => x.Empresa == id);
            IEnumerable<PadreCliente> hijos = new LinkedList<PadreCliente>();
            var vm = new ClienteRutaModelo(rutas,padres, hijos);
            return View(vm);
        }


        public ActionResult actualizarDropdownListHijos(int id) {
            IEnumerable<ClienteHijo> hijos = Conexion.Open.ClienteHijo.Where(x => x.PadreCliente == id);
            return Json(new SelectList(hijos, "Codigo", "Nombre"), JsonRequestBehavior.AllowGet);
        }



        public ActionResult AgregarRutaCliente(String id) {
            string[] codigos = id.Split('-');
            int codigoCliente = int.Parse(codigos[0]);
            int codigoRuta = int.Parse(codigos[1]);
            ClienteRuta clienteRuta = new ClienteRuta();
            clienteRuta.ClienteHijo = codigoCliente;
            clienteRuta.Ruta = codigoRuta;
            ClienteRuta clienteRutaDB;
            try
            {
                clientesRutas.Agregar(clienteRuta);
                clienteRutaDB = clientesRutas.Obtener(codigoCliente, codigoRuta);
            }
            catch (Exception e)
            {
                return Json(new { Resultado = false });
            }
            
            return Json( new { Resultado = true,
                              ClienteRuta = new ClienteRutaModelo(clienteRutaDB, clienteRutaDB.Ruta1,clienteRutaDB.ClienteHijo1.PadreCliente1, clienteRutaDB.ClienteHijo1)

                             },JsonRequestBehavior.AllowGet );
        }

        public ActionResult ObtenerRutaCliente(String id) {
            string[] codigos = id.Split('-');
            String codigoCliente = codigos[0];
            String codigoRuta = codigos[1];
            ClienteRuta clienteRuta = clientesRutas.Obtener(int.Parse(codigoCliente), int.Parse(codigoRuta));
            if (clienteRuta != null)
            {
                return Json(new { Resultado = true, ClienteRuta = new ClienteRutaModelo(clienteRuta, clienteRuta.Ruta1, clienteRuta.ClienteHijo1.PadreCliente1, clienteRuta.ClienteHijo1) }, JsonRequestBehavior.AllowGet);
            }
            else {
                return Json(new { Resultado = false });
            }

        }
        public ActionResult Listar(int id) {
            var listaClientesRutas= clientesRutas.Listar(id);
            var listaClientesRutasModelo = listaClientesRutas.Select(x => new ClienteRutaModelo(x,x.Ruta1,x.ClienteHijo1.PadreCliente1,x.ClienteHijo1));
            return Json(new { Resultado = true , Registro= listaClientesRutasModelo }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Editar(String id,String idViejo) {
            string[] codigos = id.Split('-');
            string[] codigos2 = idViejo.Split('-');
            String codigoCliente = codigos2[0];
            String codigoRuta = codigos2[1];
            String codigoClienteNuevo = codigos[0];
            String codigoRutaNuevo = codigos[1];
            ClienteRuta clienteRutaAuxViejo = clientesRutas.Obtener(int.Parse(codigoCliente), int.Parse(codigoRuta));
            ClienteRuta clienteRuta = new ClienteRuta();
            clienteRuta.ClienteHijo = int.Parse(codigoClienteNuevo);
            clienteRuta.Ruta = int.Parse(codigoRutaNuevo);
            ClienteRuta clienteRutaDB;
            if (clientesRutas.Obtener(int.Parse(codigoClienteNuevo), int.Parse(codigoRutaNuevo))== null) {
                if (clientesRutas.Remover(clienteRutaAuxViejo))
                {
                    try
                    {
                        clientesRutas.Agregar(clienteRuta);
                        clienteRutaDB = clientesRutas.Obtener(int.Parse(codigoClienteNuevo), int.Parse(codigoRutaNuevo));
                    }
                    catch (Exception e)
                    {
                        return Json(new { Resultado = false });
                    }
                    if (clienteRutaDB != null)
                    {
                        return Json(new { Resultado = true, ClienteRuta = new ClienteRutaModelo(clienteRutaDB, clienteRutaDB.Ruta1, clienteRutaDB.ClienteHijo1.PadreCliente1, clienteRutaDB.ClienteHijo1) }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { Resultado = false });
                    }
                }
                return Json(new { Resultado = false });
            }
            else
            {
                return Json(new { Resultado = false });
            }
        }


        private ConjuntoClienteRuta clientesRutas = new ConjuntoClienteRuta();
        private ConjuntoEmpresa empresas = new ConjuntoEmpresa();
    }
}