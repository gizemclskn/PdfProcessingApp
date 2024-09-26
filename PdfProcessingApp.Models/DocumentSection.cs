using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfProcessingApp.Models
{
    public class DocumentSection
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string ImageFilePath { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<Keyword> Keywords { get; set; }
        public ImageMetadata ImageMetadata { get; set; }

        public DocumentSection()
        {
           
        Keywords = new List<Keyword>();
        }
    }
}
