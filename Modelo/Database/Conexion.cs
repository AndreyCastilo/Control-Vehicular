using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Database
{
    public static class Conexion
    {
        public static ConexionDataContext Open
        {
            get { return new ConexionDataContext(); }
        }
    }
}
