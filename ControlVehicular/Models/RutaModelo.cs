using Modelo.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ControlVehicular.Models
{
    public class RutaModelo
    {
        public long Codigo { get; set; }
        public int? Empresa { get; set; }

        [MaxLength(100)]
        public string Nombre { get; set; }

        public int? Conductor { get; set; }

        public int? Vehiculo { get; set; }

        public RutaModelo(Ruta ruta) {
            this.Codigo = ruta.Codigo;
            this.Empresa = ruta.Empresa;
            this.Conductor = ruta.Conductor;
            this.Vehiculo = ruta.Vehiculo;
        }
    }
}