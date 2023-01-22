using System.Web.Optimization;

namespace ProjectPathfinder
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/bundles/adminTest/styles")
                .Include("~/assets/styles/bootstrap.css")
                .Include("~/assets/styles/main.css"));

            bundles.Add(new StyleBundle("~/bundles/styles")
                .Include("~/assets/styles/bootstrap.css")
                .Include("~/assets/styles/main.css")
                .Include("~/assets/styles/faq.css")
                .Include("~/assets/styles/invoice.css"));

            bundles.Add(new ScriptBundle("~/bundles/adminTest/scripts")
                .Include("~/scripts/jquery-2.1.4.js")
                .Include("~/scripts/jquery.validate.js")
                .Include("~/scripts/jquery.validate.unobtrusive.js")
                .Include("~/scripts/bootstrap.js")
                .Include("~/scripts/main-menu.js")
                .Include("~/scripts/googleAnalytics.js")
                .Include("~/scripts/admin.js"));

            bundles.Add(new ScriptBundle("~/bundles/scripts")
                .Include("~/scripts/jquery-2.1.4.js")
                .Include("~/scripts/jquery.validate.js")
                .Include("~/scripts/jquery.validate.unobtrusive.js")
                .Include("~/scripts/bootstrap.js")
                .Include("~/scripts/jquery.bxslider.js")
                .Include("~/scripts/jquery.fitvids.js")
                .Include("~/scripts/jquery.sequence.js")
                .Include("~/scripts/main-menu.js")
                .Include("~/scripts/template.js")
                .Include("~/scripts/validator.js")
                .Include("~/scripts/testForm.js")
                .Include("~/scripts/googleAnalytics.js"));
        }
    }
}