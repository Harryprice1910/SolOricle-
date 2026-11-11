namespace SolOricle.Api.Models
{
    public class Literature
    {
        public int Id { get; set; }                 // Primary key
        public string Title { get; set; }           // Title of the text
        public string Content { get; set; }         // Full text
        public DateTime DateAdded { get; set; } = DateTime.Now;
        public string Tags { get; set; }            // Optional tags
    }
}
