using Modelo.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ControlVehicular.Models
{
    public class SeguroModelo
    {
        public long Codigo { get; set; }
        public int? Empresa { get; set; }

        [MaxLength(100)]
        public string Nombre { get; set; }

        [MaxLength(100)]
        public string Tipo { get; set; }

        [DataType(DataType.MultilineText)]
        public string Detalle { get; set; }

        public SeguroModelo(Seguro seguro) {
            this.Codigo = seguro.Codigo;
            this.Empresa = seguro.Empresa;
            this.Nombre = seguro.Nombre;
            this.Tipo = seguro.Tipo;
            this.Detalle = seguro.Detalle;
        }
    }
}