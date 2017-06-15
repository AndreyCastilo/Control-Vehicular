using Modelo.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Modelo
{
    public class ConjuntoConductor
    {
        public Conductor Agregar(Conductor conductor)
        {
            using (var cnx = Conexion.Open)
            {
                cnx.Conductor.InsertOnSubmit(conductor);
                cnx.SubmitChanges();
            }
            return conductor;
        }

        public IEnumerable<Conductor> Listar(int empresa)
        {
            IEnumerable<Conductor> asaa = Conexion.Open.Conductor;
            List<Conductor> as1 = asaa.ToList();
            return Conexion.Open.Conductor.Where(x => x.Empresa == empresa).
                OrderBy(e => e.Nombre).
                ThenBy(e => e.FechaVencimiento);
        }

        public Boolean Remover(int codigo)
        {
            using (var cnx = Conexion.Open)
            {
                var obj = cnx.Conductor.FirstOrDefault(e => e.Codigo == codigo);
                if (obj != null)
                {
                    cnx.Conductor.DeleteOnSubmit(obj);
                    cnx.SubmitChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public Conductor Editar(Conductor conductor)
        {
            using (var cnx = Conexion.Open)
            {
                var conductorAux = cnx.Conductor.FirstOrDefault(x => x.Codigo == conductor.Codigo);
                if (conductorAux != null)
                {
                    conductorAux.Nombre = conductor.Nombre;
                    conductorAux.Empresa = conductor.Empresa;
                    conductorAux.URLFotografiaCedula = conductor.URLFotografiaCedula;
                    conductorAux.URLFotografiaLicencia = conductor.URLFotografiaLicencia;
                    conductorAux.TipoLicencia = conductor.TipoLicencia;
                    conductorAux.FechaVencimiento = conductor.FechaVencimiento;
                    cnx.SubmitChanges();
                    return conductorAux;
                }
                else
                {
                    return null;
                }
            }
        }

        public Conductor Obtener(int codigo)
        {
            Conductor conductor = null;
            conductor = Conexion.Open.Conductor.FirstOrDefault(x => x.Codigo == codigo);
            return conductor;
        }
    }
}
