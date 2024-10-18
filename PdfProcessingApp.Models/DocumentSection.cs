namespace PdfProcessingApp.Models
{
    public class DocumentSection
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string Content { get; set; }
        public string ImageFilePath { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<Keyword> Keywords { get; set; }

        public int PdfDocumentId { get; set; }  // Foreign Key
        public PdfDocument PdfDocument { get; set; }  // Navigasyon özelliği
    }
}
