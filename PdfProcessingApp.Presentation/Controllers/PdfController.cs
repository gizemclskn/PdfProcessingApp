using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PdfProcessingApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using PdfProcessingApp.Business;
using PdfProcessingApp.DAL.Repository.Abstract;
using iText.Kernel.XMP.Impl.XPath;
using System.IO.Compression;

namespace PdfProcessingApp.Presentation.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class PdfController : ControllerBase
    {
        private readonly PdfManager _pdfManager;

        public PdfController(PdfManager pdfManager)
        {
            _pdfManager = pdfManager;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadPdf(IFormFile pdfFile, [FromForm] string outputDirectory)
        {
            if (pdfFile == null || pdfFile.Length == 0)
            {
                return BadRequest("Lütfen bir PDF dosyası yükleyin.");
            }

            if (string.IsNullOrEmpty(outputDirectory))
            {
                return BadRequest("Lütfen bir çıktı dizini belirtin.");
            }

            var tempPdfPath = Path.Combine(Path.GetTempPath(), pdfFile.FileName);

            using (var stream = new FileStream(tempPdfPath, FileMode.Create))
            {
                await pdfFile.CopyToAsync(stream);
            }
            try
            {
                _pdfManager._outputDirectory = outputDirectory; 
                _pdfManager.ProcessPdf(tempPdfPath); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"PDF işleme hatası: {ex.Message}");
            }

            var pdfsFolderPath = Path.Combine(outputDirectory, "PDFs");
            var newPdfFiles = Directory.GetFiles(pdfsFolderPath, "*.pdf");

            if (newPdfFiles.Length == 0)
            {
                return NotFound("Yeni PDF'ler oluşturulamadı.");
            }

            var fileUrls = new List<string>();
            foreach (var filePath in newPdfFiles)
            {
                var fileName = Path.GetFileName(filePath);
                var fileUrl = $"{Request.Scheme}://{Request.Host}/api/Pdf/download?fileName={fileName}";
                fileUrls.Add(fileUrl);
            }

            return Ok(fileUrls);
        }
        [HttpGet("download")]
        public async Task<IActionResult> DownloadPdf(string fileName, string pdfFolderPath)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return BadRequest("Lütfen bir dosya adı belirtin.");
            }

           
            var filePath = Path.Combine(pdfFolderPath, fileName);

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound("Belirtilen PDF dosyası bulunamadı.");
            }

            var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            return File(fileStream, "application/pdf", fileName);
        }
    }
}




