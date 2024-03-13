using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Models;
using Server.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {

        // private readonly string UploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "Uploads"); 
        // private static string parentDirectory = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory); // Get parent directory of application
        private static string parentDirectory = Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).ToString(); // Get parent directory of application

        private static string relativePath = "clientCampaign/src/assets/Uploads"; // Modify this to your desired relative path
        // string uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), relativePath);
        private readonly string UploadFolder = Path.Combine(parentDirectory, relativePath);

        public ImageController(IHttpContextAccessor httpContextAccessor)
        {
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("File is empty");
            }

            if (!Directory.Exists(UploadFolder))
            {
                Directory.CreateDirectory(UploadFolder);
            }

            var filePath = Path.Combine(UploadFolder, file.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return Ok(new { message = "File uploaded successfully", fileName = file.FileName });
        }

        [HttpGet("files")]
        public IActionResult GetFiles()
        {
            if (!Directory.Exists(UploadFolder))
            {
                return NotFound("Upload directory not found");
            }

            var files = Directory.GetFiles(UploadFolder);
            var fileNames = files.Select(file => Path.GetFileName(file)).ToList();
            System.Console.WriteLine(fileNames);
            return Ok(fileNames);
        }


        [HttpDelete("{fileName}")]
        public IActionResult DeleteFile(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return BadRequest("File name is empty");
            }

            var filePath = Path.Combine(UploadFolder, fileName);

            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
                return Ok(new { message = "File deleted successfully", fileName = fileName });
            }
            else
            {
                return NotFound();
            }
        }
    }
}
