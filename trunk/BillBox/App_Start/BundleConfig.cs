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



            bundles.Add(new StyleBundle("~/Assets/neon.css").Include(
                "~/Assets/js/jquery-ui/css/no-theme/jquery-ui-1.10.3.custom.min.css",
                "~/Assets/css/font-icons/entypo/css/entypo.css",
                "~/Assets/css/bootstrap.min.css",
                "~/Assets/css/neon-core.min.css",
                "~/Assets/css/neon-theme.min.css",
                "~/Assets/css/neon-forms.min.css",
                "~/Assets/css/custom.css",
                "~/Assets/css/skins/red.css",
                "~/Assets/js/jvectormap/jquery-jvectormap-1.2.2.css",
                "~/Assets/js/rickshaw/rickshaw.min.css"));

            bundles.Add(new ScriptBundle("~/Assets/neon.js").Include(
                "~/Assets/js/gsap/main-gsap.js",
                "~/Assets/js/bootstrap.js",
                "~/Assets/js/joinable.js",
                "~/Assets/js/resizeable.js",
                "~/Assets/js/neon-api.js",
                "~/Assets/js/cookies.min.js",
                "~/Assets/js/jvectormap/jquery-jvectormap-1.2.2.min.js",
                "~/Assets/js/jvectormap/jquery-jvectormap-europe-merc-en.js",
                "~/Assets/js/jquery.sparkline.min.js",
                "~/Assets/js/rickshaw/vendor/d3.v3.js",
                "~/Assets/js/rickshaw/rickshaw.min.js",
                "~/Assets/js/raphael-min.js",
                "~/Assets/js/morris.min.js",
                "~/Assets/js/toastr.js",
                "~/Assets/js/neon-chat.js",
                "~/Assets/js/neon-custom.js",
                "~/Assets/js/neon-demo.js",
                "~/Assets/js/neon-skins.js"));
        }
    }
}