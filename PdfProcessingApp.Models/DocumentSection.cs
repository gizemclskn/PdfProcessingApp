using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfProcessingApp.Models
{
    public class DocumentSection
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string ImageFilePath { get; set; }
        public DateTime CreatedDate { get; set; }

        public DocumentSection(string title, string content, string imageFilePath = null)
        {
            Title = title;
            Content = content;
            ImageFilePath = imageFilePath;
            CreatedDate = DateTime.UtcNow;
        }
    }
}
