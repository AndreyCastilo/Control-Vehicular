using Modelo.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Modelo
{
    public class ConjuntoRuta
    {

        public Ruta Agregar(Ruta ruta)
        {
            using (var conn = Conexion.Open)
            {

                conn.Ruta.InsertOnSubmit(ruta);
                conn.SubmitChanges();
                return ruta;
            }
        }

        public IEnumerable<Ruta> ObtenerTodas(int cod)
        {
            return Conexion.Open.Ruta.Where(e => e.Empresa == cod).OrderBy(em => em.Codigo);
        }

        public Ruta Elemento(int codigo)
        {
            return Conexion.Open.Ruta.FirstOrDefault(e => e.Codigo == codigo);
        }

        public Ruta Editar(Ruta emp)
        {
            using (var cnx = Conexion.Open)
            {
                var registro = cnx.Ruta.FirstOrDefault(e => e.Codigo == emp.Codigo);
                registro.Conductor = emp.Conductor;
                registro.Vehiculo = emp.Vehiculo;
                registro.Nombre = emp.Nombre;
                cnx.SubmitChanges();
                return emp;
            }
        }
    }

    
}
