using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Hosting;

namespace moonshineAngular.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class imageUploadController : Controller
    {
        private IHostingEnvironment _hostingEnvironment;
        private string fPath = "";
        public imageUploadController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
            SetFullPath(_hostingEnvironment.ContentRootPath + "\\ClientApp\\src\\assets\\PhotoGallery");
        }
        [HttpPost, DisableRequestSizeLimit]
        public ActionResult UploadFile()
        {
            try
            {
                var file = Request.Form.Files[0];
                string folderName = "PhotoGallery";
                string photoPath = _hostingEnvironment.ContentRootPath + "\\ClientApp\\src\\assets";
                string newPath = Path.Combine(photoPath, folderName);
                
                if (!Directory.Exists(newPath))
                {
                    Directory.CreateDirectory(newPath);
                }
                if (file.Length > 0)
                {
                    string fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    string fullPath = Path.Combine(newPath, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                }
                return Json("Upload Successful.");
            }
            catch (System.Exception ex)
            {
                return Json("Upload Failed: " + ex.Message);
            }
        }

        [HttpGet, DisableRequestSizeLimit]
        public List<string> GetImagePaths()
        {
            try
            {
                List<string> paths = new List<string>();
                string startRelPath = "assets";
                foreach (var path in Directory.GetFiles(fPath))
                {
                    int relPathStartIndex = path.IndexOf(startRelPath);
                    string newPath = path.Substring(relPathStartIndex);
                    paths.Add(newPath);
                }
               return paths;
            }
            catch(Exception e)
            {
                Console.WriteLine("Fail");
                return null;
            }
        }

        private void SetFullPath(string path)
        {
            this.fPath = path;
        }

    }
}