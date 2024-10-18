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
    public class PdfCreateLogConfiguration : IEntityTypeConfiguration<PdfCreateLog>
    {
        public void Configure(EntityTypeBuilder<PdfCreateLog> builder)
        {
            builder.ToTable("PdfCreateLogs");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Info)
                   .IsRequired();
            builder.Property(e => e.CreatedDate)
                   .HasDefaultValueSql("GETDATE()");
        }
    }
}
