using Microsoft.EntityFrameworkCore;
using PdfProcessingApp.Models;

namespace PdfProcessingApp.DAL;

public class AppDbContext :DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
    {
    }
    public DbSet<PdfDocument> PdfDocuments { get; set; }
    public DbSet<DocumentSection> DocumentSections { get; set; }
    public DbSet<Keyword> Keywords { get; set; }
    public DbSet<ImageMetadata> ImageMetadatas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // One-to-Many
        modelBuilder.Entity<PdfDocument>()
            .HasMany(pd => pd.Sections)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade); 

        // One-to-Many
        modelBuilder.Entity<DocumentSection>()
            .HasMany(ds => ds.Keywords)
            .WithOne(k => k.DocumentSection)
            .HasForeignKey(k => k.DocumentSectionId)
            .OnDelete(DeleteBehavior.Cascade); 

        //One-to-One
        modelBuilder.Entity<DocumentSection>()
            .HasOne(ds => ds.ImageMetadata)
            .WithOne()
            .HasForeignKey<ImageMetadata>(im => im.FileName)
            .OnDelete(DeleteBehavior.Cascade); 
    }
}
