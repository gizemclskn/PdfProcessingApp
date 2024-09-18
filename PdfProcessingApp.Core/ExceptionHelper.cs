using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfProcessingApp.Core
{
    public static class ExceptionHelper
    {
        public static void LogException(Exception ex)
        {
            // Hata loglama işlemleri burada yapılabilir.
            Console.WriteLine($"EXCEPTION: {ex.Message}");
        }

        public static void HandleException(Exception ex)
        {
            LogException(ex);
            // İsteğe bağlı olarak daha fazla hata işleme yapılabilir.
        }
    }
}
