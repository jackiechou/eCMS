using System;
using System.Web;
using System.Web.Optimization;

namespace Eagle.WebApp
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            //bundles.IgnoreList.Clear();
            ConfigureIgnoreList(bundles.IgnoreList);

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery-migrate-1.2.1.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                       "~/Scripts/bootstrap.js"
                       ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));




            #region elFinder bundles
            bundles.Add(new StyleBundle("~/Content/elfinder").Include(
                            "~/Scripts/plugins/elfinder/css/elfinder.full.css",
                            "~/Scripts/plugins/elfinder/css/theme.css"
                            ));

            bundles.Add(new ScriptBundle("~/Scripts/elfinder").Include(
                             "~/Scripts/plugins/elfinder/js/elfinder.full.js"
                //"~/Content/elfinder/js/i18n/elfinder.pt_BR.js"
                             ));
            #endregion

            #region desktop header css and javacript ==============================
            bundles.Add(new StyleBundle("~/Themes/Default/DesktopStyle").Include(
               "~/Content/themes/base/jquery-ui.css",
               "~/Themes/Default/Content/css/style_desktop.css",
                "~/Themes/Default/Content/js/bootstrap/dist/css/bootstrap.css",
                "~/Themes/Default/Content/js/bootstrap/dist/css/bootstrap-theme.css",
                 "~/Themes/Default/Content/js/bootstrap/dist/css/font-awesome.css",
                 
                 "~/Scripts/app/plugins/superfish/css/megafish.css",
                 "~/Scripts/app/plugins/superfish/css/superfish.css",
                 "~/Scripts/app/plugins/superfish/css/superfish-vertical.css",
                 "~/Scripts/app/plugins/superfish/css/superfish-navbar.css",
                 "~/Scripts/app/plugins/tagsinput/jquery.tagsinput.css"
              ));

            bundles.Add(new ScriptBundle("~/Themes/Default/DesktopScript").Include(
               "~/Themes/Default/Content/js/jquery-1.11.1.js",
               "~/Themes/Default/Content/js/bootstrap/dist/js/bootstrap.js",
                //"~/Themes/Default/Content/js/bootstrap/dist/js/moment.js",
               //"~/Themes/Default/Content/js/bootstrap-datepicker.js",
               //"~/Themes/Default/Content/js/bootstrap-datetimepicker.js",
               "~/Themes/Default/Content/js/datatables/js/jquery.dataTables.js",
               "~/Themes/Default/Content/js/json2.js",
              "~/Themes/Default/Content/js/utility.js",
              "~/Scripts/app/plugins/superfish/js/superfish.js",
              "~/Scripts/app/plugins/superfish/js/hoverIntent.js",
              "~/Scripts/app/plugins/tagsinput/jquery.tagsinput.js"
           ));
            #endregion ========================================================================== 
    
            #region ADMIN STYLE SHHEET ==========================================================
            //LOGIN STYLE
            bundles.Add(new StyleBundle("~/Themes/Default/LoginStyle").Include(
               "~/Themes/Default/Content/js/bootstrap/dist/css/bootstrap.css",
               "~/Themes/Default/Content/js/bootstrap/dist/css/bootstrap-theme.css",
                "~/Themes/Default/Content/js/bootstrap/dist/css/font-awesome.css",
               "~/Themes/Default/Content/css/login.css"
           ));

            //MAIN LAYOUT TYPE
            bundles.Add(new StyleBundle("~/Themes/Default/TwoColumnAdminStyle").Include(
                "~/Content/themes/base/jquery-ui.css",
                 "~/Themes/Default/Content/js/bootstrap/dist/css/bootstrap.css",
               "~/Themes/Default/Content/js/bootstrap/dist/css/bootstrap-theme.css",
                "~/Themes/Default/Content/js/bootstrap/dist/css/font-awesome.css",
                //"~/Themes/Default/Content/css/bootstrap.css",
                "~/Themes/Default/Content/css/font-awesome.css",
                "~/Themes/Default/Content/css/bootstrap-datetimepicker.css",
                "~/Themes/Default/Content/css/style.css",
                "~/Themes/Default/Content/css/style_main_layout.css",
                "~/Themes/Default/Content/css/style_custom.css",
               "~/Themes/Default/Content/css/select2.css",

                "~/Scripts/app/plugins/superfish/css/megafish.css",
                "~/Scripts/app/plugins/superfish/css/superfish.css",
                "~/Scripts/app/plugins/superfish/css/superfish-vertical.css",
                "~/Scripts/app/plugins/superfish/css/superfish-navbar.css",

                "~/Scripts/app/plugins/bootstrap-select/css/bootstrap-select.css",

                "~/Scripts/app/plugins/qtip/css/jquery.qtip.css",
                "~/Scripts/app/plugins/datatables/css/jquery.dataTables_themeroller.css",
                "~/Scripts/app/plugins/datatables/css/jquery.dataTables.css",
                "~/Scripts/plugins/tablednd/tablednd_custom.css",
                "~/Scripts/app/plugins/easyui/themes/default/easyui.css",
                "~/Scripts/app/plugins/easyui/themes/icon.css",
                "~/Scripts/app/plugins/contextmenu/jquery.contextMenu.css",
                "~/Scripts/app/plugins/tagsinput/jquery.tagsinput.css"
            ));

            //FULL LAYOUT TYPE
            bundles.Add(new StyleBundle("~/Themes/Default/FullScreenAdminStyle").Include(
               "~/Content/themes/base/jquery-ui.css",
                "~/Themes/Default/Content/js/bootstrap/dist/css/bootstrap.css",
               "~/Themes/Default/Content/js/bootstrap/dist/css/bootstrap-theme.css",
                "~/Themes/Default/Content/js/bootstrap/dist/css/font-awesome.css",
                //"~/Themes/Default/Content/css/bootstrap.css",
                "~/Themes/Default/Content/css/font-awesome.css",                
                "~/Themes/Default/Content/css/bootstrap-datetimepicker.css",
                "~/Themes/Default/Content/css/style.css",
                "~/Themes/Default/Content/css/style_full_layout.css",
                "~/Themes/Default/Content/css/style_custom.css",
                 "~/Themes/Default/Content/css/select2.css",

                "~/Scripts/app/plugins/superfish/css/megafish.css",
                "~/Scripts/app/plugins/superfish/css/superfish.css",
                "~/Scripts/app/plugins/superfish/css/superfish-vertical.css",
                "~/Scripts/app/plugins/superfish/css/superfish-navbar.css",

                 "~/Scripts/app/plugins/bootstrap-select/css/bootstrap-select.css",
               // "~/Scripts/app/plugins/datepicker/css/datepicker.css",
                "~/Scripts/app/plugins/qtip/css/jquery.qtip.css",
                "~/Scripts/app/plugins/datatables/css/jquery.dataTables_themeroller.css",
                "~/Scripts/app/plugins/datatables/css/jquery.dataTables.css",
                "~/Scripts/plugins/tablednd/tablednd_custom.css",
                "~/Scripts/app/plugins/easyui/themes/default/easyui.css",
                "~/Scripts/app/plugins/easyui/themes/icon.css",
                "~/Scripts/app/plugins/contextmenu/jquery.contextMenu.css",
                "~/Scripts/app/plugins/tagsinput/jquery.tagsinput.css"
                ));


            //REPORT STYLE
            bundles.Add(new StyleBundle("~/Themes/Default/ReportStyle").Include(
               "~/Content/themes/base/jquery-ui.css",
                "~/Themes/Default/Content/js/bootstrap/dist/css/bootstrap.css",
               "~/Themes/Default/Content/js/bootstrap/dist/css/bootstrap-theme.css",
                "~/Themes/Default/Content/js/bootstrap/dist/css/font-awesome.css",
                //"~/Themes/Default/Content/css/bootstrap.css",
                "~/Themes/Default/Content/css/font-awesome.css",
                "~/Themes/Default/Content/css/bootstrap-datetimepicker.css",
                "~/Themes/Default/Content/css/report.css",
                "~/Themes/Default/Content/css/style_custom.css",
                 "~/Themes/Default/Content/css/select2.css",

                "~/Scripts/app/plugins/superfish/css/megafish.css",
                "~/Scripts/app/plugins/superfish/css/superfish.css",
                "~/Scripts/app/plugins/superfish/css/superfish-vertical.css",
                "~/Scripts/app/plugins/superfish/css/superfish-navbar.css",


                //"~/Scripts/app/plugins/datepicker/css/datepicker.css",
                "~/Scripts/app/plugins/qtip/css/jquery.qtip.css",
                "~/Scripts/app/plugins/datatables/css/jquery.dataTables_themeroller.css",
                "~/Scripts/app/plugins/datatables/css/jquery.dataTables.css",
                "~/Scripts/plugins/tablednd/tablednd_custom.css",
                "~/Scripts/app/plugins/easyui/themes/default/easyui.css",
                "~/Scripts/app/plugins/easyui/themes/icon.css",
                "~/Scripts/app/plugins/contextmenu/jquery.contextMenu.css",
                "~/Scripts/app/plugins/tagsinput/jquery.tagsinput.css"
                ));
            #endregion ======================================================================

            #region Application Script =============================================
            //Header javacript
            bundles.Add(new ScriptBundle("~/Scripts/App/LoginScript").Include(
                "~/Scripts/app/common/jquery-1.11.1.js",
                "~/Themes/Default/Content/js/bootstrap/dist/js/bootstrap.js"
            ));  

            //Header javacript
            bundles.Add(new ScriptBundle("~/Scripts/App/HeaderScript").Include(
                "~/Scripts/app/common/jquery-1.11.1.js",
                "~/Scripts/app/common/jquery-migrate-1.2.1.js",
                "~/Scripts/app/common/jquery-ui-1.10.4.js",
                "~/Scripts/app/common/json2.js",

                "~/Themes/Default/Content/js/bootstrap/dist/js/bootstrap.js",
                //"~/Scripts/app/plugins/bootstrap/bootstrap.js",
                //"~/Scripts/app/plugins/bootstrap/bootstrap-datepicker.js",
                "~/Scripts/app/plugins/bootstrap/bootstrap-datetimepicker.js",
                "~/Scripts/app/plugins/bootstrap/bootstrap-contextmenu.js",

                "~/Scripts/app/plugins/superfish/js/hoverIntent.js",
                "~/Scripts/app/plugins/superfish/js/superfish.js",
                "~/Scripts/app/plugins/superfish/js/supersubs.js",

                "~/Scripts/app/plugins/qtip/js/jquery.qtip.js",
                "~/Scripts/app/plugins/qtip/js/jquery.qtip.custom.js",
                //"~/Scripts/app/plugins/qtip/js/imagesloaded.pkg.js",   

                "~/Scripts/app/plugins/select2/select2.js",
                "~/Scripts/app/plugins/select2/select2custom.js",

                "~/Scripts/app/plugins/bootstrap-select/js/bootstrap-select.js",

                "~/Scripts/app/plugins/datatables/js/jquery.dataTables.js",
                "~/Scripts/plugins/tablednd/js/jquery.tablednd.js",
                "~/Scripts/app/plugins/easyui/jquery.easyui.min.js",
                "~/Scripts/app/plugins/contextmenu/jquery.contextMenu.js",
                "~/Scripts/app/plugins/tagsinput/jquery.tagsinput.js",
                "~/Scripts/app/plugins/monthpicker/jquery.mtz.monthpicker.js",
                "~/Scripts/app/plugins/monthpicker/jquery.mtz.monthpicker.custom.js",     

                "~/Scripts/app/common/common_functions.js",
                "~/Scripts/app/common/main.js",
                "~/Scripts/app/common/utility.js",
                "~/Scripts/app/common/message.js",
                "~/Scripts/app/permission/permision-settings.js",
                "~/Scripts/app/permission/permission-validation.js"
            ));

            //footer javacript
            bundles.Add(new ScriptBundle("~/Scripts/App/FooterScript").Include(
                "~/Scripts/app/common/footer.js"
            ));

            bundles.Add(new ScriptBundle("~/Scripts/App/Validation").Include(
                     "~/Scripts/app/plugins/validation/jquery.validate.js",
                     "~/Scripts/app/plugins/validation/jquery.validate.unobtrusive.js",
                     "~/Scripts/app/plugins/validation/jquery.unobtrusive-ajax.js",
                     "~/Scripts/app/plugins/validation/additional-methods.js"
                     ));
            #endregion =============================================================

    
            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/themes/ui-lightness/css").Include(
                                       "~/Content/themes/ui-lightness/jquery.ui.all.css"));

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
            BundleTable.EnableOptimizations = false;
        }


        public static void ConfigureIgnoreList(IgnoreList ignoreList)
        {
            if (ignoreList == null) throw new ArgumentNullException("ignoreList");

            ignoreList.Clear(); // Clear the list, then add the new patterns.

            ignoreList.Ignore("*.intellisense.js");
            ignoreList.Ignore("*-vsdoc.js");
            ignoreList.Ignore("*.debug.js", OptimizationMode.WhenEnabled);
            // ignoreList.Ignore("*.min.js", OptimizationMode.WhenDisabled);
            ignoreList.Ignore("*.min.css", OptimizationMode.WhenDisabled);
        }
    }
}