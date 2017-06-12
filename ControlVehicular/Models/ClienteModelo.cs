using Modelo.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ControlVehicular.Models
{
    public class ClienteModelo
    {
        public long Codigo { get; set; }
        public int? Empresa { get; set; }

        [MaxLength(100)]
        public string Nombre { get; set; }

        [MaxLength(100)]
        public string MostrarComo { get; set; }

        [DataType(DataType.MultilineText)]
        public string Direccion { get; set; }

        public ClienteModelo(Cliente cliente) {
            this.Codigo = cliente.Codigo;
            this.Empresa = cliente.Empresa;
            this.Nombre = cliente.Nombre;
            this.MostrarComo = cliente.MostrarComo;
            this.Direccion = cliente.Direccion;
        }
    }
}