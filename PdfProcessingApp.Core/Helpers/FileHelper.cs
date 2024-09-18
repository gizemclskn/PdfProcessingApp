using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfProcessingApp.Core.Helpers
{
    public static class FileHelper
    {
        public static string GetFilePath(string directory, string fileName)
        {
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            return Path.Combine(directory, fileName);
        }

        public static string CleanFileName(string fileName)
        {
            foreach (char c in Path.GetInvalidFileNameChars())
            {
                fileName = fileName.Replace(c, '_');
            }
            return fileName;
        }
    }
}
