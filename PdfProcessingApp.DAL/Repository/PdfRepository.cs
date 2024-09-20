using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using PdfProcessingApp.Models;
using Tesseract;
using ImageMagick;
using System.Text;
using iText.Kernel.Pdf;

namespace PdfProcessingApp.DAL.Repository
{
    public class PdfRepository
    {
        private string _tessDataPath;
        private string _outputDirectory;
        private Dictionary<string, int> _headerPageNumberPairs;

        public PdfRepository(string outputDirectory) 
        {
            _tessDataPath = @"C:\Users\ahmet\Source\Repos\PdfProcessingApp\PdfProcessingApp.DAL\tessdata";
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
            using (MagickImageCollection images = new MagickImageCollection())
            {
                var settings = new MagickReadSettings()
                {
                    Density = new Density(300)
                };

                images.Read(pdfPath, settings);

                for (int i = 0; i < images.Count; i++)
                {
                    string outputImagePath = Path.Combine(_outputDirectory, $"page-{i + 1}.png");
                    images[i].Write(outputImagePath);

                    using (var engine = new TesseractEngine(_tessDataPath, "tur+eng", EngineMode.Default))
                    {
                        using (var img = Pix.LoadFromFile(outputImagePath))
                        {
                            using (var page = engine.Process(img, PageSegMode.AutoOsd))
                            {
                                string text = page.GetText().ToUpper();
                                text = ReplaceTurkishCharacters(text);

                                foreach (var header in _headerPageNumberPairs.Keys.ToList())
                                {
                                    if (text.Contains(header))
                                    {
                                        _headerPageNumberPairs[header] = i + 1;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            CreateNewPdfsFromHeaders(pdfPath);
        }

        public string ReplaceTurkishCharacters(string input)
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

        private void CreateNewPdfsFromHeaders(string inputFilePath)
        {
            string outputFolder = Path.Combine(_outputDirectory, "PDFs");

            if (!Directory.Exists(outputFolder))
            {
                Directory.CreateDirectory(outputFolder);
            }

            using (iText.Kernel.Pdf.PdfDocument pdfDoc = new iText.Kernel.Pdf.PdfDocument(new PdfReader(inputFilePath)))
            {
                var headers = _headerPageNumberPairs
                    .OrderBy(kvp => kvp.Value)
                    .Select(kvp => kvp.Key)
                    .ToList();

                for (int i = 0; i < headers.Count - 1; i++)
                {
                    using (iText.Kernel.Pdf.PdfDocument newPdf = new iText.Kernel.Pdf.PdfDocument(new PdfWriter(Path.Combine(outputFolder, $"{headers[i]}.pdf"))))
                    {
                        int firstPageNumber = _headerPageNumberPairs[headers[i]];
                        int lastPageNumber = _headerPageNumberPairs[headers[i + 1]];

                        for (int j = firstPageNumber; j < lastPageNumber; j++)
                        {
                            pdfDoc.CopyPagesTo(j, j, newPdf);
                        }
                    }
                }
            }
        }
    }
}
