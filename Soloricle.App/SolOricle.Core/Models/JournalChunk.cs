using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/// <summary>
/// Represents a searchable section of a journal entry.
///
/// A journal entry may produce one or more chunks.
/// Each chunk can be embedded and retrieved independently.
/// </summary>


namespace SolOricle.Core.Models
{
    internal class JournalChunk
    {
        public Guid id { get; set; } = Guid.NewGuid();



    }
}
