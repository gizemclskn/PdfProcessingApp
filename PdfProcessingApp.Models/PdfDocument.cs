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
        [Required(ErrorMessage = "PDF dosyası gereklidir.")]
        [DataType(DataType.Upload)]
        public IFormFile File { get; set; }

        public string FileName { get; set; }
        public string FilePath { get; set; }

        [Required(ErrorMessage = "Anahtar kelimeler listesi boş olamaz.")]
        public List<string> Keywords { get; set; }

        public PdfDocument()
        {
            Keywords = new List<string>();
        }
    }
}
