﻿using Modelo.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ControlVehicular.Models
{
    public class ClienteRutaModelo
    {
        public int? Ruta { get; set; }

        public int? Cliente { get; set; }

        public String codigo { get; set; }
        public String NombrePadreCliente { get; set; }
        public String NombreHijoCliente { get; set; }
        public String NombreRuta { get; set; }

        public IEnumerable<SelectListItem> Rutas { get; set; }

        public IEnumerable<SelectListItem> PadreClientes { get; set; }

        public IEnumerable<SelectListItem> HijosPadre { get; set; }


        public ClienteRutaModelo(ClienteRuta clienteRuta,Ruta ruta, PadreCliente padreCliente, ClienteHijo hijoCliente)
        {
            this.Cliente = clienteRuta.ClienteHijo;
            this.Ruta = clienteRuta.Ruta;
            this.NombreRuta = ruta.Nombre;
            this.NombrePadreCliente = padreCliente.Nombre;
            this.NombreHijoCliente = hijoCliente.Nombre;
            this.codigo = clienteRuta.ClienteHijo.ToString() + "-" + clienteRuta.Ruta.ToString();

        }

        public ClienteRutaModelo(IEnumerable<Ruta> rutas, IEnumerable<PadreCliente> padres, IEnumerable<PadreCliente> hijos)
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