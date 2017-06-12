using Modelo.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ControlVehicular.Models
{
    public class ClienteRutaModelo
    {
        public int? Ruta { get; set; }

        public int? Cliente { get; set; }

        public ClienteRutaModelo(ClienteRuta rutaCliente)
        {
            this.Cliente = rutaCliente.Cliente;
            this.Ruta = rutaCliente.Ruta;
        }
    }
}