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
                string pdfFilePath = @"C:\Users\gizem\Downloads\örnek özlük dosyası.pdf"; // PDF dosyasının yolu
                string outputDirectory = @"C:\Users\gizem\Downloads\PDFs"; // Çıktıların yazılacağı dizin

                if (!Directory.Exists(outputDirectory))
                {
                    Directory.CreateDirectory(outputDirectory);
                }

                PdfService pdfService = new PdfService(outputDirectory);

                Console.WriteLine("PDF dosyası işleniyor...");
                pdfService.ProcessPdf(pdfFilePath);
                Console.WriteLine("PDF işleme tamamlandı.");


                var processedHeaders = pdfService.GetProcessedHeaders();


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
