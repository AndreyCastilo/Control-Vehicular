﻿using Modelo.Database;
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
    
        [Display(Name = "Numero de placa"), MaxLength(100)]
        public string Placa { get; set; }

        [Display(Name = "Año"), MaxLength(100)]
        public string Anno { get; set; }

        [Display(Name = "Ultimo año de revision")]
        public int? UltimoAnnoRevision {get; set;}

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

        [Display(Name = "Unidad")]
        public string URLFotografiaUnidad {get; set;}

        [Display(Name = "Tarjeta de Circulacion")]
        public string URLTarjetaCirculacion { get; set; }

        [Display(Name = "Revision Tecnica")]
        public string URLRevisionTecnica {get; set;}

        public UnidadModelo(Unidad unidad) {
            this.Codigo = unidad.Codigo;
            this.Empresa = unidad.Empresa;
            this.Placa = unidad.Placa;
            this.Anno = unidad.Anno;
            this.UltimoAnnoRevision = unidad.UltimoAnnoRevision;
            this.Marca = unidad.Marca;
            this.Modelo = unidad.Modelo;
            this.Latitud = unidad.Latitud;
            this.Longitud = unidad.Longitud;
            this.Capacidad = unidad.Capacidad;
            this.URLFotografiaUnidad = unidad.URLFotografiaUnidad;
            this.URLTarjetaCirculacion = unidad.URLTarjetaCirculacion;
            this.URLRevisionTecnica = unidad.URLRevisionTecnica;
        }

        // Fotos
        public HttpPostedFileBase foto1 { get; set; }
    }
}