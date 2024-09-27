﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageMagick;
using iText.Kernel.Pdf;
using Tesseract;
using PdfProcessingApp.Models;

namespace PdfProcessingApp.DAL.Repository
{
    public class PdfRepository
    {
        private readonly string _tessDataPath;
        private readonly string _outputDirectory;
        private readonly List<DocumentSection> _documentSections;

        public PdfRepository(string outputDirectory, List<DocumentSection> documentSections)
        {
            _tessDataPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\..\PdfProcessingApp.DAL\tessdata"));
            _outputDirectory = outputDirectory;
            _documentSections = documentSections;
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
                        ProcessImageWithTesseract(img, imgInfo.index);
                    }
                }
            });

            GenerateNewPdfsFromKeywords(pdfPath);
        }

        private MagickImageCollection ConvertPdfToImage(string pdfPath)
        {
            var settings = new MagickReadSettings { Density = new Density(300) };
            MagickImageCollection images = new MagickImageCollection();
            images.Read(pdfPath, settings);

            return images;
        }

        private void ProcessImageWithTesseract(Pix img, int index)
        {
            using (var engine = new TesseractEngine(_tessDataPath, "tur+eng", EngineMode.Default))
            {
                engine.SetVariable("user_defined_dpi", "300");

               
                var page = engine.Process(img, PageSegMode.SingleBlock); 

                string text = page.GetText().ToUpper();

                text = ReplaceTurkishCharacters(text);

                 lock (_documentSections)
                {
                    UpdateSectionWithKeyword(text, index);
                }
            }
        }


        private void UpdateSectionWithKeyword(string text, int index)
        {
            foreach (var section in _documentSections)
            {
                foreach (var keyword in section.Keywords)
                {
                    if (text.Contains(keyword.Value.ToUpper()))
                    {
                        keyword.SetDocumentSectionId(index + 1);
                    }
                }
            }
        }

        private void GenerateNewPdfsFromKeywords(string inputFilePath)
        {
            string outputFolder = CreateOutputDirectory();
            int unidentifiedDocCounter = 1;

            using (var pdfDoc = new iText.Kernel.Pdf.PdfDocument(new PdfReader(inputFilePath)))
            {
                Dictionary<string, List<int>> sectionPageMapping = new Dictionary<string, List<int>>();

                for (int i = 1; i <= pdfDoc.GetNumberOfPages(); i++)
                {
                    bool isMatched = false;

                    foreach (var section in _documentSections)
                    {
                        foreach (var keyword in section.Keywords)
                        {
                            if (keyword.DocumentSectionId == i)
                            {
                                if (!sectionPageMapping.ContainsKey(section.Title))
                                {
                                    sectionPageMapping[section.Title] = new List<int>();
                                }

                                sectionPageMapping[section.Title].Add(i);
                                isMatched = true;
                            }
                        }
                    }

                    if (!isMatched)
                    {
                        string unidentifiedDocPath = Path.Combine(outputFolder, $"BelirlenemeyenDokuman{unidentifiedDocCounter}.pdf");
                        using (var unidentifiedPdf = new iText.Kernel.Pdf.PdfDocument(new PdfWriter(unidentifiedDocPath)))
                        {
                            pdfDoc.CopyPagesTo(i, i, unidentifiedPdf);
                        }
                        unidentifiedDocCounter++;
                    }
                }

                foreach (var kvp in sectionPageMapping)
                {
                    string title = kvp.Key;
                    var pages = kvp.Value;

                    var newPdfDoc = new iText.Kernel.Pdf.PdfDocument(new PdfWriter(Path.Combine(outputFolder, $"{title}.pdf")));
                    foreach (var page in pages)
                    {
                        pdfDoc.CopyPagesTo(page, page, newPdfDoc);
                    }
                    newPdfDoc.Close();
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

        private string CreateOutputDirectory()
        {
            string outputFolder = Path.Combine(_outputDirectory, "PDFs");

            if (!Directory.Exists(outputFolder))
            {
                Directory.CreateDirectory(outputFolder);
            }

            return outputFolder;
        }

        public Dictionary<string, int> GetHeaderPageNumbers()
        {
            var headerPageNumbers = new Dictionary<string, int>();
            foreach (var section in _documentSections)
            {
                foreach (var keyword in section.Keywords)
                {
                    if (!headerPageNumbers.ContainsKey(keyword.Value))
                    {
                        headerPageNumbers[keyword.Value] = keyword.DocumentSectionId;
                    }
                }
            }
            return headerPageNumbers;
        }
    }
}
