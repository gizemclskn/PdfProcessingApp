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

    
            var zipFilePath = Path.Combine(outputDirectory, "ProcessedPdfs.zip");

            try
            {
               
                if (System.IO.File.Exists(zipFilePath))
                {
                    System.IO.File.Delete(zipFilePath);
                }

                using (var zip = ZipFile.Open(zipFilePath, ZipArchiveMode.Create))
                {
                    foreach (var filePath in newPdfFiles)
                    {
                      
                        var entryName = Path.Combine("PDFs", Path.GetFileName(filePath)); 
                        zip.CreateEntryFromFile(filePath, entryName);
                    }
                }

                var zipFileBytes = await System.IO.File.ReadAllBytesAsync(zipFilePath);
                return File(zipFileBytes, "application/zip", "ProcessedPdfs.zip");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"ZIP dosyası oluşturulurken hata oluştu: {ex.Message}");
            }
        }
    }
}




