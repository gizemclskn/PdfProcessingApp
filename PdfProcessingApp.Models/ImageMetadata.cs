namespace PdfProcessingApp.Models
{
    public class ImageMetadata
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string Format { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int DocumentSectionId { get; set; } 
        public DocumentSection DocumentSection { get; set; }  
    }
}
