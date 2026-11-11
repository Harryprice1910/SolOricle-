using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*
    PURPOSE:
    --------
    This class represents a *request* to create a journal entry.
    It models USER INTENT, not a persisted domain object.

    Key ideas:
    - This is a CONTRACT, not a model.
    - It defines the minimum information a caller must provide
      (UI, API, tests, prompts, etc.) to create an entry.
    - It does NOT represent something that exists over time.

    Design choices:
    - Properties use `init` to ensure immutability after creation.
      Once intent is expressed, it should not be mutated.
    - No Id or timestamps: those are assigned by the system,
      not supplied by the user.
    - EmotionalTone is optional: it may be user-supplied now,
      but later can be derived via AI tone analysis.

    Architectural role:
    - Lives in SolOricle.Core.Contracts because it is about
      communication and boundaries, not meaning.
    - This object is short-lived and safe to change as APIs evolve.
*/

namespace SolOricle.Core.Contracts
{
    public sealed class CreateJournalEntryRequest
    {
        public string Title { get; init; } = string.Empty;
        public string Content { get; init; } = string.Empty;

        // Optional for now; later can derive this via tone analysis.
        public string? EmotionalTone { get; init; }
    }
}
