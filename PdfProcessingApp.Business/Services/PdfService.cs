using PdfProcessingApp.DAL.Repository;
using PdfProcessingApp.Models;

namespace PdfProcessingApp.Business.Services
{
    public class PdfService
    {
        private readonly PdfRepository _pdfRepository;

        public PdfService(string outputDirectory)
        {
            var documentSections = GetPredefinedSections();
            _pdfRepository = new PdfRepository(outputDirectory, documentSections);
        }

        public void ProcessPdf(string pdfFilePath)
        {
            if (string.IsNullOrEmpty(pdfFilePath) || !File.Exists(pdfFilePath))
            {
                throw new FileNotFoundException($"PDF file not found: {pdfFilePath}");
            }

            _pdfRepository.ProcessPdf(pdfFilePath);
        }

        private List<DocumentSection> GetPredefinedSections()
        {
            return new List<DocumentSection>
        {
            new DocumentSection("IS BASVURU FORMU", new List<Keyword>
            {
                new Keyword("IS BASVURU FORMU"),new Keyword("Haftasonu çalışabilirmisiniz"), new Keyword("İş ilanımızı veya SİEM markasını nereden duydunuz")
            }),
              new DocumentSection("PERIYODIK MUAYENE FORMU", new List<Keyword>
            {
                new Keyword("PERIYODIK MUAYENE FORMU"),
                new Keyword("FIZIK MUAYENE SONUCLARI"),
                new Keyword("MUAYENE")
            }),
            new DocumentSection("TURKIYE CUMHURIYETI KIMLIK KARTI", new List<Keyword>
            {
                new Keyword("TURKIYE CUMHURIYETI KIMLIK KART")
            }),
            //new DocumentSection("ISKUR KAYIT BELGESI", new List<Keyword>
            //{
            //    new Keyword("İŞKUR"), new Keyword("kayıt")
            //}),
            //new DocumentSection("ADLI SICIL KAYDI", new List<Keyword>
            //{
            //    new Keyword("adli"), new Keyword("sicil")
            //}),
            //new DocumentSection("YERLESIM YERI VE DIGER ADRES BELGESI", new List<Keyword>
            //{
            //    new Keyword("adres"), new Keyword("yerleşim")
            //}),
            //new DocumentSection("NUFUS KAYIT ORNEGI", new List<Keyword>
            //{
            //    new Keyword("nüfus"), new Keyword("örneği")
            //}),
            //new DocumentSection("ASKERALMA GENEL MUDURLUGU", new List<Keyword>
            //{
            //    new Keyword("asker alma"), new Keyword("genel müdürlük")
            //}),
            //new DocumentSection("VADESIZ HESAP BILGILERI", new List<Keyword>
            //{
            //    new Keyword("vadesiz hesap"), new Keyword("bilgileri")
            //}),
            //new DocumentSection("OGRENIM BELGESI", new List<Keyword>
            //{
            //    new Keyword("öğrenim"), new Keyword("belgesi")
            //}),
            //new DocumentSection("ISYERI UNVAN LISTESI", new List<Keyword>
            //{
            //    new Keyword("iş yeri"), new Keyword("ünvan")
            //}),
            //new DocumentSection("KURS BITIRME BELGESI", new List<Keyword>
            //{
            //    new Keyword("kurs bitirme"), new Keyword("belgesi")
            //}),
            //new DocumentSection("IS SAGLIGI VE GUVENLIGI EGITIM KATILIM BELGESI", new List<Keyword>
            //{
            //    new Keyword("iş sağlığı"), new Keyword("katılım belgesi")
            //}),
          
        };
        }

        public Dictionary<string, int> GetHeaderPageNumbers()
        {
            return _pdfRepository.GetHeaderPageNumbers();
        }
        public List<string> GetProcessedHeaders()
        {
            List<string> processedHeaders = new List<string>();
            var headerPageNumbers = _pdfRepository.GetHeaderPageNumbers();

            foreach (var header in headerPageNumbers)
            {
                processedHeaders.Add(header.Key); // Add the header title
            }

            return processedHeaders;
        }
    }
}

