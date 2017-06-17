using Modelo.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Modelo
{
    public class ConjuntoPadresClientes
    {
        public PadreCliente Agregar(PadreCliente cliente)
        {
            using (var cnx = Conexion.Open)
            {
                cnx.PadreCliente.InsertOnSubmit(cliente);
                cnx.SubmitChanges();
            }
            return Obtener(cliente.Codigo);
        }

        public PadreCliente Editar(PadreCliente cliente)
        {
            using (var cnx = Conexion.Open)
            {
                var clienteAux = cnx.PadreCliente.FirstOrDefault(x => x.Codigo == cliente.Codigo);
                if (clienteAux != null)
                {
                    clienteAux.Empresa = cliente.Empresa;
                    clienteAux.Nombre = cliente.Nombre;
                    clienteAux.MostrarComo = cliente.MostrarComo;
                    clienteAux.Direccion = cliente.Direccion;
                    cnx.SubmitChanges();
                    return Obtener(clienteAux.Codigo);
                }
                else
                {
                    return null;
                }
            }

            

        }

        public IEnumerable<PadreCliente> GetClientes(int codigo)
        {
            return Conexion.Open.PadreCliente.OrderByDescending(x => x.Codigo);
        }

        public Boolean Remover(int codigo)
        {
            using (var cnx = Conexion.Open)
            {
                var obj = cnx.PadreCliente.FirstOrDefault(e => e.Codigo == codigo);
                if (obj != null)
                {
                    cnx.PadreCliente.DeleteOnSubmit(obj);
                    cnx.SubmitChanges();
                    return true;
                }
                else
                {
                    return false;
                }

            }
        }

        public PadreCliente Obtener(int codigo)
        {
            PadreCliente cliente = null;
            cliente = Conexion.Open.PadreCliente.FirstOrDefault(x => x.Codigo == codigo);
            return cliente;
        }
    }
}
