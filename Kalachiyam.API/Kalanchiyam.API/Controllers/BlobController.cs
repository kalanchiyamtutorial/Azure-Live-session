using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Kalanchiyam.API.Service.Model;
using Kalanchiyam.API.Security.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Kalanchiyam.API.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlobController : ControllerBase
    {
        private ILoggerManager logger;
        public BlobController(ILoggerManager _logger)
        {

            logger = _logger;
        }
        [HttpGet("Get")]
        public async Task<IActionResult> Index(string filename)
        {
         
            string bloburi = "csvpath/" + filename + ".csv";
            string blobstring = await BlobClient.DownloadString(bloburi);
            return Ok(blobstring);

        }
        [HttpGet("GetFile")]
        public async Task<IActionResult> GetFile(string filename)
        {

            string bloburi = "csvpath/" + filename + ".csv";
            Stream ms = new MemoryStream();
            ms = await BlobClient.DownloadStream(bloburi);
            ms.Position = 0;
            return File(ms, "application/octet-stream", filename + ".csv");

        }

        [HttpPost("UploadtoBlob")]
        public async Task<IActionResult> UploadtoBlob(IFormFile file)
        {
            logger.Log(NLog.LogLevel.Info, "Index", "Indexupload");
            var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            using (var stream = file.OpenReadStream())
            {
                await BlobClient.UploadStream(stream, "csvpath/" + fileName);
            }
            return Ok();
        }

        [HttpPost("DeleteBlob")]
        public async Task<IActionResult> DeleteBlob(string filename)
        {
            await BlobClient.deletefromBlob("csvpath/" + filename);
            return Ok();
        }            
    }
}
