using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfProcessingApp.Models
{
    public class PdfProcessingResult
    {
        public List<string> ProcessedPages { get; set; }
        public List<string> SavedImages { get; set; }

        public PdfProcessingResult()
        {
            ProcessedPages = new List<string>();
            SavedImages = new List<string>();
        }
    }
}
