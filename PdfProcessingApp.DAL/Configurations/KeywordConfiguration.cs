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
    public class KeywordConfiguration : IEntityTypeConfiguration<Keyword>
    {
        public void Configure(EntityTypeBuilder<Keyword> builder)
        {
            builder.ToTable("Keywords");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Value)
                   .IsRequired()
                   .HasMaxLength(100);
            builder.HasOne(e => e.DocumentSection)
                   .WithMany(s => s.Keywords)
                   .HasForeignKey(e => e.DocumentSectionId);
        }
    }
}
