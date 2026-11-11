using SolOricle.Core.Models;

namespace SolOricle.App.Services
{
    public interface IContextRetrievalService
    {
        Task<IReadOnlyList<ContextMatch>> GetRelevantContextAsync(
            JournalEntry currentEntry,
            int maxResults = 5);
    }
}