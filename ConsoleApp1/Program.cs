using PdfProcessingApp.Business.Services;
using System;
using System.IO;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // PDF dosyasının yolunu belirleyin
                string pdfFilePath = @"C:\Users\gizem\Downloads\örnek özlük dosyası.pdf";  // İşlemek istediğiniz PDF dosyasının yolu
                string outputDirectory = @"C:\Users\gizem\Downloads\PDFs";  // Çıktıların yazılacağı dizin

                // Output dizini kontrol et, yoksa oluştur
                if (!Directory.Exists(outputDirectory))
                {
                    Directory.CreateDirectory(outputDirectory);
                }

                // PdfService nesnesini oluştur
                PdfService pdfService = new PdfService(outputDirectory);

                // PDF dosyasını işleyin
                Console.WriteLine("PDF dosyası işleniyor...");
                pdfService.ProcessPdf(pdfFilePath);
                Console.WriteLine("PDF işleme tamamlandı.");

                // İşlenmiş başlıkları alın
                var processedHeaders = pdfService.GetProcessedHeaders();

                // İşleme sonuçlarını konsola yazdır
                Console.WriteLine("İşlenmiş Başlıklar:");
                foreach (var header in processedHeaders)
                {
                    Console.WriteLine(header);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Bir hata oluştu: {ex.Message}");
            }
        }
    }
}
