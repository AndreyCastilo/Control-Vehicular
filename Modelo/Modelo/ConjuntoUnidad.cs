using Modelo.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Modelo
{
    public class ConjuntoUnidad
    {
        public Unidad Agregar(Unidad unidad)
        {
            using (var conn = Conexion.Open)
            {

                conn.Unidad.InsertOnSubmit(unidad);
                conn.SubmitChanges();
                return unidad;
            }
        }

        public IEnumerable<Unidad> ObtenerTodas()
        {
            return Conexion.Open.Unidad.OrderBy(em => em.Codigo);
        }

        public Unidad Elemento(int codigo)
        {
            return Conexion.Open.Unidad.FirstOrDefault(e => e.Codigo == codigo);
        }

        public Unidad Editar(Unidad emp)
        {
            using (var cnx = Conexion.Open)
            {
                var registro = cnx.Unidad.FirstOrDefault(e => e.Codigo == emp.Codigo);
                registro.Anno = emp.Anno;
                registro.Capacidad = emp.Capacidad;
                registro.Latitud = emp.Latitud;
                registro.Longitud = emp.Longitud;
                registro.Marca = emp.Marca;
                registro.Modelo = emp.Modelo;
                registro.Placa = emp.Placa;
                registro.UltimoAnnoRevision = emp.UltimoAnnoRevision;
                registro.URLFotografiaUnidad = emp.URLFotografiaUnidad;
                registro.URLRevisionTecnica = emp.URLRevisionTecnica;
                registro.URLTarjetaCirculacion = emp.URLTarjetaCirculacion;
                cnx.SubmitChanges();
                return emp;
            }
        }

        public void Remover(int codigo)
        {
            using (var cnx = Conexion.Open)
            {
                var obj = cnx.Unidad.FirstOrDefault(e => e.Codigo == codigo);
                cnx.Unidad.DeleteOnSubmit(obj);
                cnx.SubmitChanges();
            }
        }
    }
}
