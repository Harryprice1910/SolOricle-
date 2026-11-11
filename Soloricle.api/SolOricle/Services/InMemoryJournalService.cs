using SolOricle.Core.Contracts;
using SolOricle.Core.Models;



/*
    PURPOSE:
    --------
    This class is an in-memory implementation of IJournalService.
    It exists to make the system functional without requiring
    persistence, databases, or external infrastructure.

    Key ideas:
    - This is a TEMPORARY implementation used for development,
      prototyping, and early testing.
    - It fulfils the IJournalService contract without changing
      any callers.
    - It allows the Core architecture to be exercised end-to-end
      before committing to storage or AI details.

    Design choices:
    - Uses a static in-memory List to simulate persistence.
    - Implements async methods to match the service contract,
      even though operations are currently synchronous.
    - Translates user intent (CreateJournalEntryRequest)
      into a real domain object (JournalEntry).

    Architectural role:
    - Lives outside SolOricle.Core because it is infrastructure,
      not meaning.
    - Can be replaced later by database-backed or AI-enhanced
      implementations without breaking the system.
    - Exists to validate architecture, not to be permanent.
*/


namespace SolOricle.Api.Services
{



    public sealed class InMemoryJournalService : IJournalService
    {
        private static readonly List<JournalEntry> _entries = new();

        public Task<JournalEntry> CreateAsync(CreateJournalEntryRequest request, CancellationToken ct = default)
        {
            var entry = new JournalEntry
            {
                Title = request.Title,
                Content = request.Content,
                EmotionalTone = request.EmotionalTone ?? string.Empty,
                CreatedAt = DateTime.UtcNow
            };

            _entries.Insert(0, entry);
            return Task.FromResult(entry);
        }

        public Task<IReadOnlyList<JournalEntry>> GetLatestAsync(int count = 20, CancellationToken ct = default)
        {
            IReadOnlyList<JournalEntry> latest = _entries.Take(Math.Max(0, count)).ToList();
            return Task.FromResult(latest);
        }
    }
}
