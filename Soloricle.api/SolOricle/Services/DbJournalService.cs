using Microsoft.EntityFrameworkCore;
using SolOricle.Core.Contracts;
using SolOricle.Core.Models;
using SolOricle.Api.Data;

namespace SolOricle.Api.Services
{
    public class DbJournalService : IJournalService
    {
        private readonly SolOricleContext _context;

        private readonly IAnalysisService _analysis;

        public DbJournalService(SolOricleContext context, IAnalysisService analysis)
        {
            _context = context;
            _analysis = analysis;
        }

        public async Task<JournalEntry> CreateAsync(CreateJournalEntryRequest request, CancellationToken ct)
        {
            var emotion = _analysis.DetectEmotion(request.Content);

            var entry = new JournalEntry
            {
                Title = request.Title,
                Content = request.Content,
                EmotionalTone = emotion,
                CreatedAt = DateTime.UtcNow
            };

            _context.JournalEntries.Add(entry);
            await _context.SaveChangesAsync(ct);

            return entry;
        }

        public async Task<IReadOnlyList<JournalEntry>> GetLatestAsync(int count, CancellationToken ct)
        {
            return await _context.JournalEntries
                .OrderByDescending(e => e.CreatedAt)
                .Take(count)
                .ToListAsync(ct);
        }
    }
}