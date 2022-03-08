namespace NotiPet.Domain.Models
{
    public class PersonalDocument
    {
        public PersonalDocument(string documentId, int documentType)
        {
            DocumentId = documentId;
            DocumentType = documentType;
        }

        public string DocumentId { get; set; }
        public int DocumentType { get; set; }
    }
}