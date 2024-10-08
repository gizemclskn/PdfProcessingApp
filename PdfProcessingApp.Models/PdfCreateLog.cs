using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfProcessingApp.Models
{
    public class PdfCreateLog
    {
        public int Id { get; set; }
        public string Info { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
