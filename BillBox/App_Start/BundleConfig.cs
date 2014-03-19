using System.Web;
using System.Web.Optimization;

namespace BillBox
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
						"~/Content/css/reset.css",
						"~/Content/css/site.css"));

            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                        "~/Content/themes/base/jquery.ui.core.css",
                        "~/Content/themes/base/jquery.ui.resizable.css",
                        "~/Content/themes/base/jquery.ui.selectable.css",
                        "~/Content/themes/base/jquery.ui.accordion.css",
                        "~/Content/themes/base/jquery.ui.autocomplete.css",
                        "~/Content/themes/base/jquery.ui.button.css",
                        "~/Content/themes/base/jquery.ui.dialog.css",
                        "~/Content/themes/base/jquery.ui.slider.css",
                        "~/Content/themes/base/jquery.ui.tabs.css",
                        "~/Content/themes/base/jquery.ui.datepicker.css",
                        "~/Content/themes/base/jquery.ui.progressbar.css",
                        "~/Content/themes/base/jquery.ui.theme.css"));



            bundles.Add(new StyleBundle("~/Assets/css").Include(
                "~/Assets/css/bootstrap.min.css",
                "~/Assets/font-awesome/css/font-awesome.css",
                "~/Assets/css/plugins/morris/morris-0.4.3.min.css",
                "~/Assets/css/plugins/timeline/timeline.css",
                "~/Assets/css/sb-admin.css",
                "~/Assets/css/sb.css"
            ));

            bundles.Add(new ScriptBundle("~/Assets/sb.js").Include(
                "~/Assets/js/bootstrap.min.js",
                "~/Assets/js/plugins/metisMenu/jquery.metisMenu.js",
                "~/Assets/js/plugins/morris/raphael-2.1.0.min.js",
                "~/Assets/js/plugins/morris/morris.js",
                "~/Assets/js/sb-admin.js"
                ));
        }
    }
}