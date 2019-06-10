using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace qr.App_Start
{
    public class FileUploaderController : Controller
    {
        // GET: FileUploader
        public ActionResult Upload()
        {
            var myFile = Request.Files["myFile"];
            var targetLocation = Server.MapPath("~/Content/Files/");
            var uniqueFileName = string.Format("{0}_{1}{2}",
                Path.GetFileNameWithoutExtension(myFile.FileName),
                Request["key"],
                Path.GetExtension(myFile.FileName));
            try
            {
                var path = Path.Combine(targetLocation, uniqueFileName);
                myFile.SaveAs(path);
                Session["currentFilePath"] = "/Content/Files/" + uniqueFileName;
            }
            catch
            {
                Response.StatusCode = 400;
            }
            return new EmptyResult();
        }
    }
}