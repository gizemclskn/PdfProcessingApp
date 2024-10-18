using Microsoft.EntityFrameworkCore;
using PdfProcessingApp.Business;
using PdfProcessingApp.DAL;
using PdfProcessingApp.DAL.Repository.Abstract;
using PdfProcessingApp.DAL.Repository.Concrete;
using PdfProcessingApp.Models;
using System.IO;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddScoped<IPdfRepository, PdfRepository>(serviceProvider =>
{
    var context = serviceProvider.GetRequiredService<AppDbContext>();
    var outputDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Output");
    var documentSections = GetPredefinedSections();
    return new PdfRepository(context, outputDirectory, documentSections);
});

builder.Services.AddScoped<PdfManager>(serviceProvider =>
{
    var pdfRepository = serviceProvider.GetRequiredService<IPdfRepository>();
    var outputDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Output");
    var documentSections = GetPredefinedSections(); 
    return new PdfManager(pdfRepository, outputDirectory, documentSections);
});

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
List<DocumentSection> GetPredefinedSections()
{
    var sections = new List<DocumentSection>();

    var isBasvuruFormu = new DocumentSection
    {
        Title = "IS BASVURU FORMU",
        Keywords = new List<Keyword>
                {
                    new Keyword { Value = "IS BASVURU FORMU" },
                    new Keyword { Value = "HAFTASONU CALISABILIRMISINIZ" }
                }
    };
    sections.Add(isBasvuruFormu);


    var periyodikMuayeneFormu = new DocumentSection
    {
        Title = "PERIYODIK MUAYENE FORMU",
        Keywords = new List<Keyword>
                {
                    new Keyword { Value = "PERIYODIK MUAYENE FORMU" },
                    new Keyword { Value = "FIZIK MUAYENE SONUCLARI" }
                }
    };
    sections.Add(periyodikMuayeneFormu);


    var turkiyeKimlikKarti = new DocumentSection
    {
        Title = "TURKIYE CUMHURIYETI KIMLIK KARTI",
        Keywords = new List<Keyword>
                {
                    new Keyword { Value = "TURKIYE CUMHURIYETI KIMLIK KART" }
                }
    };
    sections.Add(turkiyeKimlikKarti);


    sections.Add(new DocumentSection
    {
        Title = "ISKUR KAYIT BELGESI",
        Keywords = new List<Keyword>
                {
                    new Keyword { Value = "ISKUR KAYIT BELGESI" }
                }
    });
    sections.Add(new DocumentSection
    {
        Title = "ADLI SICIL KAYDI",
        Keywords = new List<Keyword>
                {
                    new Keyword { Value = "ADLI SICIL KAYDI" }
                }
    });
    sections.Add(new DocumentSection
    {
        Title = "YERLESIM YERI VE DIGER ADRES BELGESI",
        Keywords = new List<Keyword>
                {
                    new Keyword { Value = "YERLESIM YERI VE DIGER ADRES BELGESI" }
                }
    });
    sections.Add(new DocumentSection
    {
        Title = "NUFUS KAYIT ORNEGI",
        Keywords = new List<Keyword>
                {
                    new Keyword { Value = "NUFUS KAYIT ORNEGI" }
                }
    });
    sections.Add(new DocumentSection
    {
        Title = "ASKERALMA GENEL MUDURLUGU",
        Keywords = new List<Keyword>
                {
                    new Keyword { Value = "ASKERALMA GENEL MUDURLUGU" }
                }
    });
    sections.Add(new DocumentSection
    {
        Title = "VADESIZ HESAP BILGILERI",
        Keywords = new List<Keyword>
                {
                    new Keyword { Value = "VADESIZ HESAP BILGILERI" }
                }
    });
    sections.Add(new DocumentSection
    {
        Title = "OGRENIM BELGESI",
        Keywords = new List<Keyword>
                {
                    new Keyword { Value = "OGRENIM BELGESI" }
                }
    });
    sections.Add(new DocumentSection
    {
        Title = "ISYERI UNVAN LISTESI",
        Keywords = new List<Keyword>
                {
                    new Keyword { Value = "ISYERI UNVAN LISTESI" }
                }
    });
    sections.Add(new DocumentSection
    {
        Title = "KURS BITIRME BELGESI",
        Keywords = new List<Keyword>
                {
                    new Keyword { Value = "KURS BITIRME BELGESI" }
                }
    });
    sections.Add(new DocumentSection
    {
        Title = "IS SAGLIGI VE GUVENLIGI EGITIM KATILIM BELGESI",
        Keywords = new List<Keyword>
                {
                    new Keyword { Value = "IS SAGLIGI VE GUVENLIGI EGITIM KATILIM BELGESI" }
                }
    });
    sections.Add(new DocumentSection
    {
        Title = "T.C. SOSYAL GUVENLIK KURUMU BASKANLIGI EMEKLILIK HIZMETLERI GENEL MUDURLUGU",
        Keywords = new List<Keyword>
                {
                    new Keyword { Value = "T.C. SOSYAL GUVENLIK KURUMU BASKANLIGI EMEKLILIK HIZMETLERI GENEL MUDURLUGU" },
                    new Keyword { Value = "4a" },
                    new Keyword { Value = "UZUN VADE HIZMET DOKUMU" },
                    new Keyword { Value = "SGRT. STATU" },
                    new Keyword { Value = "SGRT" }
                }
    });
    sections.Add(new DocumentSection
    {
        Title = "SAGLIK DOKUMANI",
        Keywords = new List<Keyword>
                {                        
                    new Keyword { Value = "SAGLIK BAKANLIGI" },
                    new Keyword { Value = "OSGB" },
                    new Keyword { Value = "VITAL CAPACITY REPORT" },
                    new Keyword { Value = "TIBBI" },
                    new Keyword { Value = "HASTA" },
                }
    });

    return sections;
}