namespace PdfProcessingApp.Models
{
    public class Keyword
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public int DocumentSectionId { get; set; }
        public DocumentSection DocumentSection { get; set; }
    }
}
