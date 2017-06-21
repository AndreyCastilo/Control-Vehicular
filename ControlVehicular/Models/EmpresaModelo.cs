using Modelo.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ControlVehicular.Models
{
    public class EmpresaModelo
    {
        [Display(Name = "Código")]
        public long Codigo { get; set; }

        [MaxLength(100)]
        public string Nombre { get; set; }

        [Required]
        [Display(Name = "Física")]
        public bool Fisica { get; set; }

        [Display(Name = "Cédula"),MaxLength(10)]
        public string Cedula { get; set; }

        [Display(Name = "Teléfono")MaxLength(10)]
        public string Telefono { get; set; }

        public EmpresaModelo(Empresa empresa) {
            this.Codigo = empresa.Codigo;
            this.Nombre = empresa.Nombre;
            this.Fisica = empresa.Fisica;
            this.Cedula = empresa.Cedula;
            this.Telefono = empresa.Telefono;
        }
    }

  
}