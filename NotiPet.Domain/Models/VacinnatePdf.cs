namespace NotiPet.Domain.Models
{
    public class VaccinatePdf
    {
        public string File { get; set; }
        public byte[] FileByte =>System.Convert.FromBase64String(File);
    }
}