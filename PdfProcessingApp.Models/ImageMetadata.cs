using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfProcessingApp.Models
{
    public class ImageMetadata
    {
        public string FileName { get; set; }
        public string Format { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public ImageMetadata(string fileName, string format, int width, int height)
        {
            FileName = fileName;
            Format = format;
            Width = width;
            Height = height;
        }
    }
}
