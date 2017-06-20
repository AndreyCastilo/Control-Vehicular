﻿using Modelo.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Modelo
{
    public class ConjuntoClienteRuta
    {
        public ClienteRuta ObtenerRutaCliente(int codigo) {
            ClienteRuta clienteRuta = null;
            clienteRuta = Conexion.Open.ClienteRuta.FirstOrDefault(x => x.ClienteHijo == codigo);
            return clienteRuta;
        }

        public ClienteRuta Obtener(int codigoClienteHijo, int codigoRuta )
        {
            ClienteRuta clienteRuta = null;
            clienteRuta = Conexion.Open.ClienteRuta.FirstOrDefault(x => x.ClienteHijo == codigoClienteHijo && x.Ruta==codigoRuta);
            return clienteRuta;
        }

        public ClienteRuta Agregar (ClienteRuta clienteRuta) {
            using (var cnx = Conexion.Open)
            {
                cnx.ClienteRuta.InsertOnSubmit(clienteRuta);
                cnx.SubmitChanges();
            }
            return clienteRuta;
        }

        public IEnumerable<ClienteRuta> Listar(int codigoEmpresa) {
            return Conexion.Open.ClienteRuta.Where(x => x.Ruta1.Empresa == codigoEmpresa);

        }
    }
}
