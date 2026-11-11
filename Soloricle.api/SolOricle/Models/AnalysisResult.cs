namespace SolOricle.Api.Models
{
    public class AnalysisResult
    {
        public int Id { get; set; }
        public int LiteratureId { get; set; }       // Foreign key
        public double SimilarityScore { get; set; }
        public DateTime AnalyzedAt { get; set; } = DateTime.Now;

        public Literature Literature { get; set; } // Navigation property
    }
}
