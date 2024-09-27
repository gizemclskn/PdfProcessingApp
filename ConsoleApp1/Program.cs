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
                // PDF dosyasının yolu
                string pdfFilePath = @"C:\Users\gizem\Downloads\örnek özlük dosyası.pdf";
                // Çıktıların yazılacağı dizin
                string outputDirectory = @"C:\Users\gizem\Downloads\PDFs";

                // Çıktı dizinini kontrol et ve oluştur
                if (!Directory.Exists(outputDirectory))
                {
                    Directory.CreateDirectory(outputDirectory);
                }

                // PdfService örneği oluştur
                PdfService pdfService = new PdfService(outputDirectory);

                Console.WriteLine("PDF dosyası işleniyor...");
                pdfService.ProcessPdf(pdfFilePath); // PDF dosyasını işle
                Console.WriteLine("PDF işleme tamamlandı.");

                // İşlenmiş başlıkları al
                var processedHeaders = pdfService.GetProcessedHeaders();

                Console.WriteLine("İşlenmiş Başlıklar:");
                if (processedHeaders != null && processedHeaders.Count > 0)
                {
                    foreach (var header in processedHeaders)
                    {
                        Console.WriteLine(header); // İşlenmiş başlıkları yazdır
                    }
                }
                else
                {
                    Console.WriteLine("Hiçbir başlık işlenmedi.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Bir hata oluştu: {ex.Message}");
            }
        }
    }
}
