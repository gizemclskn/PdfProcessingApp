using iTextSharp.text.pdf;
using PdfProcessingApp.DAL.Repository;
using PdfProcessingApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PdfProcessingApp.Business.Services
{
    public class PdfService
    {
        private readonly PdfRepository _pdfRepository;

        public PdfService(PdfRepository pdfRepository)
        {
            _pdfRepository = pdfRepository;
        }

        public PdfProcessingResult ProcessPdf(Models.PdfDocument pdfDocument, string pdfFilePath)
        {
            var result = new PdfProcessingResult
            {
                Document = pdfDocument
            };

            using (var reader = new PdfReader(pdfFilePath))
            {
                var outputDirectory = Path.Combine(Directory.GetCurrentDirectory(), "output");
                if (!Directory.Exists(outputDirectory))
                    Directory.CreateDirectory(outputDirectory);

                var images = _pdfRepository.ExtractAndSaveImages(reader, outputDirectory);
                result.Images.AddRange(images);

                for (int i = 1; i <= reader.NumberOfPages; i++)
                {
                    var pageText = _pdfRepository.ExtractTextFromPage(reader, i);

                    foreach (var keyword in pdfDocument.Sections.Select(s => s.Title))
                    {
                        if (pageText.Contains(keyword))
                        {
                            string outputFilePath = Path.Combine(outputDirectory, $"Page_{i}.pdf");
                            _pdfRepository.SavePage(reader, i, outputFilePath);
                            result.Sections.Add(new DocumentSection($"Page {i}", pageText));
                        }
                    }
                }
            }

            return result;
        }
    }
}
