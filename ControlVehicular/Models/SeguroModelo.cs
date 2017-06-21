using Modelo.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ControlVehicular.Models
{
    public class SeguroModelo
    {
        [Display(Name = "Código")]
        public long Codigo { get; set; }
        public int? Empresa { get; set; }

        [Display(Name = "Nombre de la empresa")]
        public string EmpresaNombre { get; set; }

        [MaxLength(100)]
        public string Nombre { get; set; }

        [Display(Name = "Tipo de seguro"),MaxLength(100)]
        public string Tipo { get; set; }

        [DataType(DataType.MultilineText)]
        public string Detalle { get; set; }

        public IEnumerable<SelectListItem> Empresas { get; set; }

        public SeguroModelo(Seguro seguro) {
            this.Codigo = seguro.Codigo;
            this.Empresa = seguro.Empresa;
            if(seguro.Empresa1!=null)
                this.EmpresaNombre = seguro.Empresa1.Nombre;
            this.Nombre = seguro.Nombre;
            this.Tipo = seguro.Tipo;
            this.Detalle = seguro.Detalle;
        }

        public SeguroModelo(IEnumerable<Empresa> empresas) {

            List<SelectListItem> items = new List<SelectListItem>();

            foreach (var empresa in empresas)
            {
                items.Add(new SelectListItem { Text = empresa.Nombre, Value = empresa.Codigo.ToString() });
            }

            this.Empresas = items;

        }
    }
}