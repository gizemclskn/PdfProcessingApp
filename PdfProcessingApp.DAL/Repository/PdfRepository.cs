using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using PdfProcessingApp.Models;

namespace PdfProcessingApp.DAL.Repository
{
    public class PdfRepository
    {
        // Metin çıkarma işlemi
        public string ExtractTextFromPage(PdfReader reader, int pageNumber)
        {
            return PdfTextExtractor.GetTextFromPage(reader, pageNumber);
        }

        public void SavePage(PdfReader reader, int pageNumber, string outputFilePath)
        {
            using (var document = new iTextSharp.text.Document())
            {
                PdfCopy copy = new PdfCopy(document, new FileStream(outputFilePath, FileMode.Create));
                document.Open();
                copy.AddPage(copy.GetImportedPage(reader, pageNumber));
                document.Close();
            }
        }

        // resim çıkarma ve kaydetme
        public List<ImageMetadata> ExtractAndSaveImages(PdfReader reader, string outputDirectory)
        {
            var images = new List<ImageMetadata>();

            if (!Directory.Exists(outputDirectory))
            {
                Directory.CreateDirectory(outputDirectory);
            }

            for (int i = 1; i <= reader.NumberOfPages; i++)
            {
                var page = reader.GetPageN(i);
                var resources = page.GetAsDict(PdfName.RESOURCES);
                var xObject = resources.GetAsDict(PdfName.XOBJECT);

                if (xObject == null) continue;

                foreach (PdfName key in xObject.Keys)
                {
                    var obj = xObject.GetDirectObject(key);
                    if (obj is PdfStream stream && PdfName.IMAGE.Equals(stream.Get(PdfName.SUBTYPE)))
                    {
                        string imageFileName = $"Page_{i}_Image_{key}.jpg";
                        string imageFilePath = System.IO.Path.Combine(outputDirectory, imageFileName);

                        using (var imgStream = new MemoryStream(stream.GetBytes()))
                        {
                            using (var img = Image.FromStream(imgStream))
                            {
                                img.Save(imageFilePath, ImageFormat.Jpeg);

                                var metadata = new ImageMetadata(imageFileName, "jpeg", img.Width, img.Height);
                                images.Add(metadata);
                            }
                        }
                    }
                }
            }

            return images;
        }
    }
}
