using Modelo.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ControlVehicular.Models
{
    public class ClienteRutaModelo
    {
        public int? Ruta { get; set; }

        public int? Cliente { get; set; }

        public int? Unidad { get; set; }

        public int? PadreCliente { get; set; }

        public int? HijoCliente { get; set; }

        [Display(Name = "Código")]
        public String codigo { get; set; }

        [Display(Name = "Nombre del Padre")]
        public String NombrePadreCliente { get; set; }
        [Display(Name = "Nombre del Hijo")]
        public String NombreHijoCliente { get; set; }
        [Display(Name = "Nombre de la ruta")]
        public String NombreRuta { get; set; }

        public IEnumerable<SelectListItem> Rutas { get; set; }

        public IEnumerable<SelectListItem> PadreClientes { get; set; }

        public IEnumerable<SelectListItem> HijosPadre { get; set; }


        public ClienteRutaModelo(ClienteRuta clienteRuta,Ruta ruta, PadreCliente padreCliente, ClienteHijo hijoCliente)
        {
            this.Cliente = clienteRuta.ClienteHijo;
            this.Ruta = clienteRuta.Ruta;
            this.NombreRuta = ruta.Nombre;
            this.Unidad = ruta.Unidad.Codigo;
            this.PadreCliente = padreCliente.Codigo;
            this.HijoCliente = hijoCliente.Codigo;
            this.NombrePadreCliente = padreCliente.Nombre;
            this.NombreHijoCliente = hijoCliente.Nombre;
            this.codigo = clienteRuta.ClienteHijo.ToString() + "-" + clienteRuta.Ruta.ToString();

        }

        public ClienteRutaModelo(IEnumerable<Ruta> rutas, IEnumerable<PadreCliente> padres, IEnumerable<ClienteHijo> hijos)
        {

            List<SelectListItem> items = new List<SelectListItem>();
            List<SelectListItem> items2 = new List<SelectListItem>();
            List<SelectListItem> items3 = new List<SelectListItem>();


            foreach (var ruta in rutas)
            {
                items.Add(new SelectListItem { Text = ruta.Codigo.ToString()+"-"+ruta.Nombre, Value = ruta.Codigo.ToString() });
            }

            foreach (var padre in padres)
            {
                items2.Add(new SelectListItem { Text = padre.Codigo.ToString() + "-" + padre.Nombre, Value = padre.Codigo.ToString() });
            }

            foreach (var hijo in hijos)
            {
                items3.Add(new SelectListItem { Text = hijo.Codigo.ToString() + "-" + hijo.Nombre, Value = hijo.Codigo.ToString() });
            }
            this.HijosPadre = items3;
            this.PadreClientes = items2;
            this.Rutas = items;

        }
    }
}