using ElFinder;
using Eagle.Core;
using Eagle.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eagle.Model.SYS;

namespace Eagle.WebApp.Areas.Admin.Controllers.FileManager
{
    public class FilesController : BaseController
    {
        //
        // GET: /Admin/File/
        private Connector _connector;

        //public Connector Connector
        //{
        //    get
        //    {
        //        if (_connector == null)
        //        {
        //            FileSystemDriver driver = new FileSystemDriver();
        //            DirectoryInfo thumbsStorage = new DirectoryInfo(Server.MapPath("~/Files")); ;
        //            driver.AddRoot(new Root(new DirectoryInfo(Server.MapPath("~/Files")), "/Files/")
        //            {
        //                IsLocked = false,
        //                IsReadOnly = false,
        //                IsShowOnly = false,
        //                Alias = "Root",
        //                StartPath = new DirectoryInfo(Server.MapPath("~/Files")),
        //                ThumbnailsStorage = thumbsStorage,
        //                MaxUploadSizeInMb = 2.2,
        //                ThumbnailsUrl = "Thumbnails/"
        //            });
        //            _connector = new Connector(driver);
        //        }
        //        return _connector;
        //    }
        //}
        public ActionResult Index(string folder, string subFolder)
        {
            string urlFolderPath = "/Files/";
            string physicalFolderPath = Server.MapPath("~/Files");
            if (!string.IsNullOrEmpty(folder))
            {
                urlFolderPath += folder;
                physicalFolderPath = Path.Combine(physicalFolderPath, folder);
            }              
      
            if (_connector == null)
            {                
                if (Directory.Exists(physicalFolderPath))
                {  
                    DirectoryInfo thumbsStorage = new DirectoryInfo(physicalFolderPath); 
                    var root = new Root(new DirectoryInfo(physicalFolderPath), urlFolderPath)
                    {
                        IsLocked = false,
                        IsReadOnly = false,
                        IsShowOnly = false,
                        Alias = "Root",
                        //StartPath = new DirectoryInfo(physicalFolderPath),
                        MaxUploadSizeInMb = 29,
                        ThumbnailsStorage = thumbsStorage,
                        ThumbnailsUrl = "Thumbnails/"
                    };

                    // Was a subfolder selected in Home Index page?
                    if (!string.IsNullOrEmpty(subFolder))               
                        physicalFolderPath = Path.Combine(physicalFolderPath, subFolder);

                    root.StartPath = new DirectoryInfo(physicalFolderPath);
                    FileSystemDriver driver = new FileSystemDriver();
                    driver.AddRoot(root);
                    _connector = new Connector(driver);
                }
            }
            return _connector.Process(this.HttpContext.Request);
        }

        //public ActionResult InvokeFileExplorer()
        //{
        //    //FileViewModel model = new FileViewModel() { Folder = Folder, SubFolder = SubFolder };
        //   // return Json(Connector.Process(this.HttpContext.Request),JsonRequestBehavior.AllowGet);
        //    return Connector.Process(this.HttpContext.Request);
        //}

        public ActionResult SelectFile(string target)
        {
            FileSystemDriver driver = new FileSystemDriver();

            driver.AddRoot(
                new Root(
                    new DirectoryInfo(Server.MapPath("~/Files")),
                    "http://" + Request.Url.Authority + "/Files") { IsReadOnly = false });

            var connector = new Connector(driver);

            return Json(connector.GetFileByHash(target).FullName);
        }

        public ActionResult Thumbs(string tmb)
        {
            FileSystemDriver driver = new FileSystemDriver();

            driver.AddRoot(
                new Root(
                    new DirectoryInfo(Server.MapPath("~/Files")),
                    "http://" + Request.Url.Authority + "/Files") { IsReadOnly = false });
            var connector = new Connector(driver);
            return connector.GetThumbnail(Request, Response, tmb);
        }        

        //[GET("FileManager/{subFolder?}")]
        //public virtual ActionResult Files(string subFolder)
        //{        // FileViewModel contains the root MyFolder and the selected subfolder if any
        //    FileViewModel model = new FileViewModel() { Folder = "documents", SubFolder = subFolder };

        //    return View("../FileManager/Index",model);
        //}

        //public virtual ActionResult Index(string folder, string subFolder)
        //{
        //    FileViewModel model = new FileViewModel() { Folder = folder, SubFolder = subFolder };

        //    FileSystemDriver driver = new FileSystemDriver();
        //    string physicalPath = Server.MapPath("~/Files/" + folder);
        //    var root = new Root(
        //            new DirectoryInfo(physicalPath),
        //            "http://" + Request.Url.Authority + "/Files/" + folder)
        //    {
        //        // Sample using ASP.NET built in Membership functionality...
        //        // Only the super user can READ (download files) & WRITE (create folders/files/upload files).
        //        // Other users can only READ (download files)
        //        // IsReadOnly = !User.IsInRole(AccountController.SuperUser)

        //        IsReadOnly = false, // Can be readonly according to user's membership permission
        //        Alias = "Files", // Beautiful name given to the root/home folder
        //        MaxUploadSizeInKb = 50000, // Limit imposed to user uploaded file <= 50000 KB
        //        LockedFolders = new List<string>(new string[] { "Folder1" })
        //    };

        //    // Was a subfolder selected in Home Index page?
        //    if (!string.IsNullOrEmpty(subFolder))
        //    {
        //        root.StartPath = new DirectoryInfo(Path.Combine(physicalPath, subFolder));
        //    }

        //    driver.AddRoot(root);

        //    var connector = new Connector(driver);
        //    connector.Process(this.HttpContext.Request);
        //    ///return connector.Process(this.HttpContext.Request);

        //    return View("../FileManager/Index", model);
        //}

        //public virtual ActionResult SelectFile(string target)
        //{
        //    FileSystemDriver driver = new FileSystemDriver();

        //    driver.AddRoot(
        //        new Root(
        //            new DirectoryInfo(Server.MapPath("~/Files")),
        //            "http://" + Request.Url.Authority + "/Files") { IsReadOnly = false });

        //    var connector = new Connector(driver);

        //    return Json(connector.GetFileByHash(target).FullName);
        //}

        
        //[AcceptVerbs(HttpVerbs.Get)]
        //public ActionResult Index(string folder, string subFolder)
        //{
        //    //FileViewModel model = new FileViewModel() { Folder = "documents", SubFolder = subFolder };
        //    return View("../FileManager/Index", model);
        //}
    }
}
