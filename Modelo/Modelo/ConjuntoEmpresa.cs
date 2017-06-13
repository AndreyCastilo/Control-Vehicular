using Modelo.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Modelo
{
    public class ConjuntoEmpresa
    {

        public Empresa Guardar(Empresa empresa)
        {
            

            using (var cnx = Conexion.Open)
            {
                cnx.Empresa.InsertOnSubmit(empresa);
                cnx.SubmitChanges(); //Actualiza la llave primaria sobre el objeto
                return empresa;
            }
        }

        public void Editar(Empresa emp)
        {
            using (var cnx = Conexion.Open)
            {
                var registro = cnx.Empresa.FirstOrDefault(e => e.Codigo == emp.Codigo);
                registro.Cedula = emp.Cedula;
                registro.Fisica = emp.Fisica;
                registro.Nombre = emp.Nombre;
                registro.Telefono = emp.Telefono;
                cnx.SubmitChanges();
            }
        }

        public Empresa Elemento(int codigo)
        {

            return Conexion.Open.Empresa.FirstOrDefault(e => e.Codigo == codigo);

        }
        public void Remover(int codigo)
        {
            using (var cnx = Conexion.Open)
            {
                var obj = cnx.Empresa.FirstOrDefault(e => e.Codigo == codigo);
                cnx.Empresa.DeleteOnSubmit(obj);
                cnx.SubmitChanges();
            }
        }

        public IEnumerable<Empresa> ObtenerTodas() {
            return Conexion.Open.Empresa.OrderBy(em => em.Nombre);
        }

    }
}
