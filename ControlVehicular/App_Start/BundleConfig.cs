﻿using System.Web;
using System.Web.Optimization;

namespace ControlVehicular
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery.scrolly.min.js",
                        "~/Scripts/jquery.scrollex.min.js",
                        "~/Scripts/skel.min.js",
                        "~/Scripts/util.js",
                        "~/Scripts/main.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/bootstrap-theme.css",
                      "~/Content/fileinput.css",
                      "~/Content/main.css",
                      "~/Content/modal_images.css"));
            bundles.Add(new ScriptBundle("~/bundles/unidad").Include(
                       "~/Scripts/unidad.js",
                       "~/Scripts/fileinput.js",
                       "~/Scripts/es.js"));
            bundles.Add(new ScriptBundle("~/bundles/ruta").Include(
                       "~/Scripts/ruta.js"));
        }
    }
}
