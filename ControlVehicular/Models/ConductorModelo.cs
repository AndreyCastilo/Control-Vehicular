﻿using Modelo.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ControlVehicular.Models
{
    public class ConductorModelo
    {
        public long Codigo { get; set; }
        public int? Empresa { get; set; }

        [MaxLength(100)]
        public string Nombre { get; set; }

        [MaxLength(100)]
        public string TipoLicencia { get; set; }

        public string URLFotografiaCedula { get; set; }

        public string URLFotografiaLicencia { get; set; }


        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? FechaVencimiento { get; set; }

        public ConductorModelo(Conductor conductor)
        {
            this.Codigo = conductor.Codigo;
            this.Empresa = conductor.Empresa;
            this.Nombre = conductor.Nombre;
            this.TipoLicencia = conductor.TipoLicencia;
            this.FechaVencimiento = conductor.FechaVencimiento;
            this.URLFotografiaCedula = conductor.URLFotografiaCedula;
            this.URLFotografiaLicencia = conductor.URLFotografiaLicencia;
        }
    }
}