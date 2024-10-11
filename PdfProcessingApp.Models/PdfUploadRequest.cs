using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfProcessingApp.Models
{
    public class PdfUploadRequest
    {
        public IFormFile UploadedFile { get; set; }
        public string OutputDirectory { get; set; }
    }
}
