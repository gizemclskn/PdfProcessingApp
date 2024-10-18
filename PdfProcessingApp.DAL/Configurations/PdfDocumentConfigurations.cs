using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PdfProcessingApp.Models;

namespace PdfProcessingApp.DAL.Configurations
{
    public class PdfDocumentConfiguration : IEntityTypeConfiguration<PdfDocument>
    {
        public void Configure(EntityTypeBuilder<PdfDocument> builder)
        {
            builder.ToTable("PdfDocuments");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.FileName)
                   .IsRequired()
                   .HasMaxLength(255);
            builder.Property(e => e.FileSize)
                   .IsRequired();
            builder.Property(e => e.CreatedDate)
                   .HasDefaultValueSql("GETDATE()");
        }
    }
}
