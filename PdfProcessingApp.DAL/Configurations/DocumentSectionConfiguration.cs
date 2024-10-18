using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PdfProcessingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfProcessingApp.DAL.Configurations
{
    public class DocumentSectionConfiguration : IEntityTypeConfiguration<DocumentSection>
    {
        public void Configure(EntityTypeBuilder<DocumentSection> builder)
        {
            builder.ToTable("DocumentSections");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Title)
                   .HasMaxLength(255);
            builder.Property(e => e.Content)
                   .IsRequired();
            builder.HasOne(e => e.PdfDocument)
                   .WithMany(d => d.Sections)
                   .HasForeignKey(e => e.PdfDocumentId);
        }
    }
}
