//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using PdfProcessingApp.Services;
//using PdfProcessingApp.Models;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Threading.Tasks;

//namespace PdfProcessingApp.Presentation.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class PdfController : ControllerBase
//    {
//        private readonly PdfService _pdfService;

//        public PdfController(PdfService pdfService)
//        {
//            _pdfService = pdfService;
//        }


//        [HttpPost("upload")]
//        public async Task<IActionResult> UploadPdf(IFormFile pdfFile)
//        {
//            if (pdfFile == null || pdfFile.Length == 0)
//            {
//                return BadRequest("No file uploaded.");
//            }

//            var filePath = Path.Combine(Path.GetTempPath(), pdfFile.FileName);
//            using (var stream = new FileStream(filePath, FileMode.Create))
//            {
//                await pdfFile.CopyToAsync(stream);
//            }

//            return Ok(new { FilePath = filePath });
//        }


//        [HttpPost("process")]
//        public IActionResult ProcessPdf([FromForm] string filePath, [FromForm] string fileName, [FromForm] long fileSize, [FromForm] DateTime createdDate, [FromForm] List<DocumentSection> sections)
//        {
//            if (string.IsNullOrEmpty(filePath))
//            {
//                return BadRequest("File path is required.");
//            }

//            var pdfDocument = new PdfDocument
//            {
//                FileName = fileName,
//                FileSize = fileSize,
//                CreatedDate = createdDate,
//                Sections = sections
//            };

//            var result = _pdfService.ProcessPdf(pdfDocument, filePath);

//            // Optionally delete the temporary file
//            if (System.IO.File.Exists(filePath))
//            {
//                System.IO.File.Delete(filePath);
//            }

//            return Ok(result);
//        }
//    }
//}
