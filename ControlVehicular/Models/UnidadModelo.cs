using Modelo.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ControlVehicular.Models
{
    public class UnidadModelo
    {
        public long Codigo { get; set; }
        public int? Empresa {get; set;}
    
        [MaxLength(100)]
        public string Placa { get; set; }

        [MaxLength(100)]
        public string Ano { get; set; }
        public int? UltimoAnoRevision {get; set;}

        [MaxLength(100)]
        public string Marca { get; set; }

        [MaxLength(100)]
        public string Modelo { get; set; }

        [Required]
        public double Latitud { get; set; }

        [Required]
        public double Longitud{get; set;}
        [Required]
        public int Capacidad {get; set;}

        public string URLFotografiaUnidad {get; set;}
    
        public string URLTarjetaCirculacion { get; set; }
        public string URLRevisionTecnica {get; set;}

        public UnidadModelo(Unidad unidad) {
            this.Codigo = unidad.Codigo;
            this.Empresa = unidad.Empresa;
            this.Placa = unidad.Placa;
            this.Ano = unidad.Ano;
            this.UltimoAnoRevision = unidad.UltimoAnoRevision;
            this.Marca = unidad.Marca;
            this.Modelo = unidad.Modelo;
            this.Latitud = unidad.Latitud;
            this.Longitud = unidad.Longitud;
            this.Capacidad = unidad.Capacidad;
            this.URLFotografiaUnidad = unidad.URLFotografiaUnidad;
            this.URLTarjetaCirculacion = unidad.URLTarjetaCirculacion;
            this.URLRevisionTecnica = unidad.URLRevisionTecnica;
        }
    }
}