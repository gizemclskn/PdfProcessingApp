using Microsoft.EntityFrameworkCore;
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

            // PdfDocument
            modelBuilder.Entity<PdfDocument>(entity =>
            {
                entity.ToTable("PdfDocuments"); 
                entity.HasKey(e => e.Id); 
                entity.Property(e => e.FileName)
                    .IsRequired()
                    .HasMaxLength(255); 
                entity.Property(e => e.FileSize)
                    .IsRequired(); 
                entity.Property(e => e.CreatedDate)
                    .HasDefaultValueSql("GETDATE()"); 
            });

            // PdfCreateLog 
            modelBuilder.Entity<PdfCreateLog>(entity =>
            {
                entity.ToTable("PdfCreateLogs"); 
                entity.HasKey(e => e.Id); 
                entity.Property(e => e.Info)
                    .IsRequired(); // Log bilgisi zorunlu alan
                entity.Property(e => e.CreatedDate)
                    .HasDefaultValueSql("GETDATE()");
            });

            // DocumentSection 
            modelBuilder.Entity<DocumentSection>(entity =>
            {
                entity.ToTable("DocumentSections");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Title)
                    .HasMaxLength(255);
                entity.Property(e => e.Content)
                    .IsRequired();
                entity.HasOne(e => e.PdfDocument) 
                    .WithMany(d => d.Sections)
                    .HasForeignKey(e => e.PdfDocumentId); 
            });

            // Keyword 
            modelBuilder.Entity<Keyword>(entity =>
            {
                entity.ToTable("Keywords");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasMaxLength(100); 
                entity.HasOne(e => e.DocumentSection) 
                    .WithMany(s => s.Keywords)
                    .HasForeignKey(e => e.DocumentSectionId); 
            });

            ApplyGlobalConfigurations(modelBuilder);
        }

   
        private void ApplyGlobalConfigurations(ModelBuilder modelBuilder)
        {
  
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var stringProperties = entityType.GetProperties()
                    .Where(p => p.ClrType == typeof(string));

                foreach (var property in stringProperties)
                {
                    if (property.GetMaxLength() == null) 
                    {
                        property.SetMaxLength(255); 
                    }
                }
            }

            // Veritabanındaki tüm decimal türündeki alanlar için varsayılan doğruluk derecesi
            foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetProperties())
                .Where(p => p.ClrType == typeof(decimal)))
            {
                property.SetPrecision(18); // Maksimum 18 haneli
                property.SetScale(2); // 2 ondalıklı olacak şekilde
            }
        }
    }
}
