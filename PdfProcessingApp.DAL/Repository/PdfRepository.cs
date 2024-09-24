using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ImageMagick;
using iText.Kernel.Pdf;
using Org.BouncyCastle.Ocsp;
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
            _tessDataPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\..\PdfProcessingApp.DAL\tessdata"));
            _outputDirectory = outputDirectory;
            _headerPageNumberPairs = new Dictionary<string, int>
            {
                {"IS BASVURU FORMU", 0 },
                {"TURKIYE CUMHURIYETI KIMLIK KARTI", 0 },
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
            var images = ConvertPdfToImage(pdfPath);

            Parallel.ForEach(images.Select((image, index) => new { image, index }), (imgInfo) =>
            {
                using (var memoryStream = new MemoryStream())
                {
                    imgInfo.image.Write(memoryStream, MagickFormat.Png);
                    memoryStream.Position = 0;

                    using (var img = Pix.LoadFromMemory(memoryStream.ToArray()))
                    {
                        CallTesseract(img, imgInfo.index);
                    }
                }
            });

            CreateNewPdfsFromHeaders(pdfPath);
        }

        private MagickImageCollection ConvertPdfToImage(string pdfPath)
        {
            var settings = new MagickReadSettings { Density = new Density(300) };
            MagickImageCollection images = new MagickImageCollection();
            images.Read(pdfPath, settings);

            return images;
        }

        private void CallTesseract(Pix img, int index)
        {
            using (var engine = new TesseractEngine(_tessDataPath, "tur+eng", EngineMode.Default))
            {
                var page = engine.Process(img, PageSegMode.AutoOsd);

                string text = page.GetText().ToUpper();
                text = ReplaceTurkishCharacters(text);

                lock (_headerPageNumberPairs)
                {
                    SetHeaderPageNumber(text, index);
                }
            }        
        }

        private void SetHeaderPageNumber(string text, int index)
        {
            foreach (var header in _headerPageNumberPairs.Keys.ToList())
            {
                if (text.Contains(header))
                {
                    _headerPageNumberPairs[header] = index + 1;
                }
            }
        }

        private void CreateNewPdfsFromHeaders(string inputFilePath)
        {
            string outputFolder = CreateFileDirectory();

            using (PdfDocument pdfDoc = new PdfDocument(new PdfReader(inputFilePath)))
            {
                var headers = SortIdentifiedHeader();

                for (int i = 0; i < headers.Count; i++)
                {
                    int firstPageNumber = _headerPageNumberPairs[headers[i]];
                    int lastPageNumber;
                    string newPdfPath = Path.Combine(outputFolder, $"{headers[i]}.pdf");

                    using (PdfDocument newPdf = new PdfDocument(new PdfWriter(newPdfPath)))
                    {
                        if (i == headers.Count - 1)
                        {
                            lastPageNumber = pdfDoc.GetNumberOfPages();
                        }
                        else
                        {
                            lastPageNumber = _headerPageNumberPairs[headers[i + 1]] - 1;
                        }

                        pdfDoc.CopyPagesTo(firstPageNumber, lastPageNumber, newPdf);
                    }
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

        private string CreateFileDirectory()
        {
            string outputFolder = Path.Combine(_outputDirectory, "PDFs");

            if (!Directory.Exists(outputFolder))
            {
                Directory.CreateDirectory(outputFolder);
            }

            return outputFolder;
        }

        private List<string> SortIdentifiedHeader()
        {
            return _headerPageNumberPairs.Where(kvp => kvp.Value > 0)
                                     .OrderBy(kvp => kvp.Value)
                                     .Select(kvp => kvp.Key)
                                     .ToList();
        }
        public Dictionary<string, int> GetHeaderPageNumbers()
        {
            return _headerPageNumberPairs;
        }
    }
}
