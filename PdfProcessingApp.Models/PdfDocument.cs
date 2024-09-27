using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfProcessingApp.Models
{

    public class PdfDocument
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public long FileSize { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<DocumentSection> Sections { get; set; }

        public PdfDocument()
        {
            Sections = new List<DocumentSection>();
        }
    }

}