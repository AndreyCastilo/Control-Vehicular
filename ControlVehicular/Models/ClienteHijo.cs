using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ControlVehicular.Models
{
    public class ClienteHijo
    {
        public long Codigo { get; set; }

        public int? PadreCliente { get; set;}

        [MaxLength(100)]
        public string Nombre { get; set; }

        [MaxLength(100)]
        public string MostrarComo { get; set; }


        public ClienteHijo(ClienteHijo cliente)
        {
            this.Codigo = cliente.Codigo;
            this.Nombre = cliente.Nombre;
            this.MostrarComo = cliente.MostrarComo;
            this.PadreCliente = cliente.PadreCliente;
        }
    }
}