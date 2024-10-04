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
                new Keyword("IS BASVURU FORMU"),
                //new Keyword("EGITIM BILGILERI"),
                new Keyword("HAFTASONU CALISABILIRMISINIZ"),
                //new Keyword("IS ILANIMIZI VEYA SIEM MARKASINI NEREDEN DUYDUNUZ")
            }),
              new DocumentSection("PERIYODIK MUAYENE FORMU", new List<Keyword>
            {
                new Keyword("PERIYODIK MUAYENE FORMU"),
             //   new Keyword("FIZIK MUAYENE SONUCLARI"),
                new Keyword("MUAYENE")
            }),
            new DocumentSection("TURKIYE CUMHURIYETI KIMLIK KARTI", new List<Keyword>
            {
                new Keyword("TURKIYE CUMHURIYETI KIMLIK KART")
            }),
            new DocumentSection("ISKUR KAYIT BELGESI", new List<Keyword>
            {
                new Keyword("ISKUR KAYIT BELGESI")
            }),
            new DocumentSection("ADLI SICIL KAYDI", new List<Keyword>
            {
                new Keyword("ADLI SICIL KAYDI"), new Keyword("sicil")
            }),
            new DocumentSection("YERLESIM YERI VE DIGER ADRES BELGESI", new List<Keyword>
            {
                new Keyword("YERLESIM YERI VE DIGER ADRES BELGESI")
            }),
            new DocumentSection("NUFUS KAYIT ORNEGI", new List<Keyword>
            {
                new Keyword("NUFUS KAYIT ORNEGI")
            }),
            new DocumentSection("ASKERALMA GENEL MUDURLUGU", new List<Keyword>
            {
                new Keyword("ASKERALMA GENEL MUDURLUGU")
            }),
            new DocumentSection("VADESIZ HESAP BILGILERI", new List<Keyword>
            {
                new Keyword("VADESIZ HESAP BILGILERI")
            }),
            new DocumentSection("OGRENIM BELGESI", new List<Keyword>
            {
                new Keyword("OGRENIM BELGESI")
            }),
            new DocumentSection("ISYERI UNVAN LISTESI", new List<Keyword>
            {
                new Keyword("ISYERI UNVAN LISTESI")
            }),
            new DocumentSection("KURS BITIRME BELGESI", new List<Keyword>
            {
                new Keyword("KURS BITIRME BELGESI")
            }),
            new DocumentSection("IS SAGLIGI VE GUVENLIGI EGITIM KATILIM BELGESI", new List<Keyword>
            {
                new Keyword("IS SAGLIGI VE GUVENLIGI EGITIM KATILIM BELGESI")
            }),
               new DocumentSection("T.C. SOSYAL GUVENLIK KURUMU BASKANLIGI EMEKLILIK HIZMETLERI GENEL MUDURLUGU", new List<Keyword>
            {
                new Keyword("EMEKLILIK")
            }),

        };

        }

        //public Dictionary<string, int> GetHeaderPageNumbers()
        //{
        //    return _pdfRepository.GetHeaderPageNumbers();
        //}
        public List<string> GetProcessedHeaders()
        {
            List<string> processedHeaders = new List<string>();
            var headerPageNumbers = _pdfRepository.GetHeaderPageNumbers();

            foreach (var header in headerPageNumbers)
            {
                processedHeaders.Add(header.Key);
            }

            return processedHeaders;
        }
    }
}

