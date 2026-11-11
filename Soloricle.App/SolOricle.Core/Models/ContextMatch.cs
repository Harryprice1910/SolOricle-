namespace SolOricle.Core.Models
{
	public class ContextMatch
	{
		// ID of the original item that we retrieved.
		// For example, this could be the ID of a JournalEntry.
		public int SourceId { get; set; }

		// Tells us where this context came from.
		// Examples:
		// "JournalEntry"
		// "JournalChunk"
		// "Literature"
		public string SourceType { get; set; } = string.Empty;

		// The actual text that was retrieved.
		public string Content { get; set; } = string.Empty;

		// How semantically similar this content is
		// to the user's current journal entry.
		//
		// Eventually this could come from embeddings
		// and cosine similarity.
		public double SemanticScore { get; set; }

		// Measures how recent the retrieved information is.
		//
		// A recent journal entry may sometimes be more
		// contextually useful than something written years ago.
		public double RecencyScore { get; set; }

		// The overall score after combining our different
		// contextualisation factors.
		public double FinalScore { get; set; }
	}
}