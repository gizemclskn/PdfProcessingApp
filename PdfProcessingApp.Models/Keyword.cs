namespace PdfProcessingApp.Models
{
    public class Keyword
    {
        public int Id { get; private set; }
        public string Value { get; private set; }
        public int DocumentSectionId { get; private set; }
        public DocumentSection DocumentSection { get; private set; }

        public Keyword( string value)
        {
            
            Value = value;
        }
        public void SetDocumentSectionId(int id)
        {
            DocumentSectionId = id;
        }
    }
}
