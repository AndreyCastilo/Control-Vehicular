using Modelo.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ControlVehicular.Models
{
    public class PadreClienteModelo
    {
        public long Codigo { get; set; }
        public int? Empresa { get; set; }

        [MaxLength(100)]
        public string Nombre { get; set; }

        [MaxLength(100)]
        public string MostrarComo { get; set; }

        [DataType(DataType.MultilineText)]
        public string Direccion { get; set; }

        public string EmpresaNombre { get; set; }

        public IEnumerable<SelectListItem> Empresas { get; set; }
        public PadreClienteModelo(PadreCliente cliente) {
            this.Codigo = cliente.Codigo;
            this.Empresa = cliente.Empresa;
            this.Nombre = cliente.Nombre;
            if (cliente.Empresa1 != null)
                this.EmpresaNombre = cliente.Empresa1.Nombre;
            this.MostrarComo = cliente.MostrarComo;
            this.Direccion = cliente.Direccion;
        }

        public PadreClienteModelo(IEnumerable<Empresa> empresas)
        {

            List<SelectListItem> items = new List<SelectListItem>();

            foreach (var empresa in empresas)
            {
                items.Add(new SelectListItem { Text = empresa.Nombre, Value = empresa.Codigo.ToString() });
            }

            this.Empresas = items;

        }
    }
}