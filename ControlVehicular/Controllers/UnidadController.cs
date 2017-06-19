using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Modelo.Modelo;
using Modelo.Database;
using ControlVehicular.Models;
using System.IO;

namespace ControlVehicular.Controllers
{
    public class UnidadController : Controller
    {
        private readonly string fotografias = "fotografias";
        private readonly string revisionTecnica = "revisionTecnica";
        private readonly string tarjetaCirulacion = "tarjetaCirculacion";

        private ConjuntoUnidad unidades = new ConjuntoUnidad();

        // GET: Unidad
        public ActionResult Index()
        {
            return View();
        }

        #region CRUD UNIDAD
        [HttpPost]
        public ActionResult Agregar(Unidad datos, IEnumerable<HttpPostedFileBase> fotos)
        {
            guardaFotos(datos, fotos);
            var unidadDB = unidades.Agregar(datos);
            return Json(new { Resultado = true, Unidad = new UnidadModelo(unidadDB) });

        }

        public string getPath(int num, string placa, string fileName)
        {
            switch (num)
            {
                case 1:
                    return Path.Combine(Server.MapPath("/storage/unidades/" + fotografias), placa + "-" + fileName);
                case 2:
                    return Path.Combine(Server.MapPath("/storage/unidades/" + revisionTecnica), placa + "-" + fileName);
                case 3:
                    return Path.Combine(Server.MapPath("/storage/unidades/" + tarjetaCirulacion), placa + "-" + fileName);
                default:
                    return null;
            }
        }

        public JsonResult Elemento(int codigo)
        {
            var registro = unidades.Elemento(codigo);
            var pathFotografia = "../.." + substring(registro.URLFotografiaUnidad, "\\storage", "");
            var pathTarjeta = "../.." + substring(registro.URLTarjetaCirculacion, "\\storage", "");
            var pathRevision = "../.." + substring(registro.URLRevisionTecnica, "\\storage", "");
            return Json(new
            {
                Resultado = true,
                Unidad = new UnidadModelo(registro),
                urlFoto = pathFotografia,
                urlTarjeta = pathTarjeta,
                urlRevision = pathRevision
            },
                JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Editar(Unidad uni, IEnumerable<HttpPostedFileBase> fotos)
        {
            guardaFotos(uni, fotos);

            var cod = unidades.Editar(uni);
            return Json(new { Resultado = true, Unidad = new UnidadModelo(cod) });
        }

        [HttpPost]
        public JsonResult Eliminar(int codigo)
        {
            unidades.Remover(codigo);
            return Json(true);
        }

        #endregion

        #region Metodos
        public JsonResult ObtenerTodas()
        {
            var unidadesDB = unidades.ObtenerTodas().Select(em => new UnidadModelo(em)).ToList();
            return Json(new { registro = unidadesDB }, JsonRequestBehavior.AllowGet);
        }

        public string substring(string original, string start, string end)
        {
            //String s = "<term>extant<definition>still in existence</definition></term>";
            String searchString = start;
            int startIndex = original.IndexOf(searchString);
            searchString = end;
            int endIndex = original.Length;
            String substring = original.Substring(startIndex, endIndex + searchString.Length - startIndex);
            var src = substring.Replace(@"\", "/");
            return src;
        }

        public void guardaFotos(Unidad datos, IEnumerable<HttpPostedFileBase> fotos)
        {
            var path = "";
            int secuencia = 1;
            foreach (var foto in fotos)
            {
                if (foto != null && foto.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(foto.FileName);
                    path = getPath(secuencia, datos.Placa, fileName);
                    foto.SaveAs(path);
                    switch (secuencia)
                    {
                        case 1:
                            datos.URLFotografiaUnidad = path;
                            break;
                        case 2:
                            datos.URLRevisionTecnica = path;
                            break;
                        case 3:
                            datos.URLTarjetaCirculacion = path;
                            break;
                        default:
                            break;
                    }
                }
                secuencia++;
            }
        }

        #endregion

    }
}