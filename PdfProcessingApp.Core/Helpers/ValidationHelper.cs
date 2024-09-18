using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfProcessingApp.Core.Helpers
{
    public static class ValidationHelper
    {
        public static bool IsPdfFile(IFormFile file)
        {
            return file != null && file.ContentType == "application/pdf";
        }

        public static bool ValidateKeywords(List<string> keywords)
        {
            return keywords != null && keywords.Count > 0;
        }
    }
}
