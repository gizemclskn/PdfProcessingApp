//using Microsoft.EntityFrameworkCore;
//using PdfProcessingApp.Business;
//using PdfProcessingApp.DAL;
//using PdfProcessingApp.DAL.Repository.Abstract;
//using PdfProcessingApp.DAL.Repository.Concrete;
//using PdfProcessingApp.Models;
//using System;
//using System.Collections.Generic;
//using System.IO;

//namespace ConsoleApp1
//{
//    class Program
//    {
//        static void Main(string[] args)
//        {
//            string pdfFilePath = @"C:\Users\gizem\Downloads\örnek özlük dosyası.pdf";
//            string outputDirectory = @"C:\Users\gizem\Downloads";

//            List<DocumentSection> documentSections = GetPredefinedSections();

//            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
//            optionsBuilder.UseSqlServer("YourConnectionStringHere");

//            using var context = new AppDbContext(optionsBuilder.Options);

//            //      IPdfRepository pdfRepository = new PdfRepository(context, outputDirectory, documentSections);
//            // PdfManager pdfManager = new PdfManager(pdfRepository, outputDirectory, documentSections);

//            pdfManager.ProcessPdf(pdfFilePath);

//            Console.WriteLine("PDF işleme tamamlandı.");
//        }

//        private static List<DocumentSection> GetPredefinedSections()
//        {
//            var sections = new List<DocumentSection>();

//            var isBasvuruFormu = new DocumentSection
//            {
//                Title = "IS BASVURU FORMU",
//                Keywords = new List<Keyword>
//                {
//                    new Keyword { Value = "IS BASVURU FORMU" },
//                    new Keyword { Value = "HAFTASONU CALISABILIRMISINIZ" }
//                }
//            };
//            sections.Add(isBasvuruFormu);


//            var periyodikMuayeneFormu = new DocumentSection
//            {
//                Title = "PERIYODIK MUAYENE FORMU",
//                Keywords = new List<Keyword>
//                {
//                    new Keyword { Value = "PERIYODIK MUAYENE FORMU" },
//                    new Keyword { Value = "MUAYENE" }
//                }
//            };
//            sections.Add(periyodikMuayeneFormu);


//            var turkiyeKimlikKarti = new DocumentSection
//            {
//                Title = "TURKIYE CUMHURIYETI KIMLIK KARTI",
//                Keywords = new List<Keyword>
//                {
//                    new Keyword { Value = "TURKIYE CUMHURIYETI KIMLIK KART" }
//                }
//            };
//            sections.Add(turkiyeKimlikKarti);


//            sections.Add(new DocumentSection
//            {
//                Title = "ISKUR KAYIT BELGESI",
//                Keywords = new List<Keyword>
//                {
//                    new Keyword { Value = "ISKUR KAYIT BELGESI" }
//                }
//            });
//            sections.Add(new DocumentSection
//            {
//                Title = "ADLI SICIL KAYDI",
//                Keywords = new List<Keyword>
//                {
//                    new Keyword { Value = "ADLI SICIL KAYDI" },
//                    new Keyword { Value = "sicil" }
//                }
//            });
//            sections.Add(new DocumentSection
//            {
//                Title = "YERLESIM YERI VE DIGER ADRES BELGESI",
//                Keywords = new List<Keyword>
//                {
//                    new Keyword { Value = "YERLESIM YERI VE DIGER ADRES BELGESI" }
//                }
//            });
//            sections.Add(new DocumentSection
//            {
//                Title = "NUFUS KAYIT ORNEGI",
//                Keywords = new List<Keyword>
//                {
//                    new Keyword { Value = "NUFUS KAYIT ORNEGI" }
//                }
//            });
//            sections.Add(new DocumentSection
//            {
//                Title = "ASKERALMA GENEL MUDURLUGU",
//                Keywords = new List<Keyword>
//                {
//                    new Keyword { Value = "ASKERALMA GENEL MUDURLUGU" }
//                }
//            });
//            sections.Add(new DocumentSection
//            {
//                Title = "VADESIZ HESAP BILGILERI",
//                Keywords = new List<Keyword>
//                {
//                    new Keyword { Value = "VADESIZ HESAP BILGILERI" }
//                }
//            });
//            sections.Add(new DocumentSection
//            {
//                Title = "OGRENIM BELGESI",
//                Keywords = new List<Keyword>
//                {
//                    new Keyword { Value = "OGRENIM BELGESI" }
//                }
//            });
//            sections.Add(new DocumentSection
//            {
//                Title = "ISYERI UNVAN LISTESI",
//                Keywords = new List<Keyword>
//                {
//                    new Keyword { Value = "ISYERI UNVAN LISTESI" }
//                }
//            });
//            sections.Add(new DocumentSection
//            {
//                Title = "KURS BITIRME BELGESI",
//                Keywords = new List<Keyword>
//                {
//                    new Keyword { Value = "KURS BITIRME BELGESI" }
//                }
//            });
//            sections.Add(new DocumentSection
//            {
//                Title = "IS SAGLIGI VE GUVENLIGI EGITIM KATILIM BELGESI",
//                Keywords = new List<Keyword>
//                {
//                    new Keyword { Value = "IS SAGLIGI VE GUVENLIGI EGITIM KATILIM BELGESI" }
//                }
//            });
//            sections.Add(new DocumentSection
//            {
//                Title = "T.C. SOSYAL GUVENLIK KURUMU BASKANLIGI EMEKLILIK HIZMETLERI GENEL MUDURLUGU",
//                Keywords = new List<Keyword>
//                {
//                    new Keyword { Value = "EMEKLILIK" }
//                }
//            });

//            return sections;
//        }
//    }
//}
