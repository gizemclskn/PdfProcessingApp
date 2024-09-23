using PdfProcessingApp.DAL.Repository;
using PdfProcessingApp.Models; // PdfProcessingResult ve diğer model sınıflarını içermelidir.
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PdfProcessingApp.Business.Services
{
    public class PdfService
    {
        private readonly PdfRepository _pdfRepository;

        public PdfService(string outputDirectory)
        {
            _pdfRepository = new PdfRepository(outputDirectory);
        }

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
                Console.WriteLine($"PDF işleme sırasında hata oluştu: {ex.Message}");
                throw;
            }
        }

        public List<string> GetProcessedHeaders()
        {
            var headerPageNumbers = _pdfRepository.GetHeaderPageNumbers();
            return headerPageNumbers
                .Where(kvp => kvp.Value > 0)
                .Select(kvp => kvp.Key)
                .ToList();
        }
    }
}
