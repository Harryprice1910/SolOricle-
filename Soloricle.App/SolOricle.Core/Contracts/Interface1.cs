using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SolOricle.Core.Models;

/*
    PURPOSE:
    --------
    This interface defines the core behaviours for working with
    journal entries in SolOricle.

    Key ideas:
    - This is a SERVICE CONTRACT, not an implementation.
    - It describes WHAT can be done with journal entries,
      without specifying HOW it is done.
    - UI, API, and other layers depend on this interface,
      not on concrete classes.

    Responsibilities:
    - CreateAsync:
        Defines the boundary where user intent
        (CreateJournalEntryRequest)
        becomes a real domain object (JournalEntry).

    - GetLatestAsync:
        Provides read-only access to recent journal entries
        in a controlled, paginated way.

    Design choices:
    - Async methods to support I/O, storage, and AI work.
    - CancellationToken for responsiveness and cooperative cancellation.
    - IReadOnlyList to prevent consumers mutating domain state.
    - Lives in Contracts because this defines behaviour boundaries,
      not domain meaning.

    Architectural role:
    - This interface enables dependency inversion.
    - Implementations may live in API, infrastructure, or test projects.
    - The Core defines the rules; implementations conform to them.
*/



namespace SolOricle.Core.Contracts
{
    public interface IJournalService
    {
        Task<JournalEntry> CreateAsync(CreateJournalEntryRequest request, CancellationToken ct = default);
        Task<IReadOnlyList<JournalEntry>> GetLatestAsync(int count = 20, CancellationToken ct = default);
    }
}
