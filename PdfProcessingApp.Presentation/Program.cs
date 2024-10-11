using Microsoft.EntityFrameworkCore;
using PdfProcessingApp.Business;
using PdfProcessingApp.DAL;
using PdfProcessingApp.DAL.Repository.Abstract;
using PdfProcessingApp.DAL.Repository.Concrete;
using PdfProcessingApp.Models;
using System.IO;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configure dependencies for PdfRepository and PdfManager
builder.Services.AddScoped<IPdfRepository, PdfRepository>(serviceProvider =>
{
    var context = serviceProvider.GetRequiredService<AppDbContext>();
    var outputDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Output");
    var documentSections = GetPredefinedSections(); // Load predefined sections
    return new PdfRepository(context, outputDirectory, documentSections);
});

builder.Services.AddScoped<PdfManager>(serviceProvider =>
{
    var pdfRepository = serviceProvider.GetRequiredService<IPdfRepository>();
    var outputDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Output");
    var documentSections = GetPredefinedSections(); // Load predefined sections
    return new PdfManager(pdfRepository, outputDirectory, documentSections);
});

var app = builder.Build();

// Configure the HTTP request pipeline
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
                    new Keyword { Value = "MUAYENE" }
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
                    new Keyword { Value = "ADLI SICIL KAYDI" },
                    new Keyword { Value = "sicil" }
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
                    new Keyword { Value = "EMEKLILIK" }
                }
    });

    return sections;
}