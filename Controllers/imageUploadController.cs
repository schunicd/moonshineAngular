using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Hosting;
using TheMoonshineCafe.Services;
using Microsoft.Extensions.DependencyInjection;
using Amazon.S3;
using Amazon.S3.Model;

namespace moonshineAngular.Controllers
{

    [ApiController]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class imageUploadController : Controller
    {

        private readonly IS3Service _service;

        
        public imageUploadController(IS3Service service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("CreateBucket/{bucketName}")]
        public async Task<IActionResult> CreateBucket([FromRoute] string bucketName) 
        {
            var response = await _service.CreateBucketAsync(bucketName);
            return Ok(response);
        }

        [HttpPost]
        [Route("AddFile/{albumName}")]
        public async Task<IActionResult> AddFile([FromRoute] string albumName)
        {
            var file = Request.Form.Files[0];

            Console.WriteLine(file);

            var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');

            Console.WriteLine(fileName);

            await _service.UploadFileAsync(file, albumName);

            return Ok();
        }

        [HttpPost]
        [Route("AddBandImage")]
        public async Task<IActionResult> AddBandImage()
        {
            var file = Request.Form.Files[0];

            Console.WriteLine(file);

            var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');

            Console.WriteLine(fileName);

            await _service.UploadBandImageAsync(file);

            return Ok();
        }

        [HttpGet]
        [Route("GetPhotos")]
        public List<string> GetPhotos() 
        {
            List<string> photos = new List<string>();
            //Console.WriteLine("IMAGE UPLOAD CONTROLLER");
            //photos = _service.GetPhotos();
            //Console.WriteLine(_service.GetPhotos().Result.Count);
            return _service.GetPhotos().Result;
        }

        [HttpGet]
        [Route("GetAlbums")]
        public List<string> GetAlbums()
        {
            Console.WriteLine("GET ALBUMS");
            Console.WriteLine(_service.GetAlbums().Result.Count);
            return _service.GetAlbums().Result;
        }

    }

}