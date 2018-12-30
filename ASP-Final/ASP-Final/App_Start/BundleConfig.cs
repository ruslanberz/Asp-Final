using System.Web;
using System.Web.Optimization;

namespace ASP_Final
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            //            "~/Scripts/jquery-{version}.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            //            "~/Scripts/jquery.validate*"));

            //// Use the development version of Modernizr to develop with and learn from. Then, when you're
            //// ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            //            "~/Scripts/modernizr-*"));

            //bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
            //          "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Public/css").Include(
                      "~/Public/bootstrap-formhelpers.css",
                      "~/Public/bootstrap-formhelpers.min.css",
                      "~/Public/bootstrap.min.css",
                      "~/Public/font-awesome.min.css",
                      "~/Public/magnific-popup.css",
                      "~/Public/owl.carousel.min.css",
                      "~/Public/owl.theme.default.min.css",
                      "~/Public/set1.css",
                      "~/Public/simple-line-icons.css",
                      "~/Public/style.css",
                      "~/Public/swiper.min.css",
                      "~/Public/themify-icons.css"
                      ));
        }
    }
}
