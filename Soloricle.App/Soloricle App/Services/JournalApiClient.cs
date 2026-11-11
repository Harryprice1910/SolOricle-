using SolOricle.Core.Contracts;
using SolOricle.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace SolOricle.App.Services
{
    public sealed class JournalApiClient
    {
        private readonly HttpClient _http;

        public JournalApiClient(HttpClient http)
        {
            _http = http;
        }

        public async Task<JournalEntry> CreateAsync(CreateJournalEntryRequest request, CancellationToken ct = default)
        {
            var response = await _http.PostAsJsonAsync("api/journal", request, ct);
            response.EnsureSuccessStatusCode();

            var entry = await response.Content.ReadFromJsonAsync<JournalEntry>(cancellationToken: ct);
            return entry ?? throw new InvalidOperationException("API returned no JournalEntry.");
        }

        public async Task<IReadOnlyList<JournalEntry>> GetLatestAsync(int count = 20, CancellationToken ct = default)
        {
            var entries = await _http.GetFromJsonAsync<List<JournalEntry>>($"api/journal/latest?count={count}", ct);
            return entries ?? new List<JournalEntry>();
        }
    }
}
