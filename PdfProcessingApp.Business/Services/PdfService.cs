using PdfProcessingApp.DAL.Repository;
using PdfProcessingApp.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace PdfProcessingApp.Services
{
    public class PdfService
    {
        private readonly PdfRepository _pdfRepository;

        public PdfService(string outputDirectory)
        {
            _pdfRepository = new PdfRepository(outputDirectory);
        }

        /// <summary>
        /// PDF dosyasını işler ve gerekli alt bölümlere ayırır.
        /// </summary>
        /// <param name="pdfFilePath">İşlenecek PDF dosyasının yolu</param>
        public void ProcessPdf(string pdfFilePath)
        {
            if (string.IsNullOrEmpty(pdfFilePath) || !File.Exists(pdfFilePath))
            {
                throw new FileNotFoundException("PDF dosyası bulunamadı.");
            }

            try
            {
                _pdfRepository.ProcessPdf(pdfFilePath);
            }
            catch (Exception ex)
            {
                // Hata günlüğe yazılabilir veya bir üst katmana iletilebilir.
                Console.WriteLine($"PDF işleme sırasında hata oluştu: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// PDF işleme sonucundaki bölümleri ve görüntüleri döndürür.
        /// </summary>
        /// <param name="pdfFilePath">İşlenmiş PDF dosyasının yolu</param>
        /// <returns>PdfProcessingResult içeren işleme sonucu</returns>
        public PdfProcessingResult GetPdfProcessingResult(string pdfFilePath)
        {
            if (string.IsNullOrEmpty(pdfFilePath) || !File.Exists(pdfFilePath))
            {
                throw new FileNotFoundException("PDF dosyası bulunamadı.");
            }

            var result = new PdfProcessingResult
            {
                Document = new PdfDocument
                {
                    FileName = Path.GetFileName(pdfFilePath),
                    FileSize = new FileInfo(pdfFilePath).Length,
                    CreatedDate = DateTime.Now
                }
            };

            // Burada bölümleri ve görüntüleri doldurabiliriz.
            // Placeholder için basit bir örnek:
            result.Sections.Add(new DocumentSection("Örnek Başlık", "Bu bir örnek içeriği."));
            result.Images.Add(new ImageMetadata("example.png", "png", 1920, 1080));

            return result;
        }
    }
}