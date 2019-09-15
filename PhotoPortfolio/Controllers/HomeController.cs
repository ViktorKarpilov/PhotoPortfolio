using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PhotoPortfolio.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using System.IO.MemoryMappedFiles;
using System.Drawing;
using System.Text;
using Microsoft.Extensions.Logging;



namespace PhotoPortfolio.Controllers
{
    public class HomeController : Controller
    {
        public PhotoCollectionContext photoContext;
        public VideoCollectionContext videoContext;

        private readonly ILogger _logger;

        private readonly IHostingEnvironment _hostingEnvironment;


        public HomeController(IHostingEnvironment env,PhotoCollectionContext PhotoDb,VideoCollectionContext videoDb, ILogger<HomeController> logger)
        {
            _logger = logger;
            _hostingEnvironment =     env;
            photoContext        = PhotoDb;
            videoContext        = videoDb;
        }
        /*[Route("/test")]
        public IActionResult Test()
        {
            return View();
        }*/
           
        [Route("/admin")]
        public  IActionResult AdminPanel()
        {
            IEnumerable<PhotoCollectionModel> collection =  photoContext.PhotoCollections;
            return View(collection);
        }

        public IActionResult Index()
        { 
            return View();
        }
        [Route("/addPhoto")]
        public IActionResult PhotoAdding()
        {
            return View();
        }

        [Route("/addVideo")]
        public IActionResult VideoAdding()
        {
            return View();
        }

        [Route("/addVideo/compile")]
        public StatusCodeResult FileCompile(string name,int lenth)
        {
            using(var file = new FileStream($"{_hostingEnvironment.WebRootPath + "\\libruary\\" + name}", FileMode.Create))
            {
                for (int i = 0; i < lenth; i++)
                {
                    using (var fileToAppend = new FileStream($"{_hostingEnvironment.WebRootPath + "\\libruary\\" + i + name}", FileMode.Open))
                    {
                        fileToAppend.CopyTo(file);
                    }
                
                }
            }
            //videoContext.VideoCollections.AddAsync()
            return Ok();
        }

        [Route("/addVideo/post")]
        public async Task<JsonResult> FileFromForm(string name, string id, IFormFile data)
        {
            _logger.LogDebug($"File data received:\n{data.ContentDisposition}");

            var fileName = $"{_hostingEnvironment.WebRootPath + "\\libruary\\"+Convert.ToString(id)+name}";
            using (var file = new FileStream(fileName, FileMode.Create))
            {
                await data.CopyToAsync(file);
            }
            
            return Json(new { Result = "Ok", Path = fileName, Number = id });
        }

        [HttpPost("UploadPhoto")]
        public async Task<IActionResult> ImagePost(PhotoPostModel photoPost)
        {
            //TODO Repair photo formats
            PhotoCollectionModel model = photoContext.PhotoCollections.Where(s => s.Name == photoPost.CollectionName).FirstOrDefault();
            if (model == null)
            {
                model = new PhotoCollectionModel(photoPost.CollectionName);
                await photoContext.PhotoCollections.AddAsync(model);
            }
            

            FileStream file = System.IO.File.Create(_hostingEnvironment.WebRootPath + "\\photos\\" + photoPost.ImageName);
            photoPost.Image.CopyTo(file);
            file.Close();

            model.AddPhoto(_hostingEnvironment.WebRootPath + "\\photos\\" + photoPost.ImageName , photoPost.ImageName );
            //photoContext.PhotoCollections.Update(model);
            await photoContext.SaveChangesAsync();
            
            return Content("Done");
        }

        public IActionResult DeleteImage(string button)
        {
            //button = photoCollectionName_indexPhotoForDeleting
            string[] DeletingPair = new string[2];
            DeletingPair = button.Split(",").ToArray();

            photoContext.PhotoCollections.Where(s => s.Name == DeletingPair[0]).FirstOrDefault().DeletePhoto(Convert.ToInt32(DeletingPair[1]));
            photoContext.SaveChanges();
            return RedirectToAction("AdminPanel");
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
