using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SolOricle.Core.Contracts;
using SolOricle.Core.Models;


/*
    PURPOSE:
    --------
    This class represents a REAL journal entry once it exists.
    It is a core DOMAIN MODEL and the canonical object in SolOricle.

    Key ideas:
    - This object represents meaning, not input.
    - It exists over time and may be stored, edited, analysed,
      reflected on.
    - Everything else in the system ultimately orbits this type.

    Design choices:
    - Has an Id and CreatedAt to establish identity and chronology.
    - Uses `set` instead of `init` because entries are allowed
      to evolve (editing, AI enrichment, tone refinement, etc.).
    - EmotionalTone is required here: once the entry exists,
      it should always have some emotional classification,
      even if initially inferred or defaulted.

    Architectural role:
    - Lives in SolOricle.Core.Models because it represents
      the stable meaning of the application.
    - UI, API, persistence, and AI layers must conform to this model,
      not redefine it.
*/


/// note leaving the journal entries here in the core to keep the data entries of users as close to the meaning as possible, and to avoid having to map between different models in different layers. The API and UI can work directly with this model, while the service layer can handle any necessary translation from user input (CreateJournalEntryRequest) to this domain model (JournalEntry).


namespace SolOricle.Core.Models;

public class JournalEntry
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;


    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;

    public string EmotionalTone { get; set; } = string.Empty;
}



