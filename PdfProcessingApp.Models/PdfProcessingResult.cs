using PdfProcessingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfProcessingApp.Models
{
    public class PdfProcessingResult
    {
        public PdfDocument Document { get; set; }
        public List<DocumentSection> Sections { get; set; }
        public List<ImageMetadata> Images { get; set; }

        public PdfProcessingResult()
        {
            Sections = new List<DocumentSection>();
            Images = new List<ImageMetadata>();
        }
    }
}
