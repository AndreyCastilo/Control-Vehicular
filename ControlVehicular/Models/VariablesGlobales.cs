using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ControlVehicular.Models
{
    public class VariablesGlobales
    {
        public static int Codigo
        {
            get
            {
                if (HttpContext.Current.Application[""] == null)
                {
                    return 0;
                }
                else
                {
                    return (int)HttpContext.Current.Application[""];
                }
            }
            set
            {
                HttpContext.Current.Application[""] = value;
            }

        }
    }
}