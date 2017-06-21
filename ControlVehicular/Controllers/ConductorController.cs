using ControlVehicular.Models;
using Modelo.Database;
using Modelo.Modelo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ControlVehicular.Controllers
{
    public class ConductorController : Controller
    {
        // GET: Pacientes
        public ActionResult EmpresaConductor(int id) // de la empresa
        {
            ViewBag.Empresa = empresas.Elemento(id); //Cabmiar
            return View();
        }
        [HttpGet]
        public JsonResult GetConductoresEmpresa(int id)
        {
            var conductoresLista = conductores.Listar(id).ToList();
            var listaAux = conductoresLista.Select(x => new ConductorModelo(x));
            return Json(new { Registro = listaAux }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Guardar(Conductor conductor, HttpPostedFileBase FileCedulaImg, HttpPostedFileBase FileLicenciaImg)
        {

            var conductorDB = conductores.Agregar(conductor);

            if (FileCedulaImg != null && IsImage(FileCedulaImg)) {

                var fileNameCedula = Path.GetFileName(FileCedulaImg.FileName);
                var pathCedula = Path.Combine(Server.MapPath("~/storage/Conductores"),+conductorDB.Codigo +fileNameCedula);
                FileCedulaImg.SaveAs(pathCedula);
                conductorDB.URLFotografiaCedula = fileNameCedula.ToString();
            }

            if (FileLicenciaImg != null && IsImage(FileLicenciaImg) ) {

                var fileNameLicencia = Path.GetFileName(FileLicenciaImg.FileName);
                var pathLicencia = Path.Combine(Server.MapPath("~/storage/Conductores"), conductorDB.Codigo+fileNameLicencia);
                FileLicenciaImg.SaveAs(pathLicencia);
                conductorDB.URLFotografiaLicencia = fileNameLicencia.ToString();
            } 

            Conductor conductorAux = conductores.Editar(conductorDB); ;
            if (conductorAux != null)
                return Json(new { Resultado = true, Conductor = new ConductorModelo(conductorAux) }, JsonRequestBehavior.AllowGet);
            else
                return Json(new { Resultado = false }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult Editar(Conductor conductor, HttpPostedFileBase FileCedulaImgEditar, HttpPostedFileBase FileLicenciaImgEditar)
        {
            var conductorOriginal = conductores.Obtener(conductor.Codigo);

            if (FileCedulaImgEditar != null && IsImage(FileCedulaImgEditar))
            {
                var fileNameCedulaAnterior = conductorOriginal.Codigo + conductorOriginal.URLFotografiaCedula;
                if (fileNameCedulaAnterior.Length > 0)
                {
                    String fullpath = Path.Combine(Server.MapPath("~/storage/Conductores"), fileNameCedulaAnterior);
                    DeleteImagenPath(fullpath);
                }
                var fileNameCedula = Path.GetFileName(FileCedulaImgEditar.FileName);
                var pathCedula = Path.Combine(Server.MapPath("~/storage/Conductores"), conductorOriginal.Codigo + fileNameCedula);
                FileCedulaImgEditar.SaveAs(pathCedula);
                conductor.URLFotografiaCedula = fileNameCedula.ToString();
            }
            else {
                conductor.URLFotografiaCedula = conductorOriginal.URLFotografiaCedula;
            }


            if (FileLicenciaImgEditar != null && IsImage(FileLicenciaImgEditar) )
            {
                var fileNameLicenciaAnterior = conductorOriginal.Codigo + conductorOriginal.URLFotografiaLicencia;
                if (fileNameLicenciaAnterior.Length > 0)
                {
                    String fullpath = Path.Combine(Server.MapPath("~/storage/Conductores"),fileNameLicenciaAnterior);
                    DeleteImagenPath(fullpath);
                }
                var fileNameLicencia = Path.GetFileName(FileLicenciaImgEditar.FileName);
                var pathLicencia = Path.Combine(Server.MapPath("~/storage/Conductores"), conductorOriginal.Codigo + fileNameLicencia );
                FileLicenciaImgEditar.SaveAs(pathLicencia);
                conductor.URLFotografiaLicencia = fileNameLicencia.ToString();
            }
            else {
                conductor.URLFotografiaLicencia = conductorOriginal.URLFotografiaLicencia;

            }
            Conductor conductorDB = conductores.Editar(conductor);

            if (conductorDB != null)
                return Json(new { Resultado = true, Conductor = new ConductorModelo(conductorDB) }, JsonRequestBehavior.AllowGet);
            else
                return Json(new { Resultado = false }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult Remover(int codigo)
        {
            var conductorOriginal = conductores.Obtener(codigo);
            var pathCedula= Path.Combine(Server.MapPath("~/storage/Conductores"), codigo+ conductorOriginal.URLFotografiaCedula);
            var pathLicencia= Path.Combine(Server.MapPath("~/storage/Conductores"), codigo + conductorOriginal.URLFotografiaLicencia);
            DeleteImagenPath(pathCedula);
            DeleteImagenPath(pathLicencia);
            return Json(new { Resultado = conductores.Remover(codigo) });
        }


        [HttpGet]
        public JsonResult Obtener(int codigo)
        {
            Conductor registro = conductores.Obtener(codigo);
            if (registro != null) {

                var PathCompletoCedula= Path.Combine(Server.MapPath("/storage/Conductores/" + registro.Codigo + registro.URLFotografiaCedula));
                var PathCompletoLicencia = Path.Combine(Server.MapPath("/storage/Conductores/" + registro.Codigo + registro.URLFotografiaLicencia));

                var pathFotoCedula = "../../.." + substring(PathCompletoCedula, "\\storage", "");
                var pathFotoLicencia= "../../.." + substring(PathCompletoLicencia, "\\storage", "");
                return Json(new {
                    Resultado = true,
                    Conductor = new ConductorModelo(registro),
                    urlCedula = pathFotoCedula,
                    urlLicencia = pathFotoLicencia
                }, JsonRequestBehavior.AllowGet);
            }


             
            else
                return Json(new { Resultado = false }, JsonRequestBehavior.AllowGet);
        }


        private void DeleteImagenPath(String path)
        {
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }

        }

        private bool IsImage(HttpPostedFileBase file)
        {
            if (file.ContentType.Contains("image"))
            {
                return true;
            }
            string[] formats = new string[] { ".jpg", ".png", ".gif", ".jpeg" };
            foreach (var item in formats)
            {
                if (file.FileName.Contains(item))
                {
                    return true;
                }
            }
            return false;
        }

        private string substring(string original, string start, string end)
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

        private ConjuntoConductor conductores = new ConjuntoConductor();
        private ConjuntoEmpresa empresas = new ConjuntoEmpresa();
    }

}