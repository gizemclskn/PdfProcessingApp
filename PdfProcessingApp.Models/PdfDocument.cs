namespace PdfProcessingApp.Models
{
    public class PdfDocument
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public long FileSize { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<DocumentSection> Sections { get; set; }
    }
}
