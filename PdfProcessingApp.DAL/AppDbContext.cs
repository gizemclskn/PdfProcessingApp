using Microsoft.EntityFrameworkCore;
using PdfProcessingApp.DAL.Configurations;
using PdfProcessingApp.Models;
using System.Reflection;

namespace PdfProcessingApp.DAL
{
    public class AppDbContext : DbContext
    {
        public DbSet<PdfDocument> PdfDocuments { get; set; }
        public DbSet<PdfCreateLog> PdfCreateLogs { get; set; }
        public DbSet<DocumentSection> DocumentSections { get; set; }
        public DbSet<Keyword> Keywords { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new PdfDocumentConfiguration());
            modelBuilder.ApplyConfiguration(new PdfCreateLogConfiguration());
            modelBuilder.ApplyConfiguration(new DocumentSectionConfiguration());
            modelBuilder.ApplyConfiguration(new KeywordConfiguration());
        }
    }
}

