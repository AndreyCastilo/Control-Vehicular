using Modelo.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Modelo
{
    public class ConjuntoSeguro
    {
        public Seguro Guardar(Seguro seguro)
        {


            using (var cnx = Conexion.Open)
            {
                cnx.Seguro.InsertOnSubmit(seguro);
                cnx.SubmitChanges(); //Actualiza la llave primaria sobre el objeto
                return Elemento(seguro.Codigo);
            }
        }

        public Seguro Editar(Seguro seg)
        {
            using (var cnx = Conexion.Open)
            {
                var registro = cnx.Seguro.FirstOrDefault(s => s.Codigo == seg.Codigo);
                registro.Empresa = seg.Empresa;
                registro.Nombre = seg.Nombre;
                registro.Tipo = seg.Tipo;
                registro.Detalle = seg.Detalle;
                cnx.SubmitChanges();
                return Elemento(seg.Codigo);
            }
        }

        public Seguro Elemento(int codigo)
        {

            return Conexion.Open.Seguro.FirstOrDefault(e => e.Codigo == codigo);

        }
        public void Remover(int codigo)
        {
            using (var cnx = Conexion.Open)
            {
                var obj = cnx.Seguro.FirstOrDefault(e => e.Codigo == codigo);
                cnx.Seguro.DeleteOnSubmit(obj);
                cnx.SubmitChanges();
            }
        }

        public IEnumerable<Seguro> ObtenerTodas()
        {
            return Conexion.Open.Seguro.OrderBy(seg => seg.Nombre);
        }

    }
}
