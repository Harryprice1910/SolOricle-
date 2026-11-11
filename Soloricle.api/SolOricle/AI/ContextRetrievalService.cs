using SolOricle.Core.Models;
using SolOricle.Core.Contracts;




namespace SolOricle.Api.AI
{
    public class ContextRetrievalService : IContextRetrievalService
    {
        public async Task<IReadOnlyList<ContextMatch>> GetRelevantContextAsync(
            JournalEntry currentEntry,
            int maxResults = 5)
        {
            // This will eventually contain our actual
            // retrieval and contextualisation algorithm.

            var matches = new List<ContextMatch>();

            return matches;
        }
    }
}