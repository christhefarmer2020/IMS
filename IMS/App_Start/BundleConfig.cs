﻿using System.Web;
using System.Web.Optimization;

namespace IMS
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));
            bundles.Add(new ScriptBundle("~/bundles/mydatatables").Include(
                        "~/Scripts/mydatatablescript.js"));

            bundles.Add(new ScriptBundle("~/bundles/dataTables").Include(
                        "~/Scripts/DataTables/jquery.dataTables.min.js", "~/Scripts/flatpickr.js","~/Scripts/DataTables/buttons.colVis.min.js",
                        "~/Scripts/DataTables/dataTables.buttons.min.js"));

            bundles.Add(new StyleBundle("~/bundles/dataTablesStyles").Include(
                        "~/Content/DataTables/css/jquery.dataTables.css", "~/Content/flatpickr.min.css", "~/Content/DataTables/css/buttons.dataTables.min.css"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
        }
    }
}
