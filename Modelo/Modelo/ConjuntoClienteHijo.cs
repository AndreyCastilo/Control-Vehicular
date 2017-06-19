using Modelo.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Modelo
{
    public class ConjuntoClienteHijo
    {
        public ClienteHijo Agregar(ClienteHijo clienteHijo)
        {
            using (var cnx = Conexion.Open)
            {
                cnx.ClienteHijo.InsertOnSubmit(clienteHijo);
                cnx.SubmitChanges();
            }
            return clienteHijo;
        }

        public IEnumerable<ClienteHijo> Listar(int padreCliente)
        {

            return Conexion.Open.ClienteHijo.Where(x => x.PadreCliente == padreCliente).
                OrderBy(e => e.Nombre).
                ThenBy(e => e.MostrarComo);
        }

        public Boolean Remover(int codigo)
        {
            using (var cnx = Conexion.Open)
            {
                var obj = cnx.ClienteHijo.FirstOrDefault(e => e.Codigo == codigo);
                if (obj != null)
                {
                    cnx.ClienteHijo.DeleteOnSubmit(obj);
                    cnx.SubmitChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public ClienteHijo Editar(ClienteHijo clienteHijo)
        {
            using (var cnx = Conexion.Open)
            {
                var clienteHijoAux = cnx.ClienteHijo.FirstOrDefault(x => x.Codigo == clienteHijo.Codigo);
                if (clienteHijoAux != null)
                {
                    clienteHijoAux.Nombre = clienteHijo.Nombre;
                    clienteHijoAux.MostrarComo = clienteHijo.MostrarComo;
                    cnx.SubmitChanges();
                    return Obtener(clienteHijoAux.Codigo);
                }
                else
                {
                    return null;
                }
            }
        }

        public ClienteHijo Obtener(int codigo)
        {
            ClienteHijo clienteHijo = null;
            clienteHijo = Conexion.Open.ClienteHijo.FirstOrDefault(x => x.Codigo == codigo);
            return clienteHijo;
        }
    }
}
