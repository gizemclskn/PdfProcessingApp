using System.Collections.Generic;

namespace PdfProcessingApp.Models
{
    public class PdfProcessingResult
    {
        public PdfDocument Document { get; set; }
        public List<DocumentSection> Sections { get; set; }
        public List<ImageMetadata> Images { get; set; }
    }
}
