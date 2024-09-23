using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ImageMagick;
using iText.Kernel.Pdf;
using Tesseract;

namespace PdfProcessingApp.DAL.Repository
{
    public class PdfRepository
    {
        private string _tessDataPath;
        private string _outputDirectory;
        private Dictionary<string, int> _headerPageNumberPairs;

        public PdfRepository(string outputDirectory)
        {
            _tessDataPath = @"C:\Users\gizem\Source\Repos\PdfProcessingApp\PdfProcessingApp.DAL\tessdata";
            _outputDirectory = outputDirectory;
            _headerPageNumberPairs = new Dictionary<string, int>
            {
                {"IS BASVURU FORMU", 0 },
                {"TURKIYE CUMHURIYETI KIMLIK KARTI", 3 },
                {"ISKUR KAYIT BELGESI", 0 },
                {"ADLI SICIL KAYDI", 0 },
                {"YERLESIM YERI VE DIGER ADRES BELGESI", 0 },
                {"NUFUS KAYIT ORNEGI", 0 },
                {"ASKERALMA GENEL MUDURLUGU", 0 },
                {"VADESIZ HESAP BILGILERI", 0 },
                {"OGRENIM BELGESI", 0 },
                {"ISYERI UNVAN LISTESI", 0 },
                {"KURS BITIRME BELGESI", 0 },
                {"IS SAGLIGI VE GUVENLIGI EGITIM KATILIM BELGESI", 0 },
                {"PERIYODIK MUAYENE FORMU", 0 }
            };
        }

        public void ProcessPdf(string pdfPath)
        {
            var settings = new MagickReadSettings { Density = new Density(300) };

            using (MagickImageCollection images = new MagickImageCollection())
            {
                images.Read(pdfPath, settings);

                Parallel.ForEach(images.Select((image, index) => new { image, index }), (imgInfo) =>
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        imgInfo.image.Write(memoryStream, MagickFormat.Png);
                        memoryStream.Position = 0;

                        using (var img = Pix.LoadFromMemory(memoryStream.ToArray()))
                        {
                            using (var engine = new TesseractEngine(_tessDataPath, "tur+eng", EngineMode.Default))
                            {
                                using (var page = engine.Process(img, PageSegMode.Auto))
                                {
                                    string text = page.GetText().ToUpper();
                                    text = ReplaceTurkishCharacters(text); // Türkçe karakterleri düzelt

                                    lock (_headerPageNumberPairs)
                                    {
                                        foreach (var header in _headerPageNumberPairs.Keys.ToList())
                                        {
                                            if (text.Contains(header.ToUpper())) // Başlık kontrolü büyük harfle
                                            {
                                                _headerPageNumberPairs[header] = imgInfo.index + 1;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                });
            }

            CreateNewPdfsFromHeaders(pdfPath);
        }

        private void CreateNewPdfsFromHeaders(string inputFilePath)
        {
            string outputFolder = Path.Combine(_outputDirectory, "PDFs");
            if (!Directory.Exists(outputFolder))
            {
                Directory.CreateDirectory(outputFolder);
            }

            using (PdfDocument pdfDoc = new PdfDocument(new PdfReader(inputFilePath)))
            {
                var headers = _headerPageNumberPairs.Where(kvp => kvp.Value > 0)
                                                     .OrderBy(kvp => kvp.Value)
                                                     .Select(kvp => kvp.Key)
                                                     .ToList();

                for (int i = 0; i < headers.Count - 1; i++)
                {
                    string newPdfPath = Path.Combine(outputFolder, $"{headers[i]}.pdf");
                    using (PdfDocument newPdf = new PdfDocument(new PdfWriter(newPdfPath)))
                    {
                        int firstPageNumber = _headerPageNumberPairs[headers[i]];
                        int lastPageNumber = _headerPageNumberPairs[headers[i + 1]];
                        pdfDoc.CopyPagesTo(firstPageNumber, lastPageNumber - 1, newPdf);
                    }
                }

                // Son başlık için ayrı işlem
                string lastPdfPath = Path.Combine(outputFolder, $"{headers.Last()}.pdf");
                using (PdfDocument lastPdf = new PdfDocument(new PdfWriter(lastPdfPath)))
                {
                    int firstPageNumber = _headerPageNumberPairs[headers.Last()];
                    pdfDoc.CopyPagesTo(firstPageNumber, pdfDoc.GetNumberOfPages(), lastPdf);
                }
            }
        }

        private string ReplaceTurkishCharacters(string input)
        {
            StringBuilder sb = new StringBuilder(input);

            sb.Replace('İ', 'I');
            sb.Replace('Ç', 'C');
            sb.Replace('Ş', 'S');
            sb.Replace('Ğ', 'G');
            sb.Replace('Ü', 'U');
            sb.Replace('Ö', 'O');

            return sb.ToString();
        }
        public Dictionary<string, int> GetHeaderPageNumbers()
        {
            return _headerPageNumberPairs;
        }
    }
}
