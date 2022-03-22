namespace NotiPet.Domain.Models
{
    public class PersonalDocument
    {
        public PersonalDocument(int documentId, string documentType)
        {
            DocumentId = documentId;
            DocumentType = documentType;
        }

        public int DocumentId { get; set; }
        public string  DocumentType { get; set; }
    }
}