using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HMS.Data.Repository.Repositories;
using HMS.Entities.Models;

namespace HMS.Areas.Dashboard.Controllers
{
    public class UploadController : Controller
    {
        private readonly IRepositoryWrapper _repoWrapper;
        public UploadController(IRepositoryWrapper repowrapper)
        {
            _repoWrapper = repowrapper;

        }
        // GET: Upload
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult ImageUpload()
        {
            JsonResult result = new JsonResult();
            var files = Request.Files;
            var imageList = new List<Image>();
            
            for(int i = 0; i < files.Count; i++)
            {
                var pathToImages = Server.MapPath("~/Images/Upload/");
                var image = files[i];
                var fileName = Guid.NewGuid()+ image.FileName + Path.GetExtension(image.FileName);
                var filePath = Server.MapPath("~/Images/Upload/") + fileName;
                image.SaveAs(filePath);

                var uploadedImage = new Image();
                uploadedImage.ImageUrl = fileName;
                _repoWrapper.Images.Add(uploadedImage);
                imageList.Add(uploadedImage);
                

            }
            _repoWrapper.Images.Save();
            result.Data = imageList;
            return result;
        }
    }
}