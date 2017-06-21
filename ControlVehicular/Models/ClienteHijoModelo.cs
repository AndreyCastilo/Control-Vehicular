using Modelo.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ControlVehicular.Models
{
    public class ClienteHijoModelo
    {
        [Display(Name = "Código")]
        public long Codigo { get; set; }

        [Display(Name = "Padre del cliente")]
        public int? PadreCliente { get; set;}

        [MaxLength(100)]
        public string Nombre { get; set; }

        [Display(Name = "Mostrar como"),MaxLength(100)]
        public string MostrarComo { get; set; }


        public ClienteHijoModelo(ClienteHijo cliente)
        {
            this.Codigo = cliente.Codigo;
            this.Nombre = cliente.Nombre;
            this.MostrarComo = cliente.MostrarComo;
            this.PadreCliente = cliente.PadreCliente;
        }
    }
}