using PdfProcessingApp.Services;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // PDF dosyasının yolunu belirleyin
                string pdfFilePath = @"C:\Users\ahmet\Desktop\örnek özlük dosyası.pdf";  // İşlemek istediğiniz PDF dosyasının yolu
                string outputDirectory = @"C:\Users\ahmet\Downloads";  // Çıktıların yazılacağı dizin

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

                // İşleme sonucunu alın
                var result = pdfService.GetPdfProcessingResult(pdfFilePath);

                // İşleme sonuçlarını konsola yazdır
                Console.WriteLine("PDF İşleme Sonuçları:");
                Console.WriteLine($"Belge Adı: {result.Document.FileName}");
                Console.WriteLine($"Belge Boyutu: {result.Document.FileSize} bytes");
                Console.WriteLine($"Oluşturulma Tarihi: {result.Document.CreatedDate}");

                // Bölümler ve Görüntüleri yazdır
                foreach (var section in result.Sections)
                {
                    Console.WriteLine($"\nBölüm Başlığı: {section.Title}");
                    Console.WriteLine($"Bölüm İçeriği: {section.Content}");
                    Console.WriteLine($"Resim Yolu: {section.ImageFilePath}");
                    Console.WriteLine($"Oluşturulma Tarihi: {section.CreatedDate}");
                }

                foreach (var image in result.Images)
                {
                    Console.WriteLine($"\nGörüntü Dosyası: {image.FileName}");
                    Console.WriteLine($"Format: {image.Format}");
                    Console.WriteLine($"Boyutlar: {image.Width}x{image.Height}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Bir hata oluştu: {ex.Message}");
            }
        }
    }
}
