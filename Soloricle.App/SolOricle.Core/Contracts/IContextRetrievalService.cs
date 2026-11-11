using SolOricle.Core.Models;

namespace SolOricle.Core.Contracts;

public interface IContextRetrievalService
{
    Task<IReadOnlyList<ContextMatch>> GetRelevantContextAsync(
        JournalEntry currentEntry,
        int maxResults = 5);
}